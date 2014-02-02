using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

[assembly:InternalsVisibleTo("MySceneManager")]
[assembly:InternalsVisibleTo("MyCanvas")]
namespace Mine {
	
	public abstract class MyScene {
	
		#region initialization
		
		public MyScene() {
			this.name = "Scene_" + _sceneCount;
			this.Initialize();
		}
		
		public MyScene(string name) {
			this.name = name;
			this.Initialize();
		}
		
		public string name;
		
		private void Initialize() {
			_sceneCount++;
			this.InitializeContentManager();
		}
		
		private static int _sceneCount = 0;
		
		#endregion
		
		#region update

        internal bool Enabled {
            get { return this.enabled; }
        }

		internal void Update(GameTime gameTime) {
			if(this.IsLoadingTaskCompleted()) this.EndLoadingTask();
		}

        private bool enabled = false;

		#endregion
		
		#region opening / closure
		
		public Action<MyScene> OnLoadBeginning;
		public Action<MyScene> OnLoadEnding;
		public Action<MyScene> OnUnloadBeginning;
		public Action<MyScene> OnUnloadEnding;
		
		public void SetTransition(MySceneTransition transition) {
			this.transition = transition;
			this.transition.OnClosureEnding += this.PrepareToUnload;
		}
		
		internal void Open() {
			if(this.transition != null) this.transition.Open();
		}
		
		public virtual void Close() {
			if(this.transition != null) this.transition.Close();
			else this.PrepareToUnload();
		}
		
		protected virtual void Unload() {
			this.content.Unload();
			this.loaded = false;
			if(this.OnUnloadEnding != null) this.OnUnloadEnding(this);
			Console.WriteLine("Scene " + this.name + " is unloaded");
		}
		
		private void WakeUp() {
			MyTransform[] sceneTransforms = this.container.Transform.Children;
			foreach(MyTransform sceneTransform in sceneTransforms)
				sceneTransform.Actor.Activate();
			if(this.transition != null) this.transition.Open();
		}
		
		private MySceneTransition transition;
		
		#endregion
		
		#region loading
		
		private bool IsLoadingTaskCompleted() {
			TaskStatus loadingStatus = this.loadingTask.Status;
			return (loadingStatus == TaskStatus.Canceled || loadingStatus == TaskStatus.RanToCompletion);
		}
		
		internal void PrepareToLoad() {
			this.enabled = true;
            this.CreateDefaultCamera();
			this.loadingTask = new Task(this.Load);
			this.loadingTask.Start(); 
			Console.WriteLine("Scene " + this.name + " will start load");
		}
		
		private void PrepareToUnload() {
			Console.WriteLine("Scene " + this.name + " will start unload");
			this.Unload();
		}
		
		private void EndLoadingTask() {
			this.enabled = false;
			this.loaded = true;
			if(this.OnLoadEnding != null) this.OnLoadEnding(this);
			Console.WriteLine("Scene " + this.name + " is loaded");
		}
		
		protected virtual void Load() { }
		
		private Task loadingTask;
		private bool loaded = false;
		
		#endregion
		
		#region containers
		
		public void AddActor(MyActor actor) {
			if(actor.Equals(this.container)) {
				Console.WriteLine("WARNING: You can't add the container itself on the scene!");
				return;
			}
			if(this.loaded) actor.Activate();
			actor.Transform.Parent = this.container.Transform;
		}
		
		public void RemoveActor(MyActor actor) {
			if(actor.Equals(this.container)) {
				Console.WriteLine("WARNING: You can't remove the scene's container!");
				return;
			}
			actor.Transform.Parent = null;
			actor.DestroyItself();
		}

        private void CreateDefaultCamera() {
            MyActor camera = new MyActor("Default Camera");
            camera.AddBehaviour<MyCamera>();
            this.AddActor(camera);
            this.defaultCamera = camera.GetBehaviour<MyCamera>();
            this.defaultCamera.viewport = MyCanvas.Instance.DefaultViewport;
        }
		
		internal MyActor Container {
			get { return this.container; }
		}

        public MyCamera DefaultCamera {
            get { return this.defaultCamera; }
        }
		
		private MyActor container = new MyActor();
        private MyCamera defaultCamera;
		
		#endregion
		
		#region content
		
		public ContentManager Content {
			get { return this.content; }
		}
		
		private void InitializeContentManager() {
			Game game = MyCore.Instance.Game;
			ContentManager gameContent = MyCore.Instance.PersistentContent;
			this.content = new ContentManager(game.Services,gameContent.RootDirectory);
		}
		
		private ContentManager content;
		
		#endregion		
		
	}
}

