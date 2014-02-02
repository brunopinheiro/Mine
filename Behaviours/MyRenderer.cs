using System;
using Microsoft.Xna.Framework;

namespace Mine {
	
	public abstract class MyRenderer : MyBehaviour {
		
		//TODO: Create the Text Renderer
		
		#region drawing properties
		
		public Color TintColor {
			get { return this.GetColor(); }
			set { this.SetColor(value); }
		}
		
		private Color GetColor() {
			Color parentColor = this.GetParentColor();
			Color tintColor = Color.White;
			tintColor.R = Convert.ToByte(Convert.ToInt32(this.localColor.R) * (Convert.ToInt32(parentColor.R)/255));
			tintColor.G = Convert.ToByte(Convert.ToInt32(this.localColor.G) * (Convert.ToInt32(parentColor.G)/255));
			tintColor.B = Convert.ToByte(Convert.ToInt32(this.localColor.B) * (Convert.ToInt32(parentColor.B)/255));
			tintColor.A = Convert.ToByte(Convert.ToInt32(this.localColor.A) * (Convert.ToInt32(parentColor.A)/255));
			return tintColor;
		}
		
		private void SetColor(Color color) {
			Color parentColor = this.GetParentColor();
			if(parentColor.R == 0) localColor.R = 0;
			else localColor.R = Convert.ToByte(Convert.ToInt32(color.R)/(Convert.ToInt32(parentColor.R)/255));
			if(parentColor.G == 0) localColor.R = 0;
			else localColor.G = Convert.ToByte(Convert.ToInt32(color.G)/(Convert.ToInt32(parentColor.G)/255));
			if(parentColor.B == 0) localColor.R = 0;
			else localColor.B = Convert.ToByte(Convert.ToInt32(color.B)/(Convert.ToInt32(parentColor.B)/255));
			if(parentColor.A == 0) localColor.R = 0;
			else localColor.A = Convert.ToByte(Convert.ToInt32(color.A)/(Convert.ToInt32(parentColor.A)/255));
		}
		
		private Color GetParentColor() {
			Color parentColor = Color.White;
			if(this.Actor.Transform.Parent != null) {
				MyRenderer renderer = this.Transform.Parent.Actor.GetBehaviour<MyRenderer>();
				if(renderer != null) parentColor = renderer.TintColor;
			}
			return parentColor;
		}
		
		public static Vector2 ORIGIN_TOPLEFT 		{ get { return Vector2.Zero; } }
		public static Vector2 ORIGIN_TOP	 		{ get { return Vector2.UnitX * .5f; } }
		public static Vector2 ORIGIN_TOPRIGHT 		{ get { return Vector2.UnitX; } }
		public static Vector2 ORIGIN_LEFT 			{ get { return Vector2.UnitY * .5f; } }
		public static Vector2 ORIGIN_CENTER 		{ get {	return Vector2.One * .5f; } }
		public static Vector2 ORIGIN_RIGHT 			{ get { return new Vector2(1,.5f); } }
		public static Vector2 ORIGIN_BOTTOMLEFT		{ get { return Vector2.UnitY; } }
		public static Vector2 ORIGIN_BOTTOM 		{ get { return new Vector2(.5f,1); } }
		public static Vector2 ORIGIN_BOTTOMRIGHT 	{ get { return Vector2.One; } }
		
		protected Vector2 DrawingOrigin {
			get {
				Vector2 drawingOrigin = Vector2.Zero;
				drawingOrigin.X = this.Width * this.origin.X;
				drawingOrigin.Y = this.Height * this.origin.Y;
				return drawingOrigin;
			}
		}
		
		public abstract float Width {
			get;
		}
		public abstract float Height {
			get;
		}
		
		public Color localColor = Color.White;
		public float layer = 1;
		public Vector2 origin = MyRenderer.ORIGIN_CENTER;
        public bool visible = true;
		
		#endregion
		
		#region draw
		
		public virtual void Draw() {}
		
		#endregion
		
	}
	
}

