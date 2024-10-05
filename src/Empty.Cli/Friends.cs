using System.Runtime.CompilerServices;

// Allow the test project to access internal classes
[assembly:InternalsVisibleTo("Empty.Tests")]

// Allow NSubstitute to access internal interfaces
[assembly:InternalsVisibleTo("DynamicProxyGenAssembly2")]
