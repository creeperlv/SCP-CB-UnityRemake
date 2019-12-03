using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Events
{

    public class FootStepChanger : CBEvent
    {
        public string TargetFootStep;
        public override void Entered(bool isPlayer, GameObject gameObject)
        {
            if (isPlayer == true)
            {
                GameInfo.CurrentGame.mainCharacher.ChangeFootStep(TargetFootStep);
                
            }
        }
    }

}