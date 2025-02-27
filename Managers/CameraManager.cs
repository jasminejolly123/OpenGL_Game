using System.Collections.Generic;
using OpenGL_Game.Objects;
//using System.Diagnostics;

namespace OpenGL_Game.Managers
{
    public class CameraManager
    {
        public List<Camera> cameraList;

        public CameraManager()
        {
            cameraList = new List<Camera>();
        }

        public void AddCamera(Camera camera)
        {
            //Entity result = FindEntity(entity.Name);
            //Debug.Assert(result == null, "Entity '" + entity.Name + "' already exists");
            cameraList.Add(camera);
        }

        public Camera FindCamera(string name)
        {
            return cameraList[0];
        }

        public List<Camera> Cameras()
        {
            return cameraList;
        }
    }
}