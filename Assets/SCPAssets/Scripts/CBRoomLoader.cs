using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Map
{
    public class CBRoomLoader : MonoBehaviour,IStartup
    {
        public List<RoomInfo> rooms = new List<RoomInfo>();
        public bool isDebug = false;
        public void Load()
        {
            LoadRoom();
        }

        // Start is called before the first frame update
        void Start()
        {
            if (isDebug) LoadRoom();
        }
        void LoadRoom()
        {
            GameInfo.Rooms = new Dictionary<string, GameObject>();
            foreach (var item in rooms)
            {
                GameInfo.Rooms.Add(item.ID, item.Room);
            }
        }

    }
    [System.Serializable]
    public class RoomInfo
    {
        public string ID;
        public GameObject Room;
    }
}