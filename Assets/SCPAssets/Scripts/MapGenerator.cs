using SCPCB.Map;
using SCPCB.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace SCPCB
{

    public class MapGenerator : MonoBehaviour
    {
        //Incomplete map generator.
        //Currently only generates lights.
        public GameObject LightContainer;
        public GameObject LightPerfab;
        public string DebugRandomSeed;
        void Start()
        {
            if(Application.isEditor)
            CBRandom.SetSeed(DebugRandomSeed);
            {
                int MAX_ROOMS = CBRandom.NextInt(20, 50);

                for (int i = 0; i < MAX_ROOMS; i++)
                {
                    {
                        int id=CBRandom.NextInt(0, GameInfo.Rooms.Count);
                        if (i == 0)
                        {
                            Point2DD p = new Point2DD();
                            p.X = 25;
                            p.Y = 25;
                            GameInfo.CurrentGame.MainMap.Add(p, new MapNode() { room = GameInfo.Rooms.ElementAt(id).Value.GetComponent<FacilityRoomDefinition>(), rotation= RoomRotation.Zero });
                        }
                        else
                        {

                        }
                    }
                }
            }
            var rooms = transform.GetComponentsInChildren<FacilityRoomDefinition>();
            foreach (var item in rooms)
            {
                var lights = item.pointsOfLight;
                foreach (var light in lights)
                {
                    var a=GameObject.Instantiate(LightPerfab, LightContainer.transform);
                    a.transform.position = light.point.position;
                    a.GetComponent<Light>().range = light.Range;
                    a.GetComponent<Light>().intensity= light.Intensity;
                }
            }
            int MAX_SIZE = 50;
            for (int i = 0; i < MAX_SIZE; i++)
            {
                int RoomID = CBRandom.NextInt(0, GameInfo.Rooms.Count);

            }
        }
    }
    public enum RoomRotation
    {
        Zero=0,
        Ninty=1,
        UpsideDown=2,
        Ninty_UpsideDown=3,
    }
    [System.Serializable]
    public class MapNode
    {
        public Point2DD location;
        public RoomRotation rotation;
        public bool isUsedByOtherRoom;
        public FacilityRoomDefinition room;
    }
}