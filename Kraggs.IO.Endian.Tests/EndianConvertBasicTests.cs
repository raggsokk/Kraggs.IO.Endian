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
    public class EndianConvertBasicTests
    {
        #region Unsigned Integers

        [Test]
        public void UInt16Copy()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16384);
            Assert.AreEqual(
                ConvertEndian.GetUInt16Copy(testValue, 0),
                DataConverter.LittleEndian.GetUInt16(testValue, 0),
                "16384 failed."
                );

            var maxValue = BitConverter.GetBytes(UInt16.MaxValue);
            Assert.AreEqual(
                ConvertEndian.GetUInt16Copy(maxValue, 0),
                DataConverter.LittleEndian.GetUInt16(maxValue, 0),
                "UInt16.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(UInt16.MinValue);
            Assert.AreEqual(
                ConvertEndian.GetUInt16Copy(minValue, 0),
                DataConverter.LittleEndian.GetUInt16(minValue, 0),
                "UInt16.MinValue failed."
                );
        }

        [Test]
        public void UInt16Swap()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16384);
            Assert.AreEqual(
                ConvertEndian.GetUInt16Swap(testValue, 0),
                DataConverter.BigEndian.GetUInt16(testValue, 0),
                "16384 failed."
                );

            var maxValue = BitConverter.GetBytes(UInt16.MaxValue);
            Assert.AreEqual(
                ConvertEndian.GetUInt16Swap(maxValue, 0),
                DataConverter.BigEndian.GetUInt16(maxValue, 0),
                "UInt16.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(UInt16.MinValue);
            Assert.AreEqual(
                ConvertEndian.GetUInt16Swap(minValue, 0),
                DataConverter.BigEndian.GetUInt16(minValue, 0),
                "UInt16.MinValue failed."
                );
        }

        [Test]
        public void UInt32Copy()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(0102030405);
            Assert.AreEqual(
                ConvertEndian.GetUInt32Copy(testValue, 0),
                DataConverter.LittleEndian.GetUInt32(testValue, 0),
                "0102030405 failed."
                );

            var maxValue = BitConverter.GetBytes(UInt32.MaxValue);
            Assert.AreEqual(
                ConvertEndian.GetUInt32Copy(maxValue, 0),
                DataConverter.LittleEndian.GetUInt32(maxValue, 0),
                "UInt32.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(UInt32.MinValue);
            Assert.AreEqual(
                ConvertEndian.GetUInt32Copy(minValue, 0),
                DataConverter.LittleEndian.GetUInt32(minValue, 0),
                "UInt32.MinValue failed."
                );
        }

        [Test]
        public void UInt32Swap()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(0102030405);
            Assert.AreEqual(
                ConvertEndian.GetUInt32Swap(testValue, 0),
                DataConverter.BigEndian.GetUInt32(testValue, 0),
                "0102030405 failed."
                );

            var maxValue = BitConverter.GetBytes(UInt32.MaxValue);
            Assert.AreEqual(
                ConvertEndian.GetUInt32Swap(maxValue, 0),
                DataConverter.BigEndian.GetUInt32(maxValue, 0),
                "UInt32.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(UInt32.MinValue);
            Assert.AreEqual(
                ConvertEndian.GetUInt32Swap(minValue, 0),
                DataConverter.BigEndian.GetUInt32(minValue, 0),
                "UInt32.MinValue failed."
                );
        }

        [Test]
        public void UInt64Copy()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(0102030405ul);
            Assert.AreEqual(
                ConvertEndian.GetUInt64Copy(testValue, 0),
                DataConverter.LittleEndian.GetUInt64(testValue, 0),
                "0102030405ul failed."
                );

            var maxValue = BitConverter.GetBytes(UInt64.MaxValue);
            Assert.AreEqual(
                ConvertEndian.GetUInt64Copy(maxValue, 0),
                DataConverter.LittleEndian.GetUInt64(maxValue, 0),
                "UInt64.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(UInt64.MinValue);
            Assert.AreEqual(
                ConvertEndian.GetUInt64Copy(minValue, 0),
                DataConverter.LittleEndian.GetUInt64(minValue, 0),
                "UInt64.MinValue failed."
                );
        }

        [Test]
        public void UInt64Swap()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(0102030405ul);
            Assert.AreEqual(
                ConvertEndian.GetUInt64Swap(testValue, 0),
                DataConverter.BigEndian.GetUInt64(testValue, 0),
                "0102030405ul failed."
                );

            var maxValue = BitConverter.GetBytes(UInt64.MaxValue);            
            Assert.AreEqual(
                ConvertEndian.GetUInt64Swap(maxValue, 0),
                DataConverter.BigEndian.GetUInt64(maxValue, 0),
                "UInt64.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(UInt64.MinValue);
            Assert.AreEqual(
                ConvertEndian.GetUInt64Swap(minValue, 0),
                DataConverter.BigEndian.GetUInt64(minValue, 0),
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
                ConvertEndian.GetFloatCopy(maxValue, 0),
                DataConverter.LittleEndian.GetFloat(maxValue, 0),
                "Single.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(Single.MinValue);
            Assert.AreEqual(
                ConvertEndian.GetFloatCopy(minValue, 0),
                DataConverter.LittleEndian.GetFloat(minValue, 0),
                "Single.MinValue failed."
                );
        }

        [Test]
        public void FloatSwap()
        {
            var maxValue = BitConverter.GetBytes(Single.MaxValue);

            Assert.AreEqual(
                ConvertEndian.GetFloatSwap(maxValue, 0),
                DataConverter.BigEndian.GetFloat(maxValue, 0),
                "Single.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(Single.MinValue);
            Assert.AreEqual(
                ConvertEndian.GetFloatSwap(minValue, 0),
                DataConverter.BigEndian.GetFloat(minValue, 0),
                "Single.MinValue failed."
                );
        }

        [Test]
        public void DoubleCopy()
        {
            var maxValue = BitConverter.GetBytes(Double.MaxValue);

            Assert.AreEqual(
                ConvertEndian.GetDoubleCopy(maxValue, 0),
                DataConverter.LittleEndian.GetDouble(maxValue, 0),
                "Double.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(Double.MinValue);
            Assert.AreEqual(
                ConvertEndian.GetDoubleCopy(minValue, 0),
                DataConverter.LittleEndian.GetDouble(minValue, 0),
                "Double.MinValue failed."
                );
        }

        [Test]
        public void DoubleSwap()
        {
            var maxValue = BitConverter.GetBytes(Double.MaxValue);

            Assert.AreEqual(
                ConvertEndian.GetDoubleSwap(maxValue, 0),
                DataConverter.BigEndian.GetDouble(maxValue, 0),
                "Double.MaxValue failed."
                );

            var minValue = BitConverter.GetBytes(Double.MinValue);
            Assert.AreEqual(
                ConvertEndian.GetDoubleSwap(minValue, 0),
                DataConverter.BigEndian.GetDouble(minValue, 0),
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
                ConvertEndian.GetInt16Copy(testValue, 0),
                DataConverter.LittleEndian.GetInt16(testValue, 0),
                "16084 failed."
                );
            // second a simple neg value test.
            var testValue2 = BitConverter.GetBytes(-8192);
            Assert.AreEqual(
                ConvertEndian.GetInt16Copy(testValue2, 0),
                DataConverter.LittleEndian.GetInt16(testValue2, 0),
                "-8192 failed."
                );

            // test Int16 max 
            var maxInt16 = BitConverter.GetBytes(Int16.MaxValue);
            Assert.AreEqual(
                ConvertEndian.GetInt16Copy(maxInt16, 0),
                DataConverter.LittleEndian.GetInt16(maxInt16, 0),
                "Int16.MaxValue Failed."
                );

            var minInt16 = BitConverter.GetBytes(Int16.MinValue);

            Assert.AreEqual(
                ConvertEndian.GetInt16Copy(minInt16, 0),
                DataConverter.LittleEndian.GetInt16(minInt16, 0),
                "Int16.MinValue Failed."
                );

        }

        [Test]
        public void Int16Swap()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16084);
            Assert.AreEqual(
                ConvertEndian.GetInt16Swap(testValue, 0),
                DataConverter.BigEndian.GetInt16(testValue, 0),
                "16084 failed."
                );
            // second a simple neg value test.
            var testValue2 = BitConverter.GetBytes(-8192);
            Assert.AreEqual(
                ConvertEndian.GetInt16Swap(testValue2, 0),
                DataConverter.BigEndian.GetInt16(testValue2, 0),
                "-8192 failed."
                );

            // test Int16 max 
            var maxInt16 = BitConverter.GetBytes(Int16.MaxValue);
            Assert.AreEqual(
                ConvertEndian.GetInt16Swap(maxInt16, 0),
                DataConverter.BigEndian.GetInt16(maxInt16, 0),
                "Int16.MaxValue failed"
                );

            var minInt16 = BitConverter.GetBytes(Int16.MinValue);

            Assert.AreEqual(
                ConvertEndian.GetInt16Swap(minInt16, 0),
                DataConverter.BigEndian.GetInt16(minInt16, 0),
                "Int16.MinValue Failed."
                );
        }

        [Test]
        public void Int32Copy()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16084 * 4);
            Assert.AreEqual(
                ConvertEndian.GetInt32Copy(testValue, 0),
                DataConverter.LittleEndian.GetInt32(testValue, 0),
                "16084 * 4 failed."
                );
            // second a simple neg value test.
            var testValue2 = BitConverter.GetBytes(-8192 * 4);
            Assert.AreEqual(
                ConvertEndian.GetInt32Copy(testValue2, 0),
                DataConverter.LittleEndian.GetInt32(testValue2, 0),
                "-8192 * 4 failed."
                );

            // test int32 max 
            var maxInt32 = BitConverter.GetBytes(Int32.MaxValue);
            Assert.AreEqual(
                ConvertEndian.GetInt32Copy(maxInt32, 0),
                DataConverter.LittleEndian.GetInt32(maxInt32, 0),
                "Int32.MaxValue Failed."                
                );

            var minInt32 = BitConverter.GetBytes(Int32.MinValue);

            Assert.AreEqual(
                ConvertEndian.GetInt32Copy(minInt32, 0),
                DataConverter.LittleEndian.GetInt32(minInt32, 0),
                "Int32.MinValue Failed."
                );

        }

        [Test]
        public void Int32Swap()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16084 * 4);
            Assert.AreEqual(
                ConvertEndian.GetInt32Swap(testValue, 0),
                DataConverter.BigEndian.GetInt32(testValue, 0),
                "16084 * 4 failed."
                );
            // second a simple neg value test.
            var testValue2 = BitConverter.GetBytes(-8192 * 4);
            Assert.AreEqual(
                ConvertEndian.GetInt32Swap(testValue2, 0),
                DataConverter.BigEndian.GetInt32(testValue2, 0),
                "-8192 * 4 failed."
                );

            // test int32 max 
            var maxInt32 = BitConverter.GetBytes(Int32.MaxValue);
            Assert.AreEqual(
                ConvertEndian.GetInt32Swap(maxInt32, 0),
                DataConverter.BigEndian.GetInt32(maxInt32, 0),
                "Int32.MaxValue failed"
                );

            var minInt32 = BitConverter.GetBytes(Int32.MinValue);

            Assert.AreEqual(
                ConvertEndian.GetInt32Swap(minInt32, 0),
                DataConverter.BigEndian.GetInt32(minInt32, 0),
                "Int32.MinValue Failed."
                );
        }

        [Test]
        public void Int64Copy()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16084L);
            Assert.AreEqual(
                ConvertEndian.GetInt64Copy(testValue, 0),
                DataConverter.LittleEndian.GetInt64(testValue, 0),
                "16084L failed."
                );
            // second a simple neg value test.
            var testValue2 = BitConverter.GetBytes(-8192L);
            Assert.AreEqual(
                ConvertEndian.GetInt64Copy(testValue2, 0),
                DataConverter.LittleEndian.GetInt64(testValue2, 0),
                "-8192L failed."
                );

            // test Int64 max 
            var maxInt64 = BitConverter.GetBytes(Int64.MaxValue);
            Assert.AreEqual(
                ConvertEndian.GetInt64Copy(maxInt64, 0),
                DataConverter.LittleEndian.GetInt64(maxInt64, 0),
                "Int64.MaxValue Failed."
                );

            var minInt64 = BitConverter.GetBytes(Int64.MinValue);

            Assert.AreEqual(
                ConvertEndian.GetInt64Copy(minInt64, 0),
                DataConverter.LittleEndian.GetInt64(minInt64, 0),
                "Int64.MinValue Failed."
                );

        }

        [Test]
        public void Int64Swap()
        {
            // first a simple value test.
            var testValue = BitConverter.GetBytes(16084L);
            Assert.AreEqual(
                ConvertEndian.GetInt64Swap(testValue, 0),
                DataConverter.BigEndian.GetInt64(testValue, 0),
                "16084L failed."
                );
            // second a simple neg value test.
            var testValue2 = BitConverter.GetBytes(-8192L);
            Assert.AreEqual(
                ConvertEndian.GetInt64Swap(testValue2, 0),
                DataConverter.BigEndian.GetInt64(testValue2, 0),
                "-8192L failed."
                );

            // test Int64 max 
            var maxInt64 = BitConverter.GetBytes(Int64.MaxValue);
            Assert.AreEqual(
                ConvertEndian.GetInt64Swap(maxInt64, 0),
                DataConverter.BigEndian.GetInt64(maxInt64, 0),
                "Int64.MaxValue failed"
                );

            var minInt64 = BitConverter.GetBytes(Int64.MinValue);

            Assert.AreEqual(
                ConvertEndian.GetInt64Swap(minInt64, 0),
                DataConverter.BigEndian.GetInt64(minInt64, 0),
                "Int64.MinValue Failed."
                );
        }

        #endregion
    }
}
