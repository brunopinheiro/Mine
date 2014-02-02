using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

[assembly:InternalsVisibleTo("MyCore")]
namespace Mine {

	public class MyInput {
	
		#region singleton
		
		private MyInput() { }
		
		public static MyInput Instance {
			get { return _instance; }
		}
		
		private static MyInput _instance = new MyInput();
		
		#endregion
		
		#region initialization
		
		internal void Initialize() {
			Console.WriteLine("Initializing input controllers: ");
			this.mouse.Initialize();
			this.keyboard.Initialize();
			foreach(KeyValuePair<PlayerIndex,MyGamePad> gamePadKV in this.gamePads) 
				this.InitializeGamePad(gamePadKV.Value);
		}
		
		private void InitializeGamePad(MyGamePad gamePad) {
			gamePad.Initialize();
			gamePad.OnConnected += this.HandleGamePadConnection;
			gamePad.OnDisconnected += this.HandleGamePadDisconnection;
		}
		
		#endregion

        #region update

        internal void Update() {
            this.mouse.Update();
            this.keyboard.Update();
            foreach(KeyValuePair<PlayerIndex,MyGamePad> gamepadKV in this.gamePads)
                gamepadKV.Value.Update();
        }

        #endregion

        #region input values

        /// <summary>
		/// The default PlayerIndex for GamePads is One!
		/// </summary>
		public float GetInputValue(string inputid) {
			return this.GetInputValue(inputid,PlayerIndex.One);
		}
		
		public float GetInputValue(string inputid, PlayerIndex playerIndex) {
			if(MyKeyboard.ContainsInput(inputid)) return this.keyboard.GetInputValue(inputid);
			if(MyMouse.ContainsInput(inputid)) return this.mouse.GetInputValue(inputid);
			if(MyGamePad.ContainsInput(inputid)) {
				if(this.gamePads[playerIndex].IsConnected()) {
					Console.WriteLine("The GamePad " + playerIndex + " is not connected");
					return 0;
				}
				return this.gamePads[playerIndex].GetInputValue(inputid);
			}
			throw new Exception("The input " + inputid + " was not found in any of the input controllers");
		}

        public float GetLastInputValue(string inputid) {
            return this.GetLastInputValue(inputid,PlayerIndex.One);
        }

        public float GetLastInputValue(string inputid, PlayerIndex playerIndex) {
			if(MyKeyboard.ContainsInput(inputid)) return this.keyboard.GetLastInputValue(inputid);
			if(MyMouse.ContainsInput(inputid)) return this.mouse.GetLastInputValue(inputid);
			if(MyGamePad.ContainsInput(inputid)) {
				if(this.gamePads[playerIndex].IsConnected()) {
					Console.WriteLine("The GamePad " + playerIndex + " is not connected");
					return 0;
				}
				return this.gamePads[playerIndex].GetLastInputValue(inputid);
			}
			throw new Exception("The input " + inputid + " was not found in any of the input controllers");
		}
		
		#endregion
		
		#region input controllers

        public bool mouseEnabled = true;
        public bool keyboardEnabled = true;
        public bool gamepadsEnabled = true;
		
		public Action<PlayerIndex> OnGamePadConnected;
		public Action<PlayerIndex> OnGamePadDisconnected;
		
		public Vector2 GetMousePosition() {
			return this.mouse.GetPosition();
		}

        public Vector2 GetLastMousePosition() {
            return this.mouse.GetLastPosition();
        }
		
		public Vector2 GetGamePadThumbstickPosition(PlayerIndex playerIndex, MyGamePad.Sides side) {
			MyGamePad gamePad = this.gamePads[playerIndex];
			return gamePad.GetThumbstickPosition(side);
		}

        public Vector2 GetLastGamePadThumbstickPosition(PlayerIndex playerIndex, MyGamePad.Sides side) {
			MyGamePad gamePad = this.gamePads[playerIndex];
			return gamePad.GetLastThumbstickPosition(side);
		}
		
		private void HandleGamePadConnection(PlayerIndex playerIndex) {
			if(this.OnGamePadConnected != null)
				this.OnGamePadConnected(playerIndex);
		}
		
		private void HandleGamePadDisconnection(PlayerIndex playerIndex) {
			if(this.OnGamePadDisconnected != null)
				this.OnGamePadDisconnected(playerIndex);
		}
		
		private MyMouse mouse = new MyMouse();
		private MyKeyboard keyboard = new MyKeyboard();
		private Dictionary<PlayerIndex,MyGamePad> gamePads = new Dictionary<PlayerIndex,MyGamePad>()
		{
			{PlayerIndex.One, new MyGamePad(PlayerIndex.One)},
			{PlayerIndex.Two, new MyGamePad(PlayerIndex.Two)},
			{PlayerIndex.Three, new MyGamePad(PlayerIndex.Three)},
			{PlayerIndex.Four, new MyGamePad(PlayerIndex.Four)}
		};
		
		#endregion
		
	}
	
}

