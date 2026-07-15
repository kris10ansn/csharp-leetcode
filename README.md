# LeetCode Solutions

My solutions to [LeetCode](https://leetcode.com/) problems, written in C#.

All **solutions** are hand-written — no AI assistance. The test scaffolding around
them is generated.

Requires the .NET 10 SDK.

## Structure

Each problem lives in its own directory named `<number>_<ProblemName>`:

```
LeetCode.csproj      # single test project spanning every problem
test.sh              # test runner helper
1_TwoSum/
  Solution.cs        # the solution
  SolutionTests.cs   # xUnit test cases
2_AddTwoNumbers/
  Solution.cs        # the solution
  SolutionTests.cs   # xUnit test cases
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
automatically.

### The `/add-leetcode-tests` skill

This repo ships a [Claude Code](https://claude.com/claude-code) skill at
[`.claude/skills/add-leetcode-tests/`](.claude/skills/add-leetcode-tests/SKILL.md)
that scaffolds a new problem from its LeetCode URL. Invoke it with the problem link:

```
/add-leetcode-tests https://leetcode.com/problems/valid-parentheses/
```

It will:

1. Fetch the problem via LeetCode's GraphQL API (or ask you to paste it if the
   request is blocked).
2. Create the `<number>_<PascalCaseName>/` directory with the correct `P<number>`
   namespace.
3. Write a `Solution.cs` stub matching LeetCode's expected signature — left empty
   for you to solve by hand. It **never** writes a solution. (If a solution already
   exists, it adds tests only and leaves your code alone.)
4. Write `DESCRIPTION.md` with the problem statement.
5. Write `SolutionTests.cs` with xUnit cases derived from the worked examples and
   the constraints.
6. Verify the expected values independently (a throwaway scratchpad script),
   never by writing into `Solution.cs`.
7. Add the problem to the Solutions table below.

## Solutions

| # | Problem | Solution | Tests |
|---|---------|----------|-------|
| 1 | [Two Sum](https://leetcode.com/problems/two-sum/) | [Solution.cs](1_TwoSum/Solution.cs) | [SolutionTests.cs](1_TwoSum/SolutionTests.cs) |
| 2 | [Add Two Numbers](https://leetcode.com/problems/add-two-numbers/) | [Solution.cs](2_AddTwoNumbers/Solution.cs) | [SolutionTests.cs](2_AddTwoNumbers/SolutionTests.cs) |
| 3 | [Longest Substring Without Repeating Characters](https://leetcode.com/problems/longest-substring-without-repeating-characters/) | [Solution.cs](3_LongestSubstringWithoutRepeatingCharacters/Solution.cs) | [SolutionTests.cs](3_LongestSubstringWithoutRepeatingCharacters/SolutionTests.cs) |
| 4 | [Median of Two Sorted Arrays](https://leetcode.com/problems/median-of-two-sorted-arrays/) | [Solution.cs](4_MedianOfTwoSortedArrays/Solution.cs) | [SolutionTests.cs](4_MedianOfTwoSortedArrays/SolutionTests.cs) |
