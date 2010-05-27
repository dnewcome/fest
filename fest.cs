using System;
using System.Reflection;

namespace Djn 
{
	public class TestFixture
	{
		[FestTest]
		public void Test1() {
			Console.WriteLine( "ran test1" );
		}

		public static void Main() {
			Fest.Run();
			Console.ReadLine();
		}
	}

	public class FestTest : Attribute
	{ 
		
	}

	public class Fest 
	{
		public static void Run() {
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Type[] types = executingAssembly.GetTypes();
			foreach( Type type in types ) {
				MethodInfo[] methods = type.GetMethods();
				foreach( MethodInfo method in methods ) {
					object[] attributes = method.GetCustomAttributes( typeof( FestTest ), true );
					if( attributes.Length > 0 ) {
						method.Invoke( Activator.CreateInstance( type ), new object[] { } );
					}
				}
			}
			

			/*
			MemberInfo memberInfo = this.GetType(); 
			object[] attributes = memberInfo.GetCustomAttributes(
				typeof( FestTest ), true
			);
			foreach( attribute in attributes ) {
				attribute.
			}
			*/
		}
	}
} // namespace