using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SCPCB
{

    public class LoadScreen : MonoBehaviour
    {
        AsyncOperation operation;

        public Image image;
        public List<Sprite> sprites;

        public Text LoadingText;

        public Slider ProgressBar;

        // Start is called before the first frame update
        void Start()
        {

            try
            {
                //GameInfo.CurrentGame.isCurrentArrived = false;
                //var loadings = Load();
                //int WhichOne = (new System.Random()).Next(loadings.Key.Count - 1);
                int i = Random.Range(0, sprites.Count);
                image.sprite = sprites[i];
                LoadingText.text = $"<size=23>{GameInfo.Language[$"010{i}.1"]}</size>\r\n{GameInfo.Language[$"010{i}.2"]}";
            }
            catch (System.Exception e)
            {
                LoadingText.text = $"<size=23>Ops!</size>\r\nError happens!\r\nError message:<color=red>{e.Message}</color>";
                Debug.Log(e.Message);
            }

            //ProgressIndicator=transform.Find("ProgressIndicator").GetComponent<Text>();
            StartCoroutine(Navigate());//玄学¯\_(ツ)_/¯
        }

        IEnumerator Navigate()
        {
            operation = SceneManager.LoadSceneAsync(GameInfo.TargetScene);
            operation.allowSceneActivation = false;
            yield return operation;
        }
        float CurrentProgress = 0;
        bool flag = false;//Stop progress from overflow.
                          // Update is called once per frame
        void Update()
        {

            if (CurrentProgress < 0.9)
            {
                CurrentProgress = operation.progress * 20;
            }
            else if (!flag)
            {
                CurrentProgress++;
                if (CurrentProgress > 100)
                {
                    CurrentProgress = 100;
                }
            }
            ProgressBar.value = (int)CurrentProgress;
            //ProgressIndicator.text = CurrentProgress + "%";
            if (CurrentProgress >= 100)
            {
                flag = true;
                operation.allowSceneActivation = true;
            }
        }
    }

}