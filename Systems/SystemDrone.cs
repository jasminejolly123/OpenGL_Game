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
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

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

        public void OnAction(List<Entity> entites, List<Camera> cameras, List<Drone> drones)
        {
            foreach (Drone drone in drones)
            {
                foreach (Camera camera in cameras)
                {
                    if ((drone.Mask & MASK) == MASK)
                    {
                        List<IComponent> dcomponents = drone.Components;

                        IComponent positionComponent = dcomponents.Find(delegate (IComponent component)
                        {
                            return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                        });
                        IComponent velocityComponent = dcomponents.Find(delegate (IComponent component)
                        {
                            return component.ComponentType == ComponentTypes.COMPONENT_VELOCITY;
                        });



                        Motion((ComponentPosition)positionComponent, (ComponentVelocity)velocityComponent, drone, camera);
                    }
                }
            }
        }

        public void Motion(ComponentPosition position, ComponentVelocity velocity, Drone drone, Camera camera)
        {
            if (GameScene.dt > -1)
            {
                position.position = camera.oldOldCameraPosition;

            }  
        }
    }
}