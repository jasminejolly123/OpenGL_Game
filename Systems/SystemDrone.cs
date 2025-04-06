using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL;
using OpenGL_Game.Components;
using OpenGL_Game.OBJLoader;
using OpenGL_Game.Objects;
using OpenGL_Game.Scenes;
using OpenTK.Mathematics;
using System.Xml.Linq;
using OpenGL_Game.Systems;
using OpenGL_Game.Managers;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SkiaSharp;
using ObjLoader.Loader.Data.VertexData;
using static OpenGL_Game.Managers.SceneManager;
using System.Numerics;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;
using System.Windows.Forms.Automation;

namespace OpenGL_Game.Systems
{
    class SystemDrone : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_VELOCITY);
        KeyboardKeyEventArgs f;
        FrameEventArgs e;
        public bool[] keysPressed = new bool[500];
        public OpenTK.Mathematics.Vector3 droneDirection;

        public SystemDrone()
        {

        }

        public string Name
        {
            get { return "SystemDrone"; }
        }

        public void OnAction(List<Entity> entities, List<Camera> cameras)
        {
            foreach (Entity entity in entities)
            {
                if ((entity.Mask & MASK) == MASK)
                {
                    List<IComponent> components = entity.Components;

                    IComponent positionComponent = components.Find(delegate (IComponent component)
                    {
                        return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                    });
                    IComponent velocityComponent = components.Find(delegate (IComponent component)
                    {
                        return component.ComponentType == ComponentTypes.COMPONENT_VELOCITY;
                    });

                    Motion((ComponentPosition)positionComponent, (ComponentVelocity)velocityComponent);
                    if (entity.Name == "Drone")
                    {
                        Motion((ComponentPosition)positionComponent, (ComponentVelocity)velocityComponent);
                    }
                }
            }
        }

        public void Motion(ComponentPosition position, ComponentVelocity velocity)
        {

            GameScene.dt = (float)e.Time;
            System.Console.WriteLine("fps=" + (int)(1.0 / GameScene.dt));

            if (keysPressed[(char)OpenTK.Windowing.GraphicsLibraryFramework.Keys.Up])
            {
                velocity.Velocity = (-3, 0, 0);
            }
            if (keysPressed[(char)OpenTK.Windowing.GraphicsLibraryFramework.Keys.Down])
            {
                velocity.Velocity = (0, 0, 3);
            }
            if (keysPressed[(char)OpenTK.Windowing.GraphicsLibraryFramework.Keys.Right])
            {
                droneDirection = Matrix3.CreateRotationY(2) * droneDirection;
            }
            if (keysPressed[(char)OpenTK.Windowing.GraphicsLibraryFramework.Keys.Left])
            {
                droneDirection = Matrix3.CreateRotationY(-2) * droneDirection;
            }
        }
    }
}