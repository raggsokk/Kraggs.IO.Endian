//
// NativeEndianReader.cs
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
    internal sealed class NativeEndianReader : EndianReader
    {
        public NativeEndianReader(Stream stream, bool leaveOpen = false)
            : base(stream, leaveOpen)
        {

        }

        #region Mostly abstract Read Primitive Interface

        public override ushort ReadUInt16()
        {
            FillBuffer(2);
            return ConvertEndian.GetUInt16Copy(pBuffer, 0);
        }

        public override uint ReadUInt32()
        {
            FillBuffer(4);
            return ConvertEndian.GetUInt32Copy(pBuffer, 0);
        }

        public override ulong ReadUInt64()
        {
            FillBuffer(8);
            return ConvertEndian.GetUInt64Copy(pBuffer, 0);
        }

        public override float ReadFloat()
        {
            FillBuffer(4);
            return ConvertEndian.GetFloatCopy(pBuffer, 0);            
        }

        public override double ReadDouble()
        {
            FillBuffer(8);
            return ConvertEndian.GetDoubleCopy(pBuffer, 0);            
        }

        public override short ReadInt16()
        {
            FillBuffer(2);
            return ConvertEndian.GetInt16Copy(pBuffer, 0);
        }

        public override int ReadInt32()
        {
            FillBuffer(4);
            return ConvertEndian.GetInt32Copy(pBuffer, 0);
        }

        public override long ReadInt64()
        {
            FillBuffer(8);
            return ConvertEndian.GetInt64Copy(pBuffer, 0);
        }

        #endregion
    }
}
