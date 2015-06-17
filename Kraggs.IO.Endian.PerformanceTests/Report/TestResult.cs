using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Kraggs.IO.Endian.PerformanceTests
{
    /// <summary>
    /// Contains the result of a single test.
    /// That includes the different implementations in the Result Key.
    /// </summary>
    public class TestResult
    {
        public string TestName { get; protected set; }
        public long ErrorCount { get; protected set; }

        public SortedList<string, List<long>> Results { get; protected set; }

        [DebuggerNonUserCode()]
        public TestResult(string TestName)
        {
            this.TestName = TestName;
            Results = new SortedList<string, List<long>>();
        }

        [DebuggerNonUserCode()]
        public void AddErrors(long errorCount)
        {
            this.ErrorCount += errorCount;
        }

        public void AddResult(string Converter, long result)
        {
            List<long> list = null;

            if(!Results.TryGetValue(Converter, out list))
            {
                list = new List<long>();
                Results.Add(Converter, list);
            }
            list.Add(result);

            //if (Results.TryGetValue(Converter, out list))
            //{
            //    list.Add(result);
            //}
            //else
            //{
            //    list = new List<long>();
            //    list.Add(result);
            //    Results.Add(Converter, list);
            //}

        }

        [DebuggerNonUserCode()]
        public List<long> GetList(string Converter)
        {
            return Results[Converter];
        }

        [DebuggerNonUserCode()]
        public float GetAverage(string Converter)
        {
            //TODO: Remove top and bottom peaks and average over the rest. Requires at lest 5 runs thou.
            var list = GetList(Converter);

            return (float)list.Average();
        }

    }
}
