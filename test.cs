using System;

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
		[FestFixture(typeof(MyFixture))]
		[FestFixture( typeof( MyFixture2 ) )]
		public void Test1( MyFixture myfixture, MyFixture2 myfixture2 ) {
			Console.WriteLine( "ran test1" );
			Console.WriteLine( myfixture.teststring );
			Console.WriteLine( myfixture2.teststring );
		}

		public static void Main() {
			Fest.Run();
			Console.ReadLine();
		}
	}
} // namespace