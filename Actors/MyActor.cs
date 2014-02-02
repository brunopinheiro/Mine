using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("MyScene")]
namespace Mine {
	
	public class MyActor {
		
		#region initialization
		
		public MyActor() {
			this.name = "Actor_" + _actorCount;
			this.Initialize();
		}
		
		public MyActor(string name) {
			this.name = name;
			this.Initialize();
		}
		
		private void Initialize() {
			_actorCount++;
			this.AddBehaviour<MyTransform>();
		}
		
		public string name;
		private static int _actorCount = 0;
		
		#endregion
		
		#region behaviours
		
		public void AddBehaviour<T>() where T : MyBehaviour {
			Type behaviourType = typeof(T);
			ConstructorInfo behaviourConstructor = behaviourType.GetConstructor(new Type[]{});
			MyBehaviour newBehaviour = (MyBehaviour)behaviourConstructor.Invoke(new object[]{});
			this.AddBehaviour(newBehaviour);
		}
		
		public void AddBehaviour(MyBehaviour behaviour) {
			if((behaviour is MyTransform) && (this.behaviours.Count > 0)) {
				Console.WriteLine("You can't insert another transform behaviour");
				return;
			}
			
			if(this.ContainsBehaviourOfType(behaviour.GetType()))
				Console.WriteLine("WARNING: The actor already contains a behaviour if the same type!");
			
			behaviour.Actor = this;
			if(this.activated) behaviour.Activate();
			this.behaviours.Add(behaviour);
		}
		
		private bool ContainsBehaviourOfType(Type behaviourType) {
			bool containsType = this.behaviours.Exists(behaviour => {
				return behaviour.GetType().Name == behaviourType.Name;
			});
			return containsType;
		}
		
		public void DestroyBehaviour(MyBehaviour behaviour) {
			this.behaviours.Remove(behaviour);
		}
		
		public void DestroyAllBehaviours<T>() where T : MyBehaviour {
			this.behaviours.RemoveAll(behaviour => {
				return (behaviour is T);
			});
		}
		
		public void DestroyBehaviour<T>() where T : MyBehaviour {
			int firstIndex = this.behaviours.FindIndex(behaviour => {
				return (behaviour is T);
			});
			this.behaviours.RemoveAt(firstIndex);
		}
		
		public T GetBehaviour<T>() where T : MyBehaviour {
			return this.behaviours.Find(behaviour => {
				return (behaviour is T);
			}) as T;
		}

        public T[] GetAllBehaviours<T>() where T : MyBehaviour {
            List<T> foundBehaviours = new List<T>();
            foreach(MyBehaviour behaviour in this.behaviours)
                if(behaviour is T) foundBehaviours.Add((behaviour as T));
            return foundBehaviours.ToArray();
        }
		
		public T GetBehaviourInChildren<T>() where T : MyBehaviour {
			MyTransform[] children = this.Transform.Children;
			foreach(MyTransform child in children) {
				T behaviour = child.Actor.GetBehaviour<T>();
                if(behaviour != null) return behaviour; 
			}
			return null;
		}
		
		public T[] GetAllBehavioursInChildren<T>() where T : MyBehaviour {
			List<T> foundBehaviours = new List<T>();
			MyTransform[] children = this.Transform.Children;
			foreach(MyTransform child in children) {
				T[] behaviours = child.Actor.GetAllBehaviours<T>();
				foreach(T behaviour in behaviours) foundBehaviours.Add(behaviour);
                T[] childrenBehaviours = child.Actor.GetAllBehavioursInChildren<T>();
                foreach(T childBehaviour in childrenBehaviours) foundBehaviours.Add(childBehaviour);
			}
			return foundBehaviours.ToArray();
		}
		
		private List<MyBehaviour> behaviours = new List<MyBehaviour>();
		
		#endregion
		
		#region transform
		
		public MyTransform Transform {
			get { 
				if(this.transform == null) this.transform = this.GetBehaviour<MyTransform>();
				return this.transform;
			}
		}
		
		public MyTransform[] Children {
			get { return this.Transform.Children; }
		}
		
		private MyTransform transform;
		
		#endregion
		
		#region renderer
		
		public MyRenderer Renderer {
			get {
				if(this.renderer == null) this.renderer = this.GetBehaviour<MyRenderer>();
				return this.renderer;
			}
		}
		
		private MyRenderer renderer;
		
		#endregion

        #region collider

        public MyCollider Collider {
            get { 
                if(this.collider == null) this.collider = this.GetBehaviour<MyCollider>(); 
                return this.collider;
            }
        }

        private MyCollider collider;

        #endregion

        #region tag

        public MyTag tag;

        #endregion

        #region activation

        internal void Activate() {
			if(this.activated) {
				Console.WriteLine("The actor " + this.name + " is being activated two or more times");
				return;
			}
			foreach(MyBehaviour behaviour in this.behaviours) behaviour.Activate();	
			this.activated = true;
		}
		
		private bool activated = false;
		
		#endregion
		
		#region destruction
		
		internal void DestroyItself() {
			this.Dispose();
			foreach(MyBehaviour behaviour in this.behaviours)
				this.DisposeBehaviour(behaviour);
		}
		
		private void DisposeBehaviour(MyBehaviour behaviour) {
			Type behaviourType = behaviour.GetType();
			MethodInfo disposeMethod = behaviourType.GetMethod("Dispose");
			disposeMethod.Invoke(behaviour,new object[]{});	
		}		
		
		public virtual void Dispose() {}
		
		#endregion
		
	}
	
}

