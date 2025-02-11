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
    class SystemCollisionWall : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_COLLISION_WALL);

        public SystemCollisionWall()
        {
        }

        public string Name
        {
            get { return "SystemCollisionWall"; }
        }

        public void OnAction(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                if ((entity.Mask & MASK) == MASK)
                {
                    List<IComponent> components = entity.Components;

                    IComponent colComponent = components.Find(delegate (IComponent component)
                    {
                        return component.ComponentType == ComponentTypes.COMPONENT_COLLISION_WALL;
                    });
                    ComponentCollisionWall bb = ((ComponentCollisionWall)colComponent);


                }
            }
        }

        //public void Collision(Entity object1, Entity object2, ComponentPosition position1, ComponentPosition position2, ComponentCollisionSphere collision1, ComponentCollisionSphere collision2, ComponentVelocity velocity1, ComponentVelocity velocity2)
        //{
        //    if ((position1.Position - position2.Position).Length < collision1.Radius + collision2.Radius)
        //    {
        //        velocity1.Velocity = velocity1.Velocity * -1;
        //        velocity2.Velocity = velocity2.Velocity * -1;
        //    }
        //}
    }
}
