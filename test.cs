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
			Fest.Equal<string>( myfixture.teststring, "teststring");
			Fest.Equal<string>( myfixture2.teststring, "teststring2" );
			Fest.Equal<int>( 2, 1 + 1 );
			Fest.Equal<object>( new object(), new object() );
			object obj = new object();
			object obj2 = obj;
			Fest.Equal<object>( obj, obj2 );
			Fest.Equal<int>( 2, 1 + 2 );
		}

		[FestTest]
		public void Test2( MyFixture myfixture, MyFixture2 myfixture2 ) {
			Fest.Equal<int>( 1, 1 );
		}
		
		public static void Main() {
			Fest.Run();
			Console.ReadLine();
		}
	}
} // namespace