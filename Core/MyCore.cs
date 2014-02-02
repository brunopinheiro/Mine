using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mine {
	
	public class MyCore {
		
		#region singleton
		
		private MyCore(Game game) {
            this.game = game;
        }
		
		public static MyCore Instance {
			get { return _instance; }
		}

        public static void CreateCore(Game game) {
            _instance = new MyCore(game);
            MyCanvas.CreateCanvas();
        }
		
		private static MyCore _instance;
		
		#endregion
		
		#region initialization
		
		public void Initialize() {
            this.InitializeInput();
			this.InitializeCanvas();
		}
		
		#endregion
		
		#region canvas
		
		private void InitializeCanvas() {
			MyCanvas.Instance.Initialize();
		}
		
		#endregion

        #region update

        public Action OnUpdateBeginning;
		public Action OnUpdateEnding;
		
		public void Update(GameTime gameTime) {
			this.BeginUpdate();
            this.gameTime = gameTime;
            MyInput.Instance.Update();
            MyDirector.Instance.Update(gameTime);
			this.UpdateBehaviours();
			this.EndUpdate();
		}
		
		private void BeginUpdate() {
			//this.updating = true;
            if(this.OnUpdateBeginning != null) this.OnUpdateBeginning();
		}
		
		private void UpdateBehaviours() {
            MyBehaviour[] allBehaviours = this.GetAllBehaviours();
            foreach(MyBehaviour behaviour in allBehaviours) {
                Type behaviourType = behaviour.GetType();
                MethodInfo updateMethodInfo = behaviourType.GetMethod("Update");
                if (updateMethodInfo == null) continue;
                updateMethodInfo.Invoke(behaviour, new object[] { });
			}
		}

        private MyBehaviour[] GetAllBehaviours() {
            if (MyDirector.Instance.CurrentScene == null) return new MyBehaviour[] { };
            MyScene currentScene = MyDirector.Instance.CurrentScene;
            MyBehaviour[] allBehaviours = currentScene.Container.GetAllBehavioursInChildren<MyBehaviour>();
            return allBehaviours;
        }
		
		private void EndUpdate() {
			//this.updating = false;
			if(this.OnUpdateEnding != null) this.OnUpdateEnding();
		}

        public GameTime GameTime {
            get { return this.gameTime; }
        }

        private GameTime gameTime;
		
		#endregion
		
		#region game
		
		public Game Game {
			get { return this.game; }
		}
		
		private Game game;
		
		#endregion
		
		#region input
		
		private void InitializeInput() {
			MyInput.Instance.Initialize();
		}
		
		#endregion
		
		#region persistent content
		
		public ContentManager PersistentContent {
			get { return this.game.Content; }
		}
		
		#endregion
		
	}
	
}

