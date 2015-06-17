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
    internal static class ReadPerfTests
    {
        public static readonly DataConverter MonoNative = DataConverter.Native;
        public static readonly DataConverter MonoSwap = DataConverter.IsLittleEndian ?
            DataConverter.BigEndian : DataConverter.LittleEndian;

        public static EndianConverter KraggsNative;
        public static EndianConverter KraggsSwap;

        static ReadPerfTests()
        {
            KraggsNative = EndianConverter.CreateNativeConverter();
            KraggsSwap = EndianConverter.IsLittleEndian ?
                EndianConverter.CreateBitEndianConverter() :
                EndianConverter.CreateLittleEndianConverter();
        }

        #region Unsigned Integers

        public static TestResult CopyReadUInt16(byte[] buffer, int RunCount)
        {
            //TODO: Use .net  4.5 System.Runtime.CompilerServices.CallerMemberName?
            var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt16);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new UInt16[ElementCount];
            var test2Buffer = new UInt16[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for(int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoNative.GetUInt16(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for(int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsNative.ReadUInt16(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<UInt16>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }

        public static TestResult SwapReadUInt16(byte[] buffer, int RunCount)
        {
            //TODO: Use .net  4.5 System.Runtime.CompilerServices.CallerMemberName?
            var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt16);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new UInt16[ElementCount];
            var test2Buffer = new UInt16[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoSwap.GetUInt16(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsSwap.ReadUInt16(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<UInt16>(test1Buffer, test2Buffer);
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
