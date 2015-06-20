# Kraggs.IO.Endian

Kraggs.IO.Endian is a simple Portable endian aware conversion library. It started out
as an alternative to the BinaryReader/BinaryWriter apis, but has now also a BitConverter alternative.
Planned improvements are Endian Array Conversion functions for speeding things up even more
and Async Stream conversion functions. 
Instead of using the excellent [Mono.DataConvert](http://www.mono-project.com/Mono_DataConvert)  I created my own endian conversion library
which is portable and designed against .Net 4.5. Also all the conversion functions are reevaluated 
based on performance tests in order to find the most optimal implementation. Functionality which
are currently skipped/missing for now are the Perl Pack/Unpack functions and the text functions.
The speed improvements are mainly accomplished by avoiding using unsafe code, and heavy use of
[Aggressive Inlining](https://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.methodimploptions(v=vs.110).aspx) 

The code is validated against Mono.DataConvert with nUnit tests, but for now is mostly tested
on Little Endian machines since I currently don't have access to a Big Endian Machine.

The code has been performance tested on both [Mac](https://github.com/raggsokk/Kraggs.IO.Endian/blob/master/Reports/mac-report.txt), [Linux](https://github.com/raggsokk/Kraggs.IO.Endian/blob/master/Reports/linux-report.txt) and [Windows](https://github.com/raggsokk/Kraggs.IO.Endian/blob/master/Reports/win-report.txt) 
in order the most optimal code path.

## TODO	
	* ~~Add EndianWriter code.~~
	* ~~Add EndianConverter code for non stream testing.~~
	* ~~Add end user performance test for EndianConverter.~~
	* ~~Recheck EndianReader/Writer based on performance results.~~
	* Add Array Conversion functions.
	* Add Async Array Conversion to EndianReader/Writer.
	* Add Text reading/writing.
	* Actually run nUnit tests on a Big Endian machine to validate code.
	* Actually run nUnit tests on a ARM machine to validate code.

## License

See the [LICENSE](LICENSE.md) file for license rights and limitations (MIT)