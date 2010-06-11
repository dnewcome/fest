/**
* Fest is a simple .NET testing tool and is free
*	software provided under the MIT license.
*
*	See LICENSE file for full text of the license.
*	Copyright 2010 Dan Newcome.
*/

using System;
using System.Reflection;

[assembly: AssemblyVersion( "0.2.0.0" )]
[assembly: AssemblyFileVersion( "0.2.0.0" )]
namespace Djn.Testing
{
	public class Fest 
	{
		/**
		* Run searches the loaded assembly for methods marked with [FestTest]
		*	and executes them with any specified [FestFixture] attributes.
		*/
		public static void Run() {
			int testcount = 0, failcount = 0;
			Assembly executingAssembly = Assembly.GetCallingAssembly();
			Type[] types = executingAssembly.GetTypes();
			foreach( Type type in types ) {
				MethodInfo[] methods = type.GetMethods();
				foreach( MethodInfo method in methods ) {
					object[] attributes = method.GetCustomAttributes( typeof( FestTest ), true );
					if( attributes.Length > 0 ) {
						try {
							object[] fixtures = method.GetCustomAttributes( typeof( FestFixture ), true );
							object[] args;
							if( fixtures.Length > 0 ) {
								args = new object[ fixtures.Length ];
								for( int i=0; i < fixtures.Length; i++ ) {
									// NOTE: we reverse the args. This is a little suspect, will have to read up more
									// on whether the order of attributes is always the order that they are declared
									args[ fixtures.Length - 1 - i ] = 
										Activator.CreateInstance( 
											( ( FestFixture )fixtures[i] ).fixtureType 
										);
								}
							}
							else {
								args = new object[]{};
							}
							testcount++;
							method.Invoke( Activator.CreateInstance( type ), args );
						}
						catch( Exception e ) {
							failcount++;
							Console.WriteLine( e.InnerException.ToString() );
						}
					}
				}
			}
			Console.WriteLine( "Total tests: " + testcount + ", Failures: " + failcount );
		}
		
		public static void AssertEqual<T>( T expected, T actual ) {
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
				throw new Exception( "Assertion failed" );
			}
		}

		public static void AssertFalse( bool condition, string message ) {
			AssertTrue( !condition, message );
		}

		public static void AssertTrue( bool condition, string message ) {
			if( condition == false ) {
				string typename = new System.Diagnostics.StackFrame( 1 ).GetMethod().DeclaringType.Name;
				string methodname = new System.Diagnostics.StackFrame( 1 ).GetMethod().Name;
				string assertionname = new System.Diagnostics.StackFrame( 0 ).GetMethod().Name;

				Console.WriteLine();
				Console.WriteLine( "Failure " + typename + "." + methodname + " : " + assertionname );
				Console.WriteLine( message );
				throw new Exception( "Assertion failed" );
			}
		}
	} // class
	
	[AttributeUsage( AttributeTargets.Method, AllowMultiple = true )]
	public class FestFixture : Attribute {
		public Type fixtureType;
		public FestFixture( Type in_fixtureType ) {
			fixtureType = in_fixtureType;
		}
	}

	[AttributeUsage( AttributeTargets.Method, AllowMultiple = false )]
	public class FestTest : Attribute {}
	
} // namespace