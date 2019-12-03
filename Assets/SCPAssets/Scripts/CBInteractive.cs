using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Events
{

    public class CBInteractive : CBEvent
    {
        public AudioClip InteractSFX;
        public virtual void SecondaryEntered(bool isPlayer, GameObject gameObject)
        {

        }
        public virtual void SecondaryExit(bool isPlayer, GameObject gameObject)
        {

        }
        public virtual IEnumerator Operation()
        {
            yield break;
        }
        public override void Entered(bool isPlayer,GameObject gameObject)
        {
            if (isPlayer == true)
            {
                GameInfo.CurrentGame.mainCharacher.OperateButton.gameObject.SetActive(true);
                GameInfo.CurrentGame.mainCharacher.OperateButton.onClick.RemoveAllListeners();
                GameInfo.CurrentGame.mainCharacher.OperateButton.onClick.AddListener(delegate ()
                {
                    GameInfo.CurrentGame.mainCharacher.AudioSource.clip = InteractSFX;
                    GameInfo.CurrentGame.mainCharacher.AudioSource.Play();
                    StartCoroutine( Operation());
                });
            }
            SecondaryEntered(isPlayer,gameObject);
        }
        public override void Exited(bool isPlayer,GameObject gameObject)
        {
            if (isPlayer == true)
            {
                GameInfo.CurrentGame.mainCharacher.OperateButton.gameObject.SetActive(false);
            }
            SecondaryExit(isPlayer,gameObject);
        }
    }

}