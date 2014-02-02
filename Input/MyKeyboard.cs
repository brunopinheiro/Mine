using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

[assembly:InternalsVisibleTo("MyInput")]
namespace Mine {

	public class MyKeyboard {
		
		#region initialization
		
		internal void Initialize() {
			Console.WriteLine("\tInitializing keyboard controller...");
			MyCore.Instance.OnUpdateEnding += this.ExecuteAfterUpdateRoutine;
		}
		
		#endregion
		
		#region update
		
		internal void Update() {
			if(this.currentState == null) this.lastState = Keyboard.GetState();
            else this.lastState = this.currentState;
            this.currentState = Keyboard.GetState();
		}

        private KeyboardState currentState;
        private KeyboardState lastState;
		
		#endregion
		
		#region input value
		
        public float GetInputValue(string inputid) {
            return this.GetInputValue(inputid,this.currentState);
        }

        public float GetLastInputValue(string inputid) {
            return this.GetInputValue(inputid,this.lastState);
        }

		private float GetInputValue(string inputid, KeyboardState state) {
			Keys xnaKey = MyKeyboard.GetXNAKey(inputid);
			if(state.IsKeyDown(xnaKey)) return 1;
			else return 0;
		}
		
		#endregion
		
		#region input mapping
		
		public static bool ContainsInput(string inputid) {
			return _inputMap.ContainsKey(inputid);
		}
		
		protected static Keys GetXNAKey(string inputid) {
			return _inputMap[inputid];
		}
		
		private static Dictionary<string,Keys> _inputMap = new Dictionary<string,Keys>() 
		{
			{ "q", Keys.Q },
			{ "w", Keys.W },
			{ "e", Keys.E },
			{ "r", Keys.R },
			{ "t", Keys.T },
			{ "y", Keys.Y },
			{ "u", Keys.U },
			{ "i", Keys.I },
			{ "o", Keys.O },
			{ "p", Keys.P },
			{ "a", Keys.A },
			{ "s", Keys.S },
			{ "d", Keys.D },
			{ "f", Keys.F },
			{ "g", Keys.G },
			{ "h", Keys.H },
			{ "j", Keys.J },
			{ "k", Keys.K },
			{ "l", Keys.L },
			{ "z", Keys.Z },
			{ "x", Keys.X },
			{ "c", Keys.C },
			{ "v", Keys.V },
			{ "b", Keys.B },
			{ "n", Keys.N },
			{ "m", Keys.M },
			{ "+", Keys.Add },
			{ "*", Keys.Multiply },
			{ "-", Keys.Subtract },
			{ "/", Keys.Divide },
			{ "back", Keys.Back },
			{ "caps lock", Keys.CapsLock },
			{ "delete", Keys.Delete },
			{ "up", Keys.Up },
			{ "left", Keys.Left },
			{ "right", Keys.Right },
			{ "down", Keys.Down },
			{ "end", Keys.End },
			{ "enter", Keys.Enter },
			{ "esc", Keys.Escape },
			{ "f1", Keys.F1 },
			{ "f2", Keys.F2 },
			{ "f3", Keys.F3 },
			{ "f4", Keys.F4 },
			{ "f5", Keys.F5 },
			{ "f6", Keys.F6 },
			{ "f7", Keys.F7 },
			{ "f8", Keys.F8 },
			{ "f9", Keys.F9 },
			{ "f10", Keys.F10 },
			{ "f11", Keys.F11 },
			{ "f12", Keys.F12 },
			{ "insert", Keys.Insert },
			{ "left alt", Keys.LeftAlt },
			{ "right alt", Keys.RightAlt },
			{ "left ctrl", Keys.LeftControl },
			{ "right crtl", Keys.RightControl },
			{ "left shift", Keys.LeftShift },
			{ "right shift", Keys.RightShift },
			{ "left windows", Keys.LeftWindows },
			{ "right windows", Keys.RightWindows },
			{ "num lock", Keys.NumLock },
			{ "num pad0", Keys.NumPad0 },
			{ "num pad1", Keys.NumPad1 },
			{ "num pad2", Keys.NumPad2 },
			{ "num pad3", Keys.NumPad3 },
			{ "num pad4", Keys.NumPad4 },
			{ "num pad5", Keys.NumPad5 },
			{ "num pad6", Keys.NumPad6 },
			{ "num pad7", Keys.NumPad7 },
			{ "num pad8", Keys.NumPad8 },
			{ "num pad9", Keys.NumPad9 },
			{ "page up", Keys.PageUp },
			{ "page down", Keys.PageDown },
			{ "print screen", Keys.PrintScreen },
			{ "scroll", Keys.Scroll },
			{ "separator", Keys.Separator },
			{ "space", Keys.Space },
			{ "tab", Keys.Tab },
			{ "volume down", Keys.VolumeDown },
			{ "volume mute", Keys.VolumeMute },
			{ "volume up", Keys.VolumeUp }
		};
		
		#endregion
	}
	
}
