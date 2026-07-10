---
name: add-leetcode-tests
description: Scaffold a LeetCode problem solution stub and xUnit test cases from a problem URL. Use when the user provides a leetcode.com/problems/... link and wants tests (and/or a solution stub) created for it in this repo.
---

# Add LeetCode tests

Given a LeetCode problem URL, create the problem directory, a `Solution.cs` stub,
and a `SolutionTests.cs` file with xUnit test cases derived from the problem's
description. Follow this repo's conventions exactly.

## Repo conventions (must match)

- One root project, `LeetCode.csproj`, compiles every problem and its tests. Do
  **not** create a per-problem `.csproj`.
- Each problem lives in `<number>_<PascalCaseName>/` (e.g. `3_LongestSubstringWithoutRepeatingCharacters/`).
- Every problem file starts with a file-scoped namespace `P<number>;` so the
  repeated `Solution` class names don't collide (e.g. `namespace P3;`).
- Tests use xUnit (`[Theory]`/`[InlineData]` for parameterized cases, `[Fact]`
  for one-offs). Test class is `SolutionTests`.

## Steps

1. **Get the URL.** It's the argument to this skill, or ask the user for it.
   Extract the problem slug from `leetcode.com/problems/<slug>/`.

2. **Fetch the description.** Use WebFetch on the URL. Pull out: the method
   signature LeetCode expects, all worked **Examples** (input → output), and the
   **Constraints** section. If the page can't be fetched, ask the user to paste
   the description text and continue from that.

3. **Determine the problem number and name.** Ask the user for the LeetCode
   problem number if it isn't obvious from context, or infer it from the URL/page.
   Directory name is `<number>_<PascalCaseName>`; namespace is `P<number>`.

4. **Create `Solution.cs`** with the namespace and an empty stub matching
   LeetCode's expected signature — do not implement it (the user writes solutions
   by hand). **If `Solution.cs` already exists, leave it untouched** — the user has
   already solved it; only add tests in that case. Stub example:

   ```csharp
   namespace P3;

   public class Solution
   {
       public int LengthOfLongestSubstring(string s)
       {

       }
   }
   ```

5. **Create `SolutionTests.cs`** with cases derived from the description:
   - One `[Theory]` covering every worked Example from the problem statement,
     with a comment tying each case back to the stated answer.
   - Additional cases derived from the **Constraints** (e.g. empty input if the
     length lower bound is 0, single element, all-same, allowed character
     classes like digits/symbols/spaces) and classic edge cases for the problem
     type (off-by-one / pointer-reset traps, etc.).
   - Group related cases into separate `[Theory]`/`[Fact]` methods with clear
     names. Reference the constraint each group covers in a comment.
   - Put the tests in `namespace P<number>;` and `using Xunit;`.

6. **Verify the expected values are correct.** If a real solution already exists,
   just run `dotnet test --filter "FullyQualifiedName~P<number>."` against it — a
   green run confirms both the tests and the existing solution. Otherwise
   temporarily write a correct reference implementation into the stub, run the
   tests, confirm all cases pass, then **restore `Solution.cs` to the empty stub**.
   Never leave a reference implementation in place — the user writes the real solution.

7. **Update `README.md`** — add a row to the Solutions table linking the problem
   and its `Solution.cs`.

8. **Report** the run command to the user:
   `dotnet test --filter "FullyQualifiedName~P<number>."`

## Notes

- The `--filter` trailing dot matters: `FullyQualifiedName~P3.` avoids also
  matching a future `P30`.
- Build artifacts go to a single root `bin/`/`obj/` (already gitignored); don't
  create per-problem ones.
