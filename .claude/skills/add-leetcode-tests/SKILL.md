---
name: add-leetcode-tests
description: Scaffold a LeetCode solution stub and xUnit tests for a problem. Use when the user gives a leetcode.com/problems/... URL (or slug/number) and wants tests (and/or a solution stub) created in this repo.
---

# Add LeetCode tests

Scaffold a problem directory, an **empty** `Solution.cs` stub, and a
`SolutionTests.cs` with xUnit cases derived from the problem description.

> **Never write a solution.** Solving is always the user's job — no exceptions,
> not even a temporary or "reference" implementation. Every stub you create must
> have an empty method body.

## Fetch the problem

Get the slug from the URL (`leetcode.com/problems/<slug>/`) and curl LeetCode's
GraphQL API (WebFetch gets 403; this works):

```bash
curl -s https://leetcode.com/graphql \
  -H 'Content-Type: application/json' \
  -H 'User-Agent: Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36' \
  --data '{"query":"query q($slug:String!){question(titleSlug:$slug){questionFrontendId title content codeSnippets{lang code}}}","variables":{"slug":"<slug>"}}'
```

Returns JSON with:
- `questionFrontendId` → problem **number**
- `title` → problem **name**
- `codeSnippets` → use the `lang == "C#"` entry's `code` **verbatim** as the stub
- `content` → HTML description with the **Examples** and **Constraints**

If `question` is null or the body isn't JSON, the request was blocked — ask the
user to paste the description or a saved `.html` file instead.

## Repo conventions

- Single root `LeetCode.csproj` compiles everything — **no** per-problem `.csproj`.
- Directory: `<number>_<PascalCaseName>/` (e.g. `3_LongestSubstringWithoutRepeatingCharacters/`).
- Every file uses file-scoped namespace `P<number>;` (e.g. `namespace P3;`) so the
  repeated `Solution` classes don't collide.
- Tests: xUnit, class `SolutionTests`, `[Theory]`/`[InlineData]` for parameterized
  cases and `[Fact]` for one-offs.

## Steps

1. **Fetch** the problem (above). Number and name come from the JSON.

2. **`Solution.cs`** — `namespace P<number>;` + the C# snippet with an **empty**
   method body. If `Solution.cs` already exists, leave it untouched and only add
   tests.

   ```csharp
   namespace P3;

   public class Solution
   {
       public int LengthOfLongestSubstring(string s)
       {

       }
   }
   ```

3. **`DESCRIPTION.md`** — convert the GraphQL `content` HTML to clean Markdown:
   `# <number>. <title>` heading, the problem prose, each `## Example N:` with its
   input/output/explanation in a fenced block, and a `## Constraints` list. Match
   the style of `2_AddTwoNumbers/DESCRIPTION.md`.

4. **`SolutionTests.cs`** (`namespace P<number>;`, `using Xunit;`):
   - One `[Theory]` covering every worked **Example**, each case commented with
     the stated answer.
   - Extra cases from the **Constraints** (empty/single/all-same input, allowed
     character classes) and classic edge cases for the problem type.
   - Group related cases into named `[Theory]`/`[Fact]` methods; comment the
     constraint each group covers.

5. **Verify expected values without touching `Solution.cs`:**
   - If a real solution already exists, run the tests — a green run confirms both.
   - If only the empty stub exists, compute the expected answers with a throwaway
     script in the scratchpad (Python, `dotnet script`, …). The xUnit tests will
     fail against the empty stub — that's fine; the independent check is the proof.

6. **`README.md`** — add a Solutions-table row linking the problem and its `Solution.cs`.

7. **Report** the run command: `dotnet test --filter "FullyQualifiedName~P<number>."`

## Notes

- The trailing dot in `~P<number>.` avoids matching e.g. `P30` when running `P3`.
- Build artifacts live in the single root `bin/`/`obj/` (gitignored) — don't
  create per-problem ones.
