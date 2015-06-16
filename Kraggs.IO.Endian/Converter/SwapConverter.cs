﻿#region License
//
// SwapConverter.cs
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
    /// Swap Converter.
    /// </summary>
    internal sealed partial class SwapConverter : EndianConverter
    {
        #region Reading from byte Arrays

        public override float ReadFloat(byte[] data, int index)
        {
            return ReadEndian.GetFloatSwap(data, index);
        }

        public override double ReadDouble(byte[] data, int index)
        {
            return ReadEndian.GetDoubleSwap(data, index);
        }

        public override decimal ReadDecimal(byte[] data, int index)
        {
            throw new NotImplementedException();
        }

        public override ushort ReadUInt16(byte[] data, int index)
        {
            return ReadEndian.GetUInt16Swap(data, index);
        }

        public override uint ReadUInt32(byte[] data, int index)
        {
            return ReadEndian.GetUInt32Swap(data, index);
        }

        public override ulong ReadUInt64(byte[] data, int index)
        {
            return ReadEndian.GetUInt64Swap(data, index);
        }

        public override short ReadInt16(byte[] data, int index)
        {
            return ReadEndian.GetInt16Swap(data, index);
        }

        public override int ReadInt32(byte[] data, int index)
        {
            return ReadEndian.GetInt32Swap(data, index);
        }

        public override long ReadInt64(byte[] data, int index)
        {
            return ReadEndian.GetInt64Swap(data, index);
        }

        #endregion

        #region Writing to byte Arrays

        public override void Write(byte[] dest, int index, float value)
        {
            WriteEndian.PutBytesSwap(dest, index, value);
        }

        public override void Write(byte[] dest, int index, double value)
        {
            WriteEndian.PutBytesSwap(dest, index, value);
        }

        public override void Write(byte[] dest, int index, decimal scalar)
        {
            //WriteEndian.PutBytesSwap(dest, index, value);
            throw new NotImplementedException();
        }

        public override void Write(byte[] dest, int index, ushort value)
        {
            WriteEndian.PutBytesSwap(dest, index, value);
        }

        public override void Write(byte[] dest, int index, uint value)
        {
            WriteEndian.PutBytesSwap(dest, index, value);
        }

        public override void Write(byte[] dest, int index, ulong value)
        {
            WriteEndian.PutBytesSwap(dest, index, value);
        }

        public override void Write(byte[] dest, int index, short value)
        {
            WriteEndian.PutBytesSwap(dest, index, value);
        }

        public override void Write(byte[] dest, int index, int value)
        {
            WriteEndian.PutBytesSwap(dest, index, value);
        }

        public override void Write(byte[] dest, int index, long value)
        {
            WriteEndian.PutBytesSwap(dest, index, value);
        }

        #endregion
    }
}
