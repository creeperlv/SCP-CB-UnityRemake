using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Events
{

    public class WarheadControl : CBInteractive
    {
        public Animator Animator;
        public AudioSource sfx;

        public override IEnumerator Operation()
        {
            if (GameInfo.CurrentGame.isWarheadClosed == false)
            {
                Animator.SetTrigger("Off");
                yield return new WaitForSeconds(0.6f);
                sfx.Play();
                yield return new WaitForSeconds(0.4f);
                GameInfo.CurrentGame.isWarheadClosed = true;
            }

            yield break;
        }
    }

}