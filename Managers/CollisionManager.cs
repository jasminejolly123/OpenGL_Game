using OpenGL_Game.Components;
using OpenGL_Game.Objects;
using System.Collections.Generic;

namespace OpenGL_Game.Managers
{


    public enum COLLISIONTYPE
    {
        SPHERE_SPHERE,
        LINE_SPHERE,
        LINE_SPHERE_SPECIAL1,
        LINE_SPHERE_SPECIAL2,
        LINE_SPHERE_SPECIAL3,
        LINE_SPHERE_SPECIAL4,
    }

    public struct Collision
    {
        public Entity entity;
        public COLLISIONTYPE collisionType;
    }

    
    public abstract class CollisionManager
    {
        public Camera camera;
        protected List<Collision> collisionManifold = new List<Collision>();
        public CollisionManager() { }
        public void ClearManifold() { collisionManifold.Clear(); }
        public void CollisionBetweenCamera(Entity entity, COLLISIONTYPE collisionType)
        {
            // If we already have this collision in the manifold then do not add it
            foreach (Collision coll in collisionManifold)
            {
                if (coll.entity == entity) return;
            }
            Collision collision;
            collision.entity = entity;
            collision.collisionType = collisionType;
            collisionManifold.Add(collision);
        }

        public abstract void ProcessCollisions(CameraManager cameraManager);
    }

    

    //public bool SphereCollision(Entity object1, Entity object2)
    //{
    //    // Simple spherical collsion detection
    //    if (object1.isCollidable && object2.isCollidable)
    //    {
    //        if ((object1.position - object2.position).Length < object1.collisionRadius + object2.collisionRadius)
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    //public bool PlaneCollision(Entity object1, Entity object2)
    //{
    //    // There would be an implementation of player agianst maze

    //    return false;
    //}
}

