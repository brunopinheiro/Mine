using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Mine {

    public class MyScaleTransition : MyTransition {

        #region attributes

        public Vector2 targetScale = Vector2.Zero;

        #endregion

        #region controllers

        public void Play(Vector2 targetScale) {
            if(this.usingLocals) this.Play(targetScale,this.Actor.Transform.localScale);
            else this.Play(targetScale,this.Actor.Transform.Scale);
        }

        public void Play(Vector2 targetScale,Vector2 initialScale) {
            this.targetScale = targetScale;
            this.initialScale = initialScale;
            this.Play();
        }

        protected override void SetInitialConfigurations() {
            if(this.usingLocals) this.Actor.Transform.localScale = this.initialScale;
            else this.Actor.Transform.Scale = this.initialScale;
        }

        protected override void ReverseConfigurations() {
            Vector2 backup = this.targetScale;
            this.targetScale = this.initialScale;
            this.initialScale = backup;
        }

        protected override void ExecuteChanges() {
            float x = MyEase.GetValue(this.easeType,(float)this.ElapsedTime,this.duration,this.initialScale.X,this.targetScale.X);
            float y = MyEase.GetValue(this.easeType,(float)this.ElapsedTime,this.duration,this.initialScale.Y,this.targetScale.Y);
            if(this.usingLocals) this.Actor.Transform.localScale = new Vector2(x,y);
            else this.Actor.Transform.Scale = new Vector2(x,y);
        }

        private Vector2 initialScale = Vector2.Zero;

        #endregion

    }

}
