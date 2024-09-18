# Empty Project

## Introduction

This project is an empty C# project with an API Server and a CLI.
It's main purpose is to be a testing ground for new ideas
or a starting place for technical coding interviews.

## How was this created?

Here are the steps used to create this project.

### Basic Artifacts

Start with the basic artifacts

- `LICENSE`
- `README.md`
- The `.gitattributes` file

We will create the `.gitignore` file using the `dotnet` tool, below.

### C# Projects

From the repository root, run the following commands.

```sh
mkdir src test
dotnet new gitignore
dotnet new classlib -o src/Empty.Sdk -n Empty.Sdk
dotnet new webapi -o src/Empty.Api -n Empty.Api
dotnet new console -o src/Empty.Cli -n Empty.Cli
dotnet new nunit -o test/Empty.Tests -n Empty.Tests
dotnet new sln -n Empty
dotnet sln add ./src/Empty.Sdk
dotnet sln add ./src/Empty.Api
dotnet sln add ./src/Empty.Cli
dotnet sln add ./test/Empty.Tests/
```

At this point, you should be able to build the entire solution:

```sh
dotnet build
```

Now we need to connect the projects together.

```sh
# allow API and Cli to see SDK
dotnet add ./src/Empty.Api reference ./src/Empty.Sdk
dotnet add ./src/Empty.Cli reference ./src/Empty.Sdk

# allow tests to be able to see API and Cli, and SDK transitively.
dotnet add ./test/Empty.Tests reference ./src/Empty.Api
dotnet add ./test/Empty.Tests reference ./src/Empty.Cli
```

You can run `dotnet build` again and should observe that the projects
build in an order that satisfies their inter-dependencies.
