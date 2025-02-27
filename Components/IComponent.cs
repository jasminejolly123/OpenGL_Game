using System;

namespace OpenGL_Game.Components
{
    [FlagsAttribute]
    public enum ComponentTypes {
        COMPONENT_NONE     = 0,
	    COMPONENT_POSITION = 1 << 0,
        COMPONENT_GEOMETRY = 1 << 1,
        COMPONENT_VELOCITY = 1 << 2,
        COMPONENT_COLLISION_SPHERE = 1 << 3,
        COMPONENT_COLLISION_WALL = 1 << 4,
        COMPONENT_SCALE = 1 << 5,
    }

    public interface IComponent
    {
        ComponentTypes ComponentType
        {
            get;
        }
    }
}
