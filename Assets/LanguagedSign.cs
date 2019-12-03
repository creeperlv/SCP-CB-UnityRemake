using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SCPCB.UIs
{

    public class LanguagedSign : MonoBehaviour
    {
        public List<Text> text;
        public List<string> ID;
        public List<string> Fallbacks;
        void Start()
        {
            for (int i = 0; i < text.Count; i++)
            {
                try
                {
                    text[i].text = GameInfo.Language[ID[i]];
                }
                catch (System.Exception)
                {
                    text[i].text = Fallbacks[i];
                }
            }
        }

    }

}