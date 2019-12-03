using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace SCPCB.UIs
{

    public class MainMenu : MonoBehaviour
    {
        public Button NewGame;
        public Button VLowQ;
        public Button LowQ;
        public Button MedQ;
        public Button HighQ;
        public Button VHighQ;
        public Button UltraQ;
        public Button ExitButton;
        public Dropdown LanguageSettings;
        public Dropdown ResolutionSettings;
        public List<TextAsset> Languages;
        public List<Font> LanguageSpecificFont;
        public Toggle TouchControl;
        public Toggle SaticJoystick;
        public Toggle PostProcessControl;
        public Text LoadGame;
        public Text Options;
        public Text Options2;
        public Text ExitText;
        public Text Back;
        public Text Video;
        public Text VLowT;
        public Text LowT;
        public Text MedT;
        public Text HighT;
        public Text VHighT;
        public Text UltraT;
        public Text Quality;
        public Text Control;
        public Text PostProcess;
        public Text Title;
        public Text Touch;
        public Text StaticJsT;
        public Text ResolutionT;
        // Start is called before the first frame update
        void LoadLanguage(int a)
        {

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
            {
                NewGame.transform.GetChild(0).GetComponent<Text>().text = GameInfo.Language["0000"];
                NewGame.transform.GetChild(0).GetComponent<Text>().font = LanguageSpecificFont[a];
                LoadGame.text = GameInfo.Language["0001"];
                LoadGame.font = LanguageSpecificFont[a];
                Options.text = GameInfo.Language["0002"];
                Options.font = LanguageSpecificFont[a];
                ExitText.text = GameInfo.Language["0003"];
                ExitText.font = LanguageSpecificFont[a];
                Back.text = GameInfo.Language["0004"];
                Back.font = LanguageSpecificFont[a];
                Video.text = GameInfo.Language["0005"];
                Video.font = LanguageSpecificFont[a];
                Control.text = GameInfo.Language["0006"];
                Control.font = LanguageSpecificFont[a];
                Quality.text = GameInfo.Language["0007"];
                Quality.font = LanguageSpecificFont[a];
                VLowT.text = GameInfo.Language["0009"];
                VLowT.font = LanguageSpecificFont[a];
                LowT.text = GameInfo.Language["0010"];
                LowT.font = LanguageSpecificFont[a];
                MedT.text = GameInfo.Language["0011"];
                MedT.font = LanguageSpecificFont[a];
                HighT.text = GameInfo.Language["0012"];
                HighT.font = LanguageSpecificFont[a];
                VHighT.text = GameInfo.Language["0013"];
                VHighT.font = LanguageSpecificFont[a];
                UltraT.text = GameInfo.Language["0014"];
                UltraT.font = LanguageSpecificFont[a];
                PostProcess.text = GameInfo.Language["0008"];
                PostProcess.font = LanguageSpecificFont[a];
                StaticJsT.text = GameInfo.Language["0019"];
                StaticJsT.font = LanguageSpecificFont[a];
                Touch.text = GameInfo.Language["0018"];
                Touch.font = LanguageSpecificFont[a];
                ResolutionT.text = GameInfo.Language["0020"];
                ResolutionT.font = LanguageSpecificFont[a];

                Title.text = GameInfo.Language["0015"];
            }
        }
        void Start()
        {
            GameInfo.database.OpenForm();
            int l = 0;
            try
            {
                l = int.Parse((string)GameInfo.database.Query("Video", "Language"));
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
            LoadLanguage(l);
            try
            {
                PostProcessControl.isOn = bool.Parse((string)GameInfo.database.Query("Video", "PostProcess"));
            }
            catch (System.Exception)
            { }
            try
            {
                TouchControl.isOn = bool.Parse((string)GameInfo.database.Query("Control", "Touch"));
            }
            catch (System.Exception)
            { }
            try
            {
                SaticJoystick.isOn = bool.Parse((string)GameInfo.database.Query("Control", "SaticJoystick"));
            }
            catch (System.Exception)
            { }
            LanguageSettings.value = l;
            ExitButton.onClick.AddListener(delegate ()
            {
                Application.Quit();
            });
            NewGame.onClick.AddListener(delegate () { 
                GameInfo.CurrentGame = new GameInfo();
                GameInfo.CurrentGame.isKeybroadAndMouseUsing=!TouchControl.isOn;
                GameInfo.CurrentGame.isStickStatic= SaticJoystick.isOn;
                GameInfo.CurrentGame.isPostProcessingEnabled=PostProcessControl.isOn;
                GameInfo.TargetScene = 3;
                SceneManager.LoadScene(2); });
            VLowQ.onClick.AddListener(delegate () { QualitySettings.SetQualityLevel(0); });
            LowQ.onClick.AddListener(delegate () { QualitySettings.SetQualityLevel(1); });
            MedQ.onClick.AddListener(delegate () { QualitySettings.SetQualityLevel(2); });
            HighQ.onClick.AddListener(delegate () { QualitySettings.SetQualityLevel(3); });
            VHighQ.onClick.AddListener(delegate () { QualitySettings.SetQualityLevel(4); });
            UltraQ.onClick.AddListener(delegate () { QualitySettings.SetQualityLevel(5); });
            LanguageSettings.onValueChanged.AddListener(delegate (int a) {
                try
                {
                    LoadLanguage(a);
                    GameInfo.database.Save("Video", "Language", a + "");
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }
            });
            TouchControl.onValueChanged.AddListener(delegate (bool b) {
                try
                {
                    GameInfo.database.Save("Control", "Touch", b + "");
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }
            });
            SaticJoystick.onValueChanged.AddListener(delegate (bool b) {
                try
                {
                    GameInfo.database.Save("Control", "SaticJoystick", b + "");
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }
            });
            PostProcessControl.onValueChanged.AddListener(delegate (bool b) {
                try
                {
                    GameInfo.database.Save("Video", "PostProcess", b + "");
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }
            });
            {
                var r=Screen.resolutions;
                var r2=r.Reverse().ToList();
                foreach (var item in r2)
                {

                    ResolutionSettings.options.Add(new Dropdown.OptionData(item.width + "," + item.height));
                }

                try
                {
                    ResolutionSettings.value = int.Parse((string)GameInfo.database.Query("Video", "Res"));
                }
                catch (System.Exception)
                {
                    ResolutionSettings.value = 0; }
                ResolutionSettings.onValueChanged.AddListener(delegate (int a) {
                    Screen.SetResolution(r2[a].width, r2[a].height, Screen.fullScreen); try
                    {
                        GameInfo.database.Save("Video", "Res", a + "");
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                });
            }
        }

    }

}