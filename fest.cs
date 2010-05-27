using System;
using System.Reflection;

namespace Djn.Testing
{
	[AttributeUsage( AttributeTargets.Method, AllowMultiple = true )]
	public class FestFixture : Attribute {
		public Type fixtureType;
		public FestFixture( Type in_fixtureType ) {
			fixtureType = in_fixtureType;
		}
	}

	[AttributeUsage( AttributeTargets.Method, AllowMultiple = false )]
	public class FestTest : Attribute {}

	public class Fest 
	{
		public static void Run() {
			int testcount = 0, failcount = 0;
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Type[] types = executingAssembly.GetTypes();
			foreach( Type type in types ) {
				MethodInfo[] methods = type.GetMethods();
				foreach( MethodInfo method in methods ) {
					object[] attributes = method.GetCustomAttributes( typeof( FestTest ), true );
					if( attributes.Length > 0 ) {
						object[] fixtures = method.GetCustomAttributes( typeof( FestFixture ), true );
						if( fixtures.Length > 0 ) {
							object[] args = new object[ fixtures.Length ];
							for( int i=0; i < fixtures.Length; i++ ) {
								// NOTE: we reverse the args. This is a little suspect, will have to read up more
								// on whether the order of attributes is always the order that they are declared
								args[ fixtures.Length - 1 - i ] = Activator.CreateInstance( ( ( FestFixture )fixtures[i] ).fixtureType );
							}
							try {
								testcount++;
								method.Invoke( Activator.CreateInstance( type ), args );
							}
							catch {
								failcount++;
							}
						}
						else {
							try {
								testcount++;
								method.Invoke( Activator.CreateInstance( type ), new object[] { } );
							}
							catch {
								failcount++;
							}
						}
					}
				}
			}
			Console.WriteLine( "Total tests: " + testcount + ", Failures: " + failcount );
		}
		
		public static void Equal<T>( T expected, T actual ) {
			if( expected.Equals( actual ) ) {
				Console.Write( "." );
			}
			else {
				string typename = new System.Diagnostics.StackFrame( 1 ).GetMethod().DeclaringType.Name;
				string methodname = new System.Diagnostics.StackFrame( 1 ).GetMethod().Name;

				Console.WriteLine();
				Console.WriteLine( typename + "." + methodname + " : Equal<T>() Failure" );
				Console.WriteLine( "Expected: " + expected.ToString() );
				Console.WriteLine( "Actual: " + actual.ToString() );
				throw new Exception();
			}
		}
		
		public static void Assert( bool condition, string message ) {
			if( condition == false ) {
				Console.WriteLine( message );
			}
		}

	} // class
} // namespace