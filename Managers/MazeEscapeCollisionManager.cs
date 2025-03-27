using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using OpenGL_Game.Components;
using OpenGL_Game.Objects;
using OpenGL_Game.Scenes;

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
                if (collision.collisionType == COLLISIONTYPE.LINE_SPHERE_SPECIAL1)
                {
                    foreach (Camera camera in cameraManager.cameraList)
                    {
                        camera.cameraPosition.Z = camera.cameraPosition.Z + 1;
                    }
                }
                if (collision.collisionType == COLLISIONTYPE.LINE_SPHERE_SPECIAL2)
                {
                    foreach (Camera camera in cameraManager.cameraList)
                    {
                        camera.cameraPosition.Z = camera.cameraPosition.Z - 1;
                    }
                }
                if (collision.collisionType == COLLISIONTYPE.LINE_SPHERE_SPECIAL3)
                {
                   foreach(Camera camera in cameraManager.cameraList)
                    {
                        camera.cameraPosition.X = camera.cameraPosition.X + 1;
                    }
                }
                if (collision.collisionType == COLLISIONTYPE.LINE_SPHERE_SPECIAL4)
                {
                    foreach (Camera camera in cameraManager.cameraList)
                    {
                        camera.cameraPosition.X = camera.cameraPosition.X - 1;
                    }
                }
                if (collision.collisionType == COLLISIONTYPE.SPHERE_SPHERE)
                {
                    foreach (Camera camera in cameraManager.cameraList)
                    {
                        camera.cameraPosition = (-5, 1.5f, 7);
                        GameScene.lives = GameScene.lives - 1;
                    }
                }
                if (collision.collisionType == COLLISIONTYPE.SPHERE_KEY)
                {
                    foreach (Camera camera in cameraManager.cameraList)
                    {
                        GameScene.keys = GameScene.keys + 1;
                    }
                }

            }

            ClearManifold();
        }
    }
}
