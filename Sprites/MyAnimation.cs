using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;

//TODO: create a serializer

namespace Mine {

    [Serializable] public class MyAnimationSprite {
        public string name;
        [OptionalField] public float time;
    }

    [Serializable] public class MyAnimation {

        public string name;
        [OptionalField] public float defaultSpriteTime = 100;
        public MyAnimationSprite[] sprites;   
        [XmlIgnore] public bool persistent;

        #region animator

        public int Length {
            get { return this.sprites.Length; }
        }

        public MyAnimationSprite GetAnimationSprite(int index) {
            if(index < 0 || index >= this.Length) throw new IndexOutOfRangeException();
            MyAnimationSprite animationSprite = this.sprites[index];
            if(animationSprite.time <= 0) animationSprite.time = this.defaultSpriteTime;
            return animationSprite;
        }

        public int GetFirstIndex() {
            return this.GetFirstIndex(false);
        }

        public int GetFirstIndex(bool reverse) {
            if(reverse) return this.Length - 1;
            else return 0;
        }

        #endregion

        #region management

        public static void LoadAnimation(string filepath) {
            MyAnimation.LoadAnimation(filepath,false);
        }

        public static void LoadAnimation(string filepath,bool persistent) {
            MyAnimation animation = MyAnimation.GetAnimation(filepath);
            if(animation != null) return;

            animation = MyAnimationReader.GetAnimationFromFile(filepath,persistent);
            _animations.Add(animation);
        }

        public static MyAnimation GetAnimation(string name) {
            try {
                return _animations.First(animation => { return animation.name == name; });
            } catch(Exception) {
                return null;
            }
        }

        private static List<MyAnimation> _animations = new List<MyAnimation>();

        #endregion

     }


}
