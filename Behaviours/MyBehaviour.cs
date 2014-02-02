using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("MyActor")]
namespace Mine {
	
    //TODO: Create an enabled propertie, and make the MyCore check this propertie

	public abstract class MyBehaviour {
		
		#region actor
		
		public MyActor Actor {
			get { return this.actor; }
			internal set { this.actor = value; }
		}
		
		public MyTransform Transform {
			get { return this.Actor.Transform; }
		}
		
		private MyActor actor;
		
		#endregion
		
		#region activation
		
		internal void Activate() {
			Type type = this.GetType();
			MethodInfo startMethod = type.GetMethod("Start");
			if(startMethod != null) startMethod.Invoke(this,new object[]{});
		}
		
		#endregion
		
	}
	
}

