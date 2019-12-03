using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB
{
    public class CBEntity : MonoBehaviour
    {
        public List<Action> actions=new List<Action>();
        public bool isPlayer;
        public float MaxHealth = 100;
        float realHealth=100;
        public float Health
        {
            get { return realHealth; } set {
                realHealth = value;
                if (value <= 0)
                {
                    BeforeDeath( GameObject.Instantiate(deathReplacement, this.transform.position,this.transform.rotation));
                    this.gameObject.SetActive(false);
                }
            }
        }
        public GameObject deathReplacement;
        public virtual void BeforeDeath(GameObject Replacement)
        {

        }
        public virtual void SideStart() { }
        void Start()
        {
            Health = MaxHealth;
            SideStart();    
        }
    }

}