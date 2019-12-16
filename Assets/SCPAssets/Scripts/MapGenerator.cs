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
            StartCoroutine(Run());
        }
        IEnumerator Run()
        {
            yield return new WaitForSeconds(0.1f);
            if (Application.isEditor)
                CBRandom.SetSeed(DebugRandomSeed);
            {
                int MAX_ROOMS = CBRandom.NextInt(20, 50);
                GameInfo.ConsoleText += "\r\nMap Generator Running...";
                GameInfo.ConsoleText += "\r\n|MAX_ROOMS:" + MAX_ROOMS;
                MapNode LastMapNode = null;
                Point2DD p = new Point2DD();
                for (int i = 0; i < MAX_ROOMS; i++)
                {
                    {
                        int id = CBRandom.NextInt(0, GameInfo.Rooms.Count);
                        GameInfo.ConsoleText += $"\r\n|Target ID:{id}";
                        if (i == 0)
                        {
                            var r = GameInfo.Rooms.ElementAt(id).Value.GetComponent<FacilityRoomDefinition>();
                            p = new Point2DD();
                            p.X = 25;
                            p.Y = 25;
                            LastMapNode = new MapNode() { room = r, rotation = RoomRotation.Zero };
                            GameInfo.CurrentGame.MainMap.Add(p, LastMapNode);
                            GameInfo.ConsoleText += $"\r\n|ROOM 00:{r.name}|{RoomRotation.Zero.ToString()}";
                        }
                        else
                        {
                            foreach (var item in LastMapNode.room.pointsOfDoor)
                            {
                                switch (item.Position)
                                {
                                    case FacilityRoomDefinition.DoorPosition.Forward:
                                        {
                                            
                                            p.Y +=1;
                                            var r = GameInfo.Rooms.ElementAt(id).Value.GetComponent<FacilityRoomDefinition>();
                                            LastMapNode = new MapNode() { room = r, rotation = RoomRotation.Zero };
                                            GameInfo.CurrentGame.MainMap.Add(p, LastMapNode);
                                            GameInfo.ConsoleText += $"\r\n|ROOM 00:{r.name}|{RoomRotation.Zero.ToString()}";
                                        }
                                        break;
                                    case FacilityRoomDefinition.DoorPosition.Backward:
                                        break;
                                    case FacilityRoomDefinition.DoorPosition.Right:
                                        break;
                                    case FacilityRoomDefinition.DoorPosition.Left:
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            foreach (var item in GameInfo.CurrentGame.MainMap)
            {
                var room = Instantiate(item.Value.room, this.transform, false);
                var lp = room.transform.localPosition;
                lp.z = (item.Key.Y - 25) * 12;
                lp.x = (item.Key.X - 25) * 12;
                room.transform.localPosition = lp;
            }
            var rooms = transform.GetComponentsInChildren<FacilityRoomDefinition>();
            foreach (var item in rooms)
            {
                var lights = item.pointsOfLight;
                foreach (var light in lights)
                {
                    var a = GameObject.Instantiate(LightPerfab, LightContainer.transform);
                    a.transform.position = light.point.position;
                    a.GetComponent<Light>().range = light.Range;
                    a.GetComponent<Light>().intensity = light.Intensity;
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