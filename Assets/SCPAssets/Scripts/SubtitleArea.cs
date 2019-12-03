using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Events
{
    public class SubtitleArea : CBEvent
    {
        public string SubTitleID;
        public string FallbackSubtitle;
        public float Time = 3;
        public override void Entered(bool isPlayer, GameObject gameObject)
        {
            if (isPlayer == true)
            {
                isStartedEnter = true;
                try
                {

                    GameInfo.CurrentGame.mainCharacher.ShowSubTitle(GameInfo.Language[SubTitleID], Time);
                }
                catch (System.Exception)
                {
                    GameInfo.CurrentGame.mainCharacher.ShowSubTitle(FallbackSubtitle, Time);
                }
            }
        }
    }
}