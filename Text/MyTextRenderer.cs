using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mine {
    
    public class MyTextRenderer : MyRenderer {

        #region text

        public string text = "Hello World!";
        public SpriteFont font;

        #endregion

        #region draw

        public override void Draw() {
            if(this.font == null) return;
            SpriteBatch spriteBatch = MyCanvas.Instance.SpriteBatch;
            spriteBatch.DrawString(
                this.font,
                this.text,
                this.Transform.Position,
                this.TintColor,
                this.Transform.Rotation,
                this.DrawingOrigin,
                this.Transform.Scale,
                SpriteEffects.None,
                this.layer
            );
        }


        #endregion

        #region drawing attributes

        public override float Width {
            get { return this.font.MeasureString(this.text).X; }
        }

        public override float Height {
            get { return this.font.MeasureString(this.text).Y; }
        }

        #endregion
    }

}
