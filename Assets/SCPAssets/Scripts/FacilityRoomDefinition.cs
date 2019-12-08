using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Map
{

    public class FacilityRoomDefinition : MonoBehaviour
    {
        public List<Point2DD> TakeUpRooms;
        public List<FacilityRoomDefinition> AttachedRooms;
        public List<DoorDescription> pointsOfDoor;
        public List<LightDescription> pointsOfLight;
        [Serializable]
        public class DoorDescription
        {
            public bool isConnectDoor;
            public Transform point;
            public DoorTypes DoorType;
            public float targetWidthScale = 1.0f;
            public float targetHeightScale = 1.05f;
        }
        [Serializable]
        public class LightDescription
        {
            public Transform point;
            public float Range;
            public float Intensity;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

    }
    public enum DoorTypes
    {
        LCDoor,
        HCDoor,
        HeavyDoor
    }
    public enum RoomType
    {
        Tunnel,
        Room,
        AttachiveRoom
    }
}

