#region License
//
// CopyConverter.cs
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

// #define MANUAL_INLINE_UNSAFE

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
    /// Copy or native Converter. 
    /// </summary>
    internal sealed partial class CopyConverter : EndianConverter
    {
        #region Reading from byte Arrays

        public override float ReadFloat(byte[] data, int index)
        {
            return ReadEndian.GetFloatCopy(data, index);
        }

        public override double ReadDouble(byte[] data, int index)
        {
            return ReadEndian.GetDoubleCopy(data, index);
        }

        public override decimal ReadDecimal(byte[] data, int index)
        {
            throw new NotImplementedException();
        }

        public override ushort ReadUInt16(byte[] data, int index)
        {
            return ReadEndian.GetUInt16Copy(data, index);
        }

        public override uint ReadUInt32(byte[] data, int index)
        {
            return ReadEndian.GetUInt32Copy(data, index);
        }

        public override ulong ReadUInt64(byte[] data, int index)
        {
            return ReadEndian.GetUInt64Copy(data, index);
        }

        public override short ReadInt16(byte[] data, int index)
        {
            return ReadEndian.GetInt16Copy(data, index);
        }

        public override int ReadInt32(byte[] data, int index)
        {
            return ReadEndian.GetInt32Copy(data, index);
        }

        public override long ReadInt64(byte[] data, int index)
        {
            return ReadEndian.GetInt64Copy(data, index);
        }

        #endregion

        #region Writing to byte Arrays

#if MANUAL_INLINE_UNSAFE

        public unsafe override void Write(byte[] dest, int index, float value)
        {
            fixed (byte* target = &dest[index])
            {
                uint* source = (uint*)&value;

                *((uint*)target) = *source;
            }
        }

        public unsafe override void Write(byte[] dest, int index, double value)
        {
            fixed (byte* target = &dest[index])
            {
                ulong* source = (ulong*)&value;

                *((ulong*)target) = *source;
            }
        }

        public unsafe override void Write(byte[] dest, int index, ushort value)
        {
            fixed (byte* target = &dest[index])
            {
                ushort* source = (ushort*)&value;

                *((ushort*)target) = *source;
            }
        }

        public unsafe override void Write(byte[] dest, int index, uint value)
        {
            fixed (byte* target = &dest[index])
            {
                uint* source = (uint*)&value;

                *((uint*)target) = *source;
            }
        }

        public unsafe override void Write(byte[] dest, int index, ulong value)
        {
            fixed (byte* target = &dest[index])
            {
                ulong* source = (ulong*)&value;

                *((ulong*)target) = *source;
            }
        }

        public unsafe override void Write(byte[] dest, int index, short value)
        {
            fixed (byte* target = &dest[index])
            {
                ushort* source = (ushort*)&value;

                *((ushort*)target) = *source;
            }
        }

        public unsafe override void Write(byte[] dest, int index, int value)
        {
            fixed (byte* target = &dest[index])
            {
                uint* source = (uint*)&value;

                *((uint*)target) = *source;
            }
        }

        public unsafe override void Write(byte[] dest, int index, long value)
        {
            fixed (byte* target = &dest[index])
            {
                ulong* source = (ulong*)&value;

                *((ulong*)target) = *source;
            }
        }

#else


        public override void Write(byte[] dest, int index, float value)
        {
            //WriteEndian.PutBytesCopy(dest, index, value);
            WriteEndian.PutBytesCopyU(dest, index, value);
        }

        public override void Write(byte[] dest, int index, double value)
        {
            //WriteEndian.PutBytesCopy(dest, index, value);
            WriteEndian.PutBytesCopyU(dest, index, value);
        }

        public override void Write(byte[] dest, int index, ushort value)
        {
            WriteEndian.PutBytesCopy(dest, index, value);
        }

        public override void Write(byte[] dest, int index, uint value)
        {
            WriteEndian.PutBytesCopy(dest, index, value);
        }

        public override void Write(byte[] dest, int index, ulong value)
        {
            WriteEndian.PutBytesCopy(dest, index, value);
        }

        public override void Write(byte[] dest, int index, short value)
        {
            WriteEndian.PutBytesCopy(dest, index, value);
        }

        public override void Write(byte[] dest, int index, int value)
        {
            WriteEndian.PutBytesCopy(dest, index, value);
        }

        public override void Write(byte[] dest, int index, long value)
        {
            WriteEndian.PutBytesCopy(dest, index, value);
        }

#endif

        public override void Write(byte[] dest, int index, decimal scalar)
        {
            //WriteEndian.PutBytesCopy(dest, index, value);
            throw new NotImplementedException();
        }

        #endregion

        // performance testing only
    }
}
