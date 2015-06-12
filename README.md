# Kraggs.IO.Endian

Kraggs.IO.Endian is a simple Portable binary endian aware Reader and Writer library.
Instead of using the excellent [Mono.DataConvert](http://www.mono-project.com/Mono_DataConvert) I created my own which is a lot smaller
since it doesn't have the Perl like Pack/Unpack functions or the text encoding/decoding code.
On the other hand it is for most functions faster, portable and coded against .NET 4.5 api.
The speed improvements are accompished mainly by avoiding using unsafe code whenever possible
(and thereby let the compiler optimize better) and heavy use of [Aggressive Inlining](https://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.methodimploptions(v=vs.110).aspx)

The code is validated against Mono.DataConvert with nUnit tests, but for now is mostly tested
on Little Endian machines since I currently don't have access to a Big Endian Machine.

## TODO	
	* Add EndianWriter code
	* Add (Async) Array reading/writing from stream.	
	* Add Text reading/writing.
	* Actually run nUnit tests on a Big Endian machine to validate code.

## License

See the [LICENSE](LICENSE.md) file for license rights and limitations (MIT)