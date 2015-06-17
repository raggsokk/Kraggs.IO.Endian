using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;

namespace Kraggs.IO.Endian.PerformanceTests
{
    /// <summary>
    /// A simple TextWriter Report Generator.
    /// </summary>
    public class TextWriterReportVisitor : ConsoleTestReportVisitor
    {
        protected TextWriter pWriter;

        public TextWriterReportVisitor(TextWriter writer)
        {
            this.pWriter = writer;
        }

        public TextWriterReportVisitor(Stream stream)
        {
            this.pWriter = new StreamWriter(stream);
        }

        public TextWriterReportVisitor(string filename, bool DeleteOld = true)
        {
            if (DeleteOld && File.Exists(filename))
                File.Delete(filename);

            var f = File.OpenWrite(filename);
            this.pWriter = new StreamWriter(f);
        }

        [DebuggerNonUserCode()]
        public override void VisitTestReport(TestReport report)
        {
            base.VisitTestReport(report);

            pWriter.Flush();
        }

        #region Console Helper Functions Override

        [DebuggerNonUserCode()]
        protected override void Write(ConsoleColor color, string format, params object[] args)
        {
            //base.Write(color, format, args);
            pWriter.Write(format, args);
        }

        [DebuggerNonUserCode()]
        protected override void WriteLine()
        {
            //base.WriteLine();
            pWriter.WriteLine();
        }

        [DebuggerNonUserCode()]
        protected override void WriteLine(ConsoleColor color, string format, params object[] args)
        {
            //base.WriteLine(color, format, args);
            pWriter.WriteLine(format, args);
        }

        #endregion
    }
}
