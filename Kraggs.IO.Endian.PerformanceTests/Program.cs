using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using CommandLine;
using CommandLine.Text;

namespace Kraggs.IO.Endian.PerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var opt = new PerfOptions();

            // 1. First. If no arguments simply print help and exit.
            if(args.Length == 0)
            {
                Console.WriteLine(opt.GetHelp());
                Environment.Exit(0);
            }

            // 2. Parse options.
            //if(CommandLine.Parser.Default.ParseArguments(args, opt))
            if(CommandLine.Parser.Default.ParseArgumentsStrict(args, opt))
            {
                TestReport report = null;

                if(opt.PrintVersion)
                {
                    report = new TestReport(); // TODO: Ugly Hack. recode this.
                    Console.WriteLine("Kraggs.IO.Endian Version: {0,40}", report.KraggsVersion);
                    Console.WriteLine("Kraggs.IO.Endian.PerformanceTests Version: {0,23}", report.KraggsPerfVersion);
                    Environment.Exit(0);                    
                }

                if(!opt.ValidateOptions())
                {
                    Environment.Exit(1);
                }

                report = new TestReport(opt.RunCount, opt.ByteArraySize);

                // since 'MutuallyExclusiveSet' seems to not work
                // we do it another way.
                // first add all tests
                // TODO: maybe use reflection to add much of this in one go?
                report.AddReadTest(ReadPerfTests.CopyReadUInt16);
                report.AddReadTest(ReadPerfTests.SwapReadUInt16);
                report.AddReadTest(ReadPerfTests.CopyReadUInt32);
                report.AddReadTest(ReadPerfTests.SwapReadUInt32);
                report.AddReadTest(ReadPerfTests.CopyReadUInt64);
                report.AddReadTest(ReadPerfTests.SwapReadUInt64);

                report.AddReadTest(ReadPerfTests.CopyReadInt16);
                report.AddReadTest(ReadPerfTests.SwapReadInt16);
                report.AddReadTest(ReadPerfTests.CopyReadInt32);
                report.AddReadTest(ReadPerfTests.SwapReadInt32);
                report.AddReadTest(ReadPerfTests.CopyReadInt64);
                report.AddReadTest(ReadPerfTests.SwapReadInt64);

                report.AddReadTest(ReadPerfTests.CopyReadFloat);
                report.AddReadTest(ReadPerfTests.SwapReadFloat);
                report.AddReadTest(ReadPerfTests.CopyReadDouble);
                report.AddReadTest(ReadPerfTests.SwapReadDouble);

                report.AddWriteTest(WritePerfTests.CopyWriteUInt16);
                report.AddWriteTest(WritePerfTests.SwapWriteUInt16);
                report.AddWriteTest(WritePerfTests.CopyWriteUInt32);
                report.AddWriteTest(WritePerfTests.SwapWriteUInt32);
                report.AddWriteTest(WritePerfTests.CopyWriteUInt64);
                report.AddWriteTest(WritePerfTests.SwapWriteUInt64);

                report.AddWriteTest(WritePerfTests.CopyWriteInt16);
                report.AddWriteTest(WritePerfTests.SwapWriteInt16);
                report.AddWriteTest(WritePerfTests.CopyWriteInt32);
                report.AddWriteTest(WritePerfTests.SwapWriteInt32);
                report.AddWriteTest(WritePerfTests.CopyWriteInt64);
                report.AddWriteTest(WritePerfTests.SwapWriteInt64);

                report.AddWriteTest(WritePerfTests.CopyWriteFloat);
                report.AddWriteTest(WritePerfTests.SwapWriteFloat);
                report.AddWriteTest(WritePerfTests.CopyWriteDouble);
                report.AddWriteTest(WritePerfTests.SwapWriteDouble);

                if (opt.SkipReadTests)
                {
                    report.ReadTests.Clear();
                }

                if(opt.SkipWriteTests)
                {
                    report.WriteTests.Clear();
                }

                // TODO: this is rather ugly code, but it should work.
                // TODO: change ReadTests to dictionary with funcname from reflection?
                if(!string.IsNullOrWhiteSpace(opt.Tests))
                {
                    //TODO: Handle tests not fount.

                    var tests = opt.Tests.Split(new char[] { ',' });
                    var stest = new SortedSet<string>();
                    foreach(var t in tests)
                        stest.Add(t.ToLowerInvariant());
                    
                    var readtests = new List<delRunTest>();

                    foreach(var r in report.ReadTests)
                        if(stest.Contains(r.Method.Name.ToLowerInvariant()))
                            readtests.Add(r);

                    report.ReadTests.Clear();
                    foreach (var r in readtests)
                        report.AddReadTest(r);

                    var writetest = new List<delRunTest>();

                    foreach (var w in report.WriteTests)
                        if (stest.Contains(w.Method.Name.ToLowerInvariant()))
                            writetest.Add(w);

                    report.WriteTests.Clear();
                    foreach (var w in writetest)
                        report.AddWriteTest(w);
                }

                //if(opt.Debug != 0)
                //{
                //    if(opt.Debug == 1)
                //        report.AddReadTest(ReadPerfTests.MacDebug);

                //    report.RunTests();
                //    report.GenerateReport(new ConsoleTestReportVisitor());
                //    Environment.Exit(0);
                //}


                report.RunTests();

                if(!opt.Quiet)
                {
                    report.GenerateReport(new ConsoleTestReportVisitor());
                }

                if(!string.IsNullOrWhiteSpace(opt.TextReportFilename))
                {
                    report.GenerateReport(new TextWriterReportVisitor(opt.TextReportFilename));
                }
            }

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to continue...");
                var key = Console.ReadKey();
            }
        }


    }
}
