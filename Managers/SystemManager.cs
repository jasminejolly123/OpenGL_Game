using System.Collections.Generic;
using OpenGL_Game.Systems;
using OpenGL_Game.Objects;

namespace OpenGL_Game.Managers
{
    class SystemManager
    {
        List<ISystem> systemList = new List<ISystem>();

        public SystemManager()
        {
        }

        public void ActionSystems(EntityManager entityManager, CameraManager cameraManager)
        {
            List<Entity> entityList = entityManager.Entities();
            List<Camera> cameraList = cameraManager.Cameras();
            foreach (ISystem system in systemList)
            {
                system.OnAction(entityList, cameraList);
            }
        }

        public void AddSystem(ISystem system)
        {
            //ISystem result = FindSystem(system.Name);
            //Debug.Assert(result != null, "System '" + system.Name + "' already exists");
            systemList.Add(system);
        }

        private ISystem FindSystem(string name)
        {
            return systemList.Find(delegate(ISystem system)
            {
                return system.Name == name;
            }
            );
        }
    }
}
