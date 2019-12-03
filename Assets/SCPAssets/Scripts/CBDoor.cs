using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Events
{
    public class CBDoor : CBInteractive
    {
        public GameObject Door1;
        public GameObject Door2;
        public AudioSource source;
        public bool isTimedDoor = false;
        public float Cutdown = 10f;
        public List<AudioClip> openSFX;
        public AudioClip alertaSFX;
        public List<AudioClip> closeSFX;
        public bool isLocked = false;
        public string LockMessage = "The Door is locked.";
        bool isPending = false;
        int SFXIndex1 = 0;
        int SFXIndex2 = 0;
        public override void SecondaryEntered(bool isPlayer, GameObject gameObject)
        {
            if (isPlayer != true)
            {
                try
                {

                    gameObject.GetComponent<CBEntity>().actions.Clear();
                    gameObject.GetComponent<CBEntity>().actions.Add(() =>
                    {
                        StartCoroutine(Operation());
                        gameObject.GetComponent<CBEntity>().actions.Clear();
                    });
                }
                catch (System.Exception)
                {
                }
            }
        }

        public override IEnumerator Operation()
        {
            if (isPending == false)
            {
                isPending = true;
                if (isLocked == false)
                {
                    if (isTimedDoor == true)
                    {
                        {

                            source.clip = openSFX[SFXIndex1];
                            source.Play();
                            SFXIndex1++;
                            if (SFXIndex1 == openSFX.Count)
                            {
                                SFXIndex1 = 0;
                            }
                            Vector3 movement = Vector3.left;
                            while (Door1.transform.localPosition.z <= 1.1f)
                            {
                                Door1.transform.Translate(movement * Time.deltaTime);
                                Door2.transform.Translate(movement * Time.deltaTime);
                                yield return null;
                            }
                        }
                        yield return new WaitForSeconds(Cutdown - 2);
                        source.clip = alertaSFX;
                        source.Play();
                        yield return new WaitForSeconds(2);
                        {

                            source.clip = closeSFX[SFXIndex2];
                            source.Play();
                            SFXIndex2++;
                            if (SFXIndex2 == closeSFX.Count)
                            {
                                SFXIndex2 = 0;
                            }
                            Vector3 movement = -Vector3.left;
                            while (Door1.transform.localPosition.z >= 0f)
                            {
                                Door1.transform.Translate(movement * Time.deltaTime);
                                Door2.transform.Translate(movement * Time.deltaTime);
                                yield return null;
                            }
                            {
                                var lp = Door1.transform.localPosition;
                                lp.z = 0;
                                Door1.transform.localPosition = lp;
                            }
                            {
                                var lp = Door2.transform.localPosition;
                                lp.z = 0;
                                Door2.transform.localPosition = lp;
                            }
                        }
                    }
                    else
                    {

                        if (Door1.transform.localPosition.z == 0)
                        {
                            //Open
                            source.clip = openSFX[SFXIndex1];
                            source.Play();
                            SFXIndex1++;
                            if (SFXIndex1 == openSFX.Count)
                            {
                                SFXIndex1 = 0;
                            }
                            Vector3 movement = Vector3.left;
                            while (Door1.transform.localPosition.z <= 1.1f)
                            {
                                Door1.transform.Translate(movement * Time.deltaTime);
                                Door2.transform.Translate(movement * Time.deltaTime);
                                yield return null;
                            }
                        }
                        else
                        {
                            //Close
                            source.clip = closeSFX[SFXIndex2];
                            source.Play();
                            SFXIndex2++;
                            if (SFXIndex2 == closeSFX.Count)
                            {
                                SFXIndex2 = 0;
                            }
                            Vector3 movement = -Vector3.left;
                            while (Door1.transform.localPosition.z >= 0f)
                            {
                                Door1.transform.Translate(movement * Time.deltaTime);
                                Door2.transform.Translate(movement * Time.deltaTime);
                                yield return null;
                            }
                            {
                                var lp = Door1.transform.localPosition;
                                lp.z = 0;
                                Door1.transform.localPosition = lp;
                            }
                            {
                                var lp = Door2.transform.localPosition;
                                lp.z = 0;
                                Door2.transform.localPosition = lp;
                            }
                        }
                    }
                }
                else
                {
                    GameInfo.CurrentGame.mainCharacher.ShowSubTitle(LockMessage);
                    yield return new WaitForSeconds(0.5f);
                }
                isPending = false;
            }
        }
    }

}