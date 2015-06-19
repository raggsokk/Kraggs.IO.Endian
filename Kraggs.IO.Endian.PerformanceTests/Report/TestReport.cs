using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Microsoft.Win32;

using Mono;

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
        public string DotNetRuntimeInfo { get; protected set; }

        // asm info
        public string MonoDataConverterCommit { get; protected set; }
        public bool MonoDataConverterValidationDisabled { get; protected set; }
        public Version KraggsVersion { get;protected set; }
        public Version KraggsPerfVersion { get; protected set; }
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
            //this.OSVersionString = Environment.OSVersion.VersionString;
            this.OSVersionString = PerfMiscUtils.GetHostInfo();
            this.CPUInfo = PerfMiscUtils.GetCPuInfo();
            this.DotNetVersion = Environment.Version;
            this.DotNetRuntimeInfo = PerfMiscUtils.GetMonoRuntimeInfo();

            this.MonoDataConverterCommit = DataConverter.GitCommitHash;
            this.MonoDataConverterValidationDisabled = DataConverter.BuildValidationDisabled;
            this.KraggsVersion = PerfMiscUtils.GetAssemblyVersionFromType(typeof(Kraggs.IO.EndianConverter));
            this.KraggsPerfVersion = PerfMiscUtils.GetAssemblyVersionFromType(this.GetType());


            this.DebugMode = PerfMiscUtils.IsDebugMode;
            
        }

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
