using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Managers
{
    public class MazeEscapeCollisionManager : CollisionManager
    {
        public override void ProcessCollisions(CameraManager cameraManager)
        {
            //Collision collision;
            //if (collision.collisionType == COLLISIONTYPE.LINE_SPHERE)
            //{

            //}
            //else
            //{
            //    foreach (Camera camera in cameraManager.cameraList)
            //    {
            //        camera.PutBack();
            //    }
            //}
            foreach (Camera camera in cameraManager.cameraList)
            {
                camera.PutBack();
            }
        }
    }
}
