using OpenTK.Graphics.OpenGL;
using OpenGL_Game.Components;
using OpenGL_Game.Systems;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;
using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using SkiaSharp;
using ObjLoader.Loader.Data.VertexData;
using static OpenGL_Game.Managers.SceneManager;
using System.Numerics;
using System.Windows.Forms;

namespace OpenGL_Game.Scenes
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class GameScene : Scene
    {
        public static float dt = 0;
        EntityManager entityManager;
        SystemManager systemManager;
        public Camera camera;
        public static GameScene gameInstance;
        public bool[] keysPressed = new bool[500];

        public GameScene(SceneManager sceneManager) : base(sceneManager)
        {
            gameInstance = this;
            entityManager = new EntityManager();
            systemManager = new SystemManager();

            // Set the title of the window
            sceneManager.Title = "Game";
            // Set the Render and Update delegates to the Update and Render methods of this class
            sceneManager.renderer = Render;
            sceneManager.updater = Update;
            // Set Keyboard events to go to a method in this class
            sceneManager.keyboardDownDelegate += Keyboard_KeyDown;
            sceneManager.keyboardUpDelegate += Keyboard_KeyUp;

            // Enable Depth Testing
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            // Set Camera
            camera = new Camera(new OpenTK.Mathematics.Vector3(0, 4, 7), new OpenTK.Mathematics.Vector3(0, 0, 0), (float)(sceneManager.Size.X) / (float)(sceneManager.Size.Y), 0.1f, 100f);

            CreateEntities();
            CreateSystems();

            // TODO: Add your initialization logic here
        }

        public void CreateEntities()
        {
            Entity newEntity;
            Entity newEntity2;
            Entity newEntity3;
            Entity newEntity4;
            Entity newEntity5;

            newEntity = new Entity("Moon");
            newEntity.AddComponent(new ComponentPosition(-2.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/Moon/moon.obj"));
            newEntity.AddComponent(new ComponentVelocity(1, 0, 0));
            newEntity.AddComponent(new ComponentCollisionSphere(1));
            entityManager.AddEntity(newEntity);

            newEntity2 = new Entity("Wraith_Raider_Starship");
            newEntity2.AddComponent(new ComponentPosition(2, 0, 0));
            newEntity2.AddComponent(new ComponentGeometry("Geometry/Wraith_Raider_Starship/Wraith_Raider_Starship.obj"));
            newEntity.AddComponent(new ComponentVelocity(-1, 0, 0));
            newEntity.AddComponent(new ComponentCollisionSphere(1));
            entityManager.AddEntity(newEntity2);

            //newEntity3 = new Entity("Wraith_Raider_Starship");
            //newEntity3.AddComponent(new ComponentPosition(0, 0, 0));
            //newEntity3.AddComponent(new ComponentGeometry("Geometry/Wraith_Raider_Starship/Wraith_Raider_Starship.obj"));
            //entityManager.AddEntity(newEntity3);

            //newEntity4 = new Entity("Intergalactic_Spaceship");
            //newEntity4.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            //newEntity4.AddComponent(new ComponentGeometry("Geometry/Intergalactic_Spaceship/Intergalactic_Spaceship.obj"));
            //entityManager.AddEntity(newEntity4);

            

            newEntity5 = new Entity("Maze");
            newEntity5.AddComponent(new ComponentPosition(0, 0, 0));
            newEntity5.AddComponent(new ComponentGeometry("Geometry/Maze/maze.obj"));
            entityManager.AddEntity(newEntity5);

        }

        private void CreateSystems()
        {
            ISystem newSystem;

            newSystem = new SystemRender();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemPhysics();
            systemManager.AddSystem(newSystem);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        public override void Update(FrameEventArgs e)
        {
            dt = (float)e.Time;
            System.Console.WriteLine("fps=" + (int)(1.0 / dt));

            //TODO: Add your update logic here

            switch (e.Key)
            {
                case System.Windows.Forms.Keys.Up:
                    camera.MoveForward(0.1f);
                    break;
                case System.Windows.Forms.Keys.Down:
                    camera.MoveForward(-0.1f);
                    break;
                case System.Windows.Forms.Keys.Left:
                    camera.RotateY(-0.01f);
                    break;
                case System.Windows.Forms.Keys.Right:
                    camera.RotateY(0.01f);
                    break;
                case System.Windows.Forms.Keys.M:
                    sceneManager.StartMenu();
                    break;
            }

            if (keysPressed[(char)System.Windows.Forms.Keys.Up])
            {
                camera.MoveForward(0.1f);
            }
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        public override void Render(FrameEventArgs e)
        {
            GL.Viewport(0, 0, sceneManager.Size.X, sceneManager.Size.Y);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Action ALL systems
            systemManager.ActionSystems(entityManager);

            // Render score
            GUI.DrawText("Score: 000", 30, 80, 30, 255, 255, 255);
            GUI.Render();
        }

        /// <summary>
        /// This is called when the game exits.
        /// </summary>
        public override void Close()
        {
            sceneManager.keyboardDownDelegate -= Keyboard_KeyDown;
            ResourceManager.RemoveAllAssets();

            // Need to remove assets (except Text) from Resource Manager
        }

        public void Keyboard_KeyDown(KeyboardKeyEventArgs e)
        {
            keysPressed[(char)e.Key] = true;
        }

        public void Keyboard_KeyUp(KeyboardKeyEventArgs e)
        {
            keysPressed[(char)e.Key] = false;
        }

        public void PressM(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.M:
                    sceneManager.UpdateMain();
                    break;
            }
        }

        public void PressG(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.M:
                    sceneManager.UpdateNone();
                    break;
            }
        }
    }
}