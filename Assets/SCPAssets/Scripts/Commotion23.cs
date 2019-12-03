using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Events
{
    public class Commotion23 : CBEvent
    {
        IEnumerator story()
        {
            yield return new WaitForSeconds(2);
            GameInfo.CurrentGame.mainCharacher.ShakeHead(3);

        }
        public override void Entered(bool isPlayer, GameObject gameObject)
        {
            if (isPlayer == true)
            {
                isStartedEnter = true;
                StartCoroutine(story());
            }
        }
    }
}
