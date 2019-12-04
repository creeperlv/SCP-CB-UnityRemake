using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB
{

    public class MapGenerator : MonoBehaviour
    {
        //Incomplete map generator.
        //Currently only generates lights.
        public GameObject LightContainer;
        public GameObject LightPerfab;
        void Start()
        {
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