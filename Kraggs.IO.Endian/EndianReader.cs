﻿//
// EndianReader.cs
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
using System.Threading;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Kraggs.IO
{
    public abstract partial class EndianReader : IDisposable
    {
        //protected Stream pStream;
        public Stream BaseStream { get; protected set; }        
        public static readonly bool IsLittleEndian = BitConverter.IsLittleEndian;

        protected bool flagLeaveOpen = false;
        protected byte[] pBuffer;

        #region Constructors

        protected EndianReader(Stream stream, bool leaveOpen = false)
        {
            // arg validation.
            if (stream == null)
                throw new ArgumentNullException("stream", "Cant read endian data from null straem");
            if (!stream.CanRead)
                throw new ArgumentException("Stream dont support reading!", "stream");

            this.BaseStream = stream;
            this.flagLeaveOpen = leaveOpen;
            // size of buffer is sizeof(double) since thats largest primitive supported for now.
            this.pBuffer = new byte[sizeof(double)];
        }

        public static EndianReader CreateNativeReader(Stream stream, bool leaveOpen = false)
        {
            throw new NotImplementedException();
        }

        public static EndianReader CreateLittleEndianReader(Stream stream, bool leaveOpen = false)
        {
            throw new NotImplementedException();
        }

        public static EndianReader CreateBigEndianReader(Stream stream, bool leaveOpen = false)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Mostly abstract Read Primitive Interface

        /// <summary>
        /// Fills buffer for converting to primitive.
        /// </summary>
        /// <param name="count"></param>
        protected virtual void FillBuffer(int count)
        {
            if (IsDisposed)
                throw new ObjectDisposedException("EndianReader", "Cant read from a disposed EndianReader!");

            var read = FillBufferFast(count);

            if (read != count)
                throw new EndOfStreamException(string.Format(
                    "Tried to read '{0}' bytes, but could only read '{1}' bytes from stream!", count, read));
        }

        /// <summary>
        /// Fast, nonvalidating fill buffer, meant for inlinging.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int FillBufferFast(int count)
        {
            return BaseStream.Read(pBuffer, 0, count);                        
        }

        public abstract UInt16 ReadUInt16();
        public abstract UInt32 ReadUInt32();
        public abstract UInt64 ReadUInt64();

        public abstract float ReadFloat();
        public abstract double ReadDouble();

        public abstract Int16 ReadInt16();
        public abstract Int32 ReadInt32();
        public abstract Int64 ReadInt64();


        #endregion

        #region Array Reading

        /// <summary>
        /// Simply wraps BaseStream.Read
        /// NO Endian Handling!
        /// </summary>
        /// <param name="buffer">Buffer to write data too.</param>
        /// <param name="index">Where in buffer to start writing at.</param>
        /// <param name="count">number of bytes to read or fill buffer.</param>
        /// <returns>number of bytes read.</returns>
        public virtual int Read(byte[] buffer, int index = 0, int count = -1)
        {
            if (count == -1)
                count = buffer.Length - index;

            return BaseStream.Read(buffer, index, count);
        }

        public async Task<int> ReadAsync(byte[] buffer, int index = 0, int count = -1, CancellationToken? cancellationToken = null )
        {
            if (count == -1)
                count = buffer.Length - index;

            if(cancellationToken.HasValue)
                return await BaseStream.ReadAsync(buffer, index, count, cancellationToken.Value);
            else
                return await BaseStream.ReadAsync(buffer, index, count);
        }

        #endregion

        #region IDisposable implementation

        //Why do we have this?
        public bool IsDisposed { get; protected set; }

        public void Dispose()
        {
            //throw new NotImplementedException();
            this.Dispose(true);
        }

        //TODO: Maybe revisit this Dispose code?
        protected virtual void Dispose(bool Disposing)
        {
            if(Disposing)
            {
                if(BaseStream != null && !flagLeaveOpen)
                    BaseStream.Dispose();
            }

            IsDisposed = true;
        }

        #endregion
    }
}
