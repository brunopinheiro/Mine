using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Mine {

    public class MyMovementTransition : MyTransition {

        #region attributes

        public Vector2 targetPosition = Vector2.Zero;

        #endregion

        #region controllers

        public void Play(Vector2 targetPosition) {
            if(this.usingLocals) this.Play(targetPosition,this.Actor.Transform.localPosition);
            else this.Play(targetPosition,this.Actor.Transform.Position);
        }

        public void Play(Vector2 targetPosition,Vector2 initialPosition) {
            this.targetPosition = targetPosition;
            this.initialPosition = initialPosition;
            this.Play();
        }

        protected override void SetInitialConfigurations() {
            if(this.usingLocals) this.Actor.Transform.localPosition = this.initialPosition;
            else this.Actor.Transform.Position = this.initialPosition;
        }

        protected override void ReverseConfigurations() {
            Vector2 backup = this.targetPosition;
            this.targetPosition = this.initialPosition;
            this.initialPosition = backup;
        }

        protected override void ExecuteChanges() {
            float x = MyEase.GetValue(this.easeType,(float)this.ElapsedTime,this.duration,this.initialPosition.X,this.targetPosition.X);
            float y = MyEase.GetValue(this.easeType,(float)this.ElapsedTime,this.duration,this.initialPosition.Y,this.targetPosition.Y);
            if(this.usingLocals) this.Actor.Transform.localPosition = new Vector2(x,y);
            else this.Actor.Transform.Position = new Vector2(x,y);
        }

        private Vector2 initialPosition = Vector2.Zero;

        #endregion

    }

}
