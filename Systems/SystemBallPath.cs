using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL;
using OpenGL_Game.Components;
using OpenGL_Game.OBJLoader;
using OpenGL_Game.Objects;
using OpenGL_Game.Scenes;
using OpenTK.Mathematics;

namespace OpenGL_Game.Systems
{
    class SystemBallPath : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_VELOCITY);

        public SystemBallPath()
        {
        }

        public string Name
        {
            get { return "SystemBallPath"; }
        }

        public void OnAction(List<Entity> entities, List<Camera> cameras)
        {
            foreach (Entity entity in entities)
            {
                if ((entity.Mask & MASK) == MASK)
                {
                    List<IComponent> components = entities[0].Components;

                    IComponent positionComponent = components.Find(delegate (IComponent component)
                    {
                        return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                    });
                    IComponent velocityComponent = components.Find(delegate (IComponent component)
                    {
                        return component.ComponentType == ComponentTypes.COMPONENT_VELOCITY;
                    });

                    Motion((ComponentPosition)positionComponent, (ComponentVelocity)velocityComponent, entity.Name);
                }
            }
        }

        public void Motion(ComponentPosition position, ComponentVelocity velocity, string name)
        {
            if (name == "Ball1")
            {
                bool flag = true;
                while (flag)
                {
                    position.position = (-1.5f, 0, 43);
                    velocity.Velocity = (0, 0, 1);
                }
            }
        }
    }
}