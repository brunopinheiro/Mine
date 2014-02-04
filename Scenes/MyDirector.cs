using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

[assembly:InternalsVisibleTo("MyCore")]
namespace Mine {
	
	public class MyDirector {
	
		#region singleton
		
		private MyDirector() { }
		
		public static MyDirector Instance {
			get { return _instance; }
		}
		
		private static MyDirector _instance = new MyDirector();
		
		#endregion
		
		#region scenes
		
		public MyScene CurrentScene {
			get { return this.currentScene; }
		}
		
		public void OpenScene(MyScene scene) {
			this.OpenScene(scene,null);
		}
		
		public void OpenScene(MyScene scene,MyScene loadingScene) {
			this.nextScene = scene;
			this.loadingScene = loadingScene;
			if(this.currentScene != null) this.CloseCurrentScene();
			else this.LoadLoadingScene(this.nextScene);
		}
		
		private void CloseCurrentScene() {
			this.currentScene.OnUnloadEnding += this.LoadLoadingScene;
			this.currentScene.Close();
		}
		
		private void LoadLoadingScene(MyScene nextScene) {
            if(this.loadingScene != null) {
				this.currentScene = this.loadingScene;
				this.loadingScene.OnLoadEnding += this.LoadNextScene;
				this.loadingScene.PrepareToLoad();
			}
			else {
				this.currentScene = this.nextScene;
				this.LoadNextScene(this.nextScene);
			}
		}
		
		private void LoadNextScene(MyScene loadedScene) {
			if(this.loadingScene != null) this.loadingScene.Open();
			this.nextScene.OnLoadEnding += this.HideLoadingScene;
			this.nextScene.PrepareToLoad();
		}
		
		private void HideLoadingScene(MyScene loadedScene) {
			if(this.loadingScene != null) {
				this.loadingScene.OnUnloadEnding += this.ShowNextScene;
				this.loadingScene.Close();
			}
			else this.ShowNextScene(this.nextScene);
		}
		
		private void ShowNextScene(MyScene unloadedScene) {
			this.currentScene = this.nextScene;
			this.nextScene = null;
			this.currentScene.Open();
		}
		
		private MyScene nextScene;
		private MyScene currentScene;
		private MyScene loadingScene;
		
		#endregion

        #region update

        internal void Update(GameTime gameTime) {
            if (this.CurrentScene != null && this.CurrentScene.Enabled) this.CurrentScene.Update(gameTime);
        }

        #endregion
    }
	
}

