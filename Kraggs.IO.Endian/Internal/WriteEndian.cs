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

//using MonoTMP;

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
        //private static readonly DataConverter Native = DataConverter.Native;
        //private static readonly DataConverter Swap = DataConverter.IsLittleEndian ?
        //    DataConverter.BigEndian : DataConverter.LittleEndian;

        #region UInt16

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // this proably dont work thou.
        public unsafe static void PutBytesCopyU(byte[] buffer, int index, UInt16 value)
        {
            //Check (dest, destIdx, 2);
            fixed(byte* target = &buffer[index])
            {
                ushort* source = (ushort*)&value;

                *((ushort*)target) = *source;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, UInt16 value)
        {
            //Native.PutBytes(buffer, index, value);
            buffer[index] = (byte)(value & 0xFF);
            buffer[index + 1] = (byte)((value >> 8) & 0xFF);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, UInt16 value)
        {
            //Swap.PutBytes(buffer, index, value);
            buffer[index + 1] = (byte)(value & 0xFF);
            buffer[index] = (byte)((value >> 8) & 0xFF);
        }

        #endregion

        #region UInt32

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // this proably dont work thou.
        public unsafe static void PutBytesCopyU(byte[] buffer, int index, UInt32 value)
        {
            //Check (dest, destIdx, 2);
            fixed (byte* target = &buffer[index])
            {
                uint* source = (uint*)&value;

                *((uint*)target) = *source;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, UInt32 value)
        {
            // this is faster since it doesn't validate input which unsafe code has to do.
            // but its only 5% or so, and dependent on inlining.
            buffer[index] = (byte)(value & 0xFF);
            buffer[index + 1] = (byte)((value >> 8) & 0xFF);
            buffer[index + 2] = (byte)((value >> 16) & 0xFF);
            buffer[index + 3] = (byte)((value >> 24));

            //Native.PutBytes(buffer, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, UInt32 value)
        {
            // this is a lot faster I think
            buffer[index + 3] = (byte)(value & 0xFF);
            buffer[index + 2] = (byte)((value >> 8) & 0xFF);
            buffer[index + 1] = (byte)((value >> 16) & 0xFF);
            buffer[index] = (byte)((value >> 24));

            //Swap.PutBytes(buffer, index, value);
        }

        #endregion

        #region UInt64

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // this proably dont work thou.
        public unsafe static void PutBytesCopyU(byte[] buffer, int index, UInt64 value)
        {
            //if (buffer == null)
            //    throw new ArgumentNullException("buffer");
            //if (index < 0 || index > buffer.Length - sizeof(UInt64))
            //    throw new ArgumentException("Index out of bounds", "index");

            fixed (byte* ptr = &buffer[index])
            {
                UInt64* source = (UInt64*)&value;
                *((UInt64*)ptr) = *source;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, UInt64 value)
        {
            UInt32 low = (UInt32)(value & 0XFFFFFFFF);
            UInt32 high = (UInt32)((value >> 32));
            //UInt32 high = (UInt32)((value >> 32) & 0XFFFF);
            PutBytesCopy(buffer, index, low);
            PutBytesCopy(buffer, index + 4, high);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, UInt64 value)
        {
            //Swap.PutBytes(buffer, index, value);
            UInt32 low = (UInt32)(value & 0XFFFFFFFF);
            UInt32 high = (UInt32)((value >> 32));
            //UInt32 high = (UInt32)((value >> 32) & 0XFFFF);
            PutBytesSwap(buffer, index + 4, low);
            PutBytesSwap(buffer, index, high);
        }

        #endregion

        #region Float

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // unsafe is not inlined thou.
        public unsafe static void PutBytesCopyU(byte[] buffer, int index, float value)
        {
            //Check (dest, destIdx, 2);

            fixed (byte* target = &buffer[index])
            {
                uint* source = (uint*)&value;

                *((uint*)target) = *source;
            }   
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, float value)
        {
            var s = new ReadEndian.Byte4()
            {
                Float = value
            };
            PutBytesCopy(buffer, index, s.UInt32);
            //Native.PutBytes(buffer, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // unsafe is not inlined thou.
        public unsafe static void PutBytesSwapU(byte[] buffer, int index, float value)
        {
            uint source = *(uint*)&value;
            buffer[index] = (byte)((source >> 24));
            buffer[index + 1] = (byte)((source >> 16) & 0xFF);
            buffer[index + 2] = (byte)((source >> 8) & 0xFF);
            buffer[index + 3] = (byte)(source & 0xFF);

            //PutBytesSwap(buffer, index, *source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, float value)
        {
            var s = new ReadEndian.Byte4()
            {
                Float = value
            };
            PutBytesSwap(buffer, index, s.UInt32);
            //Swap.PutBytes(buffer, index, value);
        }

        #endregion

        #region Double

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // unsafe is not inlined thou.
        public unsafe static void PutBytesCopyU(byte[] buffer, int index, double value)
        {
            //Check (dest, destIdx, 2);

            fixed (byte* target = &buffer[index])
            {
                ulong* source = (ulong*)&value;

                *((ulong*)target) = *source;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, double value)
        {
            //Native.PutBytes(buffer, index, value);
            var d = BitConverter.DoubleToInt64Bits(value);
            PutBytesCopy(buffer, index, d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // unsafe is not inlined thou.
        public unsafe static void PutBytesSwapU(byte[] buffer, int index, double value)
        {
            ulong source = *(ulong*)&value;

            uint low = (uint)(source & 0XFFFFFFFF);
            uint high = (uint)((source >> 32));

            buffer[index] = (byte)((high >> 24) & 0xFF);
            buffer[index + 1] = (byte)((high >> 16) & 0xFF);
            buffer[index + 2] = (byte)((high >> 8) & 0xFF);
            buffer[index + 3] = (byte)(high & 0xFF);
            buffer[index + 4] = (byte)((low >> 24) & 0xFF);
            buffer[index + 5] = (byte)((low >> 16) & 0xFF);
            buffer[index + 6] = (byte)((low >> 8) & 0xFF);
            buffer[index + 7] = (byte)(low & 0xFF);
            
            //var d = BitConverter.DoubleToInt64Bits(value);
            //PutBytesSwap(buffer, index, d);
            //Swap.PutBytes(buffer, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, double value)
        {
            var d = BitConverter.DoubleToInt64Bits(value);
            PutBytesSwap(buffer, index, d);
            //Swap.PutBytes(buffer, index, value);
        }

        #endregion

        #region Int16

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // unsafe is not inlined thou.
        public unsafe static void PutBytesCopyU(byte[] buffer, int index, Int16 value)
        {
            //Check (dest, destIdx, 2);
            fixed (byte* target = &buffer[index])
            {
                ushort* source = (ushort*)&value;

                *((ushort*)target) = *source;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, Int16 value)
        {
            //Native.PutBytes(buffer, index, value);
            buffer[index] = (byte)(value & 0xFF);
            buffer[index + 1] = (byte)((value >> 8) & 0xFF);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, Int16 value)
        {
            //Swap.PutBytes(buffer, index, value);
            buffer[index + 1] = (byte)(value & 0xFF);
            buffer[index] = (byte)((value >> 8) & 0xFF);
        }

        #endregion

        #region Int32

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // unsafe is not inlined thou.
        public unsafe static void PutBytesCopyU(byte[] buffer, int index, Int32 value)
        {
            //Check (dest, destIdx, 2);
            fixed (byte* target = &buffer[index])
            {
                uint* source = (uint*)&value;

                *((uint*)target) = *source;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, Int32 value)
        {
            //Native.PutBytes(buffer, index, value);
            // this is faster since it doesn't validate input which unsafe code has to do.
            // but its only 5% or so, and dependent on inlining.
            buffer[index] = (byte)(value & 0xFF);
            buffer[index + 1] = (byte)((value >> 8) & 0xFF);
            buffer[index + 2] = (byte)((value >> 16) & 0xFF);
            buffer[index + 3] = (byte)((value >> 24));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, Int32 value)
        {
            //Swap.PutBytes(buffer, index, value);
            // this is faster since it doesn't validate input which unsafe code has to do.
            // but its only 5% or so, and dependent on inlining.
            buffer[index + 3] = (byte)(value & 0xFF);
            buffer[index + 2] = (byte)((value >> 8) & 0xFF);
            buffer[index + 1] = (byte)((value >> 16) & 0xFF);
            buffer[index] = (byte)((value >> 24));
        }

        #endregion

        #region Int64

        public unsafe static void PutBytesCopyU(byte[] buffer, int index, Int64 value)
        {
            //if (buffer == null)
            //    throw new ArgumentNullException("buffer");
            //if (index < 0 || index > buffer.Length - sizeof(Int64))
            //    throw new ArgumentException("Index out of bounds", "index");
            fixed (byte* ptr = &buffer[index])
            {
                ulong* source = (ulong*)&value;
                *((ulong*)ptr) = *source;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesCopy(byte[] buffer, int index, Int64 value)
        {
            UInt32 low = (UInt32)(value & 0XFFFFFFFF);
            UInt32 high = (UInt32)((value >> 32));
            //UInt32 high = (UInt32)((value >> 32) & 0XFFFF);
            PutBytesCopy(buffer, index, low);
            PutBytesCopy(buffer, index + 4, high);            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PutBytesSwap(byte[] buffer, int index, Int64 value)
        {
            //Swap.PutBytes(buffer, index, value);
            //Swap.PutBytes(buffer, index, value);
            UInt32 low = (UInt32)(value & 0XFFFFFFFF);
            UInt32 high = (UInt32)((value >> 32));
            //UInt32 high = (UInt32)((value >> 32) & 0XFFFF);
            PutBytesSwap(buffer, index + 4, low);
            PutBytesSwap(buffer, index, high);
        }

        #endregion
    }
}
