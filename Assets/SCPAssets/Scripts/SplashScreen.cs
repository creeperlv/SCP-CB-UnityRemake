using SCPCB.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
namespace SCPCB
{

    public class SplashScreen : MonoBehaviour
    {
        public VideoPlayer videoPlayer;
        public AudioSource audioSource;
        public VideoClip clip1;
        public VideoClip clip2;
        public AudioClip clip3;
        public AudioClip clip4;
        // Start is called before the first frame update
        public List<TextAsset> Languages;
        public Button btn;
        public CBInventoryItemLoader loader;
        void Start()
        {
            int a = 0;
            try
            {
                a = int.Parse((string)GameInfo.database.Query("Video", "Language"));
            }
            catch (Exception e)
            {

            }
            {
                GameInfo.Language = new Dictionary<string, string>();
                var s = Languages[a].text;
                var c = s.Split('\n');
                var l = c.ToList();
                //l.RemoveAt(0);
                foreach (var item in l)
                {
                    if (item.IndexOf("=") < 0)
                    {
                        continue;
                    }
                    //Debug.Log(item);
                    GameInfo.Language.Add(item.Substring(0, (item.IndexOf("="))), item.Substring(item.IndexOf("=") + 1));
                }
            }
            StartCoroutine(Sequence());
            btn.onClick.AddListener(delegate () {
                isSkipped = true;
                GameInfo.TargetScene = 1;
                SceneManager.LoadScene(2);
            });
        }
        bool isSkipped = false;
        IEnumerator Sequence()
        {
            audioSource.clip = clip3;
            audioSource.Play();
            videoPlayer.clip = clip1;
            videoPlayer.Play();
            yield return new WaitForSeconds(11);
            videoPlayer.clip = clip2;
            videoPlayer.Play();
            audioSource.clip = clip4;
            audioSource.Play();
            yield return new WaitForSeconds(8);
            if (isSkipped != true)
            {

                GameInfo.TargetScene = 1;
                SceneManager.LoadScene(2);
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}