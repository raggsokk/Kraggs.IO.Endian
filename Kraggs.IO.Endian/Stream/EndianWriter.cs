//
// EndianWriter.cs
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
    /// <summary>
    /// Used to write big endian or little endian to a stream.
    /// </summary>
    public abstract partial class EndianWriter : IDisposable
    {
        public Stream BaseStream { get; protected set; }
        public static readonly bool IsLittleEndian = BitConverter.IsLittleEndian;

        protected bool flagLeaveOpen = false;
        protected byte[] pBuffer;
        //private const int BUF_SIZE = 2048;
        //protected int pBufPos = 0;

        #region Constructors

        protected EndianWriter(Stream stream, bool leaveOpen = false)
        { 
            // arg validation.
            if (stream == null)
                throw new ArgumentNullException("stream", "Cant write endian data to null straem");
            if (!stream.CanWrite)
                throw new ArgumentException("Stream dont support writing!", "stream");

            this.BaseStream = stream;
            this.flagLeaveOpen = leaveOpen;
            this.pBuffer = new byte[sizeof(double)]; // largest primitive to write.
        }

        public static EndianWriter CreateNativeWriter(Stream stream, bool leaveOpen = false)
        {
            return new NativeEndianWriter(stream, leaveOpen);
        }

        public static EndianWriter CreateBigEndianWriter(Stream stream, bool leaveOpen = false)
        {
            if (IsLittleEndian)
                return new SwapEndianWriter(stream, leaveOpen);
            else
                return new NativeEndianWriter(stream, leaveOpen);
        }

        public static EndianWriter CreateLittleEndianWriter(Stream stream, bool leaveOpen = false)
        {
            if (IsLittleEndian)
                return new NativeEndianWriter(stream, leaveOpen);
            else
                return new SwapEndianWriter(stream, leaveOpen);
        }

        #endregion

        #region Mostly abstract write Primitive Interface

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //private void FlushInternal()
        //{
        //    this.BaseStream.Write(pBuffer, 0, pBufPos);
        //    pBufPos = 0;
        //}
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //private async Task FlushInternalAsync()
        //{
        //    await this.BaseStream.WriteAsync(pBuffer, 0, pBufPos);
        //    pBufPos = 0;
        //}

        public void Flush()
        {
            //FlushInternal();
            BaseStream.Flush();
        }

        public async Task FlushAsync()
        {
            //await FlushInternalAsync();
            await BaseStream.FlushAsync();
        }

        /// <summary>
        /// Writes an UInt16 to the basestream, handling endian conversion as needed.
        /// </summary>
        /// <param name="value"></param>
        public abstract void Write(UInt16 value);
        /// <summary>
        /// Writes an UInt32 to the basestream, handling endian conversion as needed.
        /// </summary>
        /// <param name="value"></param>
        public abstract void Write(UInt32 value);
        /// <summary>
        /// Writes an UInt64 to the basestream, handling endian conversion as needed.
        /// </summary>
        /// <param name="value"></param>
        public abstract void Write(UInt64 value);

        /// <summary>
        /// Writes a float to the basestream, handling endian conversion as needed.
        /// </summary>
        /// <param name="value"></param>
        public abstract void Write(float value);

        /// <summary>
        /// Writes a double to the basestream, handling endian conversion as needed.
        /// </summary>
        /// <param name="value"></param>
        public abstract void Write(double value);

        /// <summary>
        /// Writes an Int16 to the basestream, handling endian conversion as needed.
        /// </summary>
        /// <param name="value"></param>
        public abstract void Write(Int16 value);
        /// <summary>
        /// Writes an Int32 to the basestream, handling endian conversion as needed.
        /// </summary>
        /// <param name="value"></param>
        public abstract void Write(Int32 value);
        /// <summary>
        /// Writes an Int64 to the basestream, handling endian conversion as needed.
        /// </summary>
        /// <param name="value"></param>
        public abstract void Write(Int64 value);

        #endregion

        #region Array Writing

        /// <summary>
        /// Simply wraps BaseStream.Write
        /// NO Endian Handling!
        /// </summary>
        /// <param name="buffer">source buffer with data to write to stream.</param>
        /// <param name="index">where in source buffer to start at</param>
        /// <param name="count">number of bytes to write from buffer.</param>
        public void Write(byte[] buffer, int index = 0, int count = -1)
        {
            if(count == -1)
                count = buffer.Length - index;

            BaseStream.Write(buffer, index, count);
            
        }

        /// <summary>
        /// Simply wraps baseStream.ReadAsync.
        /// NO Endian Handling!
        /// </summary>
        /// <param name="buffer">source buffer with data to write to stream.</param>
        /// <param name="index">where in source buffer to start at.</param>
        /// <param name="count">number of bytes to write.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task WriteAsync(byte[] buffer, int index = 0, int count = -1, CancellationToken? cancellationToken = null )
        {
            if(count == -1)
                count = buffer.Length - index;

            if (cancellationToken.HasValue)
                await BaseStream.WriteAsync(buffer, index, count, cancellationToken.Value);
            else
                await BaseStream.WriteAsync(buffer, index, count);
        }

        #endregion

        #region IDisposable Implementation

        public bool IsDisposed { get; protected set; }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool Disposing)
        {
            if(Disposing)
            {
                if (BaseStream != null || !flagLeaveOpen)
                    BaseStream.Dispose();
            }

            IsDisposed = true;
        }

        #endregion
    }
}
