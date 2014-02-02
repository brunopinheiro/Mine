using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

[assembly:InternalsVisibleTo("MyInput")]
namespace Mine {
	
	public class MyGamePad {
		
		#region initialization
		
		public MyGamePad(PlayerIndex playerIndex) {
			this.playerIndex = playerIndex;
		}
		
		internal void Initialize() {
			Console.WriteLine("\tInitializing GamePad " + this.playerIndex + " controller");
			MyCore.Instance.OnUpdateEnding += this.ExecuteAfterUpdateRoutine;
		}
		
		#endregion
		
		#region side
		
		public enum Sides {
			Left,
			Right
		}
		
		#endregion
		
		#region update
		
        internal void Update() {
            if(this.currentState == null) this.lastState = GamePad.GetState(this.playerIndex);
            else this.lastState = this.currentState;
            this.currentState = GamePad.GetState(this.playerIndex);
        }

		private void ExecuteAfterUpdateRoutine() {
			bool connectedNow = this.IsConnected();
			if(connectedNow != this.connected) {
				if(connectedNow && this.OnConnected != null) this.OnConnected(this.playerIndex);
				else if(!connectedNow && this.OnDisconnected != null) this.OnDisconnected(this.playerIndex);
			}
			this.connected = connectedNow;
		}

        private GamePadState currentState;
        private GamePadState lastState;
		
		#endregion
		
		#region controller connection
		
		public Action<PlayerIndex> OnConnected;
		public Action<PlayerIndex> OnDisconnected;
		
		public bool IsConnected() {
			GamePadState xnaGamePadState = GamePad.GetState(this.playerIndex);
			return xnaGamePadState.IsConnected;
		}
		
		private bool connected = false;
		private PlayerIndex playerIndex;
		
		#endregion
		
		#region input value
		
        public Vector2 GetThumbstickPosition(Sides side) {
            return this.GetThumbstickPosition(side,this.currentState);
        }

        public Vector2 GetLastThumbstickPosition(Sides side) {
            return this.GetThumbstickPosition(side,this.lastState);
        }

		private Vector2 GetThumbstickPosition(Sides side, GamePadState state) {
			Vector2 thumbstickPosition = Vector2.Zero;
			if(side == Sides.Left) {
				thumbstickPosition.X = state.ThumbSticks.Left.X;
				thumbstickPosition.Y = state.ThumbSticks.Left.Y;
			} else {
				thumbstickPosition.X = state.ThumbSticks.Right.X;
				thumbstickPosition.Y = state.ThumbSticks.Right.Y;
			}
			return thumbstickPosition;
		}
		
        public float GetInputValue(string inputid) {
            return this.GetInputValue(inputid,this.currentState);
        }

        public float GetLastInputValue(string inputid) {
            return this.GetInputValue(inputid,this.lastState);
        }

		private float GetInputValue(string inputid, GamePadState state) {
			if(inputid == "gamepad lthumbstick x") return state.ThumbSticks.Left.X;
			if(inputid == "gamepad lthumbstick y") return state.ThumbSticks.Left.Y;
			if(inputid == "gamepad rthumbstick x") return state.ThumbSticks.Right.X;
			if(inputid == "gamepad rthumbstick y") return state.ThumbSticks.Right.Y;
			
			Buttons xnaButton = MyGamePad.GetXNAButton(inputid);
			if(xnaButton == Buttons.LeftTrigger) return state.Triggers.Left;
			if(xnaButton == Buttons.RightTrigger) return state.Triggers.Right;
			
			if(state.IsButtonDown(xnaButton)) return 1;
			else return 0;
		}
		
		#endregion
		
		#region input mapping
		
		public static bool ContainsInput(string inputid) {
			return _inputMap.ContainsKey(inputid);
		}
		
		protected static Buttons GetXNAButton(string inputid) {
			return _inputMap[inputid];
		}
		
		private static Dictionary<string,Buttons> _inputMap = new Dictionary<string,Buttons>()
		{
			{ "gamepad a", Buttons.A },
			{ "gamepad x", Buttons.X },
			{ "gamepad y", Buttons.Y },
			{ "gamepad b", Buttons.B },
			{ "gamepad back", Buttons.Back },
			{ "gamepad start", Buttons.Start },
			{ "gamepad bigbutton", Buttons.BigButton },
			{ "gamepad dleft", Buttons.DPadLeft },
			{ "gamepad dup", Buttons.DPadUp },
			{ "gamepad ddown", Buttons.DPadDown },
			{ "gamepad dright", Buttons.DPadRight },
			{ "gamepad ls", Buttons.LeftShoulder },
			{ "gamepad rs", Buttons.RightShoulder },
			{ "gamepad l3", Buttons.LeftStick },
			{ "gamepad r3", Buttons.RightStick },
			{ "gamepad lt", Buttons.LeftTrigger },
			{ "gamepad rt", Buttons.RightTrigger }
		};
		
		#endregion
		
	}
	
}

