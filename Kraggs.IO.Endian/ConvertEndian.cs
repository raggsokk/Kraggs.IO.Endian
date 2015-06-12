#region License
//
// ConvertEndian.cs
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
    /// Internal Static impl class.
    /// 
    /// For now, must functions are using Mono.DataConvert as fallback
    /// in order to establish baseline and something to test again.
    /// 
    /// NOTE: No input validation is done.
    /// </summary>
    internal static class ConvertEndian
    {
        #region UInt16

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort GetUInt16Copy(byte[] buffer, int index)
        {
            unchecked
            {
                return (ushort)(
                    (buffer[index + 1] << 8) |
                    (buffer[index]));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort GetUInt16Swap(byte[] buffer, int index)
        {
            unchecked
            {
                return (ushort)(
                    (buffer[index] << 8) |
                    (buffer[index + 1])
                    );
            }

            //if (MonoConverter.IsLittleEndian)
            //    return MonoConverter.BigEndian.GetUInt16(buffer, index);
            //else
            //    return MonoConverter.LittleEndian.GetUInt16(buffer, index);
        }

        #endregion

        #region UInt32

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetUInt32Copy(byte[] buffer, int index)
        {
            unchecked
            {
                return (uint)(
                    (buffer[index + 3] << 24) |
                    (buffer[index + 2] << 16) |
                    (buffer[index + 1] << 8) |
                    (buffer[index]));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetUInt32Swap(byte[] buffer, int index)
        {
            unchecked
            {
                return (uint)(
                    (buffer[index] << 24) |
                    (buffer[index + 1] << 16) |
                    (buffer[index + 2] << 8) |
                    (buffer[index + 3])
                    );
            }
            //if (MonoConverter.IsLittleEndian)
            //    return MonoConverter.BigEndian.GetUInt32(buffer, index);
            //else
            //    return MonoConverter.LittleEndian.GetUInt32(buffer, index);
        }

        #endregion

        #region UInt64

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetUInt64Copy(byte[] buffer, int index)
        {
            {
                ulong low = GetUInt32Copy(buffer, index);
                ulong high = GetUInt32Copy(buffer, index + 4);

                return (ulong)((high << 32) | low);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetUInt64Swap(byte[] buffer, int index)
        {
            ulong high = GetUInt32Swap(buffer, index);
            ulong low = GetUInt32Swap(buffer, index + 4);
            
            return (ulong)((high << 32) | low);

            //if (MonoConverter.IsLittleEndian)
            //    return MonoConverter.BigEndian.GetUInt64(buffer, index);
            //else
            //    return MonoConverter.LittleEndian.GetUInt64(buffer, index);
        }

        #endregion

        #region Double

        [StructLayout(LayoutKind.Explicit)]
        private struct Byte8
        {
            [FieldOffset(0)]
            public ulong UInt64;

            [FieldOffset(0)]
            public double Double;
        }

        // function code contains unsafe section so unsure if this is valid.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDoubleCopy(byte[] buffer, int index)
        {
            //TODO: This might be faster
            //BitConverter.Int64BitsToDouble(GetInt64Copy(buffer, index));

            var s = new Byte8()
            {
                UInt64 = GetUInt64Copy(buffer, index)
            };
            return s.Double;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] 
        public static double GetDoubleSwap(byte[] buffer, int index)
        {
            //TODO: This might be faster
            //BitConverter.Int64BitsToDouble(GetInt64Swap(buffer, index));
            var s = new Byte8()
            {
                UInt64 = GetUInt64Swap(buffer, index)
            };
            return s.Double;

            //if (MonoConverter.IsLittleEndian)
            //    return MonoConverter.BigEndian.GetDouble(buffer, index);
            //else
            //    return MonoConverter.LittleEndian.GetDouble(buffer, index);
        }

        #endregion

        #region Float

        [StructLayout(LayoutKind.Explicit)]
        private struct Byte4
        {
            [FieldOffset(0)]
            public uint UInt32;

            //This doesnt work.
            //[FieldOffset(0)]
            //public int Int32;

            [FieldOffset(0)]
            public float Float;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] 
        public static float GetFloatCopy(byte[] buffer, int index)
        {
            var s = new Byte4()
            {
                UInt32 = GetUInt32Copy(buffer, index),
            };
            return s.Float;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] 
        public static float GetFloatSwap(byte[] buffer, int index)
        {
            var s = new Byte4()
            {
                UInt32 = GetUInt32Swap(buffer, index),
            };
            return s.Float;

            //if (MonoConverter.IsLittleEndian)
            //    return MonoConverter.BigEndian.GetFloat(buffer, index);
            //else
            //    return MonoConverter.LittleEndian.GetFloat(buffer, index);
        }

        #endregion

        #region Int16

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short GetInt16Copy(byte[] buffer, int index)
        {
            unchecked
            {
                return (short)(
                    (buffer[index + 1] << 8) |
                    (buffer[index]));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short GetInt16Swap(byte[] buffer, int index)
        {
            unchecked
            {
                return (short)(
                    (buffer[index] << 8) |
                    (buffer[index + 1]));
            }

            //if (MonoConverter.IsLittleEndian)
            //    return MonoConverter.BigEndian.GetInt16(buffer, index);
            //else
            //    return MonoConverter.LittleEndian.GetInt16(buffer, index);
        }

        #endregion

        #region Int32

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetInt32Copy(byte[] buffer, int index)
        {
            unchecked
            {
                return (int)(
                    (buffer[index + 3] << 24) |
                    (buffer[index + 2] << 16) |
                    (buffer[index + 1] << 8) |
                    (buffer[index]));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetInt32Swap(byte[] buffer, int index)
        {
            return (int)(
                (buffer[index] << 24) |
                (buffer[index + 1] << 16) |
                (buffer[index + 2] << 8) |
                (buffer[index + 3]));

            //if (MonoConverter.IsLittleEndian)
            //    return MonoConverter.BigEndian.GetInt32(buffer, index);
            //else
            //    return MonoConverter.LittleEndian.GetInt32(buffer, index);
        }

        #endregion

        #region Int64

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetInt64Copy(byte[] buffer, int index)
        {
            {
                ulong low = GetUInt32Copy(buffer, index);
                ulong high = GetUInt32Copy(buffer, index + 4);

                return (long)((high << 32) | low);
            }

            //if (MonoConverter.IsLittleEndian)
            //    return MonoConverter.LittleEndian.GetInt64(buffer, index);
            //else
            //    return MonoConverter.BigEndian.GetInt64(buffer, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetInt64Swap(byte[] buffer, int index)
        {
            {
                ulong high = GetUInt32Swap(buffer, index);
                ulong low = GetUInt32Swap(buffer, index + 4);                

                return (long)((high << 32) | low);
            }

            //if (MonoConverter.IsLittleEndian)
            //    return MonoConverter.BigEndian.GetInt64(buffer, index);
            //else
            //    return MonoConverter.LittleEndian.GetInt64(buffer, index);
        }

        #endregion
    }
}
