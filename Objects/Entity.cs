using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenGL_Game.Components;
using OpenGL_Game.OBJLoader;
using OpenTK.Mathematics;

namespace OpenGL_Game.Objects
{
    class Entity
    {
        string name;
        List<IComponent> componentList = new List<IComponent>();
        ComponentTypes mask;
        public Vector3 position;
        public Vector3 velocity;
        public Geometry geometry;
        public bool isCollidable;
        public float collisionRadius;

        public Entity (string name, ComponentPosition position, ComponentVelocity velocity, Geometry geometry,
                bool isCollidable, float collisionRadius)
        {
            this.name = name;
            this.position = position.position;
            this.velocity = velocity.Velocity;
            this.geometry = geometry;
            this.isCollidable = isCollidable;
            this.collisionRadius = collisionRadius;
        }

        /// <summary>Adds a single component</summary>
        public void AddComponent(IComponent component)
        {
            Debug.Assert(component != null, "Component cannot be null");

            componentList.Add(component);
            mask |= component.ComponentType;
        }

        public String Name
        {
            get { return name; }
        }

        public ComponentTypes Mask
        {
            get { return mask; }
        }

        public List<IComponent> Components
        {
            get { return componentList; }
        }

        
    }
}
