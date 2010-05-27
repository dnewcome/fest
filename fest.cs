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
							method.Invoke( Activator.CreateInstance( type ), args );
						}
						else {
							method.Invoke( Activator.CreateInstance( type ), new object[] { } );
						}
					}
				}
			}
		}
	}
} // namespace