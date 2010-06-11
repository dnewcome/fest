using System;
using Djn.Testing;

namespace Djn 
{
	public class MyFixture { 
		public string teststring = "teststring";
	}

	public class MyFixture2 {
		public string teststring = "teststring2";
	}

	public class Tests
	{
		[FestTest]
		[FestFixture( typeof( MyFixture ) )]
		[FestFixture( typeof( MyFixture2 ) )]
		public void Test1( MyFixture myfixture, MyFixture2 myfixture2 ) {
			Fest.AssertEqual<string>( myfixture.teststring, "teststring");
			Fest.AssertEqual<string>( myfixture2.teststring, "teststring2" );
			Fest.AssertEqual<int>( 2, 1 + 1 );
			Fest.AssertEqual<object>( new object(), new object() );
			object obj = new object();
			object obj2 = obj;
			Fest.AssertEqual<object>( obj, obj2 );
			Fest.AssertEqual<int>( 2, 1 + 2 );
		}

		[FestTest]
		public void Test2() {
			Fest.AssertEqual<int>( 1, 1 );
			Fest.AssertTrue( false, "intentionally false assertion" );
		}
		
		public static void Main() {
			Fest.Run();
			Console.ReadLine();
		}
	}
} // namespace