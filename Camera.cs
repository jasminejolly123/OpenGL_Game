﻿using OpenGL_Game.Scenes;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Xml;

namespace OpenGL_Game
{
    public class Camera
    {
        public Matrix4 view, projection;
        public Vector3 oldCameraPosition, cameraPosition, cameraDirection, cameraUp;
        private Vector3 targetPosition;
        public float cameraradius;
        public Vector3 oldOldCameraPosition;
        FrameEventArgs e;

        public Camera()
        {
            cameraPosition = new Vector3(0.0f, 0.0f, 0.0f);
            cameraDirection = new Vector3(0.0f, 0.0f, -1.0f);
            cameraUp = new Vector3(0.0f, 1.0f, 0.0f);
            UpdateView();
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), 1.0f, 0.1f, 100f);
        }

        public Camera(Vector3 cameraPos, Vector3 targetPos, float ratio, float near, float far)
        {
            cameraUp = new Vector3(0.0f, 1.0f, 0.0f);
            cameraPosition = cameraPos;
            cameraDirection = targetPos-cameraPos;
            cameraradius = 2;
            cameraDirection.Normalize();
            UpdateView();
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), ratio, near, far);
        }

        public void PutBack()
        {
            cameraPosition = oldCameraPosition;
        }

        public void MoveForward(float move)
        {
            oldCameraPosition = cameraPosition;
            cameraPosition += move*cameraDirection;
            UpdateView();
        }

        public void Translate(Vector3 move)
        {
            oldCameraPosition = cameraPosition;
            cameraPosition += move;
            UpdateView();
        }

        public void RotateY(float angle)
        {
            cameraDirection = Matrix3.CreateRotationY(angle) * cameraDirection;
            UpdateView();
        }

        public void UpdateView()
        {
            targetPosition = cameraPosition + cameraDirection;
            view = Matrix4.LookAt(cameraPosition, targetPosition, cameraUp);
            
            if (GameScene.dt % 5 == 0)
            {
                oldOldCameraPosition = cameraPosition;
            }
        }
    }
}
