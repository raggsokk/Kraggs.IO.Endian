//
// SwapEndianWriter.cs
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
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kraggs.IO
{
    internal sealed class SwapEndianWriter : EndianWriter
    {
        public SwapEndianWriter(Stream stream, bool leaveOpen = false)
            : base(stream, leaveOpen)
        {

        }

        #region EndianWrite Implementation

        public override void Write(UInt16 value)
        {
            WriteEndian.PutBytesSwap(pBuffer, 0, value);
            BaseStream.Write(pBuffer, 0, sizeof(UInt16));
        }

        public override void Write(UInt32 value)
        {
            WriteEndian.PutBytesSwap(pBuffer, 0, value);
            BaseStream.Write(pBuffer, 0, sizeof(UInt32));
        }

        public override void Write(UInt64 value)
        {
            WriteEndian.PutBytesSwap(pBuffer, 0, value);
            BaseStream.Write(pBuffer, 0, sizeof(UInt64));
        }

        public override void Write(float value)
        {
            WriteEndian.PutBytesSwap(pBuffer, 0, value);
            BaseStream.Write(pBuffer, 0, sizeof(float));
        }

        public override void Write(double value)
        {
            WriteEndian.PutBytesSwap(pBuffer, 0, value);
            BaseStream.Write(pBuffer, 0, sizeof(double));
        }

        public override void Write(Int16 value)
        {
            WriteEndian.PutBytesSwap(pBuffer, 0, value);
            BaseStream.Write(pBuffer, 0, sizeof(Int16));
        }

        public override void Write(Int32 value)
        {
            WriteEndian.PutBytesSwap(pBuffer, 0, value);
            BaseStream.Write(pBuffer, 0, sizeof(Int32));
        }

        public override void Write(Int64 value)
        {
            WriteEndian.PutBytesSwap(pBuffer, 0, value);
            BaseStream.Write(pBuffer, 0, sizeof(Int64));
        }


        #endregion
    }

}
