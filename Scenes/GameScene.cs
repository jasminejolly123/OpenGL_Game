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
using static System.Formats.Asn1.AsnWriter;
using System.Windows.Forms.Automation;

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
        CameraManager cameraManager;
        DroneManager droneManager;
        CollisionManager collisionManager;
        public Camera camera;
        public Drone drone;
        public static Score newScore;
        public static GameScene gameInstance;
        public bool[] keysPressed = new bool[500];
        FrameEventArgs e;
        KeyboardKeyEventArgs f;
        private float elapsedTime = 0f;
        public static int lives = 3;
        public static int keys = 0;

        public GameScene(SceneManager sceneManager) : base(sceneManager)
        {
            gameInstance = this;
            entityManager = new EntityManager();
            systemManager = new SystemManager();
            cameraManager = new CameraManager();
            droneManager = new DroneManager();
            collisionManager = new MazeEscapeCollisionManager();
            newScore = new Score();

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

            CreateCamera();
            //CreateDrone();
            CreateEntities();
            CreateSystems();

            // TODO: Add your initialization logic here
        }

        Camera newCamera;

        public void CreateCamera()
        {

            newCamera = new Camera(new OpenTK.Mathematics.Vector3(-5, 1.5f, 7), new OpenTK.Mathematics.Vector3(0, 1.5f, 0), (float)(sceneManager.Size.X) / (float)(sceneManager.Size.Y), 0.1f, 100f);
            cameraManager.AddCamera(newCamera);
        }

        //public void CreateDrone()
        //{
        //    newDrone = new Drone(new OpenTK.Mathematics.Vector3(0, 3.5f, 0), new OpenTK.Mathematics.Vector3(0, 1.5f, 0), (new ComponentGeometry("Geometry/Drone/droneobject.obj")));
        //    droneManager.AddDrone(newDrone);
        //}

        public void CreateEntities()
        {
            Entity newEntity;
            Drone drone;

            drone = new Drone(new OpenTK.Mathematics.Vector3(-5, 1.5f, 7), new OpenTK.Mathematics.Vector3(-5, 1.5f, 7), "Geometry/Drone/droneobject.obj");
            drone.AddComponent(new ComponentGeometry("Geometry/Drone/droneobject.obj"));
            drone.AddComponent(new ComponentScale(1, 1, 1));
            drone.AddComponent(new ComponentPosition(5, 3, 7));
            drone.AddComponent(new ComponentVelocity(0, 0, 0));
            droneManager.AddDrone(drone);

            newEntity = new Entity("Key");
            newEntity.AddComponent(new ComponentPosition(-8, 0, 46));
            newEntity.AddComponent(new ComponentScale(1, 1, 1));
            newEntity.AddComponent(new ComponentCollisionSphere(0.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/Key/key.obj"));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Key");
            newEntity.AddComponent(new ComponentPosition(-47, 0, 46));
            newEntity.AddComponent(new ComponentScale(1, 1, 1));
            newEntity.AddComponent(new ComponentCollisionSphere(0.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/Key/key.obj"));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Key");
            newEntity.AddComponent(new ComponentPosition(-47, 0, 8));
            newEntity.AddComponent(new ComponentScale(1, 1, 1));
            newEntity.AddComponent(new ComponentCollisionSphere(0.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/Key/key.obj"));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Ball1");
            newEntity.AddComponent(new ComponentPosition(-1.5f, 0, 43));
            newEntity.AddComponent(new ComponentScale(0.5f, 0.5f, 0.5f));
            newEntity.AddComponent(new ComponentVelocity(0, 0, 0));
            newEntity.AddComponent(new ComponentCollisionSphere(0.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/Ball/ball.obj"));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Ball2");
            newEntity.AddComponent(new ComponentPosition(-63, 0, 1.5f));
            newEntity.AddComponent(new ComponentScale(0.5f, 0.5f, 0.5f));
            newEntity.AddComponent(new ComponentVelocity(0, 0, 0));
            newEntity.AddComponent(new ComponentCollisionSphere(0.5f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/Ball/ball.obj"));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Maze");
            newEntity.AddComponent(new ComponentPosition(0, 0, 0));
            newEntity.AddComponent(new ComponentScale(1, 1, 1));
            newEntity.AddComponent(new ComponentGeometry("Geometry/Maze/maze.obj"));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB1");
            newEntity.AddComponent(new ComponentCollisionWall(-100000000, 10000000, 100000000, 1.5f, 1));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB2");
            newEntity.AddComponent(new ComponentCollisionWall(-100000000, 10000000, 52, -10000000, 1));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB3");
            newEntity.AddComponent(new ComponentCollisionWall(-100000000, -2, 100000000, -10000000, 1));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB4");
            newEntity.AddComponent(new ComponentCollisionWall(-52, 10000000, 100000000, -100000000, 1));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB13");
            newEntity.AddComponent(new ComponentCollisionWall(-42, -15.5f, 6.5f, -10000000, 3));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB14");
            newEntity.AddComponent(new ComponentCollisionWall(-42, -15.5f, 1000000, 50, 3));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB15");
            newEntity.AddComponent(new ComponentCollisionWall(-6.5f, 1000000, 15.5f, 41.5f, 4));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB16");
            newEntity.AddComponent(new ComponentCollisionWall(-1000000, -50, 15.5f, 41.5f, 4));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB5");
            newEntity.AddComponent(new ComponentCollisionWall(-27, -15.5f, 10.5f, 22, 2));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB6");
            newEntity.AddComponent(new ComponentCollisionWall(-21, -11, 16, 26, 2));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB7");
            newEntity.AddComponent(new ComponentCollisionWall(-21, -11, 31, 41, 2));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB8");
            newEntity.AddComponent(new ComponentCollisionWall(-27, -15.5f, 36, 46, 2));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB9");
            newEntity.AddComponent(new ComponentCollisionWall(-41, -31, 10.5f, 22, 2));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB10");
            newEntity.AddComponent(new ComponentCollisionWall(-46, -36, 16, 26, 2));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB11");
            newEntity.AddComponent(new ComponentCollisionWall(-46, -36, 31, 41, 2));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("BB12");
            newEntity.AddComponent(new ComponentCollisionWall(-41, -31, 36, 46, 2));
            entityManager.AddEntity(newEntity);

            //newEntity = new Entity("Moon");
            //newEntity.AddComponent(new ComponentPosition(0, 0, 0));
            //newEntity.AddComponent(new ComponentGeometry("Geometry/Moon/moon.obj"));
            //entityManager.AddEntity(newEntity);


            //newEntity = new Entity("BB2");
            //newEntity.AddComponent(new ComponentCollisionWall(1));
            //entityManager.AddEntity(newEntity);

        }

        private void CreateSystems()
        {
            ISystem newSystem;

            newSystem = new SystemRender();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemPhysics();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemCollisionSphere(collisionManager);
            systemManager.AddSystem(newSystem);

            newSystem = new SystemCollisionWall(collisionManager);
            systemManager.AddSystem(newSystem);

            newSystem = new SystemBallPath();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemDrone();
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

            if (keysPressed[(char)OpenTK.Windowing.GraphicsLibraryFramework.Keys.Up])
            {
                newCamera.MoveForward(5f * dt);
            }
            if (keysPressed[(char)OpenTK.Windowing.GraphicsLibraryFramework.Keys.Down])
            {
                newCamera.MoveForward(-5f * dt);
            }
            if (keysPressed[(char)OpenTK.Windowing.GraphicsLibraryFramework.Keys.Right])
            {
                newCamera.RotateY(2 * dt);
            }
            if (keysPressed[(char)OpenTK.Windowing.GraphicsLibraryFramework.Keys.Left])
            {
                newCamera.RotateY(-2 * dt);
            }

        }

        //public Score newScore = new Score();

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        public override void Render(FrameEventArgs e)
        {
            GL.Viewport(0, 0, sceneManager.Size.X, sceneManager.Size.Y);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Action ALL systems
            systemManager.ActionSystems(entityManager, cameraManager, droneManager);

           // if (collisionManager != null)
            //{
                collisionManager.ProcessCollisions(cameraManager);
            //}
            //collisionManager.ProcessCollisions(cameraManager);

            // Render score
            elapsedTime += (float)e.Time;

            // Display elapsed time
            string timeText = "Score: " + elapsedTime.ToString("F2");
            GUI.DrawText(timeText, 30, 80, 30, 255, 255, 255);

            newScore.Value = elapsedTime.ToString("F2");

            string livesText = "Lives: " + lives.ToString();
            GUI.DrawText(livesText, 1010, 80, 30, 255, 255, 255);

            NoLives(lives);

            string keysText = "Keys: " + keys.ToString();
            GUI.DrawText(keysText, 30, 120, 30, 255, 255, 255);


            //score += (float)e.Time;
            //score = score / 10;
            //string scoreText = "Score: " + score.ToString("F2") + " seconds";
            //GUI.DrawText(scoreText, 30, 80, 30, 255, 255, 255);
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
                case (OpenTK.Windowing.GraphicsLibraryFramework.Keys)System.Windows.Forms.Keys.M:
                    sceneManager.UpdateMain();
                    break;
            }
        }

        public void PressG(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case (OpenTK.Windowing.GraphicsLibraryFramework.Keys)System.Windows.Forms.Keys.M:
                    sceneManager.UpdateNone();
                    break;
            }
        }

        public void NoLives(int lives)
        {
            if (lives == 0)
            {
                sceneManager.GameOver();
                sceneManager.UpdateNone();
            }
        }
    }
}