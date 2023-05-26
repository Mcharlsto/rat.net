# Horizontally spinning rat - now written in C#!

A C# port of the [Linux horizontally spinning rat](https://github.com/Mcharlsto/rat), complete with soundtrack and spin counter.

![rat](https://www.horizontallyspinningrat.tk/rat.gif)

## Arguments
```
-m | ratmark - benchmark timedemo
-u | uncap the framerate
```

## Usage

### Recommended
Download [rat-net472-win.exe](https://github.com/Mcharlsto/rat.net/releases/download/1.0.0/rat-net472-win.exe).
On Windows, run the executable.

On Linux, install `mono`, then run `mono rat-net472-win.exe`

### Experimental - .NET Core Linux build
There is no audio support in the .NET Core version, this is due to a cross-platform API weirdness.

The archive is also massive, as it has to include its own copy of the framework.

Download [rat-net70-linux.7z](https://github.com/Mcharlsto/rat.net/releases/download/1.0.0/rat-net70-linux.7z).

Extract the archive, then run the `rat` binary.
