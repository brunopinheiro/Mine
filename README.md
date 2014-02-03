Mine
====
Mine is a simple 2D game engine written in C# / XNA. It was created on 2011 with the main goal of help new game programmers. Using Mine, it is not necessary to worry about managing who should be updated or draw every frame, sprite animations and content in memory. This way, it is possible to focus more and more on your game logic.


Version
-----
0.7


Installation
-----
You can add the project to your solution and then add a reference to it. Or you can just add all the code files into your main project.

Modify your Game1.cs

```````c#
//Game1.cs

using Mine;

public class Game1 : Game {

    public Game1() {
        this.Content.RootDirectory = "YOUR CONTENT ROOT DIRECTORY";
        MyCore.CreateCore(this);
        MyCanvas.Instance.VirtualResolution = new Vector2(1280,720);
        MyCanvas.Instance.Resolution = new Vector2(1280,720);
        MyCanvas.Instance.FullScreen = false;
    }

    protected override void Initialize() {
        MyCore.Instance.Initialize();
        base.Initialize();
    }

    protected override void LoadContent() {
        MyDirector.Instance.OpenScene(new FIRST_SCENE());
    }


    protected override void Update(GameTime gameTime) {
        MyCore.Instance.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        MyCanvas.Instance.Draw();
        base.Draw(gameTime);
    }
}

```````


Getting Started
-------

````````c#
//NewScene.cs

//First, you should create a class that inherits from MyScene and override the protected method Load.
public class NewScene : MyScene {
  
    protected override void Load() {
        MySprite.LoadSimpleSprite("Mario","Resources/Characters/mario");
        this.CreateMarioActor();
        base.Load();
    }
  
}

//You can open your new scene using:
MyDirector.Instance.OpenScene(new NewScene());

````````
In this code above, we are making your NewScene be loaded and rendered on the screen.
Inside the Load method, it was created a **MySprite** instance. **MySprite** is the class responsable for store all the data needed to render a **Texture2D** on screen. In this case, the instance is named *Mario* and has the **Texture2D** from file "Resources/Characters/mario". Below, you can see the example of how you use the created **MySprite** instance.

```````c#

//NewScene.cs 

private void CreateMarioActor() {
  
  MyActor mario = new MyActor("Mario");
  mario.AddBehaviour<MySpriteRenderer>();
  MySpriteRenderer spriteRenderer = mario.GetBehaviour<MySpriteRenderer>();
  spriteRenderer.sprite = MySprite.GetSprite("Mario");
  mario.Transform.localPosition = MyCanvas.Instance.Center;
  this.AddActor(mario);
  
}

```````
Actors represent every object on the game. Every actor is controlled by one or more behaviours. In this code we are creating a new **MyActor** called *Mario* and adding a **MySpriteRenderer** behaviour to it. **MySpriteRenderer** is responsable for use the data stored on **MySprite** and the attributes from the behaviour **MyTransform**, to render the Texture with the correct position, rotation and scale on the screen. Another thing we are doing there (and the most important), is adding our Mario actor to the screen. Without this step, our actor would be useless.


License
-----
MIT
