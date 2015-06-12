using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit;
using NUnit.Core;
using NUnit.Framework;

using Mono;
using Kraggs.IO.Endian;


namespace Kraggs.IO.Endian.Tests
{
    [TestFixture()]
    public class EndianReadRandomTests
    {
        private byte[] pBuffer;
        const int BUFFERSIZE = 1000000;

        private DataConverter Native;
        private DataConverter Swap;

        [SetUp]
        public void Setup()
        {
            if(pBuffer == null)
            {
                // create and randomize.
                this.pBuffer = new byte[BUFFERSIZE * 8];
                var r = new Random();
                r.NextBytes(this.pBuffer);
            }

            if (Native == null)
                Native = DataConverter.Native;
            if (Swap == null)
                Swap = DataConverter.IsLittleEndian ? DataConverter.BigEndian : DataConverter.LittleEndian;
         
        }

        #region Unsigned integer Random bytes tests

        [Test]
        public void UInt16CopyValidate()
        {
            var test1Buffer = new UInt16[BUFFERSIZE / sizeof(UInt16)];
            var test2Buffer = new UInt16[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetUInt16Copy(pBuffer, bytePos);
                test2Buffer[i] = Native.GetUInt16(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        [Test]
        public void UInt16SwapValidate()
        {
            var test1Buffer = new UInt16[BUFFERSIZE / sizeof(UInt16)];
            var test2Buffer = new UInt16[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetUInt16Swap(pBuffer, bytePos);
                test2Buffer[i] = Swap.GetUInt16(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        [Test]
        public void UInt32CopyValidate()
        {
            var test1Buffer = new UInt32[BUFFERSIZE / sizeof(UInt32)];
            var test2Buffer = new UInt32[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetUInt32Copy(pBuffer, bytePos);
                test2Buffer[i] = Native.GetUInt32(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        [Test]
        public void UInt32SwapValidate()
        {
            var test1Buffer = new UInt32[BUFFERSIZE / sizeof(UInt32)];
            var test2Buffer = new UInt32[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetUInt32Swap(pBuffer, bytePos);
                test2Buffer[i] = Swap.GetUInt32(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        [Test]
        public void UInt64CopyValidate()
        {
            var test1Buffer = new UInt64[BUFFERSIZE / sizeof(UInt64)];
            var test2Buffer = new UInt64[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetUInt64Copy(pBuffer, bytePos);
                test2Buffer[i] = Native.GetUInt64(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        [Test]
        public void UInt64SwapValidate()
        {
            var test1Buffer = new UInt64[BUFFERSIZE / sizeof(UInt64)];
            var test2Buffer = new UInt64[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetUInt64Swap(pBuffer, bytePos);
                test2Buffer[i] = Swap.GetUInt64(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        #endregion


        #region Floating point random bytes tests

        [Test]
        public void FloatCopyValidate()
        {
            var test1Buffer = new Single[BUFFERSIZE / sizeof(Single)];
            var test2Buffer = new Single[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetFloatCopy(pBuffer, bytePos);
                test2Buffer[i] = Native.GetFloat(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        [Test]
        public void FloatSwapValidate()
        {
            var test1Buffer = new Single[BUFFERSIZE / sizeof(Single)];
            var test2Buffer = new Single[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetFloatSwap(pBuffer, bytePos);
                test2Buffer[i] = Swap.GetFloat(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        [Test]
        public void DoubleCopyValidate()
        {
            var test1Buffer = new Double[BUFFERSIZE / sizeof(Double)];
            var test2Buffer = new Double[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetDoubleCopy(pBuffer, bytePos);
                test2Buffer[i] = Native.GetDouble(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        [Test]
        public void DoubleSwapValidate()
        {
            var test1Buffer = new Double[BUFFERSIZE / sizeof(Double)];
            var test2Buffer = new Double[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetDoubleSwap(pBuffer, bytePos);
                test2Buffer[i] = Swap.GetDouble(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        #endregion

        #region Signed integer random bytes tests

        [Test]
        public void Int16CopyValidate()
        {
            var test1Buffer = new Int16[BUFFERSIZE / sizeof(Int16)];
            var test2Buffer = new Int16[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetInt16Copy(pBuffer, bytePos);
                test2Buffer[i] = Native.GetInt16(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        [Test]
        public void Int16SwapValidate()
        {
            var test1Buffer = new Int16[BUFFERSIZE / sizeof(Int16)];
            var test2Buffer = new Int16[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetInt16Swap(pBuffer, bytePos);
                test2Buffer[i] = Swap.GetInt16(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        [Test]
        public void Int32CopyValidate()
        {
            var test1Buffer = new Int32[BUFFERSIZE / sizeof(Int32)];
            var test2Buffer = new Int32[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetInt32Copy(pBuffer, bytePos);
                test2Buffer[i] = Native.GetInt32(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        [Test]
        public void Int32SwapValidate()
        {
            var test1Buffer = new Int32[BUFFERSIZE / sizeof(Int32)];
            var test2Buffer = new Int32[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetInt32Swap(pBuffer, bytePos);
                test2Buffer[i] = Swap.GetInt32(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        [Test]
        public void Int64CopyValidate()
        {
            var test1Buffer = new Int64[BUFFERSIZE / sizeof(Int64)];
            var test2Buffer = new Int64[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetInt64Copy(pBuffer, bytePos);
                test2Buffer[i] = Native.GetInt64(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        [Test]
        public void Int64SwapValidate()
        {
            var test1Buffer = new Int64[BUFFERSIZE / sizeof(Int64)];
            var test2Buffer = new Int64[test1Buffer.Length];
            var bytePos = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
            {
                test1Buffer[i] = ReadEndian.GetInt64Swap(pBuffer, bytePos);
                test2Buffer[i] = Swap.GetInt64(pBuffer, bytePos);
            }

            var errorCount = 0;

            for (int i = 0; i < test1Buffer.Length; i++)
                if (test1Buffer[i] != test2Buffer[i])
                    errorCount++;

            Assert.AreEqual(errorCount, 0,
                string.Format("Failed {0} of {1} random test conversions",
                errorCount, test1Buffer.Length));
        }

        #endregion
    }
}
