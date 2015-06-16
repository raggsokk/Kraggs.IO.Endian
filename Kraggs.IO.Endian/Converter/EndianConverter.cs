#region License
//
// EndianConverter.cs
// 
// The MIT License (MIT)
//
// Copyright (c) 2015 Jarle Hansen
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE. 
//
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

//using Mono;

namespace Kraggs.IO
{
    /// <summary>
    /// This is basically a reimplement of Mono DataConverter unfortunately.
    /// But some of the code is changed based on performance tests.
    /// And if possible, unsafe code is avoided in order to enable JIT optimizations.
    /// </summary>
    public abstract partial class EndianConverter
    {
        public static readonly bool IsLittleEndian = BitConverter.IsLittleEndian;

        internal static readonly EndianConverter CopyConverter = new CopyConverter();
        internal static readonly EndianConverter SwapConverter = new SwapConverter();

        //public EndianConverter()
        //{
        //    BitConverter.
        //}

        #region Constuctors

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EndianConverter CreateLittleEndianConverter()
        {
            return IsLittleEndian ? CopyConverter : SwapConverter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EndianConverter CreateBitEndianConverter()
        {
            return IsLittleEndian ? SwapConverter : CopyConverter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EndianConverter CreateNativeConverter()
        {
            return CopyConverter;
        }

        //public static EndianConverter LittleEndian
        //{
        //    get
        //    {
        //        return IsLittleEndian
        //    }
        //}

        #endregion

        #region Reading from byte Arrays

        // floatingpoint
        public abstract float ReadFloat(byte[] data, int index);
        public abstract double ReadDouble(byte[] data, int index);
        public abstract decimal ReadDecimal(byte[] data, int index);

        // unsigned integer
        //[CLSCompliant(false)]
        public abstract UInt16 ReadUInt16(byte[] data, int index);
        //[CLSCompliant(false)]
        public abstract UInt32 ReadUInt32(byte[] data, int index);
        //[CLSCompliant(false)]
        public abstract UInt64 ReadUInt64(byte[] data, int index);

        // signed integer
        public abstract Int16 ReadInt16(byte[] data, int index);
        public abstract Int32 ReadInt32(byte[] data, int index);
        public abstract Int64 ReadInt64(byte[] data, int index);

        #endregion

        #region Writing to byte Arrays

        public abstract void Write(byte[] dest, int index, float value);
        public abstract void Write(byte[] dest, int index, double value);
        public abstract void Write(byte[] dest, int index, decimal scalar);

        // unsigned integer
        //[CLSCompliant(false)]
        public abstract void Write(byte[] dest, int index, UInt16 value);
        //[CLSCompliant(false)]
        public abstract void Write(byte[] dest, int index, UInt32 value);
        //[CLSCompliant(false)]
        public abstract void Write(byte[] dest, int index, UInt64 value);

        // signed integer
        public abstract void Write(byte[] dest, int index, Int16 value);
        public abstract void Write(byte[] dest, int index, Int32 value);
        public abstract void Write(byte[] dest, int index, Int64 value);

        #endregion

        #region ReadWrite Bytes

        //public abstract void ReadBytes(float[] src, int srcIndex, byte[] dst, int dstIndex);
        //public abstract void WriteBytes(float[] dst, int dstIndex, byte[] src, int srcIndex);

        #endregion
    }
}
