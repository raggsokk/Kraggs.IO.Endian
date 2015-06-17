using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraggs.IO.Endian.PerformanceTests
{
    internal static class TestValidate
    {
        /// <summary>
        /// Compares 2 generic buffers of same type.
        /// Slower than real type funsions but a lot less to code....
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="leftBuffer"></param>
        /// <param name="rightBuffer"></param>
        /// <returns></returns>
        internal static long ValidateBuffers<T>(T[] leftBuffer, T[] rightBuffer) where T : struct
        {
            if (leftBuffer.Length != rightBuffer.Length)
                return Math.Max(leftBuffer.Length, rightBuffer.Length);

            var count = leftBuffer.Length;
            long errorCount = 0;

            for (int i = 0; i < count; i++)
            {
                if (Comparer<T>.Default.Compare(leftBuffer[i], rightBuffer[i]) != 0)
                    errorCount++;
            }


            return errorCount;
        }

        internal static long ValidateBuffers(byte[] leftBuffer, byte[] rightBuffer)
        {
            if (leftBuffer.Length != rightBuffer.Length)
                return Math.Max(leftBuffer.Length, rightBuffer.Length);

            var count = leftBuffer.Length;
            long errorCount = 0;

            for (int i = 0; i < count;i++)
            {
                if (leftBuffer[i] != rightBuffer[i])
                    errorCount++;
            }

            return errorCount;
        }
    }
}
