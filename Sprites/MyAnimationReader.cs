using System;
using System.Xml;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;

namespace Mine {

    public static class MyAnimationReader {

         public static MyAnimation GetAnimationFromFile(string filepath,bool persistent) {
            string contentRootDir = MyCore.Instance.Game.Content.RootDirectory;
            XmlTextReader xmlReader = new XmlTextReader(contentRootDir + "/" + filepath);
            MyAnimation animation = new MyAnimation();
            List<MyAnimationSprite> sprites = new List<MyAnimationSprite>();
            while(xmlReader.Read()) {
                if(xmlReader.NodeType != XmlNodeType.Element) continue;
                if(xmlReader.Name == "Animation") {
                    animation = MyAnimationReader.GetAnimationFromNode(xmlReader);
                    continue;
                }
                if(xmlReader.Name == "sprite") {
                    sprites.Add(MyAnimationReader.GetSpriteFromNode(xmlReader));
                    continue;
                }
            }
            xmlReader.Close();
            animation.sprites = sprites.ToArray();
            return animation;
        }

        private static MyAnimation GetAnimationFromNode(XmlReader reader) {
            int defaultSpriteTime = Convert.ToInt32(reader.GetAttribute("defaultSpriteTime"));
            if(defaultSpriteTime == 0) defaultSpriteTime = 100;
            string name = reader.GetAttribute("name");
            MyAnimation animation = new MyAnimation();
            animation.name = name;
            animation.defaultSpriteTime = defaultSpriteTime;
            return animation;
        }

        private static MyAnimationSprite GetSpriteFromNode(XmlReader xmlReader) {
            MyAnimationSprite animationSprite = new MyAnimationSprite();
            animationSprite.name = xmlReader.GetAttribute("name");
            animationSprite.time = Convert.ToInt32(xmlReader.GetAttribute("time"));
            return animationSprite;
        }

    }

}
