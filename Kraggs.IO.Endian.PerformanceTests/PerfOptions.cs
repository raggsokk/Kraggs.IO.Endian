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
    public class PerfOptions
    {
        public const int BYTEARRAYSIZEMIN = 16;
        public const int BYTEARRAYSIZEMAX = 100000000;
        public const int BYTEARRAYDEFAULT = 20000000;

        public const int RUNCOUNTMIN = 1;
        public const int RUNCOUNTMAX = 10;
        public const int RUNCOUNTDEFAULT = 3;

        [Option('c', "count", // DefaultValue=RUNCOUNTDEFAULT, 
            HelpText="Number of times tests are run.")]
        public int RunCount { get; set; }

        [Option('s', "size", //DefaultValue = BYTEARRAYDEFAULT, 
            HelpText="Size of byte array to run test at")]
        public int ByteArraySize { get; set; }

        [Option("SkipReadTests", HelpText = "Skip running read tests.")]
        public bool SkipReadTests { get; set; }

        [Option("SkipWriteTests", HelpText = "Skip running write tests.")]
        public bool SkipWriteTests { get; set; }

        // reports

        [Option('q', "quiet", HelpText="Dont print report results to console.")]
        public bool Quiet { get; set; }

        [Option("txtreport", HelpText = "Output a txt report to this filename")]
        public string TextReportFilename { get; set; }

        [HelpOption('h', "help")]
        public string GetHelp()
        {
            var help = new HelpText()
            {
                AddDashesToOption = true,
                AdditionalNewLineAfterOption = true,
            };

            help.AddPreOptionsLine(string.Format(
                "Usage: <app> -c {0} -s {1}", RUNCOUNTDEFAULT, BYTEARRAYDEFAULT));
            help.AddOptions(this);

            return help;
        }

        public bool ValidateOptions()
        {
            bool flagError = false;

            if(RunCount < RUNCOUNTMIN || RunCount > RUNCOUNTMAX)
            {
                Console.WriteLine("Error: Arg Count must be between '{0}' and '{1}'. Currently '{2}'",
                    RUNCOUNTMIN, RUNCOUNTMAX, RunCount);
                flagError = true;
            }

            if(ByteArraySize < BYTEARRAYSIZEMIN || ByteArraySize > BYTEARRAYSIZEMAX)
            {
                Console.WriteLine("Error: Arg Size mist be between '{0}' and '{1}'. Currently '{2}'",
                    BYTEARRAYSIZEMIN, BYTEARRAYSIZEMAX, ByteArraySize);
                flagError = true;
            }

            if(!string.IsNullOrWhiteSpace(TextReportFilename))
            {
                if (File.Exists(TextReportFilename))
                {
                    Console.WriteLine("Error: Cant write Text report to existing file '{0}'", TextReportFilename);
                    flagError = true;
                }
            }

            if(flagError)
            {
                Console.WriteLine("Maybe try '--help' for more information?");
            }

            return !flagError;
        }
    }
}
