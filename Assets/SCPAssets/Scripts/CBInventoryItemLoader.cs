using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Inventory
{
    public class CBInventoryItemLoader : MonoBehaviour, IStartup
    {
        public List<CBInventoryItem> ItemsToLoad;
        public bool Debug = false;
        public void Start()
        {
            if(Debug)
            LoadItems();
        }
        public void Load()
        {
            LoadItems();
        }
        public void LoadItems()
        {
            GameInfo.Items = new Dictionary<string, CBInventoryItem>();
            foreach (var item in ItemsToLoad)
            {
                GameInfo.Items.Add(item.ID, item);
            }
        }
    }

}