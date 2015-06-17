using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;

using Kraggs.IO;

using Mono;

namespace Kraggs.IO.Endian.PerformanceTests
{
    internal static class WritePerfTests
    {
        public static readonly DataConverter MonoNative = DataConverter.Native;
        public static readonly DataConverter MonoSwap = DataConverter.IsLittleEndian ?
            DataConverter.BigEndian : DataConverter.LittleEndian;

        public static EndianConverter KraggsNative;
        public static EndianConverter KraggsSwap;

        static WritePerfTests()
        {
            KraggsNative = EndianConverter.CreateNativeConverter();
            KraggsSwap = EndianConverter.IsLittleEndian ?
                EndianConverter.CreateBitEndianConverter() :
                EndianConverter.CreateLittleEndianConverter();
        }

        #region Unsigned Integers

        public static TestResult CopyWriteUInt16(byte[] buffer, int RunCount)
        {
            //TODO: Use .net  4.5 System.Runtime.CompilerServices.CallerMemberName?
            var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt16);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new UInt16[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoNative.GetUInt16(buffer, bytePos);
                bytePos += ElementSize;
            }

            // create test buffers to write test data to.
            var test1Buffer = new byte[buffer.Length];
            var test2Buffer = new byte[buffer.Length];

            for (int i = 0; i < RunCount; i++)
            {
                // run Mono Converter
                bytePos = 0;
                w.Restart();
                for(int j = 0; j < ElementCount; j++)
                {
                    MonoNative.PutBytes(test1Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for(int j = 0; j < ElementCount; j++)
                {
                    KraggsNative.Write(test2Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add additionally Endian Converters here!

                // Validate
                var errorCount = TestValidate.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult SwapWriteUInt16(byte[] buffer, int RunCount)
        {
            //TODO: Use .net  4.5 System.Runtime.CompilerServices.CallerMemberName?
            var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt16);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new UInt16[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoSwap.GetUInt16(buffer, bytePos);
                bytePos += ElementSize;
            }

            // create test buffers to write test data to.
            var test1Buffer = new byte[buffer.Length];
            var test2Buffer = new byte[buffer.Length];

            for (int i = 0; i < RunCount; i++)
            {
                // run Mono Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    MonoSwap.PutBytes(test1Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    KraggsSwap.Write(test2Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add additionally Endian Converters here!

                // Validate
                var errorCount = TestValidate.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }



        #endregion

        #region Signed Integers

        #endregion

        #region Floatingpoint

        #endregion
    }
}
