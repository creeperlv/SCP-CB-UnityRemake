using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace SCPCB.Entities.Enemy
{

    public class CBEnemy173 : CBEnemy
    {

        bool Spotted = false;

        public AudioSource Horror;
        public List<AudioClip> Snaps;
        bool canAttack = false;
        float AttackTimed = 0;
        public override void Attack()
        {
            if (AttackTimed > 0.2)
            { 
                if (canAttack == false)
                {
                    Horror.clip = Snaps[Random.Range(0, Snaps.Count)];
                    Horror.Play();
                }
                AttackTimed = 0;
            }
            AttackTimed += Time.deltaTime;

        }
        public bool IsInView(Vector3 worldPos)
        {
            Transform camTransform = Camera.main.transform;
            Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
            Vector3 dir = (worldPos - camTransform.position).normalized;
            float dot = Vector3.Dot(camTransform.forward, dir);

            if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
                return true;
            else
                return false;
        }
        float Timed = 0f;
        public override bool SideUpdate01()
        {
            if (Timed > 3)
            {

                if (actions.Count != 0)
                {
                    for (int i = 0; i < actions.Count; i++)
                    {
                        actions[i]();
                    }

                }
                Timed = 0;
            }
            Timed += Time.deltaTime;
            Ray r = new Ray(Target.transform.position, this.transform.position - Target.transform.position);

            RaycastHit h;
            if (Physics.Raycast(r, out h, 20, 1 << 0, QueryTriggerInteraction.Ignore))
            {
                //Camera.main.On
                //Debug.Log("" + Vector3.Angle(Target.transform.forward, (this.transform.position-Target.transform.position).normalized));
                //return false;
                //if (Vector3.Angle(Target.transform.forward, this.transform.position.normalized) > 90 && Vector3.Angle(Target.transform.forward, this.transform.position.normalized) < 160)
                if (Vector3.Angle(Target.transform.forward, (this.transform.position - Target.transform.position).normalized) < 60)
                {
                    //Debug.Log("Fetching..." + h.collider.gameObject.name + " ... ");// + Vector3.Distance(h.collider.gameObject.transform.position, this.transform.position));
                    if (h.collider.gameObject == this.gameObject)
                    {
                        if (GameInfo.CurrentGame.mainCharacher.isBlinking == true)
                        {
                            //Debug.Log("BLINKING");
                            return true;
                        }
                        AttackTimed = 0;
                        //Debug.Log("STOP");
                        return false;
                    }
                    else
                    {
                        //Debug.Log("Not me! Hahaha!");
                    }
                }
            }
            if (Vector3.Distance(Target.transform.position, this.transform.position) < 0.5)
            {
                return false;
            }
            return true;
            //bool res =
            //IsInView(this.transform.position);
            //if (GameInfo.CurrentGame.mainCharacher.isBlinking)
            //{
            //    return true;
            //}
            //Spotted = false;
            //return !res;
        }
    }

}