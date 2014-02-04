using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

[assembly:InternalsVisibleTo("MyTexturePackerReader")]
namespace Mine {
	
	public class MySprite {
		
		#region block public constructor
		
		internal MySprite() {}
		
		#endregion
		
		#region manager
		
		public static void LoadSimpleSprite(string spriteName, string filepath) {
			MySprite.LoadSimpleSprite(spriteName,filepath,false);
		}
		
		public static void LoadSimpleSprite(string spriteName,string filepath,bool persistent) {
			MySprite sprite = MySprite.GetSprite(spriteName);
			if(sprite != null) return;
			
			ContentManager content;
			if(persistent) content = MyCore.Instance.PersistentContent;
			else content = MyDirector.Instance.CurrentScene.Content;
			
			Texture2D texture = content.Load<Texture2D>(filepath);
			sprite = MySprite.CreateSimpleSpriteFromTexture(spriteName,texture,persistent);
			
			_sprites.Add(sprite);
		}
		
		private static MySprite CreateSimpleSpriteFromTexture(string spriteName,Texture2D texture, bool persistent) {
			MySprite simpleSprite = new MySprite();
			simpleSprite.name = spriteName;
			simpleSprite.width = texture.Width;
			simpleSprite.height = texture.Height;
			simpleSprite.persistent = persistent;
			simpleSprite.source = new Rectangle(0,0,texture.Width,texture.Height);
			simpleSprite.texture = texture;
			return simpleSprite;
		}

        public static void LoadTexturePackerAtlas(string filepath, string textureBaseDir) {
            MySprite.LoadTexturePackerAtlas(filepath,textureBaseDir,false);
        }
		
        public static void LoadTexturePackerAtlas(string filepath, string textureBaseDir, bool persistent) {
            MySprite[] sprites = MyTexturePackerReader.GetSpritesFromFile(filepath,textureBaseDir,persistent);
            foreach(MySprite sprite in sprites) {
                MySprite existentSprite = MySprite.GetSprite(sprite.Name);
			    if(existentSprite != null) continue;
                _sprites.Add(sprite);
            }
        }

		public static MySprite GetSprite(string spriteName) {
            try {
                return _sprites.First(sprite => { return sprite.name == spriteName; });
            } catch (Exception) {
                return null;
            }
		}
		
		private static HashSet<MySprite> _sprites = new HashSet<MySprite>();
		
		#endregion
		
		#region texture attributes
		
		public string Name {
			get { return this.name; }
            internal set { this.name = value; }
		}
		
		public float Width {
			get { return this.width; }
            internal set { this.width = value; }
		}
		
		public float Height {
			get { return this.height; }
            internal set { this.height = value; }
		}
		
		public bool Persistent {
			get { return this.persistent; }
            internal set { this.persistent = value; }
		}
		
		public Rectangle Source {
			get { return this.source; }
            internal set { this.source = value; }
		}
		
		public Texture2D Texture {
			get { return this.texture; }
            internal set { this.texture = value; }
		}
		
        public bool Rotated {
            get { return this.rotated; }
            internal set { this.rotated = value; }
        }

		private string name = "unknow";
		private float width = 0;
		private float height = 0;
		private bool persistent = false;
        private bool rotated = false;
		private Rectangle source = new Rectangle();
		private Texture2D texture;
		
		#endregion
		
	}
	
}

