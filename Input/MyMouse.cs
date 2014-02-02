using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

[assembly:InternalsVisibleTo("MyInput")]
namespace Mine {
	
	public class MyMouse {
	
		#region initialization
		
		internal void Initialize() {
			Console.WriteLine("\tInitializing mouse controller...");
			MyCore.Instance.OnUpdateEnding += this.ExecuteAfterUpdateRoutine;
		}
		
		#endregion
		
		#region update
		
		internal void Update() {
            if(this.currentState == null) this.lastState = Mouse.GetState();
            else this.lastState = this.currentState;
            this.currentState = Mouse.GetState();
        }

        private MouseState lastState;
        private MouseState currentState;
		
		#endregion
		
		#region input values
		
        public float GetInputValue(string inputid) {
            return this.GetInputValue(inputid,this.currentState);
        }

        public float GetLastInputValue(string inputid) {
            return this.GetInputValue(inputid,this.lastState);
        }

		private float GetInputValue(string inputid, MouseState state) {
			if(inputid == "mouse 0" || inputid == "mouse 1" || inputid == "mouse 2") {
				ButtonState xnaButtonState = this.GetXNAButtonState(inputid,state);
				if(xnaButtonState == ButtonState.Pressed) return 1;
				else return 0;
			}
		
			if(inputid == "mouse scrollwheel") return state.ScrollWheelValue;
			if(inputid == "mouse x") return state.X;
			if(inputid == "mouse y") return state.Y;
			
			throw new Exception("The input " + inputid + " was not found in the mouse controller!");
		}

        public Vector2 GetLastPosition() {
            return new Vector2(this.lastState.X,this.lastState.Y);
        }
		
		public Vector2 GetPosition() {
			return new Vector2(this.currentState.X,this.currentState.Y);
		}
		
		#endregion
		
		#region input state
		
        private ButtonState GetXNAButtonState(string inputid) {
            return this.GetXNAButtonState(inputid,this.currentState);
        }

		private ButtonState GetXNAButtonState(string inputid, MouseState state) {
			if(inputid == "mouse 0") return state.LeftButton;
			if(inputid == "mouse 1") return state.RightButton;
			if(inputid == "mouse 2") return state.MiddleButton;
			return ButtonState.Released;
		}
		
		#endregion
		
		#region avaiable inputs
		
		public static bool ContainsInput(string inputid) {
			return _avaiableInputs.Contains(inputid);
		}
		
		private static List<string> _avaiableInputs = new List<string>() 
		{
			"mouse 0",
			"mouse 1",
			"mouse 2",
			"mouse scrollwheel",
			"mouse x",
			"mouse y"
		};
		
		#endregion
		
	}
	
}

