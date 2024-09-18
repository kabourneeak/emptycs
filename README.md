# Empty Project

## Introduction

This project is an empty C# project with an API Server and a CLI.
It's main purpose is to be a testing ground for new ideas
or a starting place for technical coding interviews.

## Steps

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
dotnet new classlib -o src/Empty.Sdk -n Empty.Sdk
dotnet new webapi -o src/Empty.Api -n Empty.Api
dotnet new console -o src/Empty.Cli -n Empty.Cli
dotnet new nunit -o test/Empty.Tests -n Empty.Tests
dotnet new gitignore
dotnet new sln -n Empty
dotnet sln add ./src/Empty.Sdk
dotnet sln add ./src/Empty.Api
dotnet sln add ./src/Empty.Cli
dotnet sln add ./test/Empty.Tests/
```
