using SCPCB.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Inventory
{

    public class CBInventoryItem : CBInteractive
    {
        public Sprite Icon;
        public string ID;
        public int AccessLevel;
        public bool isStorage;
        public virtual IEnumerator Use()
        {
            yield break;
        }
        public override IEnumerator Operation()
        {
            try
            {

                GameInfo.CurrentGame.Storages["Bag"].Put(this);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
            return base.Operation();
        }
    }

}