# DotnetInstallerForDotnetInstallers

This is a set of utilities for installers written in .NET, to let you, yes, install .NET.

**WARNING:** At time of writing, this has been *completely untested*. This package is being uploaded to NuGet for *testing purposes only*.

## Install

Coming soon... this is being extracted out of another project of mine...

## Why?

~~*yo dawg, i heard you like installers...*~~

**TL;DR install .NET 5+ with .NET Framework 3.5-4.8.1 installed.**

If you're a long-time .NET developer or have been out of the loop for a while, you may remember that it used to be possible to assume that certain version(s) of .NET Framework were installed on a user's computer based on their Windows version.

While this wasn't a guarantee that your targeted .NET Framework version would be supported by the majority of users, it became a pretty safe assumption at some point. For example, [Microsoft lists .NET Framework 3.5](https://learn.microsoft.com/en-us/dotnet/framework/migration-guide/versions-and-dependencies) as pre-installed on all versions of Windows 7 up to Windows 10. With the longevity of Windows 10, that almost became shorthand for the vast majority of Windows installations worldwide!

Once again however, with the evolution of .NET Framework into .NET Core (now simply .NET starting from .NET 5), this assumption is no longer reliable, as .NET releases are now completely decoupled from Windows releases. Hence, distributing a desktop application with a modern .NET dependency once again requires pulling .NET prerequisites, if necessary..?

Not anymore, there *are* alternatives! For instance, .NET Core 3 introduced self-contained deployment, .NET 7 introduced the beginnings of native AOT deployment, and .NET 8 is improving on native AOT further.

But... what if you *would* like to take the classic approach of downloading and installing modern .NET prerequisites on behalf of your users?

There's a fantastic project called **DotnetRuntimeBootstrapper** ([GitHub repo](https://github.com/Tyrrrz/DotnetRuntimeBootstrapper)) that can wrap your application executable in a bootstrapper that takes care of installing the target .NET runtime automatically. However, it's designed exactly to do that, *not* do so at time of installing your application. Please consider using it if that works for your use case.

You can consider using **DotnetInstallerForDotnetInstallers** (This repo) instead if your application's installer is written in .NET (e.g. a WiX bootstrapper), and you'd prefer to install .NET prerequisites along with your application. This project reuses much of the great work done in DotnetRuntimeBootstrapper, adapting it for standalone use, which you can then use in your installer.

In other words, install .NET, with your installer written in .NET.

## Features

- Install .NET
- Bonus: Install Windows App SDK (requires .NET Framework 4.6.1+)
