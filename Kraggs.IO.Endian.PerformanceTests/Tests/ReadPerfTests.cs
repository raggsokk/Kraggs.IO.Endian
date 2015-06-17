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
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt16);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new UInt16[ElementCount];
            var test2Buffer = new UInt16[ElementCount];
            var test3Buffer = new UInt16[ElementCount];

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
                bytePos = 0;
                w.Restart();
                for(int j = 0; j < ElementCount; j++)
                {
                    test3Buffer[j] = DataConverter.UInt16FromNative(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono2", w.ElapsedMilliseconds);

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<UInt16>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }

        public static TestResult SwapReadUInt16(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt16);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new UInt16[ElementCount];
            var test2Buffer = new UInt16[ElementCount];
            var test3Buffer = new UInt16[ElementCount];

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
                bytePos = 0;
                w.Restart();
                var flagLittleEndian = DataConverter.IsLittleEndian;
                for (int j = 0; j < ElementCount; j++)
                {
                    if(flagLittleEndian)
                        test3Buffer[j] = DataConverter.UInt16FromBE(buffer, bytePos);
                    else
                        test3Buffer[j] = DataConverter.UInt16FromLE(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("MonoS", w.ElapsedMilliseconds);


                // validate results.
                var errorCount = TestValidate.ValidateBuffers<UInt16>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }

        public static TestResult CopyReadUInt32(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt32);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new UInt32[ElementCount];
            var test2Buffer = new UInt32[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoNative.GetUInt32(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsNative.ReadUInt32(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<UInt32>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }

        public static TestResult SwapReadUInt32(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt32);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new UInt32[ElementCount];
            var test2Buffer = new UInt32[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoSwap.GetUInt32(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsSwap.ReadUInt32(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<UInt32>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }


        public static TestResult CopyReadUInt64(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt64);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new UInt64[ElementCount];
            var test2Buffer = new UInt64[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoNative.GetUInt64(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsNative.ReadUInt64(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<UInt64>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }

        public static TestResult SwapReadUInt64(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt64);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new UInt64[ElementCount];
            var test2Buffer = new UInt64[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoSwap.GetUInt64(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsSwap.ReadUInt64(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<UInt64>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }


        #endregion

        #region Signed Integers

        public static TestResult CopyReadInt16(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(Int16);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new Int16[ElementCount];
            var test2Buffer = new Int16[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoNative.GetInt16(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsNative.ReadInt16(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<Int16>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }

        public static TestResult SwapReadInt16(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(Int16);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new Int16[ElementCount];
            var test2Buffer = new Int16[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoSwap.GetInt16(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsSwap.ReadInt16(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<Int16>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }

        public static TestResult CopyReadInt32(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(Int32);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new Int32[ElementCount];
            var test2Buffer = new Int32[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoNative.GetInt32(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsNative.ReadInt32(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<Int32>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }

        public static TestResult SwapReadInt32(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(Int32);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new Int32[ElementCount];
            var test2Buffer = new Int32[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoSwap.GetInt32(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsSwap.ReadInt32(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<Int32>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }


        public static TestResult CopyReadInt64(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(Int64);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new Int64[ElementCount];
            var test2Buffer = new Int64[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoNative.GetInt64(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsNative.ReadInt64(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<Int64>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }

        public static TestResult SwapReadInt64(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(Int64);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new Int64[ElementCount];
            var test2Buffer = new Int64[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoSwap.GetInt64(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsSwap.ReadInt64(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<Int64>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }


        #endregion

        #region Floatingpoint

        public static TestResult CopyReadFloat(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(float);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new float[ElementCount];
            var test2Buffer = new float[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoNative.GetFloat(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsNative.ReadFloat(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<float>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }

        public static TestResult SwapReadFloat(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(float);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new float[ElementCount];
            var test2Buffer = new float[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoSwap.GetFloat(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsSwap.ReadFloat(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<float>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }

        public static TestResult CopyReadDouble(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(double);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new double[ElementCount];
            var test2Buffer = new double[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoNative.GetDouble(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsNative.ReadDouble(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<double>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }

        public static TestResult SwapReadDouble(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = TestValidate.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(double);
            var ElementCount = buffer.Length / ElementSize;
            var test1Buffer = new double[ElementCount];
            var test2Buffer = new double[ElementCount];

            for (int i = 0; i < RunCount; i++)
            {
                int bytePos = 0;

                // run Mono Converter
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test1Buffer[j] = MonoSwap.GetDouble(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    test2Buffer[j] = KraggsSwap.ReadDouble(buffer, bytePos);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add other Endian implementatins here!

                // validate results.
                var errorCount = TestValidate.ValidateBuffers<double>(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }

            return result;
        }

        #endregion
    }
}
