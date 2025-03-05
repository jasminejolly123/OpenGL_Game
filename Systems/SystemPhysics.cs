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

namespace OpenGL_Game.Systems
{
    class SystemPhysics : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_VELOCITY);

        public SystemPhysics()
        {
        }

        public string Name
        {
            get { return "SystemPhysics"; }
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

                    Motion((ComponentPosition)positionComponent, (ComponentVelocity)velocityComponent, entity.Name);
                }
            }
        }

        public void Motion(ComponentPosition position, ComponentVelocity velocity, string name)
        {
            position.position = position.position + velocity.Velocity * GameScene.dt;

            if (name == "Ball1")
            {
                velocity.Velocity = (0, 0, 1);

                if (position.position.Z >= 53)
                {
                    velocity.Velocity = (-1, 0, 0);
                }
                if (position.position.X <= -13)
                {
                    velocity.Velocity = (0, 0, -1);
                }
                if (position.position.Z <= 43 && position.Position.X < -1.5f)
                {
                    velocity.Velocity = (1, 0, 0);
                }

            }

            if (name == "Ball2")
            {
                velocity.Velocity = (1, 0, 0);
            }
        }
    }
}
