using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mine {

    public static class MyTexturePackerReader {

        public static MySprite[] GetSpritesFromFile(string filepath,string textureBaseDir,bool persistent) {
            string contentRootDir = MyCore.Instance.Game.Content.RootDirectory;
            string texturepath = textureBaseDir + "/";
            MySprite[] sprites = new MySprite[]{};
            XmlTextReader xmlReader = new XmlTextReader(contentRootDir + "/" + filepath);
            while(xmlReader.Read()) {
                if(xmlReader.NodeType != XmlNodeType.Element) continue;
                if(xmlReader.Name != "TextureAtlas") continue;
                string texturefile = xmlReader.GetAttribute("imagePath");
                texturepath += MyTexturePackerReader.CutOffFileType(texturefile);
                sprites = MyTexturePackerReader.GetSpritesFromXmlNode(xmlReader,texturepath,persistent);
            }
            xmlReader.Close();
            return sprites;
        }

        private static MySprite[] GetSpritesFromXmlNode(XmlReader xmlReader, string texturepath,bool persistent) {
            List<MySprite> sprites = new List<MySprite>();
            while(xmlReader.Read() && xmlReader.NodeType != XmlNodeType.EndElement) {
                if(xmlReader.NodeType != XmlNodeType.Element) continue;
                MySprite sprite = MyTexturePackerReader.GetSimpleSpriteFromNode(xmlReader,texturepath,persistent);
                sprites.Add(sprite);
            }
            return sprites.ToArray();
        }

        private static MySprite GetSimpleSpriteFromNode(XmlReader xmlReader, string texturepath,bool persistent) {
            string name = xmlReader.GetAttribute("n");
            int x = Convert.ToInt32(xmlReader.GetAttribute("x"));
            int y = Convert.ToInt32(xmlReader.GetAttribute("y"));
            int w = Convert.ToInt32(xmlReader.GetAttribute("w"));
            int h = Convert.ToInt32(xmlReader.GetAttribute("h"));
            int oX = Convert.ToInt32(xmlReader.GetAttribute("oX"));
            int oY = Convert.ToInt32(xmlReader.GetAttribute("oY"));
            int oW = Convert.ToInt32(xmlReader.GetAttribute("oW"));
            int oH = Convert.ToInt32(xmlReader.GetAttribute("oH"));
            bool rotated = xmlReader.GetAttribute("r") == "y";
            return MyTexturePackerReader.GetSprite(name,texturepath,x,y,w,h,rotated,persistent);
        }

        private static MySprite GetSprite(string name,string texturepath,int x,int y,int width,int height,bool rotated,bool persistent) {
            MySprite sprite = new MySprite();
            sprite.Name = MyTexturePackerReader.CutOffFileType(name);
            sprite.Width = width;
            sprite.Height = height;
            sprite.Rotated = rotated;
            sprite.Source = new Rectangle(x,y,width,height);
            sprite.Texture = MyTexturePackerReader.GetContent(persistent).Load<Texture2D>(texturepath);
            return sprite;
        }

        private static ContentManager GetContent(bool persistent) {
            if(persistent) return MyCore.Instance.PersistentContent;
            else return MyDirector.Instance.CurrentScene.Content;
        }

        private static string CutOffFileType(string filename) {
            int typeIndex = filename.LastIndexOf('.');
            if(typeIndex == filename.Length - 4) return filename.Substring(0,typeIndex);
            return filename;
        }

    }

}
