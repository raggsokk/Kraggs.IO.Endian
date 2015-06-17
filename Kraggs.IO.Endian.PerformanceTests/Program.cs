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
            var report = new TestReport(5, 2000000);
#endif

            // 2. add read tests
            report.AddReadTest(ReadPerfTests.CopyReadUInt16);
            report.AddReadTest(ReadPerfTests.SwapReadUInt16);

            // 3. add write tests
            report.AddWriteTest(WritePerfTests.CopyWriteUInt16);
            report.AddWriteTest(WritePerfTests.SwapWriteUInt16);

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
