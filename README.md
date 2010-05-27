# About
Fest is a simple testing tool. Fest only has two configuration attributes and does
not use a separate test runner. Fest runs any method in the assembly that is marked
as a test - there are no special test classes. Test data can be supplied to
the tests via fixtures, which are separate from the tests and can be shared
between many different tests.