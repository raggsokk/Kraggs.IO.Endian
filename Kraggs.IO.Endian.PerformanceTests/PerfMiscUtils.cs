using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

using Microsoft.Win32;

namespace Kraggs.IO.Endian
{
    public static class PerfMiscUtils
    {
        /// <summary>
        /// Compares 2 generic buffers of same type.
        /// Slower than real type funsions but a lot less to code....
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="leftBuffer"></param>
        /// <param name="rightBuffer"></param>
        /// <returns></returns>
        public static long ValidateBuffers<T>(T[] leftBuffer, T[] rightBuffer) where T : struct, IComparable<T>
        {

            if (leftBuffer.Length != rightBuffer.Length)
                return Math.Max(leftBuffer.Length, rightBuffer.Length);

            var count = leftBuffer.Length;
            long errorCount = 0;

            for (int i = 0; i < count; i++)
            {
                if (Comparer<T>.Default.Compare(leftBuffer[i], rightBuffer[i]) != 0)
                    //if (leftBuffer[i].CompareTo(rightBuffer[i]) != 0)
                    errorCount++;
            }


            return errorCount;
        }

        /// <summary>
        /// .Net 4.5 Compiler Magic.
        /// </summary>
        /// <param name="FuncName"></param>
        /// <returns></returns>
        public static string GetCallingFunctionName(
            [System.Runtime.CompilerServices.CallerMemberName] string FuncName = "")
        {
            return FuncName;
        }

        /// <summary>
        /// Gets the assembly version of the specified type, or Version(0,0,0,0) if failed.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Version GetAssemblyVersionFromType(Type type)
        {
            try
            {
                var fullname = type.Assembly.FullName;
                if (!string.IsNullOrWhiteSpace(fullname))
                {
                    var posVersion = fullname.IndexOf("Version=");

                    if (posVersion > 0)
                    {
                        posVersion += 8;
                        var len = fullname.IndexOf(",", posVersion);

                        var ver = fullname.Substring(posVersion, len - posVersion);

                        return new Version(ver);
                    }
                }
            }
            catch(Exception)
            {}

            return new Version(0, 0, 0, 0);
        }

        /// <summary>
        /// Is this a debugmode build?
        /// </summary>
        public static readonly bool IsDebugMode =
#if DEBUG
            true;
#else
            false;
#endif
        #region Get Mono Version

        public static bool IsRunningOnMono
        {
            get
            {
                return Type.GetType("Mono.Runtime", false) != null;
            }
        }

        [DllImport("__Internal", EntryPoint = "mono_get_runtime_build_info")]
        private extern static string GetMonoVersion();

        public static string GetMonoRuntimeInfo()
        {
            var DEFAULT_STRING = "Failed to retrive .Net Runtime info.";

            try
            {
                // are we running on Mono?
                var type = Type.GetType("Mono.Runtime", false);

                if (type != null)
                {
                    var methDisplayName = type.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);
                    if (methDisplayName != null)
                        return methDisplayName.Invoke(null, null).ToString();
                    else
                        return GetMonoVersion();
                }
                else
                {
                    return "Not running on Mono.";
                    ////TODO: Ugly hack .net runtime windows.
                    //var sb = new StringBuilder();
                    //sb.Append("At least Microsoft Runtime :");
                    //// .net 4.5.1 test
                    //var flag451 = Type.GetType("System.Runtime.GCLargeObjectHeapCompactionMode", false) != null;
                    //var flag45 = Type.GetType("System.Reflection.ReflectionContext", false) != null;
                    //if (flag451)
                    //    sb.Append(" v4.5.1");
                    //else if (flag45)
                    //    sb.Append(" v4.5");
                    //else
                    //    sb.Append(" v4.0");

                    //return sb.ToString();
                }

            }
            catch (Exception)
            { }

            return DEFAULT_STRING;
        }

        #endregion

        #region GetInfoFuncyHacks

        [DllImport("libc")]
        private static extern int uname(IntPtr buf);
        private static string sCachedUname;

        private static string Uname()
        {
            if (!String.IsNullOrWhiteSpace(sCachedUname))
                return sCachedUname;

            var buf = IntPtr.Zero;
            try
            {
                buf = Marshal.AllocHGlobal(8192);

                if (uname(buf) == 0)
                {
                    sCachedUname = Marshal.PtrToStringAnsi(buf);
                }
                return sCachedUname;
            }
            catch (Exception)
            {                
            }
            finally
            {
                if (buf != IntPtr.Zero)
                    Marshal.FreeHGlobal(buf);
            }
            sCachedUname = "Failed to call 'uname'";
            return sCachedUname;
        }

        public static PlatformID CorrectlyGetPlatform()
        {
            var platform = Environment.OSVersion.Platform;

            switch (platform)
            {
                case PlatformID.Unix:
                case PlatformID.MacOSX:
                    {
                        if (Uname().Contains("Darwin"))
                            return PlatformID.MacOSX;
                        else
                            return PlatformID.Unix;
                    }
                default:
                    return platform;
            }
        }

