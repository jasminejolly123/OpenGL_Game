using OpenGL_Game.Components;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL_Game.Scenes;

namespace OpenGL_Game.Systems
{
    class SystemCollisionWall : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_COLLISION_WALL);
        public Camera camera;
        public CollisionManager collisionManager;
        //public bool[] keysPressed = new bool[500];
        public SystemCollisionWall()
        {

        }

        public string Name
        {
            get { return "SystemCollisionWall"; }
        }

        public void OnAction(List<Entity> entities, List<Camera> cameras)
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

                    foreach (Camera camera in cameras)
                    {
                        if (camera.cameraPosition.Y > 2)
                        {
                            camera.cameraPosition.Y = camera.cameraPosition.Y - 0.5f;
                        }
                        if (camera.cameraPosition.Y < 1)
                        {
                            camera.cameraPosition.Y = camera.cameraPosition.Y + 0.5f;
                        }

                        if (bb.Type == 1)
                        {
                            if (camera.cameraPosition.X >= bb.MaxX)
                            {
                                camera.cameraPosition.X = camera.cameraPosition.X - 1;
                            }
                            if (camera.cameraPosition.X <= bb.MinX)
                            {
                                camera.cameraPosition.X = camera.cameraPosition.X + 1;
                            }
                            if (camera.cameraPosition.Z >= bb.MinZ)
                            {
                                camera.cameraPosition.Z = camera.cameraPosition.Z - 1;
                            }
                            if (camera.cameraPosition.Z <= bb.MaxZ)
                            {
                                camera.cameraPosition.Z = camera.cameraPosition.Z + 1;
                            }
                            //collisionManager.CollisionBetweenCamera(entity, COLLISIONTYPE.LINE_SPHERE);
                        }
                        if (bb.Type == 2)
                        {
                            if (camera.cameraPosition.X <= bb.MaxX && camera.cameraPosition.X >= bb.MinX && camera.cameraPosition.Z <= bb.MaxZ && camera.cameraPosition.Z >= bb.MinZ)
                            {

                                camera.PutBack();

                                //collisionManager.CollisionBetweenCamera(entity, COLLISIONTYPE.LINE_SPHERE);
                                //if (camera.cameraPosition.X < bb.MaxX)
                                //{
                                //    camera.cameraPosition.X += 5;
                                //}
                                //if (camera.cameraPosition.X == bb.MinX)
                                //{
                                //    camera.cameraPosition.X += 5;
                                //}
                                //if (camera.cameraPosition.Z == bb.MaxZ)
                                //{
                                //    camera.cameraPosition.Z -= 5;
                                //}
                                //if (camera.cameraPosition.Z == bb.MinZ)
                                //{
                                //    camera.cameraPosition.Z += 5;
                                //}
                            }
                        }
                        if (bb.Type == 3)
                        {
                            if (camera.cameraPosition.X <= bb.MaxX && camera.cameraPosition.X > bb.MinX)
                            {
                                if (camera.cameraPosition.Z <= bb.MinZ)
                                {
                                    camera.cameraPosition.Z = camera.cameraPosition.Z + 1;
                                }
                                if(camera.cameraPosition.Z >= bb.MaxZ)
                                {
                                    camera.cameraPosition.Z = camera.cameraPosition.Z - 1;
                                }
                            }
                        }
                        if (bb.Type == 4)
                        {
                            if (camera.cameraPosition.Z <= bb.MaxZ && camera.cameraPosition.Z > bb.MinZ)
                            {
                                if (camera.cameraPosition.X <= bb.MinX)
                                {
                                    camera.cameraPosition.X = camera.cameraPosition.X + 1;
                                }
                                if (camera.cameraPosition.X >= bb.MaxX)
                                {
                                    camera.cameraPosition.X = camera.cameraPosition.X - 1;
                                }
                            }
                        }

                    }

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
