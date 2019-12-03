using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Events
{

    public class CBElevatorDetector : CBEvent
    {
        public bool isPlayerIn = false;
        public override void Exited(bool isPlayer,GameObject gameObject)
        {
            if (isPlayer)
            {
                isPlayerIn = false;
            }
        }
        public override void Entered(bool isPlayer,GameObject gameObject)
        {
            if (isPlayer)
            {
                isPlayerIn = true;
            }
        }
    }

}