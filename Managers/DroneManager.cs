using System.Collections.Generic;
using OpenGL_Game.Objects;

namespace OpenGL_Game.Managers
{
    public class DroneManager
    {
        public List<Drone> droneList;

        public DroneManager()
        {
            droneList = new List<Drone>();
        }

        public void AddDrone(Drone drone)
        {
            droneList.Add(drone);
        }

        public Drone FindDone(string name)
        {
            return droneList[0];
        }

        public List<Drone> Drones()
        {
            return droneList;
        }
    }
}