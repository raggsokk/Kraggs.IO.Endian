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
    /// This is basically a reimplement of Mono DataConverter interface unfortunately.
    /// But some of the code are going to be changed based on performance tests.
    /// And if possible, unsafe code is avoided in order to enable JIT optimizations
    /// and aggressive inlining. 
    /// (Agressive inlining dont work on unsafe and virtual methods).
    /// </summary>
    public abstract partial class EndianConverter
    {
        /// <summary>
        /// Is this machine using little endian or big endian layout?
        /// </summary>
        public static readonly bool IsLittleEndian = BitConverter.IsLittleEndian;

        internal static readonly EndianConverter CopyConverter = new CopyConverter();
        internal static readonly EndianConverter SwapConverter = new SwapConverter();

        //public EndianConverter()
        //{
        //    //TODO: default constructor public?
        //}

        #region Constuctors

        /// <summary>
        /// Returns an Endian Converter which read/write little endian data.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EndianConverter CreateLittleEndianConverter()
        {
            return IsLittleEndian ? CopyConverter : SwapConverter;
        }

        /// <summary>
        /// Returns an Endian Converter which read/write big endian data.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EndianConverter CreateBitEndianConverter()
        {
            return IsLittleEndian ? SwapConverter : CopyConverter;
        }

        /// <summary>
        /// Returns an Endian Converter which read/write native endian data.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EndianConverter CreateNativeConverter()
        {
            return CopyConverter;
        }

        #endregion

        #region Reading from byte Arrays

        // floatingpoint
        /// <summary>
        /// Reads a float from buffer at index.
        /// </summary>
        /// <param name="buffer">byte buffer</param>
        /// <param name="index">index to read at.</param>
        /// <returns></returns>
        public abstract float ReadFloat(byte[] buffer, int index);
        /// <summary>
        /// Reads a double from buffer at index pos.
        /// </summary>
        /// <param name="buffer">byte buffer to read from.</param>
        /// <param name="index">index to read at.</param>
        /// <returns></returns>
        public abstract double ReadDouble(byte[] buffer, int index);
        /// <summary>
        /// Reads a decimal from buffer at index position.
        /// </summary>
        /// <param name="buffer">byte buffer to read from.</param>
        /// <param name="index">index position to read at.</param>
        /// <returns></returns>
        public abstract decimal ReadDecimal(byte[] buffer, int index);

        // unsigned integer        
        /// <summary>
        /// Reads an UInt16 from buffer at index position.
        /// </summary>
        /// <param name="buffer">byte buffer to read from.</param>
        /// <param name="index">index position to read at.</param>
        /// <returns></returns>
        public abstract UInt16 ReadUInt16(byte[] buffer, int index);        
        /// <summary>
        /// Reads an UInt32 from buffer at index position.
        /// </summary>
        /// <param name="buffer">byte buffer to read from.</param>
        /// <param name="index">index position to read at.</param>
        /// <returns></returns>
        public abstract UInt32 ReadUInt32(byte[] buffer, int index);        
        /// <summary>
        /// Reads an UInt64 from buffer at index position.
        /// </summary>
        /// <param name="buffer">byte buffer to read from.</param>
        /// <param name="index">index position to read at.</param>
        /// <returns></returns>
        public abstract UInt64 ReadUInt64(byte[] buffer, int index);

        // signed integer
        /// <summary>
        /// Reads an Int16 from buffer at index position.
        /// </summary>
        /// <param name="buffer">byte buffer to read from.</param>
        /// <param name="index">index position to read at.</param>
        /// <returns></returns>
        public abstract Int16 ReadInt16(byte[] buffer, int index);
        /// <summary>
        /// Reads an Int32 from buffer at index position.
        /// </summary>
        /// <param name="buffer">byte buffer to read from.</param>
        /// <param name="index">index position to read at.</param>
        /// <returns></returns>
        public abstract Int32 ReadInt32(byte[] buffer, int index);
        /// <summary>
        /// Reads an Int64 from buffer at index position.
        /// </summary>
        /// <param name="buffer">byte buffer to read from.</param>
        /// <param name="index">index position to read at.</param>
        /// <returns></returns>
        public abstract Int64 ReadInt64(byte[] buffer, int index);

        #endregion

        #region Writing to byte Arrays

        /// <summary>
        /// Write a float to byte buffer at speficied index.
        /// </summary>
        /// <param name="dest">destination to write value to.</param>
        /// <param name="index">index in buffer to write at.</param>
        /// <param name="value">value to write.</param>
        public abstract void Write(byte[] dest, int index, float value);
        /// <summary>
        /// Write a double to byte buffer at speficied index.
        /// </summary>
        /// <param name="dest">destination to write value to.</param>
        /// <param name="index">index in buffer to write at.</param>
        /// <param name="value">value to write.</param>
        public abstract void Write(byte[] dest, int index, double value);
        /// <summary>
        /// Write a decimal to byte buffer at speficied index.
        /// </summary>
        /// <param name="dest">destination to write value to.</param>
        /// <param name="index">index in buffer to write at.</param>
        /// <param name="value">value to write.</param>
        public abstract void Write(byte[] dest, int index, decimal scalar);

        // unsigned integer
        /// <summary>
        /// Write an UInt16 to byte buffer at speficied index.
        /// </summary>
        /// <param name="dest">destination to write value to.</param>
        /// <param name="index">index in buffer to write at.</param>
        /// <param name="value">value to write.</param>
        public abstract void Write(byte[] dest, int index, UInt16 value);
        /// <summary>
        /// Write an UInt32 to byte buffer at speficied index.
        /// </summary>
        /// <param name="dest">destination to write value to.</param>
        /// <param name="index">index in buffer to write at.</param>
        /// <param name="value">value to write.</param>
        public abstract void Write(byte[] dest, int index, UInt32 value);
        /// <summary>
        /// Write an UInt64 to byte buffer at speficied index.
        /// </summary>
        /// <param name="dest">destination to write value to.</param>
        /// <param name="index">index in buffer to write at.</param>
        /// <param name="value">value to write.</param>
        public abstract void Write(byte[] dest, int index, UInt64 value);

        // signed integer
        /// <summary>
        /// Write an Int16 to byte buffer at speficied index.
        /// </summary>
        /// <param name="dest">destination to write value to.</param>
        /// <param name="index">index in buffer to write at.</param>
        /// <param name="value">value to write.</param>
        public abstract void Write(byte[] dest, int index, Int16 value);
        /// <summary>
        /// Write an Int32 to byte buffer at speficied index.
        /// </summary>
        /// <param name="dest">destination to write value to.</param>
        /// <param name="index">index in buffer to write at.</param>
        /// <param name="value">value to write.</param>
        public abstract void Write(byte[] dest, int index, Int32 value);
        /// <summary>
        /// Write an Int64 to byte buffer at speficied index.
        /// </summary>
        /// <param name="dest">destination to write value to.</param>
        /// <param name="index">index in buffer to write at.</param>
        /// <param name="value">value to write.</param>
        public abstract void Write(byte[] dest, int index, Int64 value);

        #endregion

        #region ReadWrite Bytes

        //public abstract void ReadBytes(float[] src, int srcIndex, byte[] dst, int dstIndex);
        //public abstract void WriteBytes(float[] dst, int dstIndex, byte[] src, int srcIndex);

        #endregion
    }
}
