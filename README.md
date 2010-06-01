# About
Fest is a simple embedded testing tool for the .NET framework.
 It is intended to produce executable
test assemblies. Fest only has two configuration attributes and does
not use a separate test runner. Fest runs any method in the assembly that is marked
as a test - there are no special test classes. Test data can be supplied to
the tests via fixtures, which are separate from the tests and can be shared
between many different tests.

# Usage
Any method in the assembly may be denoted as a test using the FestTest attribute.
	[FestTest]
	public void Test1() {
		string testString = "test";
		Fest.Equal<string>( testString, "test");
	}

Test fixtures may be declared separately from the tests themselves.
	public class MyFixture { 
		public string teststring = "teststring";
	}

Fixtures are applied to tests declaratively using FestFixture attributes.
	[FestTest]
	[FestFixture( typeof( MyFixture ) )]
	public void Test1( MyFixture myfixture ) {
		Fest.Equal<string>( myfixture.teststring, "teststring");
	}

All tests must reside in the current assembly and are run using:
	Fest.Run()

# Future Work
Fest supplies almost none of its own assertions, although the only real requirement
is that the assert method throw an exception, so assert methods from other libraries
would likely work, however they may not yield useful information at stdout. One of 
the main goals of Fest was to provide a little structure while allowing quick
executable test programs to be created. If more features/complexity is needed, 
it probably makes sense to go to something like NUnit.