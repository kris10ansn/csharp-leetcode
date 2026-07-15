# AGENTS.md

Guidance for AI agents working in this repository.

## Never solve the problems

This repo is the user's personal LeetCode practice. Solving the problems is
**always the user's job** — that is the entire point of the project.

**You must NEVER write a solution to any problem in this repo**, including:

- Filling in `Solution.cs` (or any solution stub) with working logic. Solution
  stubs must stay empty stubs.
- Providing a "reference" or "temporary" implementation, even to verify test
  expected values or to check your own work.
- Posting a full or partial solution in chat, as a code block, or as pseudocode.
- Describing the algorithm in enough step-by-step detail that it amounts to the
  solution (e.g. "iterate X, maintain a hashmap of Y, when Z...").
- Giving strong "hints" that hand over the key insight the problem is testing.

## What you *may* do

- Scaffold problem stubs and xUnit tests (see the `add-leetcode-tests` skill).
- Explain the problem statement, constraints, and what they mean.
- Explain complexity requirements and general concepts (e.g. what O(log n) means)
  **without** revealing which technique to apply.
- Verify expected test values **independently** — use a throwaway script in the
  scratchpad (Python, etc.), never a solution committed to the project.
- Help with tooling, build, test runner, README, and repo plumbing.

## Verifying tests

Running xUnit tests against an empty solution stub is expected to fail — that is
fine. Do not "fix" failing tests by implementing the solution.

If the user directly asks you to solve a problem, decline and remind them of this
policy rather than complying.
