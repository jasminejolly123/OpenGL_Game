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
    class SystemCollisionSphere
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_COLLISION_SPHERE);

        public SystemCollisionSphere()
        {
        }

        public string Name
        {
            get { return "SystemCollisionSphere"; }
        }

        public void OnAction(Entity object1, Entity object2)
        {
            if ((object1.Mask & object2.Mask) == MASK)
            {
                List<IComponent> components1 = object1.Components;
                List<IComponent> components2 = object2.Components;

                IComponent collComponent1 = components1.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_COLLISION_SPHERE;
                });
                IComponent collComponent2 = components2.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_COLLISION_SPHERE;
                });

                ComponentCollisionSphere collision1 = (ComponentCollisionSphere) collComponent1;
                ComponentCollisionSphere collision2 = (ComponentCollisionSphere) collComponent2;

                IComponent posComponent1 = components1.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });
                IComponent posComponent2 = components2.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });

                ComponentPosition position1 = (ComponentPosition) posComponent1;
                ComponentPosition position2 = (ComponentPosition)posComponent1;

                IComponent velComponent1 = components1.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_VELOCITY;
                });
                IComponent velComponent2 = components2.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_VELOCITY;
                });

                ComponentVelocity velocity1 = (ComponentVelocity) velComponent1;
                ComponentVelocity velocity2 = (ComponentVelocity) velComponent2;

                Collision(object1, object2, position1, position2, collision1, collision2, velocity1, velocity2);

            }
        }

        public void Collision(Entity object1, Entity object2, ComponentPosition position1, ComponentPosition position2, ComponentCollisionSphere collision1, ComponentCollisionSphere collision2, ComponentVelocity velocity1, ComponentVelocity velocity2)
        {
            if ((position1.Position - position2.Position).Length < collision1.Radius + collision2.Radius)
            {
                velocity1.Velocity = velocity1.Velocity * -1;
                velocity2.Velocity = velocity2.Velocity * -1;
            }
        }
    }
}
