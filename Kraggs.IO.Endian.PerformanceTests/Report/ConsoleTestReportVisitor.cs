using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;

namespace Kraggs.IO.Endian.PerformanceTests
{
    public class ConsoleTestReportVisitor : TestReportVisitor
    {
        public bool UseConsoleColors { get; protected set; }

        protected string pTestResultStringFormat;
        protected Stopwatch pStopWatch;

        [DebuggerNonUserCode()]
        public ConsoleTestReportVisitor(bool UseConsoleColors = true)
        {
            this.UseConsoleColors = UseConsoleColors;
            this.pStopWatch = new Stopwatch();
        }

        #region Helper Functions

        [DebuggerNonUserCode()]
        protected virtual void Write(ConsoleColor color, string format, params object[] args)
        {
            if (UseConsoleColors)
            {
                var oldcolor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.Write(format, args);
                Console.ForegroundColor = oldcolor;
            }
            else
                Console.Write(format, args);
        }

        [DebuggerNonUserCode()]
        protected virtual void WriteLine()
        {
            Console.WriteLine();
        }

        [DebuggerNonUserCode()]
        protected virtual void WriteLine(ConsoleColor color, string format, params object[] args)
        {
            if (UseConsoleColors)
            {
                var oldcolor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(format, args);
                Console.ForegroundColor = oldcolor;
            }
            else
                Console.WriteLine(format, args);
        }

        protected virtual bool GenerateTestStringFormat(TestReport report, out string TestStringFormat, out object[] HeaderValues)
        {
            //TODO: impl get longest test name func.
            var MaxTestNameLength = 20;
            const int RUNTEXTSIZE = 6;

            var fmt = new StringBuilder();
            var values = new List<object>();

            // add "Test"
            fmt.AppendFormat("{{0,-{0}}}", MaxTestNameLength);
            values.Add("Test");

            // add "Impl"
            fmt.AppendFormat("{{1,-{0}}} ", RUNTEXTSIZE);
            values.Add("Impl");

            // add each test run
            for (int i = 0; i < report.RunCount; i++)
            {
                // add "Run" + i.ToString()
                fmt.AppendFormat("{{{0},{1}}}", i + 2, RUNTEXTSIZE);
                values.Add("Run" + (i + 1).ToString());
            }

            // add "AVG"
            fmt.AppendFormat("    {{{0},{1}:##.##}}", report.RunCount + 2, RUNTEXTSIZE);
            values.Add("AVG");

            TestStringFormat = fmt.ToString();
            HeaderValues = values.ToArray();

            return true;                
        }

        #endregion

        #region TestReportVisitor Implementation

        protected override void VisitHeader(TestReport report)
        {
            pStopWatch.Start();

            WriteLine(ConsoleColor.White, "Host Testing information");
            WriteLine(ConsoleColor.Gray, "--------------------------------------------------------------------------");
            WriteLine(ConsoleColor.Gray, "  Operating System:               {0,40}", report.OSVersionString);
            WriteLine(ConsoleColor.Gray, "  CPUInfo:                        {0,40}", report.CPUInfo);
            WriteLine(ConsoleColor.Gray, "  .net version:                   {0,40}", report.DotNetVersion);
            WriteLine();

            WriteLine(ConsoleColor.White, "Assembly information");
            WriteLine(ConsoleColor.Gray, "--------------------------------------------------------------------------");
            WriteLine(ConsoleColor.Gray, "  Mono DataConverter Commit:      {0,40}",
                report.MonoDataConverterCommit);
            WriteLine(ConsoleColor.Gray, "  Mono DataConverter Validation Enabled:  {0,32}",
                report.MonoDataConverterValidationEnabled);
            WriteLine(ConsoleColor.Gray, "  Using Kraggs.IO.Endian Version: {0,40}",
                report.KraggsVersion);

            WriteLine();

            if (report.DebugMode)
                WriteLine(ConsoleColor.Yellow, "WARNING: Running test in debug mode!");
            else
                WriteLine(ConsoleColor.Gray, "Running in release mode...");

            WriteLine();

            WriteLine(ConsoleColor.White, "Running test {0} times with test bufer size of {1} bytes.",
                report.RunCount, report.TestBufferSize);

        }

