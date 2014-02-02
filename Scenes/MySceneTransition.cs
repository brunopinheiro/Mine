using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("MySceneManager")]
namespace Mine {
	
	public class MySceneTransition {
		
		#region initialization
		
		public MySceneTransition(MyScene scene) {
			this.scene = scene;
			this.scene.SetTransition(this);
		}
		
		#endregion
		
		#region execution
		
		public Action OnOpeningBeginning;
		public Action OnOpeningEnding;
		public Action OnClosureBeginning;
		public Action OnClosureEnding;
		
		public MyScene Scene {
			get { return this.scene; }
		}
		
		internal void Open() {
			if(this.OnOpeningBeginning != null) this.OnOpeningBeginning();
			Type transitionType = this.GetType();
			MethodInfo executeOpeningMethod = transitionType.GetMethod("ExecuteOpening");
			if(executeOpeningMethod != null) executeOpeningMethod.Invoke(this,new object[]{});
			else this.StopOpening();
		}
		
		protected virtual void StopOpening() {
			if(this.OnOpeningEnding != null) this.OnOpeningEnding();
		}
		
		internal void Close() {
			if(this.OnClosureBeginning != null) this.OnClosureBeginning();
			Type transitionType = this.GetType();
			MethodInfo executeClosureMethod = transitionType.GetMethod("ExecuteClosure");
			if(executeClosureMethod != null) executeClosureMethod.Invoke(this,new object[]{});
			else this.StopClosure();
		}
		
		protected virtual void StopClosure() {
			if(this.OnClosureEnding != null) this.OnClosureEnding();
		}
		
		private MyScene scene;
		
		#endregion
		
		
	}
	
}

