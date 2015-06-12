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
    public class EndianWriteBasicTests
    {
        private DataConverter Native;
        private DataConverter Swap;

        [SetUp]
        public void Setup()
        {
            if (Native == null)
                Native = DataConverter.Native;
            if (Swap == null)
                Swap = DataConverter.IsLittleEndian ? DataConverter.BigEndian : DataConverter.LittleEndian;
        }

        private static bool AreEqual(byte[] buf1, byte[] buf2)
        {
            if (buf1 == null & buf2 == null)
                return true;
            if (buf1 == null || buf2 == null)
                return false;
            if (buf1.Length != buf2.Length)
                return false;

            for (int i = 0; i < buf1.Length; i++)
                if (buf1[i] != buf2[i])
                    return false;

            return true;
        }

        #region Unsigned Integers

        [Test]
        public void UInt16Copy()
        {
            // first a simple value test.
            UInt16 val = 16384;
            var test1Buffer = new byte[sizeof(UInt16)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = UInt16.MaxValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = UInt16.MinValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        [Test]
        public void UInt16Swap()
        {
            // first a simple value test.
            UInt16 val = 16384;
            var test1Buffer = new byte[sizeof(UInt16)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = UInt16.MaxValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = UInt16.MinValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        [Test]
        public void UInt32Copy()
        {
            // first a simple value test.
            UInt32 val = 16384;
            var test1Buffer = new byte[sizeof(UInt32)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = UInt32.MaxValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = UInt32.MinValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        [Test]
        public void UInt32Swap()
        {
            // first a simple value test.
            UInt32 val = 16384;
            var test1Buffer = new byte[sizeof(UInt32)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = UInt32.MaxValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = UInt32.MinValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        [Test]
        public void UInt64Copy()
        {
            // first a simple value test.
            UInt64 val = 16384;
            var test1Buffer = new byte[sizeof(UInt64)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = UInt64.MaxValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = UInt64.MinValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        [Test]
        public void UInt64Swap()
        {
            // first a simple value test.
            UInt64 val = 16384;
            var test1Buffer = new byte[sizeof(UInt64)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = UInt64.MaxValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = UInt64.MinValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        #endregion

        #region Floating Point 

        [Test]
        public void SingleCopy()
        {
            // first a simple value test.
            Single val = 16384;
            var test1Buffer = new byte[sizeof(Single)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = Single.MaxValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = Single.MinValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        [Test]
        public void SingleSwap()
        {
            // first a simple value test.
            Single val = 16384;
            var test1Buffer = new byte[sizeof(Single)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = Single.MaxValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = Single.MinValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        [Test]
        public void DoubleCopy()
        {
            // first a simple value test.
            Double val = 16384;
            var test1Buffer = new byte[sizeof(Double)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = Double.MaxValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = Double.MinValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        [Test]
        public void DoubleSwap()
        {
            // first a simple value test.
            Double val = 16384;
            var test1Buffer = new byte[sizeof(Double)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = Double.MaxValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = Double.MinValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        #endregion

        #region Signed integers

        [Test]
        public void Int16Copy()
        {
            // first a simple value test.
            Int16 val = 16384;
            var test1Buffer = new byte[sizeof(Int16)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = Int16.MaxValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = Int16.MinValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        [Test]
        public void Int16Swap()
        {
            // first a simple value test.
            Int16 val = 16384;
            var test1Buffer = new byte[sizeof(Int16)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = Int16.MaxValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = Int16.MinValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        [Test]
        public void Int32Copy()
        {
            // first a simple value test.
            Int32 val = 16384;
            var test1Buffer = new byte[sizeof(Int32)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = Int32.MaxValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = Int32.MinValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        [Test]
        public void Int32Swap()
        {
            // first a simple value test.
            Int32 val = 16384;
            var test1Buffer = new byte[sizeof(Int32)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = Int32.MaxValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = Int32.MinValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        [Test]
        public void Int64Copy()
        {
            // first a simple value test.
            Int64 val = 16384;
            var test1Buffer = new byte[sizeof(Int64)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = Int64.MaxValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = Int64.MinValue;
            WriteEndian.PutBytesCopy(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }

        [Test]
        public void Int64Swap()
        {
            // first a simple value test.
            Int64 val = 16384;
            var test1Buffer = new byte[sizeof(Int64)];
            var test2Buffer = new byte[test1Buffer.Length];
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "16384 failed.");

            // maxvalue
            val = Int64.MaxValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MaxValue failed.");

            // maxvalue
            val = Int64.MinValue;
            WriteEndian.PutBytesSwap(test1Buffer, 0, val);
            Swap.PutBytes(test2Buffer, 0, val);

            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "MinValue failed.");

        }


        #endregion
    }
}
