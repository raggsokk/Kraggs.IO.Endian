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
    [TestFixture]
    public class EndianReadBasicTests
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

        #region Unsigned Integers

        [Test]
        public void UInt16Copy()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16384);
            Assert.AreEqual(
                ReadEndian.GetUInt16Copy(testValue, 0),
                Native.GetUInt16(testValue, 0),
                "16384 failed."
                );

            var maxValue = BitConverter.GetBytes(UInt16.MaxValue);
            Assert.AreEqual(
                ReadEndian.GetUInt16Copy(maxValue, 0),
                Native.GetUInt16(maxValue, 0),
                "UInt16.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(UInt16.MinValue);
            Assert.AreEqual(
                ReadEndian.GetUInt16Copy(minValue, 0),
                Native.GetUInt16(minValue, 0),
                "UInt16.MinValue failed."
                );
        }

        [Test]
        public void UInt16Swap()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16384);
            Assert.AreEqual(
                ReadEndian.GetUInt16Swap(testValue, 0),
                Swap.GetUInt16(testValue, 0),
                "16384 failed."
                );

            var maxValue = BitConverter.GetBytes(UInt16.MaxValue);
            Assert.AreEqual(
                ReadEndian.GetUInt16Swap(maxValue, 0),
                Swap.GetUInt16(maxValue, 0),
                "UInt16.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(UInt16.MinValue);
            Assert.AreEqual(
                ReadEndian.GetUInt16Swap(minValue, 0),
                Swap.GetUInt16(minValue, 0),
                "UInt16.MinValue failed."
                );
        }

        [Test]
        public void UInt32Copy()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(0102030405);
            Assert.AreEqual(
                ReadEndian.GetUInt32Copy(testValue, 0),
                Native.GetUInt32(testValue, 0),
                "0102030405 failed."
                );

            var maxValue = BitConverter.GetBytes(UInt32.MaxValue);
            Assert.AreEqual(
                ReadEndian.GetUInt32Copy(maxValue, 0),
                Native.GetUInt32(maxValue, 0),
                "UInt32.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(UInt32.MinValue);
            Assert.AreEqual(
                ReadEndian.GetUInt32Copy(minValue, 0),
                Native.GetUInt32(minValue, 0),
                "UInt32.MinValue failed."
                );
        }

        [Test]
        public void UInt32Swap()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(0102030405);
            Assert.AreEqual(
                ReadEndian.GetUInt32Swap(testValue, 0),
                Swap.GetUInt32(testValue, 0),
                "0102030405 failed."
                );

            var maxValue = BitConverter.GetBytes(UInt32.MaxValue);
            Assert.AreEqual(
                ReadEndian.GetUInt32Swap(maxValue, 0),
                Swap.GetUInt32(maxValue, 0),
                "UInt32.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(UInt32.MinValue);
            Assert.AreEqual(
                ReadEndian.GetUInt32Swap(minValue, 0),
                Swap.GetUInt32(minValue, 0),
                "UInt32.MinValue failed."
                );
        }

        [Test]
        public void UInt64Copy()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(0102030405ul);
            Assert.AreEqual(
                ReadEndian.GetUInt64Copy(testValue, 0),
                Native.GetUInt64(testValue, 0),
                "0102030405ul failed."
                );

            var maxValue = BitConverter.GetBytes(UInt64.MaxValue);
            Assert.AreEqual(
                ReadEndian.GetUInt64Copy(maxValue, 0),
                Native.GetUInt64(maxValue, 0),
                "UInt64.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(UInt64.MinValue);
            Assert.AreEqual(
                ReadEndian.GetUInt64Copy(minValue, 0),
                Native.GetUInt64(minValue, 0),
                "UInt64.MinValue failed."
                );
        }

        [Test]
        public void UInt64Swap()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(0102030405ul);
            Assert.AreEqual(
                ReadEndian.GetUInt64Swap(testValue, 0),
                Swap.GetUInt64(testValue, 0),
                "0102030405ul failed."
                );

            var maxValue = BitConverter.GetBytes(UInt64.MaxValue);            
            Assert.AreEqual(
                ReadEndian.GetUInt64Swap(maxValue, 0),
                Swap.GetUInt64(maxValue, 0),
                "UInt64.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(UInt64.MinValue);
            Assert.AreEqual(
                ReadEndian.GetUInt64Swap(minValue, 0),
                Swap.GetUInt64(minValue, 0),
                "UInt64.MinValue failed."
                );
        }

        #endregion

        #region Floatingpoint

        [Test]
        public void FloatCopy()
        {
            var maxValue = BitConverter.GetBytes(Single.MaxValue);

            Assert.AreEqual(
                ReadEndian.GetFloatCopy(maxValue, 0),
                Native.GetFloat(maxValue, 0),
                "Single.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(Single.MinValue);
            Assert.AreEqual(
                ReadEndian.GetFloatCopy(minValue, 0),
                Native.GetFloat(minValue, 0),
                "Single.MinValue failed."
                );
        }

        [Test]
        public void FloatSwap()
        {
            var maxValue = BitConverter.GetBytes(Single.MaxValue);

            Assert.AreEqual(
                ReadEndian.GetFloatSwap(maxValue, 0),
                Swap.GetFloat(maxValue, 0),
                "Single.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(Single.MinValue);
            Assert.AreEqual(
                ReadEndian.GetFloatSwap(minValue, 0),
                Swap.GetFloat(minValue, 0),
                "Single.MinValue failed."
                );
        }

        [Test]
        public void DoubleCopy()
        {
            var maxValue = BitConverter.GetBytes(Double.MaxValue);

            Assert.AreEqual(
                ReadEndian.GetDoubleCopy(maxValue, 0),
                Native.GetDouble(maxValue, 0),
                "Double.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(Double.MinValue);
            Assert.AreEqual(
                ReadEndian.GetDoubleCopy(minValue, 0),
                Native.GetDouble(minValue, 0),
                "Double.MinValue failed."
                );
        }

        [Test]
        public void DoubleSwap()
        {
            var maxValue = BitConverter.GetBytes(Double.MaxValue);

            Assert.AreEqual(
                ReadEndian.GetDoubleSwap(maxValue, 0),
                Swap.GetDouble(maxValue, 0),
                "Double.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(Double.MinValue);
            Assert.AreEqual(
                ReadEndian.GetDoubleSwap(minValue, 0),
                Swap.GetDouble(minValue, 0),
                "Double.MinValue failed."
                );
        }

        #endregion

        #region Signed integers

        [Test]
        public void Int16Copy()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16084);
            Assert.AreEqual(
                ReadEndian.GetInt16Copy(testValue, 0),
                Native.GetInt16(testValue, 0),
                "16084 failed."
                );
            // second a simple neg value test.
            var testValue2 = BitConverter.GetBytes(-8192);
            Assert.AreEqual(
                ReadEndian.GetInt16Copy(testValue2, 0),
                Native.GetInt16(testValue2, 0),
                "-8192 failed."
                );

            // test Int16 max 
            var maxInt16 = BitConverter.GetBytes(Int16.MaxValue);
            Assert.AreEqual(
                ReadEndian.GetInt16Copy(maxInt16, 0),
                Native.GetInt16(maxInt16, 0),
                "Int16.MaxValue Failed."
                );

            var minInt16 = BitConverter.GetBytes(Int16.MinValue);

            Assert.AreEqual(
                ReadEndian.GetInt16Copy(minInt16, 0),
                Native.GetInt16(minInt16, 0),
                "Int16.MinValue Failed."
                );

        }

        [Test]
        public void Int16Swap()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16084);
            Assert.AreEqual(
                ReadEndian.GetInt16Swap(testValue, 0),
                Swap.GetInt16(testValue, 0),
                "16084 failed."
                );
            // second a simple neg value test.
            var testValue2 = BitConverter.GetBytes(-8192);
            Assert.AreEqual(
                ReadEndian.GetInt16Swap(testValue2, 0),
                Swap.GetInt16(testValue2, 0),
                "-8192 failed."
                );

            // test Int16 max 
            var maxInt16 = BitConverter.GetBytes(Int16.MaxValue);
            Assert.AreEqual(
                ReadEndian.GetInt16Swap(maxInt16, 0),
                Swap.GetInt16(maxInt16, 0),
                "Int16.MaxValue failed"
                );

            var minInt16 = BitConverter.GetBytes(Int16.MinValue);

            Assert.AreEqual(
                ReadEndian.GetInt16Swap(minInt16, 0),
                Swap.GetInt16(minInt16, 0),
                "Int16.MinValue Failed."
                );
        }

        [Test]
        public void Int32Copy()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16084 * 4);
            Assert.AreEqual(
                ReadEndian.GetInt32Copy(testValue, 0),
                Native.GetInt32(testValue, 0),
                "16084 * 4 failed."
                );
            // second a simple neg value test.
            var testValue2 = BitConverter.GetBytes(-8192 * 4);
            Assert.AreEqual(
                ReadEndian.GetInt32Copy(testValue2, 0),
                Native.GetInt32(testValue2, 0),
                "-8192 * 4 failed."
                );

            // test int32 max 
            var maxInt32 = BitConverter.GetBytes(Int32.MaxValue);
            Assert.AreEqual(
                ReadEndian.GetInt32Copy(maxInt32, 0),
                Native.GetInt32(maxInt32, 0),
                "Int32.MaxValue Failed."                
                );

            var minInt32 = BitConverter.GetBytes(Int32.MinValue);

            Assert.AreEqual(
                ReadEndian.GetInt32Copy(minInt32, 0),
                Native.GetInt32(minInt32, 0),
                "Int32.MinValue Failed."
                );

        }

        [Test]
        public void Int32Swap()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16084 * 4);
            Assert.AreEqual(
                ReadEndian.GetInt32Swap(testValue, 0),
                Swap.GetInt32(testValue, 0),
                "16084 * 4 failed."
                );
            // second a simple neg value test.
            var testValue2 = BitConverter.GetBytes(-8192 * 4);
            Assert.AreEqual(
                ReadEndian.GetInt32Swap(testValue2, 0),
                Swap.GetInt32(testValue2, 0),
                "-8192 * 4 failed."
                );

            // test int32 max 
            var maxInt32 = BitConverter.GetBytes(Int32.MaxValue);
            Assert.AreEqual(
                ReadEndian.GetInt32Swap(maxInt32, 0),
                Swap.GetInt32(maxInt32, 0),
                "Int32.MaxValue failed"
                );

            var minInt32 = BitConverter.GetBytes(Int32.MinValue);

            Assert.AreEqual(
                ReadEndian.GetInt32Swap(minInt32, 0),
                Swap.GetInt32(minInt32, 0),
                "Int32.MinValue Failed."
                );
        }

        [Test]
        public void Int64Copy()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16084L);
            Assert.AreEqual(
                ReadEndian.GetInt64Copy(testValue, 0),
                Native.GetInt64(testValue, 0),
                "16084L failed."
                );
            // second a simple neg value test.
            var testValue2 = BitConverter.GetBytes(-8192L);
            Assert.AreEqual(
                ReadEndian.GetInt64Copy(testValue2, 0),
                Native.GetInt64(testValue2, 0),
                "-8192L failed."
                );

            // test Int64 max 
            var maxInt64 = BitConverter.GetBytes(Int64.MaxValue);
            Assert.AreEqual(
                ReadEndian.GetInt64Copy(maxInt64, 0),
                Native.GetInt64(maxInt64, 0),
                "Int64.MaxValue Failed."
                );

            var minInt64 = BitConverter.GetBytes(Int64.MinValue);

            Assert.AreEqual(
                ReadEndian.GetInt64Copy(minInt64, 0),
                Native.GetInt64(minInt64, 0),
                "Int64.MinValue Failed."
                );

        }

        [Test]
        public void Int64Swap()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16084L);
            Assert.AreEqual(
                ReadEndian.GetInt64Swap(testValue, 0),
                Swap.GetInt64(testValue, 0),
                "16084L failed."
                );
            // second a simple neg value test.
            var testValue2 = BitConverter.GetBytes(-8192L);
            Assert.AreEqual(
                ReadEndian.GetInt64Swap(testValue2, 0),
                Swap.GetInt64(testValue2, 0),
                "-8192L failed."
                );

            // test Int64 max 
            var maxInt64 = BitConverter.GetBytes(Int64.MaxValue);
            Assert.AreEqual(
                ReadEndian.GetInt64Swap(maxInt64, 0),
                Swap.GetInt64(maxInt64, 0),
                "Int64.MaxValue failed"
                );

            var minInt64 = BitConverter.GetBytes(Int64.MinValue);

            Assert.AreEqual(
                ReadEndian.GetInt64Swap(minInt64, 0),
                Swap.GetInt64(minInt64, 0),
                "Int64.MinValue Failed."
                );
        }

        #endregion
    }
}
