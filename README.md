# LeetCode Solutions

My solutions to [LeetCode](https://leetcode.com/) problems, written in C#.

All **solutions** are hand-written — no AI assistance. The test scaffolding around
them is generated.

Requires the .NET 10 SDK.

## Structure

Each problem lives in its own directory named `<number>_<ProblemName>`:

```
LeetCode.csproj      # single test project spanning every problem
1_TwoSum/
  Solution.cs        # the solution
2_AddTwoNumbers/
  Solution.cs        # the solution
  DESCRIPTION.md     # the problem statement
3_LongestSubstring.../
  Solution.cs        # the solution
  SolutionTests.cs   # xUnit test cases
```

- `Solution.cs` — the solution, usually a `public class Solution` matching LeetCode's expected signature. A link to the original problem is often included as a comment at the top.
- `SolutionTests.cs` — xUnit test cases for that problem, when present.
- `DESCRIPTION.md` — the problem statement, when included.

## Tests

There's a single [`LeetCode.csproj`](LeetCode.csproj) at the repo root that compiles
every problem and its tests. Because each problem defines its own `Solution` class,
each one lives in a numbered namespace (`P1`, `P2`, `P3`, ...) at the top of its files
to avoid name collisions. Drop the namespace line when pasting a solution back into
LeetCode.

Use the `test.sh` helper:

```
./test.sh        # run every problem's tests
./test.sh 3      # run only problem 3 (the P3 namespace)
```

Or call `dotnet` directly:

```
dotnet test                                    # everything
dotnet test --filter "FullyQualifiedName~P3."  # just problem 3
```

The trailing dot in the filter keeps `P3` from also matching a future `P30`.

## Adding a problem

1. Create a directory `<number>_<PascalCaseName>/`.
2. Add `Solution.cs` and (optionally) `SolutionTests.cs`, each starting with the
   file-scoped namespace `P<number>;`.

No project file is needed — the root `LeetCode.csproj` picks up every `.cs` file
automatically. The `/add-leetcode-tests` skill can scaffold the directory, a
solution stub, and test cases straight from a problem URL.

## Solutions

| # | Problem | Solution | Tests |
|---|---------|----------|-------|
| 1 | [Two Sum](https://leetcode.com/problems/two-sum/) | [Solution.cs](1_TwoSum/Solution.cs) | — |
| 2 | [Add Two Numbers](https://leetcode.com/problems/add-two-numbers/) | [Solution.cs](2_AddTwoNumbers/Solution.cs) | — |
| 3 | [Longest Substring Without Repeating Characters](https://leetcode.com/problems/longest-substring-without-repeating-characters/) | [Solution.cs](3_LongestSubstringWithoutRepeatingCharacters/Solution.cs) | [SolutionTests.cs](3_LongestSubstringWithoutRepeatingCharacters/SolutionTests.cs) |
