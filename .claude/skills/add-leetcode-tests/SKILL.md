---
name: add-leetcode-tests
description: Scaffold a LeetCode problem solution stub and xUnit test cases from a problem's description. Use when the user provides a LeetCode problem (as pasted text/HTML or a saved .html file) and wants tests (and/or a solution stub) created for it in this repo.
---

# Add LeetCode tests

Given a LeetCode problem's description, create the problem directory, a
`Solution.cs` stub, and a `SolutionTests.cs` file with xUnit test cases derived
from the description. Follow this repo's conventions exactly.

## Input: how the problem description arrives

LeetCode returns **403** to automated fetchers (WebFetch), so **do not** try to
fetch `leetcode.com/problems/...` directly. Instead the user supplies the
problem description in one of these forms:

- **A saved HTML file** — the user saved the problem page (or its description
  panel) to a `.html` file and gives you the path. Read it with the Read tool.
- **Pasted text or HTML** — the user pastes the problem statement (or the raw
  HTML of the description) straight into the conversation.

If the user only gives a URL with no description, ask them to either paste the
problem text/HTML or save the page to an `.html` file and give you the path.
Still take the URL when offered — it gives the slug and (often) the number.

## Repo conventions (must match)

- One root project, `LeetCode.csproj`, compiles every problem and its tests. Do
  **not** create a per-problem `.csproj`.
- Each problem lives in `<number>_<PascalCaseName>/` (e.g. `3_LongestSubstringWithoutRepeatingCharacters/`).
- Every problem file starts with a file-scoped namespace `P<number>;` so the
  repeated `Solution` class names don't collide (e.g. `namespace P3;`).
- Tests use xUnit (`[Theory]`/`[InlineData]` for parameterized cases, `[Fact]`
  for one-offs). Test class is `SolutionTests`.

## Steps

1. **Get the input.** Take whatever the user provided (see "Input" above): an
   `.html` file path (Read it), pasted text/HTML (use it directly), and/or a
   URL. If none of these carry the actual problem statement, ask the user to
   paste the text/HTML or save the page to an `.html` file. Extract the problem
   slug from the URL (`leetcode.com/problems/<slug>/`) if one was given.

2. **Parse the description.** From the file/pasted content, pull out: the method
   signature LeetCode expects (the C# starter code), all worked **Examples**
   (input → output), and the **Constraints** section. If the content is raw
   HTML, read through the tags to recover this — the examples and constraints
   are in the description body; the signature is in the code snippet block.

3. **Determine the problem number and name.** Infer them from the description,
   URL, or page title (e.g. a heading like "3. Longest Substring…"). Ask the
   user for the LeetCode problem number only if it can't be determined.
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
