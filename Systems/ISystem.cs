using OpenGL_Game.Objects;
using System.Collections.Generic;

namespace OpenGL_Game.Systems
{
    interface ISystem
    {
        void OnAction(List<Entity> entities, List<Camera> cameras, List<Drone> drones);

        // Property signatures: 
        string Name
        {
            get;
        }
    }
}