        #endregion

        

        #region Get host info

        public static string GetHostInfo()
        {
            var platform = CorrectlyGetPlatform();

            switch (platform)
            {
                case PlatformID.MacOSX:
                    return GetHostInfoMac();
                case PlatformID.Unix:
                    return GetHostInfoLinux();
                default:
                    return Environment.OSVersion.VersionString;
            }
        }

        public static string GetHostInfoMac()
        {
            //return "MAVOX";
            var macinfo = GetProcessOutput("uname", "-v");            
            if (string.IsNullOrWhiteSpace(macinfo))
                return string.Format("MacOSX: {0}", Environment.OSVersion);

            // trim this down a bit.
            //Darwin Kernel Version 14.3.0: Mon Mar 23 11:59:05 PDT 2015; root:xnu-2782.20.48~5/RELEASE_X86_64            
            //Darwin Kernel Version 14.3.0: xnu-2782.20.48~5/RELEASE_X86_64
            var posColon = macinfo.IndexOf(':');
            if (posColon != -1)
            {
                var sb = new StringBuilder();
                sb.Append(macinfo.Substring(0, posColon));

                var posxnu = macinfo.IndexOf("xnu-");

                if(posxnu != -1)
                {
                    var posRel = macinfo.IndexOf('/', posxnu);
                    if (posRel != -1)
                    {
                        sb.Append(": ");
                        sb.Append(macinfo.Substring(posxnu, posRel - posxnu));
                    }
                }

                //sb.Append(":");
                //var posSemi = macinfo.IndexOf(';', posColon);
                //posColon = macinfo.IndexOf(':', posSemi);
                //sb.Append(macinfo.Substring(posColon));

                return sb.ToString();
            }

            return macinfo;
        }
        public static string GetHostInfoLinux()
        {
            var macinfo = GetProcessOutput("uname", "-r");
            if (string.IsNullOrWhiteSpace(macinfo))
                return string.Format("Linux: {0}", Environment.OSVersion);

            return macinfo;
        }

        #endregion

        #region Get CPU Info

        /// <summary>
        /// Returns the cpu info string dependent of which os we are running on.
        /// </summary>
        /// <returns></returns>
        public static string GetCPuInfo()
        {
            //var platform = Environment.OSVersion.Platform;
            var platform = CorrectlyGetPlatform();

            var NotImplString = string.Format("Code for getting cpu info on '{0}' is not implemented.", platform);

            switch(platform)
            {
                case PlatformID.Unix:
                    return GetCPUInfoForLinux();
                case PlatformID.MacOSX:
                    return GetCPUInfoFromSysCtl();
                    //if (Uname() == "Darwin")
                    //    return GetCPUInfoFromSysCtl();
                    //else
                    //    return NotImplString;
                case PlatformID.Xbox:
                    return NotImplString;
                default:
                    return GetCPUInfoFromRegistry();
            }

            //// default not impl case.
            //return NotImplString;
        }

        public static string GetCPUInfoFromRegistry()
        {
            const string DEFAULT_ERROR = "Failed to get prosessor info.";

            try
            {
                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    using (var cpu0 = hklm.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", false))
                    {
                        return cpu0.GetValue("ProcessorNameString", DEFAULT_ERROR) as string;
                    }
                }
            }
            catch (Exception)
            {
                return DEFAULT_ERROR;
            }
        }

        private static string GetProcessOutput(string filename, string args)
        {
            try
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = filename,
                        Arguments = args,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                if(proc.Start())
                {
                    return proc.StandardOutput.ReadToEnd().Trim();
                }
            }
            catch
            { }

            return null;
        }

        public static string GetCPUInfoFromSysCtl()
        {
            const string DEFAULT_ERROR = "Failed to get prosessor info from sysctl.";

            var sysctl = GetProcessOutput("sysctl", "-n \"machdep.cpu.brand_string\"");

            if (string.IsNullOrWhiteSpace(sysctl))
                return DEFAULT_ERROR;

            return sysctl;

        }

        public static string GetCPUInfoForLinux()
        {
            const string DEFAULT_ERROR = "Failed to get prosessor info from /proc/cpuinfo.";

            try
            {
                //var f = File.OpenRead("/proc/cpuinfo");
                var lines = File.ReadAllLines("/proc/cpuinfo");

                foreach(var s in lines)
                {
                    if(s.StartsWith("model name"))
                    {
                        var posColon = s.IndexOf(':');

                        if(posColon != -1)
                        {
                            return s.Substring(posColon + 1).Trim();
                        }
                    }
                }
            }
            catch(Exception)
            {                
            }
            return DEFAULT_ERROR;
        }

        #endregion

    }
}
