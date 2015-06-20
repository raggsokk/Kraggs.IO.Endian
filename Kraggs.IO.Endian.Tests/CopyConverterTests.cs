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
    public class CopyConverterTests
    {
        private DataConverter Native;
        private DataConverter Swap;

        //private readonly EndianConverter TestConverter = EndianConverter.IsLittleEndian ?
        //    EndianConverter. : EndianConverter.LittleEndian;
        //private readonly EndianConverter TestConverter = EndianConverter.SwapConverter;
        private readonly EndianConverter TestConverter = EndianConverter.CopyConverter;

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

        #region Floatingpoint Tests

        [Test]
        public void ConvertCopyReadFloat()
        {
            var testBytes = BitConverter.GetBytes(243.385f);
            Assert.AreEqual(
                TestConverter.ReadFloat(testBytes, 0),
                Native.GetFloat(testBytes, 0),
                "Float '243.285f' failed."
                );

            testBytes = BitConverter.GetBytes(float.MaxValue);
            Assert.AreEqual(
                TestConverter.ReadFloat(testBytes, 0),
                Native.GetFloat(testBytes, 0),
                "Float 'float.MaxValue' failed."
                );

            testBytes = BitConverter.GetBytes(float.MinValue);
            Assert.AreEqual(
                TestConverter.ReadFloat(testBytes, 0),
                Native.GetFloat(testBytes, 0),
                "Float 'float.MinValue' failed."
                );
        }

        [Test]
        public void ConvertCopyWriteFloat()
        {
            float val = 243.385f;
            var test1Buffer = new byte[sizeof(float)];
            var test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteFloat '{0}' failed.", val);

            val = float.MaxValue;
            test1Buffer = new byte[sizeof(float)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteFloat '{0}' failed.", val);

            val = float.MinValue;
            test1Buffer = new byte[sizeof(float)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteFloat '{0}' failed.", val);
        }

        [Test]
        public void ConvertCopyReadDouble()
        {
            var testBytes = BitConverter.GetBytes(243.385d);
            Assert.AreEqual(
                TestConverter.ReadDouble(testBytes, 0),
                Native.GetDouble(testBytes, 0),
                "Double '243.285f' failed."
                );

            testBytes = BitConverter.GetBytes(Double.MaxValue);
            Assert.AreEqual(
                TestConverter.ReadDouble(testBytes, 0),
                Native.GetDouble(testBytes, 0),
                "Double 'Double.MaxValue' failed."
                );

            testBytes = BitConverter.GetBytes(Double.MinValue);
            Assert.AreEqual(
                TestConverter.ReadDouble(testBytes, 0),
                Native.GetDouble(testBytes, 0),
                "Double 'Double.MinValue' failed."
                );
        }

        [Test]
        public void ConvertCopyWriteDouble()
        {
            double val = 243.385d;
            var test1Buffer = new byte[sizeof(double)];
            var test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteDouble '{0}' failed.", val);

            val = double.MaxValue;
            test1Buffer = new byte[sizeof(double)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteDouble '{0}' failed.", val);

            val = double.MinValue;
            test1Buffer = new byte[sizeof(double)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteDouble '{0}' failed.", val);
        }


        [Test]
        public void ConvertCopyReadDecimal()
        {
            Assert.Ignore("Decimal not implemented yet.");

            //var testBytes = BitConverter.GetBytes((decimal)243.385d);
            //Assert.AreEqual(
            //    TestConverter.ReadDecimal(testBytes, 0),
            //    Native.GetDecimal(testBytes, 0),
            //    "Decimal '243.285f' failed."
            //    );

            //testBytes = BitConverter.GetBytes(Decimal.MaxValue);
            //Assert.AreEqual(
            //    TestConverter.ReadDecimal(testBytes, 0),
            //    Native.GetDecimal(testBytes, 0),
            //    "Decimal 'Decimal.MaxValue' failed."
            //    );

            //testBytes = BitConverter.GetBytes(Decimal.MinValue);
            //Assert.AreEqual(
            //    TestConverter.ReadDecimal(testBytes, 0),
            //    Native.GetDecimal(testBytes, 0),
            //    "Decimal 'Decimal.MinValue' failed."
            //    );
        }

        [Test]
        public void ConvertCopyWriteDecimal()
        {
            Assert.Ignore("Decimal not implemented yet.");

            //decimal val = (decimal)243.385d;
            //var test1Buffer = new byte[sizeof(decimal)];
            //var test2Buffer = new byte[test1Buffer.Length];
            //TestConverter.Write(test1Buffer, 0, val);
            //Native.PutBytes(test2Buffer, 0, val);
            //Assert.True(AreEqual(test1Buffer, test2Buffer),
            //    "WriteDecimal '{0}' failed.", val);

            //val = decimal.MaxValue;
            //test1Buffer = new byte[sizeof(decimal)];
            //test2Buffer = new byte[test1Buffer.Length];
            //TestConverter.Write(test1Buffer, 0, val);
            //Native.PutBytes(test2Buffer, 0, val);
            //Assert.True(AreEqual(test1Buffer, test2Buffer),
            //    "WriteDecimal '{0}' failed.", val);

            //val = decimal.MinValue;
            //test1Buffer = new byte[sizeof(decimal)];
            //test2Buffer = new byte[test1Buffer.Length];
            //TestConverter.Write(test1Buffer, 0, val);
            //Native.PutBytes(test2Buffer, 0, val);
            //Assert.True(AreEqual(test1Buffer, test2Buffer),
            //    "WriteDecimal '{0}' failed.", val);
        }

        #endregion

        #region unsigned integers

        [Test]
        public void ConvertCopyReadUInt16()
        {
            //Assert.Ignore("Decimal not implemented yet.");

            var testBytes = BitConverter.GetBytes((UInt16)29347);
            Assert.AreEqual(
                TestConverter.ReadUInt16(testBytes, 0),
                Native.GetUInt16(testBytes, 0),
                "UInt16 '29347' failed."
                );

            testBytes = BitConverter.GetBytes(UInt16.MaxValue);
            Assert.AreEqual(
                TestConverter.ReadUInt16(testBytes, 0),
                Native.GetUInt16(testBytes, 0),
                "UInt16 'UInt16.MaxValue' failed."
                );

            testBytes = BitConverter.GetBytes(UInt16.MinValue);
            Assert.AreEqual(
                TestConverter.ReadUInt16(testBytes, 0),
                Native.GetUInt16(testBytes, 0),
                "UInt16 'UInt16.MinValue' failed."
                );
        }

        [Test]
        public void ConvertCopyWriteUInt16()
        {
            UInt16 val = (UInt16)29347;
            var test1Buffer = new byte[sizeof(UInt16)];
            var test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteUInt16 '{0}' failed.", val);

            val = UInt16.MaxValue;
            test1Buffer = new byte[sizeof(UInt16)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteUInt16 '{0}' failed.", val);

            val = UInt16.MinValue;
            test1Buffer = new byte[sizeof(UInt16)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteUInt16 '{0}' failed.", val);
        }

        [Test]
        public void ConvertCopyReadUInt32()
        {
            //Assert.Ignore("Decimal not implemented yet.");

            var testBytes = BitConverter.GetBytes((UInt32)29343437);
            Assert.AreEqual(
                TestConverter.ReadUInt32(testBytes, 0),
                Native.GetUInt32(testBytes, 0),
                "UInt32 '29347' failed."
                );

            testBytes = BitConverter.GetBytes(UInt32.MaxValue);
            Assert.AreEqual(
                TestConverter.ReadUInt32(testBytes, 0),
                Native.GetUInt32(testBytes, 0),
                "UInt32 'UInt32.MaxValue' failed."
                );

            testBytes = BitConverter.GetBytes(UInt32.MinValue);
            Assert.AreEqual(
                TestConverter.ReadUInt32(testBytes, 0),
                Native.GetUInt32(testBytes, 0),
                "UInt32 'UInt32.MinValue' failed."
                );
        }

        [Test]
        public void ConvertCopyWriteUInt32()
        {
            UInt32 val = (UInt32)29343437;
            var test1Buffer = new byte[sizeof(UInt32)];
            var test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteUInt32 '{0}' failed.", val);

            val = UInt32.MaxValue;
            test1Buffer = new byte[sizeof(UInt32)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteUInt32 '{0}' failed.", val);

            val = UInt32.MinValue;
            test1Buffer = new byte[sizeof(UInt32)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteUInt32 '{0}' failed.", val);
        }

        [Test]
        public void ConvertCopyReadUInt64()
        {
            //Assert.Ignore("Decimal not implemented yet.");

            var testBytes = BitConverter.GetBytes((UInt64)29343437345);
            Assert.AreEqual(
                TestConverter.ReadUInt64(testBytes, 0),
                Native.GetUInt64(testBytes, 0),
                "UInt64 '29347' failed."
                );

            testBytes = BitConverter.GetBytes(UInt64.MaxValue);
            Assert.AreEqual(
                TestConverter.ReadUInt64(testBytes, 0),
                Native.GetUInt64(testBytes, 0),
                "UInt64 'UInt64.MaxValue' failed."
                );

            testBytes = BitConverter.GetBytes(UInt64.MinValue);
            Assert.AreEqual(
                TestConverter.ReadUInt64(testBytes, 0),
                Native.GetUInt64(testBytes, 0),
                "UInt64 'UInt64.MinValue' failed."
                );
        }

        [Test]
        public void ConvertCopyWriteUInt64()
        {
            UInt64 val = (UInt64)29343437345;
            var test1Buffer = new byte[sizeof(UInt64)];
            var test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteUInt64 '{0}' failed.", val);

            val = UInt64.MaxValue;
            test1Buffer = new byte[sizeof(UInt64)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteUInt64 '{0}' failed.", val);

            val = UInt64.MinValue;
            test1Buffer = new byte[sizeof(UInt64)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteUInt64 '{0}' failed.", val);
        }

        #endregion

        #region signed integers

        [Test]
        public void ConvertCopyReadInt16()
        {
            //Assert.Ignore("Decimal not implemented yet.");

            var testBytes = BitConverter.GetBytes((Int16)29347);
            Assert.AreEqual(
                TestConverter.ReadInt16(testBytes, 0),
                Native.GetInt16(testBytes, 0),
                "Int16 '29347' failed."
                );

            testBytes = BitConverter.GetBytes(Int16.MaxValue);
            Assert.AreEqual(
                TestConverter.ReadInt16(testBytes, 0),
                Native.GetInt16(testBytes, 0),
                "Int16 'Int16.MaxValue' failed."
                );

            testBytes = BitConverter.GetBytes(Int16.MinValue);
            Assert.AreEqual(
                TestConverter.ReadInt16(testBytes, 0),
                Native.GetInt16(testBytes, 0),
                "Int16 'Int16.MinValue' failed."
                );
        }

        [Test]
        public void ConvertCopyWriteInt16()
        {
            Int16 val = (Int16)29347;
            var test1Buffer = new byte[sizeof(Int16)];
            var test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteInt16 '{0}' failed.", val);

            val = Int16.MaxValue;
            test1Buffer = new byte[sizeof(Int16)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteInt16 '{0}' failed.", val);

            val = Int16.MinValue;
            test1Buffer = new byte[sizeof(Int16)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteInt16 '{0}' failed.", val);
        }


        [Test]
        public void ConvertCopyReadInt32()
        {
            //Assert.Ignore("Decimal not implemented yet.");

            var testBytes = BitConverter.GetBytes((Int32)29343437);
            Assert.AreEqual(
                TestConverter.ReadInt32(testBytes, 0),
                Native.GetInt32(testBytes, 0),
                "Int32 '29347' failed."
                );

            testBytes = BitConverter.GetBytes(Int32.MaxValue);
            Assert.AreEqual(
                TestConverter.ReadInt32(testBytes, 0),
                Native.GetInt32(testBytes, 0),
                "Int32 'Int32.MaxValue' failed."
                );

            testBytes = BitConverter.GetBytes(Int32.MinValue);
            Assert.AreEqual(
                TestConverter.ReadInt32(testBytes, 0),
                Native.GetInt32(testBytes, 0),
                "Int32 'Int32.MinValue' failed."
                );
        }

        [Test]
        public void ConvertCopyWriteInt32()
        {
            Int32 val = (Int32)29343437;
            var test1Buffer = new byte[sizeof(Int32)];
            var test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteInt32 '{0}' failed.", val);

            val = Int32.MaxValue;
            test1Buffer = new byte[sizeof(Int32)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteInt32 '{0}' failed.", val);

            val = Int32.MinValue;
            test1Buffer = new byte[sizeof(Int32)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteInt32 '{0}' failed.", val);
        }

        [Test]
        public void ConvertCopyReadInt64()
        {
            //Assert.Ignore("Decimal not implemented yet.");

            var testBytes = BitConverter.GetBytes((Int64)29343437345);
            Assert.AreEqual(
                TestConverter.ReadInt64(testBytes, 0),
                Native.GetInt64(testBytes, 0),
                "Int64 '29347' failed."
                );

            testBytes = BitConverter.GetBytes(Int64.MaxValue);
            Assert.AreEqual(
                TestConverter.ReadInt64(testBytes, 0),
                Native.GetInt64(testBytes, 0),
                "Int64 'Int64.MaxValue' failed."
                );

            testBytes = BitConverter.GetBytes(Int64.MinValue);
            Assert.AreEqual(
                TestConverter.ReadInt64(testBytes, 0),
                Native.GetInt64(testBytes, 0),
                "Int64 'Int64.MinValue' failed."
                );
        }

        [Test]
        public void ConvertCopyWriteInt64()
        {
            Int64 val = (Int64)29343437345;
            var test1Buffer = new byte[sizeof(Int64)];
            var test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteInt64 '{0}' failed.", val);

            val = Int64.MaxValue;
            test1Buffer = new byte[sizeof(Int64)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteInt64 '{0}' failed.", val);

            val = Int64.MinValue;
            test1Buffer = new byte[sizeof(Int64)];
            test2Buffer = new byte[test1Buffer.Length];
            TestConverter.Write(test1Buffer, 0, val);
            Native.PutBytes(test2Buffer, 0, val);
            Assert.True(AreEqual(test1Buffer, test2Buffer),
                "WriteInt64 '{0}' failed.", val);
        }

        #endregion
    }
}
