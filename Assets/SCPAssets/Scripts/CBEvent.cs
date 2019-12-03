using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Events
{

    public class CBEvent : MonoBehaviour
    {
        [HideInInspector]
        public bool isStartedEnter = false;
        [HideInInspector]
        public bool isStartedExit = false;
        public virtual void Entered(bool isPlayer,GameObject gameObject)
        {

        }
        public virtual void Exited(bool isPlayer, GameObject gameObject)
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<CBEntity>() != null)
            {
                //Judge if player entered the area.
                if (isStartedEnter == false) 
                Entered(other.gameObject.GetComponent<CBEntity>().isPlayer,other.gameObject);
            }

        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<CBEntity>() != null)
            {
                //Judge if player entered the area.
                if (isStartedEnter == false)
                    Entered(collision.gameObject.GetComponent<CBEntity>().isPlayer,collision .gameObject);
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.GetComponent<CBEntity>() != null)
            {
                //Judge if player entered the area.
                if (isStartedExit == false)
                    Exited(collision.gameObject.GetComponent<CBEntity>().isPlayer,  collision.gameObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<CBEntity>() != null)
            {
                //Judge if player entered the area.
                if (isStartedExit == false)
                    Exited(other.gameObject.GetComponent<CBEntity>().isPlayer,other.gameObject);
            }
        }
    }

}