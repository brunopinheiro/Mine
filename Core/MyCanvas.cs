using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

[assembly:InternalsVisibleTo("MyCore")]
namespace Mine {
	
	public class MyCanvas {
		
		#region singleton
		
		private MyCanvas() {
            this.graphics = new GraphicsDeviceManager(MyCore.Instance.Game);
            this.Resolution = this.VirtualResolution;            
        }

        internal static void CreateCanvas() {
            _instance = new MyCanvas();           
        }

		public static MyCanvas Instance {
			get { return _instance; }
		}
		
		private static MyCanvas _instance;
		
		#endregion
		
		#region initialization
		
		internal void Initialize() {
            this.spriteBatch = new SpriteBatch(this.Graphics.GraphicsDevice);
            this.defaultViewport = this.GraphicsDevice.Viewport;
		}
		
		#endregion

        #region graphics

        public bool FullScreen {
            get { return this.Graphics.IsFullScreen; }
            set { this.Graphics.IsFullScreen = value; }
        }

        public void ToggleFullScreen() {
            this.Graphics.ToggleFullScreen();
        }

        public void ApplyGraphicChanges() {
            this.ConfigureResolutionSettings();
            this.Graphics.ApplyChanges();
        }

        public GraphicsDevice GraphicsDevice {
            get { return this.Graphics.GraphicsDevice; }
        }

        public GraphicsDeviceManager Graphics {
            get { return this.graphics; }
        }

        private GraphicsDeviceManager graphics;
        
        #endregion

        #region resolution

        public Vector2 VirtualResolution {
            get { return this.virtualResolution; }
            set { this.virtualResolution = value; }
        }

        public Vector2 Resolution {
            get {
                Vector2 resolution = Vector2.Zero;
                resolution.X = this.Graphics.PreferredBackBufferWidth;
                resolution.Y = this.Graphics.PreferredBackBufferHeight;
                return resolution;
            }
            set {
                this.Graphics.PreferredBackBufferWidth = (int)value.X;
                this.Graphics.PreferredBackBufferHeight = (int)value.Y;
            }
        }

        public Vector2 ResolutionScale {
            get {
                Vector2 resolutionScale = Vector2.Zero;
                resolutionScale.X = this.Resolution.X / this.VirtualResolution.X;
                resolutionScale.Y = this.Resolution.Y / this.VirtualResolution.Y;
                return resolutionScale;
            }
        }

        public Viewport DefaultViewport {
            get { return this.defaultViewport; }
        }

        public Vector2 Center {
            get { return this.VirtualResolution * .5f; }
        }

        public float VirtualAspectRatio {
            get { return this.virtualResolution.X / this.virtualResolution.Y; }
        }

        private void ConfigureResolutionSettings() {
            if (this.FullScreen) this.ConfigureFullScreenSettings();
            else this.ConfigureWindowSettings();
            this.GraphicsDevice.Viewport = this.defaultViewport;
        }

        private void ConfigureFullScreenSettings() {
            float newWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            float newHeight = newWidth / this.VirtualAspectRatio;

            if (newHeight > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height) {
                newHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                newWidth = newHeight * this.VirtualAspectRatio;
            }

            this.defaultViewport.X = 0;
            this.defaultViewport.Y = 0;
            this.defaultViewport.Width = (int)newWidth;
            this.defaultViewport.Height = (int)newHeight;
            this.defaultViewport.MinDepth = 0;
            this.defaultViewport.MaxDepth = 1;
            this.Resolution = new Vector2(this.defaultViewport.Width,this.defaultViewport.Height);
        }

        private void ConfigureWindowSettings() {
            this.defaultViewport.X = 0;
            this.defaultViewport.Y = 0;
            this.defaultViewport.Width = (int)Math.Ceiling(this.Resolution.X);
            this.defaultViewport.Height = (int)Math.Ceiling(this.Resolution.Y);
            this.defaultViewport.MinDepth = 0;
            this.defaultViewport.MaxDepth = 1;
            this.Resolution = new Vector2(this.defaultViewport.Width,this.defaultViewport.Height);
        }

        private Viewport defaultViewport;
        private Vector2 virtualResolution = new Vector2(800, 600);

        #endregion

        #region drawing

        public SpriteBatch SpriteBatch {
            get { return this.spriteBatch; }
        }

        public void Draw() {
            MyRenderer[] renderers = this.GetAllRenderers();
            MyCamera[] cameras = this.GetAllCameras();
            foreach(MyCamera camera in cameras) this.DrawCamera(camera,renderers);
        }

        private void DrawCamera(MyCamera camera,MyRenderer[] renderers) {
            this.GraphicsDevice.Viewport = camera.viewport;
            this.ClearBuffer(camera.backgroundColor);
            spriteBatch.Begin(
                    SpriteSortMode.BackToFront, 
                    BlendState.AlphaBlend,
                    null,
                    null,
                    null,
                    null,
                    camera.GetTransformationMatrix()
                );
            camera.Draw(renderers);
            spriteBatch.End();
        }

        private void ClearBuffer(Color color) {
            this.graphics.GraphicsDevice.Clear(color);
        }

        private MyRenderer[] GetAllRenderers() {
            MyActor sceneContainer = MyDirector.Instance.CurrentScene.Container;
            MyRenderer[] renderers = sceneContainer.GetAllBehavioursInChildren<MyRenderer>();
            Array.Sort(renderers, delegate(MyRenderer rendererA, MyRenderer rendererB)
            {
                return rendererA.layer.CompareTo(rendererB.layer);
            });
            return renderers.Where(renderer => { return renderer.visible; }).ToArray();
        }

        private MyCamera[] GetAllCameras() {
            if(MyDirector.Instance.CurrentScene == null) return new MyCamera[]{};
            return MyDirector.Instance.CurrentScene.Container.GetAllBehavioursInChildren<MyCamera>();
        }

        private SpriteBatch spriteBatch;

        #endregion

        #region transformations

        

        #endregion

    }
	
}

