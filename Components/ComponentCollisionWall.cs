using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Components
{
    class ComponentCollisionWall : IComponent
    {
        float minX;
        float maxX;
        float minZ;
        float maxZ;
        int type;

        public ComponentCollisionWall(float minX, float maxX, float minZ, float maxZ, int type)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.minZ = minZ;
            this.maxZ = maxZ;
            this.type = type;
        }

        public float MinX
        {
            get { return minX; }
            set { minX = value; }
        }
        public float MaxX
        {
            get { return maxX; }
            set { maxX = value; }
        }
        public float MinZ
        {
            get { return minZ; }
            set { minZ = value; }
        }
        public float MaxZ
        {
            get { return maxZ; }
            set { maxZ = value; }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_COLLISION_WALL; }
        }
    }
}