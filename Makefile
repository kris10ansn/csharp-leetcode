# Test runner.
#
#   make test                                  # run every problem's tests
#   make test P=3                              # run only problem 3 (the P3 namespace)
#   make test T=HandlesDescriptionExamples     # run that test across all problems
#   make test P=3 T=HandlesDescriptionExamples # run that test within problem 3
#   make test V=normal                         # quieter output (default is detailed)
#
# V sets the console logger verbosity; detailed surfaces per-test output even
# when tests pass. The trailing dot in the P filter keeps "P3" from also
# matching "P30".
.PHONY: test
P ?=
T ?=
V ?= detailed

test:
	@filter=""; \
	if [ -n "$(P)" ]; then filter="FullyQualifiedName~P$(P)."; fi; \
	if [ -n "$(T)" ]; then \
	  if [ -n "$$filter" ]; then filter="$$filter&FullyQualifiedName~$(T)"; \
	  else filter="FullyQualifiedName~$(T)"; fi; \
	fi; \
	if [ -n "$$filter" ]; then dotnet test --filter "$$filter" --logger "console;verbosity=$(V)"; \
	else dotnet test --logger "console;verbosity=$(V)"; fi
