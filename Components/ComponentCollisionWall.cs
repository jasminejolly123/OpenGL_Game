using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Components
{
    class ComponentCollisionWall : IComponent
    {
        float radius;

        public ComponentCollisionWall(float radius)
        {
            this.radius = radius;
        }

        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_COLLISION_WALL; }
        }
    }
}