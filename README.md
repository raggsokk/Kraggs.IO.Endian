# Kraggs.IO.Endian

Kraggs.IO.Endian is a simple Portable Binary Reader and Writer library for my own use.
Instead of using the excellent [Mono.DataConvert](http://www.mono-project.com/Mono_DataConvert) I created my own which is a lot smaller
since it doesn't have the Perl like Pack/Unpack functions. It is also for most functions
a lot faster. This is accomplished by avoiding using unsafe whenever possible and heavy use 
of [Aggressive Inlining](https://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.methodimploptions(v=vs.110).aspx)

The code is validated against Mono.DataConvert with nUnit tests, but for now is mostly tested
on Little Endian machines since I currently don't have access to a Big Endian Machine.

## TODO	
	* Add Async Array reading from stream.

## License

See the [LICENSE](LICENSE.md) file for license rights and limitations (MIT)