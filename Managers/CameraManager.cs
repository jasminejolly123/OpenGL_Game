using System.Collections.Generic;
using OpenGL_Game.Objects;

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