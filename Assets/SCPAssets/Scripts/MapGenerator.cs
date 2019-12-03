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

}