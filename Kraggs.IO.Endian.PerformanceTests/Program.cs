using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Kraggs.IO.Endian.PerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. make report
#if DEBUG
            var report = new TestReport(3, 20000);
#else
            var report = new TestReport(5, 20000000);
#endif

            // 2. add read tests
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

            // 3. add write tests
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

            // 4. run tests
            report.RunTests();

            // 5 generate reports
            report.GenerateReport(new ConsoleTestReportVisitor());

            // 6. generate file report too.
#if !DEBUG
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                var fname = @"D:\Temp\EndianPerformanceReport.txt";
                report.GenerateReport(new TextWriterReportVisitor(fname, true));                        
            }
#endif

            // 7. prevent console to exit if inside debugger.
            if(System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to continue...");
                var key = Console.ReadKey();
            }
        }
    }
}
