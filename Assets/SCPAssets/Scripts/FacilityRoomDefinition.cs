using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB
{
    public class FacilityRoomDefinition : MonoBehaviour
    {
        public List<DoorDescription> pointsOfDoor;
        public List<LightDescription> pointsOfLight;
        [Serializable]
        public class DoorDescription
        {
            public Transform point;
            public string DoorKind;
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

        // Update is called once per frame
        void Update()
        {

        }
    }
}

