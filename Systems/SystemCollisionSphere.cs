using OpenGL_Game.Components;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Systems
{
    class SystemCollisionSphere : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_COLLISION_SPHERE);
        public Camera camera;

        public SystemCollisionSphere()
        {
        }

        public string Name
        {
            get { return "SystemCollisionSphere"; }
        }

        public void OnAction(List<Entity> entities, List<Camera> cameras)
        {
            //if ((entities[0].Mask & entities[1].Mask) == MASK)
            //{
            //    List<IComponent> components1 = entities[0].Components;
            //    List<IComponent> components2 = entities[1].Components;

            //    IComponent collComponent1 = components1.Find(delegate (IComponent component)
            //    {
            //        return component.ComponentType == ComponentTypes.COMPONENT_COLLISION_SPHERE;
            //    });
            //    IComponent collComponent2 = components2.Find(delegate (IComponent component)
            //    {
            //        return component.ComponentType == ComponentTypes.COMPONENT_COLLISION_SPHERE;
            //    });

            //    ComponentCollisionSphere collision1 = (ComponentCollisionSphere) collComponent1;
            //    ComponentCollisionSphere collision2 = (ComponentCollisionSphere) collComponent2;

            //    IComponent posComponent1 = components1.Find(delegate (IComponent component)
            //    {
            //        return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
            //    });
            //    IComponent posComponent2 = components2.Find(delegate (IComponent component)
            //    {
            //        return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
            //    });

            //    ComponentPosition position1 = (ComponentPosition) posComponent1;
            //    ComponentPosition position2 = (ComponentPosition)posComponent1;

            //    IComponent velComponent1 = components1.Find(delegate (IComponent component)
            //    {
            //        return component.ComponentType == ComponentTypes.COMPONENT_VELOCITY;
            //    });
            //    IComponent velComponent2 = components2.Find(delegate (IComponent component)
            //    {
            //        return component.ComponentType == ComponentTypes.COMPONENT_VELOCITY;
            //    });

            //    ComponentVelocity velocity1 = (ComponentVelocity) velComponent1;
            //    ComponentVelocity velocity2 = (ComponentVelocity) velComponent2;

            //    Collision(entities[0], entities[1], position1, position2, collision1, collision2, velocity1, velocity2);

            //}

            foreach (Entity entity in entities)
            {
                if ((entity.Mask & MASK) == MASK)
                {
                    List<IComponent> components = entity.Components;

                    IComponent posComponent = components.Find(delegate (IComponent component)
                    {
                        return component.ComponentType == ComponentTypes.COMPONENT_COLLISION_WALL;
                    });
                    ComponentCollisionSphere position = (ComponentCollisionSphere) posComponent;

                    IComponent collComponent = components.Find(delegate (IComponent component)
                    {
                    return component.ComponentType == ComponentTypes.COMPONENT_COLLISION_SPHERE;
                    });
                    ComponentCollisionSphere collision = (ComponentCollisionSphere) collComponent;

                    IComponent velComponent = components.Find(delegate (IComponent component)
                    {
                        return component.ComponentType == ComponentTypes.COMPONENT_VELOCITY;
                    });
                    ComponentVelocity velocity1 = (ComponentVelocity) velComponent;

                    //Collision()
                }
            }
        }

        public void Collision(ComponentPosition position, ComponentCollisionSphere collision, ComponentVelocity velocity)
        {
            if ((position.Position - camera.cameraPosition).Length < collision.Radius + 2)
            {
                //camera.cameraVelocity = velocity1.Velocity * -1;
                //velocity2.Velocity = velocity2.Velocity * -1;
            }
        }
    }
}
