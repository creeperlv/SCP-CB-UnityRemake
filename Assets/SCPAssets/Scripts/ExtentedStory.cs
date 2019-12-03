using SCPCB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtentedStory : MonoBehaviour
{
    public AudioSource source;
    public AudioClip WarheadStart;
    public AudioClip WarheadCancel;
    public AudioClip WarheadBoomUp;
    // Start is called before the first frame update
    void Start()
    {

    }
    bool isWarheadStarted = false;
    bool isWarheadStopped = false;
    bool isWarheadBoomed = false;
    public float StoryTime = 130;
    // Update is called once per frame
    void Update()
    {
        if (StoryTime <= 122f)
        {
            if (isWarheadStarted == false)
            {
                source.clip = WarheadStart;
                source.Play();
                Debug.Log("Warhead...");
                try
                {

                    GameInfo.CurrentGame.mainCharacher.ShowSubTitle(GameInfo.Language["1002"]);

                }
                catch (System.Exception)
                {
                    GameInfo.CurrentGame.mainCharacher.ShowSubTitle("Due to emergency situation, the Alpha warhead detonation sequence engaged, the underground part of the facility will be detonated in 110 seconds.");
                }
                isWarheadStarted = true;
            }
            if (GameInfo.CurrentGame.isWarheadClosed == true)
            {
                if (isWarheadStopped == false)
                {

                    source.clip = WarheadCancel;
                    source.Play();
                    try
                    {

                        GameInfo.CurrentGame.mainCharacher.ShowSubTitle(GameInfo.Language["1003"]);

                    }
                    catch (System.Exception)
                    {
                        GameInfo.CurrentGame.mainCharacher.ShowSubTitle("Detonation canceled, restarting systems.");
                    }
                    isWarheadStopped = true;
                }

            }
            else
            {
                if (StoryTime < 0)
                {
                    if (isWarheadBoomed == false)
                    {

                        source.clip = WarheadBoomUp;
                        source.Play();
                        isWarheadBoomed = true;
                        GameInfo.CurrentGame.mainCharacher.Health -= GameInfo.CurrentGame.mainCharacher.MaxHealth;
                    }

                }
            }
        }
        StoryTime -= Time.deltaTime;
    }
}
