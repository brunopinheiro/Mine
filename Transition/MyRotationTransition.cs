using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Mine {

    public class MyRotationTransition : MyTransition {

        #region attributes

        public float targetRotation = 0;

        #endregion

        #region controllers

        public void Play(float targetRotation) {
            if(this.usingLocals) this.Play(targetRotation,this.Actor.Transform.localRotation);
            else this.Play(targetRotation,this.Actor.Transform.Rotation);
        }

        public void Play(float targetRotation,float initialRotation) {
            this.targetRotation = targetRotation;
            this.initialRotation = initialRotation;
            this.Play();
        }

        protected override void SetInitialConfigurations() {
            if(this.usingLocals) this.Actor.Transform.localRotation = this.initialRotation;
            else this.Actor.Transform.Rotation = this.initialRotation;
        }

        protected override void ReverseConfigurations() {
            float backup = this.targetRotation;
            this.targetRotation = this.initialRotation;
            this.initialRotation = backup;
        }

        protected override void ExecuteChanges() {
            float r = MyEase.GetValue(this.easeType,(float)this.ElapsedTime,this.duration,this.initialRotation,this.targetRotation);
            if(this.usingLocals) this.Actor.Transform.localRotation = r;
            else this.Actor.Transform.Rotation = r;
        }

        private float initialRotation = 0;

        #endregion

    }

}
