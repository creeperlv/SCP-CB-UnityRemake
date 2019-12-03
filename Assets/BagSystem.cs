using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SCPCB.Inventory
{
    public class BagSystem : MonoBehaviour
    {
        public string StorageID;
        public Sprite Blank;
        public List<Button> Items;
        void Start()
        {
        }
        void InitArray(ref bool[]Arr)
        {
            for (int i = 0; i < Arr.Length; i++)
            {
                Arr[i] = false;
            }
        }
        void Update()
        {
            bool [] Fetched = new bool[8];
            InitArray(ref Fetched);
            foreach (var item in GameInfo.CurrentGame.Storages["Bag"].items)
            {
                //Debug.Log(item.Value);
                var p=item.Key;
                int i = (p.X) + 4 *( p.Y);
                Debug.Log(i);
                Fetched[i] = true;
                Items[i].transform.GetChild(0).GetComponent<Image>().sprite = GameInfo.Items[item.Value].Icon;
            }
            for (int i = 0; i < Fetched.Length; i++)
            {
                if (Fetched[i] == false)
                {
                    Items[i].transform.GetChild(0).GetComponent<Image>().sprite = Blank;
                }
            }
        }
    }

}