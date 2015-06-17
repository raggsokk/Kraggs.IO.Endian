using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraggs.IO.Endian.PerformanceTests
{
    /// <summary>
    /// Abstract TestReport visitor base class for templating generating report from test results.
    /// </summary>
    public abstract class TestReportVisitor
    {
        //TODO: Redesign code to not have TestReport as call variable everywhere.

        public virtual void VisitTestReport(TestReport report)
        {
            // 1. write header.
            this.VisitHeader(report);

            // 2. visit read tests.
            this.VisitReadTests(report);

            // 3. visit write tests.
            this.VisitWriteTests(report);

            // 4. visit footer
            this.VisitFooter(report);
        }

        protected abstract void VisitHeader(TestReport report);
        protected abstract void VisitFooter(TestReport report);

        protected abstract void VisitReadTests(TestReport report);
        protected abstract void VisitWriteTests(TestReport report);

        protected abstract void VisitTestResult(TestResult result);
    }
}
