#!/usr/bin/env bash
# Run the test suite.
#
#   ./test.sh        # run every problem's tests
#   ./test.sh 3      # run only problem 3 (the P3 namespace)
#
# The trailing dot in the filter keeps "P3" from also matching "P30".
set -euo pipefail

cd "$(dirname "$0")"

if [ $# -eq 0 ]; then
  exec dotnet test
else
  exec dotnet test --filter "FullyQualifiedName~P$1."
fi
