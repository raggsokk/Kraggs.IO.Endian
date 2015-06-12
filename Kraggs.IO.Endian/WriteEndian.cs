#region License
//
// WriteEndian.cs
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

using MonoTMP;

namespace Kraggs.IO
{
    /// <summary>
    /// Internal Static impl class.
    /// 
    /// Seperate from ReadEndina(ConvertEndian) for write endian code.
    /// 
    /// NOTE: No input validation is done.
    /// </summary>
    internal static class WriteEndian
    {
        private static readonly DataConverter Native = DataConverter.Native;
        private static readonly DataConverter Swap = DataConverter.IsLittleEndian ?
            DataConverter.BigEndian : DataConverter.LittleEndian;

        #region UInt16

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, UInt16 value)
        {
            Native.PutBytes(buffer, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, UInt16 value)
        {
            Swap.PutBytes(buffer, index, value);
        }

        #endregion

        #region UInt32

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, UInt32 value)
        {
            Native.PutBytes(buffer, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, UInt32 value)
        {
            Swap.PutBytes(buffer, index, value);
        }

        #endregion

        #region UInt64

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, UInt64 value)
        {
            Native.PutBytes(buffer, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, UInt64 value)
        {
            Swap.PutBytes(buffer, index, value);
        }

        #endregion

        #region Float

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, float value)
        {
            Native.PutBytes(buffer, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, float value)
        {
            Swap.PutBytes(buffer, index, value);
        }

        #endregion

        #region Double

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, double value)
        {
            Native.PutBytes(buffer, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, double value)
        {
            Swap.PutBytes(buffer, index, value);
        }

        #endregion

        #region Int16

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, Int16 value)
        {
            Native.PutBytes(buffer, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, Int16 value)
        {
            Swap.PutBytes(buffer, index, value);
        }

        #endregion

        #region Int32

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, Int32 value)
        {
            Native.PutBytes(buffer, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, Int32 value)
        {
            Swap.PutBytes(buffer, index, value);
        }

        #endregion

        #region Int64

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, Int64 value)
        {
            Native.PutBytes(buffer, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, Int64 value)
        {
            Swap.PutBytes(buffer, index, value);
        }

        #endregion
    }
}
