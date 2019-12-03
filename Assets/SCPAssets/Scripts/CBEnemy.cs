using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SCPCB.Entities.Enemy
{

    public class CBEnemy : CBEntity
    {
        public GameObject Target;
        public float AttackRange = 1.0f;
        
        public virtual void Attack()
        {

        }
        public virtual void SideStart02()
        {

        }
        [HideInInspector]
        public NavMeshAgent agent;
        [HideInInspector]
        public Rigidbody body;
        public override void SideStart()
        {

            agent=GetComponent<NavMeshAgent>();
            body = GetComponent<Rigidbody>();
            SideStart02();
        }
        public virtual bool SideUpdate01()
        {
            return true;
        }
        public void Update()
        {
            agent.SetDestination(Target.transform.position);
            if (SideUpdate01())
            {
                Debug.Log("DIstance:"+ Vector3.Distance(Target.transform.position, this.transform.position));
                if (Vector3.Distance(Target.transform.position, this.transform.position) <= AttackRange) {
                    Debug.Log("Attack!");
                    Attack();
                }
                //body.isKinematic = false;
                agent.isStopped = false;
            }
            else
            {
                body.velocity = Vector3.zero;
                agent.isStopped = true;
                //body.Sleep();
                //body.isKinematic = true;
                //body.sto
                body.velocity = new Vector3(0, 0, 0);

            }

        }
    }

}