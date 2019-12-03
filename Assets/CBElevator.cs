using SCPCB.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCPCB.Events
{

    public class CBElevator : CBInteractive
    {
        public enum Direction
        {
            Up, Down

        }
        public Direction ElevatorTeleportDirection = Direction.Down;
        public float Distance = 20f;
        [HideInInspector]
        public bool isPending = false;
        public GameObject Door1;
        public GameObject Door2;
        public GameObject Door3;
        public GameObject Door4;
        public AudioSource source;
        public AudioSource source2;
        public List<AudioClip> openSFX;
        public AudioClip alertaSFX;
        public List<AudioClip> closeSFX;
        public CBElevatorDetector elevatorDetector;
        int SFXIndex1 = 0;
        int SFXIndex2 = 0;
        public override IEnumerator Operation()
        {
            if (isPending == false)
            {
                isPending = true;
                elevatorDetector.transform.parent.GetComponentInChildren<CBElevator>().isPending = true;

                if (Door1.transform.localPosition.z == 0)
                {
                    //Open
                    source.clip = alertaSFX;
                    source.Play();
                    yield return new WaitForSeconds(10);
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
                    //Close - Detect - Move - Open
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
                    source.clip = alertaSFX;
                    source.Play();
                    yield return new WaitForSeconds(10);
                    if (elevatorDetector.isPlayerIn)
                    {
                        switch (ElevatorTeleportDirection)
                        {
                            case Direction.Up:
                                {
                                    GameInfo.CurrentGame.mainCharacher.transform.Translate(Vector3.up * Distance);
                                }
                                break;
                            case Direction.Down:
                                {
                                    GameInfo.CurrentGame.mainCharacher.transform.Translate(Vector3.down * Distance);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    {
                        //Open

                        source2.clip = openSFX[SFXIndex1];
                        source2.Play();
                        SFXIndex1++;
                        if (SFXIndex1 == openSFX.Count)
                        {
                            SFXIndex1 = 0;
                        }
                        Vector3 movement = Vector3.left;
                        while (Door3.transform.localPosition.z <= 1.1f)
                        {
                            Door3.transform.Translate(movement * Time.deltaTime);
                            Door4.transform.Translate(movement * Time.deltaTime);
                            yield return null;
                        }
                    }
                }

                isPending = false;
                elevatorDetector.transform.parent.GetComponentInChildren<CBElevator>().isPending = false;
            }
            else
            {

            }
            yield break;
        }
    }

}