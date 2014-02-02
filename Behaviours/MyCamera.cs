using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mine {
    
    public class MyCamera : MyBehaviour {

        #region transformations

        public Matrix GetTransformationMatrix() {
            Matrix translation = this.GetTranslationMatrix();
            Matrix viewportTranslation = this.GetViewportTranslationMatrix();
            Matrix rotation = this.GetRotationMatrix();
            Matrix scale = this.GetScaleMatrix();
            return translation * rotation * scale * viewportTranslation;
        }

        private Matrix GetTranslationMatrix() {
            Vector3 translationVector = Vector3.Zero;
            translationVector.X = this.Transform.Position.X;
            translationVector.Y = this.Transform.Position.Y;
            return Matrix.CreateTranslation(translationVector * -1);
        }

        private Matrix GetViewportTranslationMatrix() {
            Vector3 viewportTranslationVector = Vector3.Zero;
            //viewportTranslationVector.X = this.viewport.Width;
            //viewportTranslationVector.Y = this.viewport.Height;
            return Matrix.CreateTranslation(viewportTranslationVector);
        }

        private Matrix GetRotationMatrix() {
            return Matrix.CreateRotationZ(this.Transform.Rotation);
        }

        private Matrix GetScaleMatrix() {
            Vector2 resolutionScale = MyCanvas.Instance.ResolutionScale;
            return Matrix.CreateScale(resolutionScale.X,resolutionScale.Y,1);
        }

        #endregion

        #region draw

        public Color backgroundColor = Color.Black;

        public void Draw(MyRenderer[] renderers) {
            foreach(MyRenderer renderer in renderers) renderer.Draw();
        }

        public Viewport viewport;

        #endregion

    }

}
