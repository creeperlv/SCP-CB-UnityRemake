using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Events
{
    public class TriggerAudio : CBEvent
    {
        public AudioSource AS;
        public AudioClip clip;
        [Tooltip(tooltip: "By checking this, it will play only one time.\r\n勾选此项，将会只播放一次")]
        public bool onlyOnce = true;
        public override void Entered(bool isPlayer, GameObject gameObject)
        {
            if (isPlayer == true)
            {

                if (onlyOnce == true)
                {
                    isStartedEnter = true;
                }
                AS.clip = clip;
                AS.Play();
            }
        }
    }

}