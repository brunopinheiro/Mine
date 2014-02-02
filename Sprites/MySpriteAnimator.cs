using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Mine {
    
    public class MySpriteAnimator : MyBehaviour {

        #region actor

        protected MySpriteRenderer SpriteRenderer {
            get {
                try {
                    return this.Actor.Renderer as MySpriteRenderer;
                } catch(Exception) {
                    throw new Exception(this.Actor.name + " does not contain a MySpriteRenderer Behaviour!");
                }
            }
        }

        #endregion

        #region animation

        public Action<MySpriteAnimator> OnLoop;
        public Action<MySpriteAnimator> OnSpriteChanged;
        public Action<MySpriteAnimator> OnFinished;

        public string AnimationName {
            get { return this.animation.name; }
        }

        public int AnimationIndex {
            get { return this.index; }
        }

        public float LoopCount {
            get { return this.loopCount; }
        }

        //Properties
        public MyLoop.LoopType loopType = MyLoop.LoopType.Straight;
        public int loops = MyLoop.INFINITE;
        public bool reverse = false;

        public void SetAnimation(string animation) {
            this.animation = MyAnimation.GetAnimation(animation);
            if(this.animation == null) throw new Exception(animation + "animation was not found! Maybe it is not loaded yet!");
            this.ResetControllers();
        }

        public void Play() {
            if(this.animation == null) {
                Console.WriteLine("The animation is not set!!");
                return;
            }
            this.ResetControllers();
            this.stopped = false;
            this.SetSprite();
        }

        public void Pause() {
            this.stopped = true;
        }

        public void Resume() {
            this.stopped = false;
        }

        public void Stop() {
            if(this.animation == null) return;
            this.ResetControllers();
            this.stopped = true;
            this.SetSprite();            
        }

        private void ResetControllers() {
            this.index = this.animation.GetFirstIndex(this.reverse);
            this.elapsedTime = 0;
            this.loopCount = 0;
            this.stopped = true;
        }

        private void SetSprite() {
            this.SetSprite(this.animation.GetAnimationSprite(this.index));
        }

        private void SetSprite(MyAnimationSprite animationSprite) {
            MySprite sprite = MySprite.GetSprite(animationSprite.name);
            MySpriteRenderer spriteRenderer = this.SpriteRenderer;
            spriteRenderer.sprite = sprite;
            this.spriteTime = animationSprite.time;
            this.elapsedTime = 0;
        }

        public void Update() {
            if(this.stopped) return;
            this.elapsedTime += MyCore.Instance.GameTime.ElapsedGameTime.TotalMilliseconds;
            if(this.elapsedTime > this.spriteTime) this.MoveNextSprite();
        }

        private void MoveNextSprite() {
            bool spriteChanged = false;
            if(this.reverse) {
                this.index--;
                if(this.index < 0) this.Loop();
                else spriteChanged = true;
            }
            else {
                this.index++;
                if(this.index >= this.animation.Length) this.Loop();
                else spriteChanged = true;
            }
            if(spriteChanged && this.OnSpriteChanged != null) this.OnSpriteChanged(this);
            this.SetSprite();
        }

        private void Loop() {
            if(this.loopType == MyLoop.LoopType.PingPong) {
                this.reverse = !this.reverse;
                this.loopCount += .5f;
            } else this.loopCount++;
            this.index = this.animation.GetFirstIndex(this.reverse);
            if(this.loops > MyLoop.INFINITE && this.loopCount >= this.loops) {
                this.Stop();
                if(this.OnFinished != null) this.OnFinished(this);
            } 
            else {
                if(this.OnSpriteChanged != null) this.OnSpriteChanged(this);
                if(this.OnLoop != null) this.OnLoop(this);
            }
        }

        //Controllers
        private bool stopped = true;
        private int index = 0;
        private float loopCount = 0;
        private double elapsedTime = 0;
        private float spriteTime = 0;
        private MyAnimation animation;

        #endregion

    }

}
