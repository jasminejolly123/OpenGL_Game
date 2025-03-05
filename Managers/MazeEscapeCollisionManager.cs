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
            foreach(Collision collision in collisionManifold)
            {
                if (collision.collisionType == COLLISIONTYPE.LINE_SPHERE)
                {
                    foreach (Camera camera in cameraManager.cameraList)
                    {
                        camera.PutBack();
                    }
                }
                //else
                //{
                //    foreach (Camera camera in cameraManager.cameraList)
                //    {
                //        camera.PutBack();
                //    }
                //}
            }

            
            //foreach (Camera camera in cameraManager.cameraList)
            //{
            //    camera.PutBack();
            //}

            ClearManifold();
        }
    }
}
