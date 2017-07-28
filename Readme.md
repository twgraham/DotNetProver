# dotnet-prover

[![CircleCI](https://circleci.com/gh/twgraham/DotNetProver/tree/master.svg?style=svg)](https://circleci.com/gh/twgraham/DotNetProver/tree/master)
[![NuGet](https://img.shields.io/nuget/v/DotNetProver.svg)](https://www.nuget.org/packages/DotNetProver)

.NET CLI tool to get versions of C# projects. Simple as that. Especially useful for CI situations.

## Installing

Edit your `.csproj` and add:
```xml
<ItemGroup>
    <DotNetCliToolReference Include="DotNetProver" Version="*" />
</ItemGroup>
```
## Usage
```
dotnet prover

DotNet Project Version

Usage: dotnet prover [options]

Options:
  -?|-h|--help              Show help information
  -p|--projects <PROJECTS>  Project(s) to get version(s) for (default: current directory)
  -v|--versions-only        Only print the version(s) for projects

```