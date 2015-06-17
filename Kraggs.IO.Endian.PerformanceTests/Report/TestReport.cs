using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Microsoft.Win32;

namespace Kraggs.IO.Endian.PerformanceTests
{
    /// <summary>
    /// This is the function prototype for a single test.
    /// </summary>
    /// <param name="testBuffer"></param>
    /// <param name="RunCount"></param>
    /// <returns></returns>
    public delegate TestResult delRunTest(byte[] testBuffer, int RunCount);

    /// <summary>
    /// The complete TestReport.
    /// </summary>
    public class TestReport
    {
#if DEBUG
        private const int DEFAULT_RUN_COUNT = 3;
        private const int DEFAULT_TESTBUFFER_SIZE = 20000;
#else
        private const int DEFAULT_RUN_COUNT = 5;
        private const int DEFAULT_TESTBUFFER_SIZE = 2000000;
#endif
        // Header Runtime Info.
        public string OSVersionString { get; protected set; }
        public string CPUInfo { get; protected set; }
        public Version DotNetVersion { get; protected set; }

        // asm info
        public string MonoDataConverterCommit { get; protected set; }
        public bool MonoDataConverterValidationEnabled { get; protected set; }
        public Version KraggsVersion { get;protected set; }
        public bool DebugMode { get; protected set; }

        // run settings
        public int RunCount { get; protected set; }
        public int TestBufferSize { get; protected set; }

        // time holders
        public long TimeWarmup { get; protected set; }
        public long TimeReadTests { get; protected set; }
        public long TimeWriteTests { get; protected set; }

        public virtual long TimeTotal { get { return TimeWarmup + TimeReadTests + TimeWriteTests; } }

        // the actual tests
        public List<delRunTest> ReadTests { get; protected set; }
        public List<delRunTest> WriteTests { get; protected set; }

        // the test results.
        public List<TestResult> ReadResults { get; protected set; }
        public List<TestResult> WriteResults { get; protected set; }

        public TestReport(int RunCount = DEFAULT_RUN_COUNT, int TestBufferSize = DEFAULT_TESTBUFFER_SIZE)
        {
            this.RunCount = RunCount;
            this.TestBufferSize = TestBufferSize;

            ReadTests = new List<delRunTest>();
            WriteTests = new List<delRunTest>();

            ReadResults = new List<TestResult>();
            WriteResults = new List<TestResult>();



            // this is technically part of warmup

            // 1. First get host indipendent info
            this.DotNetVersion = Environment.Version;
            this.OSVersionString = Environment.OSVersion.VersionString;

            this.KraggsVersion = GetAssemblyVersionFromType(typeof(Kraggs.IO.EndianConverter));
            this.MonoDataConverterCommit = "c04f7e75bfbdf1e3f976193ab0bc0d034679e358";
#if PERFTEST_DISABLE_VALIDATION
            this.MonoDataConverterValidationEnabled = false;
#else
            this.MonoDataConverterValidationEnabled = true;
#endif
#if DEBUG
            this.DebugMode = true;
#else
            this.DebugMode = false;
#endif

            // set default cpuinfo result.
            this.CPUInfo = string.Format("Code for getting cpu info on '{0}' is not implemented.", Environment.OSVersion.Platform);
            // 2. get host dependent info
            if (Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                // do macox specific header info getting.
            }
            else if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                // do unix/linux specific header info getting.
            }
            else if (Environment.OSVersion.Platform == PlatformID.Xbox)
            {
                // do xbox specific header info getting.
            }                
            else
            {
                // windows.
                this.CPUInfo = GetCPUInfoFromRegistry();
            }

        }

        #region Util Functions

        protected virtual string GetCPUInfoFromRegistry()
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

        protected virtual Version GetAssemblyVersionFromType(Type type)
        {
            // try hack version first.

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
            { }

            // then try get filevesrion attribute.

            var attribs = type.Assembly.GetCustomAttributes(false);

            foreach(var o in attribs)
            {
                var otype = o.GetType();
               
                if(otype == typeof(System.Reflection.AssemblyFileVersionAttribute))
                {
                    var asmFileVer = o as System.Reflection.AssemblyFileVersionAttribute;

                    return new Version(asmFileVer.Version);
                }
            }

            Debug.Assert(false);
            throw new NotImplementedException("TODO: Detect why we got here! (GetAssemblyVersionFromType)"); 
        }


        #endregion

        #region Public interface

        [DebuggerNonUserCode()]
        public void AddReadTest(delRunTest ReadTest)
        {
            ReadTests.Add(ReadTest);
        }

        [DebuggerNonUserCode()]
        public void AddWriteTest(delRunTest WriteTest)
        {
            WriteTests.Add(WriteTest);
        }

        protected virtual void RunAllTests(byte[] testBuffer, int runCount)
        {
            // reset in all cases
            ReadResults.Clear();
            WriteResults.Clear();
            // skip reset time variables.
            var w = new Stopwatch();

            // run read tests first
            w.Restart();
            foreach(var readTest in ReadTests)
            {
                ReadResults.Add(readTest(testBuffer, runCount));
            }
            w.Stop();
            this.TimeReadTests = w.ElapsedMilliseconds;

            // run write tests next
            w.Restart();
            foreach(var writeTest in WriteTests)
            {
                WriteResults.Add(writeTest(testBuffer, runCount));
            }
            w.Stop();
            this.TimeWriteTests = w.ElapsedMilliseconds;

        }

        //TODO: add run and buffer size to runtests
        public virtual void RunTests()
        {
            var w = new Stopwatch();
            w.Start();

            // run all code warmup
            var warmUpBuffer = new byte[sizeof(double) * 4];
            RunAllTests(warmUpBuffer, 1);

            // generate random test buffer
            var testBuffer = new byte[this.TestBufferSize];
            var r = new Random();
            r.NextBytes(testBuffer);
            w.Stop();
            this.TimeWarmup = w.ElapsedMilliseconds;

            w.Restart();
            RunAllTests(testBuffer, RunCount);
            w.Stop();

            // timeTotal = w.ElapsedMilliseconds
        }

        [DebuggerNonUserCode()]
        public void GenerateReport(TestReportVisitor visitor)
        {
            visitor.VisitTestReport(this);
        }

        #endregion
    }
}
