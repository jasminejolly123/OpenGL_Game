using OpenGL_Game.Components;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;
using OpenTK.Audio.OpenAL;
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
        public CollisionManager collisionManager;
        public float distance;
        public float radius;
        public float distance1;
        public float distance2;

        public SystemCollisionSphere(CollisionManager collisionManager)
        {
            this.collisionManager = collisionManager;
        }

        public string Name
        {
            get { return "SystemCollisionSphere"; }
        }

        public void OnAction(List<Entity> entities, List<Camera> cameras, List<Drone> drones)
        { 
            foreach (Entity entity in entities)
            {
                if ((entity.Mask & MASK) == MASK)
                {
                    List<IComponent> components = entity.Components;

                    IComponent posComponent = components.Find(delegate (IComponent component)
                    {
                        return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                    });
                    ComponentPosition position = (ComponentPosition) posComponent;

                    IComponent collComponent = components.Find(delegate (IComponent component)
                    {
                    return component.ComponentType == ComponentTypes.COMPONENT_COLLISION_SPHERE;
                    });
                    ComponentCollisionSphere collision = (ComponentCollisionSphere) collComponent;


                    foreach (Camera camera in cameras)
                    {
                        distance1 = (position.position.X - camera.cameraPosition.X) * (position.position.X - camera.cameraPosition.X);
                        distance2 = (position.position.Z - camera.cameraPosition.Z) * (position.position.Z - camera.cameraPosition.Z);
                        distance = distance1 + distance2;
                        distance = (float)Math.Sqrt(distance);

                        radius = collision.Radius + camera.cameraradius;

                        if (distance < radius)
                        {
                            if (entity.Name == "Key")
                            {
                                collisionManager.CollisionBetweenCamera(entity, COLLISIONTYPE.SPHERE_KEY);
                                position.position = (1000000000, 1000000000, 10000000);
                            }
                            else
                            {
                                collisionManager.CollisionBetweenCamera(entity, COLLISIONTYPE.SPHERE_SPHERE);
                            }
                        }
                    }
                }
            }
        }
    }
}
