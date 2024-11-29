using OpenGL_Game.Objects;
using System.Collections.Generic;

namespace OpenGL_Game.Managers
{


    enum COLLISIONTYPE
    {
        SPHERE_SPHERE,
        LINE_SPHERE
    }

    struct Collision
    {
        public Entity entity;
        public COLLISIONTYPE collisionType;
    }

    abstract class CollisionManager
    {
        protected List<Collision> collisions = new List<Collision>();
        public CollisionManager()
        {
        }
        public abstract void ProcessCollisions();
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

