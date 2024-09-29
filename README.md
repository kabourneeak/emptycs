# Empty Project

## Introduction

This project is an empty C# project with an API Server and a CLI.
Its main purpose is to be a testing ground for new ideas
or a starting place for technical coding interviews.

## What does it include

The project is divided into three parts

- An API Server that can respond to various endpoints.
- A CLI that works with the API Server.
- An SDK that contains the shared models and features of both.

## Prerequisites

To build the project, you require the latest [Dotnet SDK](https://dotnet.microsoft.com/en-us/download).

## Build

To build the project,
run the following command from the root of the repository:

```sh
$ dotnet build
```

The tests can be run with:

```sh
$ dotnet test
```

## How was this created?

Here are the steps used to create this project.

### Basic Artifacts

Start with the basic artifacts

- `LICENSE`
- `README.md`
- `.gitattributes`
- `.gitignore`

See [GitHub's documentation](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
for licensing a repository for more information about licenses,
how to choose them,
and where to get license files that you can add to your repository.

The `.gitignore` file is created using the `dotnet` tool.

```sh
$ dotnet new gitignore
```

### C# Projects

From the repository root, run the following commands.

```sh
$ mkdir src test

# create the projects themselves
$ dotnet new classlib -o src/Empty.Sdk -n Empty.Sdk
$ dotnet new webapi -o src/Empty.Api -n Empty.Api
$ dotnet new console -o src/Empty.Cli -n Empty.Cli
$ dotnet new nunit -o test/Empty.Tests -n Empty.Tests

# create a solution file to make building and testing easier
$ dotnet new sln -n Empty
$ dotnet sln add ./src/Empty.Sdk
$ dotnet sln add ./src/Empty.Api
$ dotnet sln add ./src/Empty.Cli
$ dotnet sln add ./test/Empty.Tests/
```

At this point, you should be able to build the entire solution:

```sh
$ dotnet build
```

Now we need to connect the projects together.

```sh
# allow API and CLI to see SDK
$ dotnet add ./src/Empty.Api reference ./src/Empty.Sdk
$ dotnet add ./src/Empty.Cli reference ./src/Empty.Sdk

# allow tests to be able to see API and CLI, and SDK transitively.
$ dotnet add ./test/Empty.Tests reference ./src/Empty.Api
$ dotnet add ./test/Empty.Tests reference ./src/Empty.Cli
```

You can run `dotnet build` again and should observe that the projects
build in an order that satisfies their inter-dependencies.

You should be able to run the projects manually.

```sh
$ dotnet run --project src/Empty.Cli 
Hello, World!

$ dotnet run --project src/Empty.Api
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5197
```

Finally, you should be able to run the (still non-existent) tests.

```sh
$ dotnet test
<snip>
Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: 24 ms - Empty.Tests.dll (net8.0)
```

## VS Code Artifacts

A `tasks.json` and `launch.json` are required for
building,
launching,
and debugging
the projects within VS Code, respectively.

The files are provided in this repository.
