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
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
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
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult SwapWriteUInt16(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
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
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult CopyWriteUInt32(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt32);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new UInt32[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoNative.GetUInt32(buffer, bytePos);
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
                    MonoNative.PutBytes(test1Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    KraggsNative.Write(test2Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add additionally Endian Converters here!

                // Validate
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult SwapWriteUInt32(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt32);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new UInt32[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoSwap.GetUInt32(buffer, bytePos);
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
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult CopyWriteUInt64(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt64);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new UInt64[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoNative.GetUInt64(buffer, bytePos);
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
                    MonoNative.PutBytes(test1Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    KraggsNative.Write(test2Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add additionally Endian Converters here!

                // Validate
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult SwapWriteUInt64(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(UInt64);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new UInt64[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoSwap.GetUInt64(buffer, bytePos);
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
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }


        #endregion

        #region Signed Integers

        public static TestResult CopyWriteInt16(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(Int16);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new Int16[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoNative.GetInt16(buffer, bytePos);
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
                    MonoNative.PutBytes(test1Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    KraggsNative.Write(test2Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add additionally Endian Converters here!

                // Validate
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult SwapWriteInt16(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(Int16);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new Int16[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoSwap.GetInt16(buffer, bytePos);
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
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult CopyWriteInt32(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(Int32);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new Int32[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoNative.GetInt32(buffer, bytePos);
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
                    MonoNative.PutBytes(test1Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    KraggsNative.Write(test2Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add additionally Endian Converters here!

                // Validate
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult SwapWriteInt32(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(Int32);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new Int32[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoSwap.GetInt32(buffer, bytePos);
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
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult CopyWriteInt64(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(Int64);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new Int64[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoNative.GetInt64(buffer, bytePos);
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
                    MonoNative.PutBytes(test1Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    KraggsNative.Write(test2Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add additionally Endian Converters here!

                // Validate
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult SwapWriteInt64(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(Int64);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new Int64[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoSwap.GetInt64(buffer, bytePos);
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
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }


        #endregion

        #region Floatingpoint

        public static TestResult CopyWriteFloat(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(float);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new float[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoNative.GetFloat(buffer, bytePos);
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
                    MonoNative.PutBytes(test1Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    KraggsNative.Write(test2Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add additionally Endian Converters here!

                // Validate
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult SwapWriteFloat(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(float);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new float[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoSwap.GetFloat(buffer, bytePos);
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
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult CopyWriteDouble(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(double);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new double[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoNative.GetDouble(buffer, bytePos);
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
                    MonoNative.PutBytes(test1Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Mono", w.ElapsedMilliseconds);

                // run Kraggs Converter
                bytePos = 0;
                w.Restart();
                for (int j = 0; j < ElementCount; j++)
                {
                    KraggsNative.Write(test2Buffer, bytePos, origBuffer[j]);
                    bytePos += ElementSize;
                }
                w.Stop();
                result.AddResult("Kraggs", w.ElapsedMilliseconds);

                // Add additionally Endian Converters here!

                // Validate
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        public static TestResult SwapWriteDouble(byte[] buffer, int RunCount)
        {
            //var funcName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var funcName = PerfMiscUtils.GetCallingFunctionName();
            var result = new TestResult(funcName);
            var w = new Stopwatch();

            var ElementSize = sizeof(double);
            var ElementCount = buffer.Length / ElementSize;
            // generate test data from bytebuffer.
            int bytePos = 0;
            var origBuffer = new double[ElementCount];
            for (int i = 0; i < origBuffer.Length; i++)
            {
                origBuffer[i] = MonoSwap.GetDouble(buffer, bytePos);
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
                var errorCount = PerfMiscUtils.ValidateBuffers(test1Buffer, test2Buffer);
                if (errorCount > 0)
                    result.AddErrors(errorCount);
            }


            return result;
        }

        #endregion
    }
}
