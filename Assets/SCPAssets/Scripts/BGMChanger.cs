using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Events
{
    public class BGMChanger : CBEvent
    {
        public AudioSource source;
        public AudioClip clip;
        public override void Entered(bool isPlayer, GameObject gameObject)
        {
            if (isPlayer == true)
            {
                if (source.clip != clip)
                {
                    source.clip = clip;
                    source.Play();
                }
            }
        }
    }
}
