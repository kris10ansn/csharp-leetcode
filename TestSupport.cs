using System.Text;
using Xunit;
using Xunit.Abstractions;

// Applied once for the whole test assembly: run tests serially so redirected
// Console output stays in order and each test's output is attributed to the
// right test (Console.Out is a global static, so parallel tests would stomp
// each other's redirect).
[assembly: CollectionBehavior(DisableTestParallelization = true)]

/// <summary>
/// Base class for tests that want their Console output — including anything
/// written from inside a Solution — captured and shown per-test.
///
/// To reuse: make the test class inherit and forward the injected helper:
///
///     public class SolutionTests(ITestOutputHelper output)
///         : ConsoleCapturingTest(output) { ... }
///
/// Existing Console.WriteLine calls need no changes; they're routed to xUnit
/// and printed under the specific test. See them (passing tests included) with:
///
///     dotnet test --logger "console;verbosity=detailed"
/// </summary>
public abstract class ConsoleCapturingTest
{
    protected ConsoleCapturingTest(ITestOutputHelper output) =>
        Console.SetOut(new TestOutputWriter(output));

    // Forwards each completed line to ITestOutputHelper. Every Console.Write /
    // WriteLine overload funnels down to Write(char), so overriding just that
    // captures them all without touching call sites.
    sealed class TestOutputWriter(ITestOutputHelper output) : TextWriter
    {
        readonly StringBuilder _line = new();
        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char c)
        {
            if (c == '\n') { output.WriteLine(_line.ToString()); _line.Clear(); }
            else if (c != '\r') _line.Append(c);
        }
    }
}