        protected override void VisitFooter(TestReport report)
        {
            WriteLine();
            WriteLine(ConsoleColor.White, "Testing Statistics");
            WriteLine(ConsoleColor.Gray, "--------------------------------------------------------------------------");
            WriteLine(ConsoleColor.Gray, "Number of read tests:             {0,40}", report.ReadTests.Count);
            WriteLine(ConsoleColor.Gray, "Number of write tests:            {0,40}", report.WriteTests.Count);
            WriteLine(ConsoleColor.Gray, "Total number of tests:            {0,40}", report.ReadTests.Count + report.WriteTests.Count);
            WriteLine(ConsoleColor.Gray, "Total time of warm up:            {0,37} ms", report.TimeWarmup);
            WriteLine(ConsoleColor.Gray, "Total time of read tests:         {0,37} ms", report.TimeReadTests);
            WriteLine(ConsoleColor.Gray, "Total time of write tests:        {0,37} ms", report.TimeWriteTests);
            WriteLine(ConsoleColor.Gray, "Total test time including warmup: {0,37} ms", report.TimeTotal);

            pStopWatch.Stop();

            WriteLine(ConsoleColor.Gray, "Time used to generate this rapport:         {0,27} ms", pStopWatch.ElapsedMilliseconds);
            WriteLine();            
        }

        protected override void VisitReadTests(TestReport report)
        {
            // write header.
            this.WriteLine();
            this.WriteLine();
            this.WriteLine(ConsoleColor.Green, "====  Reading Tests  =====");

            if(report.ReadResults == null || report.ReadResults.Count == 0)
            {
                this.WriteLine(ConsoleColor.Gray, "No Read tests was added.");
                this.WriteLine();
                return;
            }

            string stringFormat = null;
            object[] headerValues = null;

            if (!GenerateTestStringFormat(report, out stringFormat, out headerValues))
            {
                throw new ApplicationException("Failed to generate TestResult StringFormat String!");
            }
            else //TODO: remove this ugly hack.
                pTestResultStringFormat = stringFormat;

            // write header and line
            this.WriteLine(ConsoleColor.White, stringFormat, headerValues);
            this.WriteLine(ConsoleColor.White, "-------------------------------------------------------");

            // write read result using string format
            foreach(var readResult in report.ReadResults)
            {
                VisitTestResult(readResult);
                flagOddEven = !flagOddEven;
            }

        }

        protected override void VisitWriteTests(TestReport report)
        {
            this.WriteLine();
            this.WriteLine();
            this.WriteLine(ConsoleColor.Green, "====  Writing Tests  =====");
            this.WriteLine();

            if (report.WriteResults == null || report.WriteResults.Count == 0)
            {
                this.WriteLine(ConsoleColor.Gray, "No Write Tests was added.");
                this.WriteLine();
                return;
            }

            string stringFormat = null;
            object[] headerValues = null;

            if (!GenerateTestStringFormat(report, out stringFormat, out headerValues))
            {
                throw new ApplicationException("Failed to generate TestResult StringFormat String!");
            }
            else //TODO: remove this ugly hack.
                pTestResultStringFormat = stringFormat;

            // write header and line
            this.WriteLine(ConsoleColor.White, stringFormat, headerValues);
            this.WriteLine(ConsoleColor.White, "-------------------------------------------------------");

            // write read result using string format
            foreach (var writeResult in report.WriteResults)
            {
                VisitTestResult(writeResult);
                flagOddEven = !flagOddEven;
            }

        }

        protected bool flagOddEven = false;

        /// <summary>
        /// Writes testresult (both read and write)
        /// </summary>
        /// <param name="result"></param>
        protected override void VisitTestResult(TestResult result)
        {
            var values = new List<object>();

            ConsoleColor color = ConsoleColor.Gray;

            foreach(var key in result.Results.Keys)
            {
                if (result.ErrorCount != 0)
                    color = ConsoleColor.Red;
                else
                    color = flagOddEven ? ConsoleColor.Gray : ConsoleColor.DarkGreen;

                values.Clear();
                values.Add(result.TestName);
                values.Add(key);

                var list = result.GetList(key);

                foreach (var r in list)
                    values.Add(r);

                values.Add(result.GetAverage(key));

                this.WriteLine(color, pTestResultStringFormat, values.ToArray());
            }
        }

        #endregion
    }
}
