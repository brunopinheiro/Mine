using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Mine {
    
    public abstract class MyTransition : MyBehaviour {

        #region attributes

        public bool usingLocals = true;
        public float duration = 0;
        public int loops = MyLoop.INFINITE;
        public MyLoop.LoopType loopType = MyLoop.LoopType.PingPong;
        public MyEase.EaseTypes easeType = MyEase.EaseTypes.Linear;

        #endregion

        #region controllers

        public Action<MyTransition> OnFinished;
        public Action<MyTransition> OnLoop;

        private void ResetControllers() {
            this.stopped = true;
            this.elapsedTime = 0;
            this.loopCount = 0;
        }

        protected void Play() {
            this.ResetControllers();
            this.stopped = false;
        }

        public void Pause() {
            this.stopped = true;
        }

        public void Resume() {
            this.stopped = false;
        }

        public void Stop() {
            this.ResetControllers();
            this.stopped = true;
        }

        public void Update() {
            if(this.stopped) return;
            this.elapsedTime += MyCore.Instance.GameTime.ElapsedGameTime.TotalMilliseconds;
            if(this.elapsedTime >= this.duration) this.Loop();
            else this.ExecuteChanges();
        }

        private void Loop() {
            if(this.loopType == MyLoop.LoopType.PingPong) this.loopCount += .5f;
            else this.loopCount++;
            if(this.loops > MyLoop.INFINITE && this.loopCount >= this.loops) {
                this.Stop();
                if(this.OnFinished != null) this.OnFinished(this);
            } else {
                if(this.loopType == MyLoop.LoopType.PingPong) this.ReverseConfigurations();
                this.SetInitialConfigurations();
                if(this.OnLoop != null) this.OnLoop(this);
            }
            this.elapsedTime = 0;
        }

        protected abstract void ExecuteChanges();
        protected abstract void SetInitialConfigurations();
        protected abstract void ReverseConfigurations();

        protected double ElapsedTime {
            get { return this.elapsedTime; }
        }

        private bool stopped = true;
        private double elapsedTime = 0;
        private float loopCount = 0;
        
        #endregion

    }

}
