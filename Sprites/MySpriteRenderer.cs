using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mine {
	
	public class MySpriteRenderer : MyRenderer {
		
		#region drawing attributes
		
		public override float Width {
			get { return this.sprite.Width; }
		}
		
		public override float Height {
			get { return this.sprite.Height; }
		}
		
		#endregion
		
		#region draw
		
		public override void Draw() {
			if(this.sprite == null) return;
            float rotation = this.Transform.Rotation;
            if(this.sprite.Rotated) rotation -= (float)Math.PI * .5f;
			SpriteBatch spriteBatch = MyCanvas.Instance.SpriteBatch;
			spriteBatch.Draw
			(
				this.sprite.Texture,
				this.Transform.Position,
				this.sprite.Source,
				this.TintColor,
				rotation,
				this.DrawingOrigin,
				this.Transform.Scale,
				SpriteEffects.None,
				this.layer
			);
		}
		
		public MySprite sprite;
		
		#endregion
		
	}
}

