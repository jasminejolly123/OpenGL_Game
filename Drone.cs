using OpenGL_Game.Components;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OpenGL_Game
{
    public class Drone
    {
        public Matrix4 view, projection;
        public Vector3 oldCameraPosition, cameraPosition, cameraDirection, cameraUp;
        private Vector3 targetPosition;
        public float cameraradius;
        List<IComponent> componentList = new List<IComponent>();
        ComponentTypes mask;

        public Drone()
        {
            cameraPosition = new Vector3(0.0f, 0.0f, 0.0f);
            cameraDirection = new Vector3(0.0f, 0.0f, -1.0f);
            cameraUp = new Vector3(0.0f, 1.0f, 0.0f);
            UpdateView();
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), 1.0f, 0.1f, 100f);
        }

        public Drone(Vector3 cameraPos, Vector3 targetPos, string geometry)
        {
            cameraUp = new Vector3(0.0f, 1.0f, 0.0f);
            cameraPosition = cameraPos;
            cameraDirection = targetPos - cameraPos;
            cameraradius = 2;
            cameraDirection.Normalize();
            UpdateView();
        }

        public void AddComponent(IComponent component)
        {
            Debug.Assert(component != null, "Component cannot be null");
            componentList.Add(component);
            mask |= component.ComponentType;
        }

        public ComponentTypes Mask
        {
            get { return mask; }
        }
        public List<IComponent> Components
        {
            get { return componentList; }
        }

        public void PutBack()
        {
            cameraPosition = oldCameraPosition;
        }

        public void MoveForward(float move)
        {
            oldCameraPosition = cameraPosition;
            cameraPosition += move * cameraDirection;
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
        }
    }
}
