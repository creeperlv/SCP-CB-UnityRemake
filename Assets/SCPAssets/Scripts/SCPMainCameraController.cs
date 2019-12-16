using System;
using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SCPCB
{
    public class SCPMainCameraController : CBEntity
    {

        [Header("MainCameraController Settings", order = 2)]
        #region UI and blinking etc...
        public Button Pause;
        public Button Bag;
        public GameObject TouchControls;
        public GameObject PausePanel;
        public GameObject BagPanel;
        public GameObject BlankCover;
        public GameObject Console;
        public Button ResumeButton;
        public Button Resume2Button;
        public Button MainMenuButton;
        public Slider BlinkBar;
        public Slider StaminaBar;
        public InputField ConsoleField;
        public Text ConsoleText;
        #endregion
        public Button OperateButton;
        public Sprite Operate1;
        public Sprite Operate2;
        #region Blink and Stamina Settings
        public float BlinkCutdownSpeed = 2.5f;
        public float StaminaRecoverSpeed = 3f;
        public float StaminaConsumeSpeed = 4.5f;
        #endregion

        #region Language Settings
        [Header("Language Settings", order = 2)]
        public Text MainMenuText;
        #endregion

        [Header("Movement Settings", order = 2)]
        public TCKJoystick leftStick;

        public float WalkSpeed = 0.25f;
        public float RunSpeed = 0.5f;

        CharacterController controller;
        Transform myTransform, cameraTransform;
        [Header("Footstep Settings", order = 2)]
        public List<AudioClip> footSteps;
        public List<AudioClip> footStepsMetal;
        List<AudioClip> RealSteps;
        public AudioSource footsetpSrc;
        public AudioSource AudioSource;
        public Text Subtitle;

        [HideInInspector]
        public bool isBlinking = false;

        float rotation;
        float TimeStamina = 13.9f;
        float TimeBlink = 13.9f;
        // Start is called before the first frame update
        bool Crouch, prevGrounded, isPorjectileCube;
        [HideInInspector]
        public bool Running = false;//Only effect in Keybroad mode.
        int fotstpIndex = 0;
        Transform Head;
        float SubTime = 0;
        [HideInInspector]
        public bool isGodMode = false;
        public override void BeforeDeath(GameObject Replacement)
        {
            Replacement.GetComponent<DeadBody>().TouchControls = TouchControls;
        }
        public void ShowSubTitle(string text, float time = 3f)
        {
            Subtitle.text = text;
            SubTime = time;
        }
        public override void SideStart()
        {
            if (!GameInfo.CurrentGame.Storages.ContainsKey("Bag"))
            {
                GameInfo.CurrentGame.Storages.Add("Bag", new Storage() { Border = new Vector2(4, 2) });
            }
            ConsoleField.onEndEdit.AddListener(delegate (string s)
            {
                ConsoleField.text = "";
                if (s.ToUpper() == "GODMODE")
                {
                    if (isGodMode == false)
                    {
                        GameInfo.ConsoleText += "\r\n<color=#2080E0>You are in godmode now.</color>";
                        isGodMode = !isGodMode;
                    }
                    else
                    {
                        GameInfo.ConsoleText += "\r\n<color=red>You are not in godmode now.</color>";
                        isGodMode = !isGodMode;
                    }
                }
                else if (s.ToUpper() == "CLS")
                {
                    GameInfo.ConsoleText = "SCP - Containment Breach Unity Remake Console\r\nVersion:0.1.2.0";
                }
                else if (s.ToUpper() == "SHUTDOWNNUKE")
                {
                    GameInfo.ConsoleText += "\r\n<color=green>Shuting down nuke...</color>";
                    GameInfo.CurrentGame.isWarheadClosed = true;
                }
                else if (s.ToUpper() == "EXIT")
                {
                    ispaused = false;
                    Time.timeScale = 1;
                    AudioListener.pause = false;
                    PausePanel.SetActive(false);
                    BagPanel.SetActive(false);
                    Console.SetActive(false);
                    if (GameInfo.CurrentGame.isKeybroadAndMouseUsing == false)
                        TouchControls.SetActive(true);
                    if (GameInfo.CurrentGame.isKeybroadAndMouseUsing == true)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                }
            });
            cameraAni = GetComponent<Animator>();
            Head = transform.Find("Head");
            if (GameInfo.CurrentGame.isStickStatic == true)
            {
                leftStick.IsStatic = true;
            }
            if (GameInfo.CurrentGame.isPostProcessingEnabled == false)
                cameraTransform.gameObject.GetComponent<PostProcessLayer>().enabled = false;
            GameInfo.CurrentGame.mainCharacher = this;
            {
                try
                {

                    MainMenuText.text = GameInfo.Language["0016"];
                }
                catch (Exception)
                {
                }
            }
            if (GameInfo.CurrentGame.isKeybroadAndMouseUsing == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                TouchControls.SetActive(false);
                Pause.gameObject.SetActive(false);
            }
            Pause.onClick.AddListener(delegate ()
            {
                ispaused = true;
                Time.timeScale = 0;
                AudioListener.pause = true;
                PausePanel.SetActive(true);
                BagPanel.SetActive(false);
                TouchControls.SetActive(false);
            });
            Bag.onClick.AddListener(delegate ()
            {
                ispaused = true;
                Time.timeScale = 0;
                AudioListener.pause = true;
                BagPanel.SetActive(true);
                PausePanel.SetActive(false);
                TouchControls.SetActive(false);
            });
            ResumeButton.onClick.AddListener(delegate ()
            {
                ispaused = false;
                Time.timeScale = 1;
                AudioListener.pause = false;
                PausePanel.SetActive(false);
                if (GameInfo.CurrentGame.isKeybroadAndMouseUsing == false)
                    TouchControls.SetActive(true);
                if (GameInfo.CurrentGame.isKeybroadAndMouseUsing == true)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            });
            Resume2Button.onClick.AddListener(delegate ()
            {
                ispaused = false;
                Time.timeScale = 1;
                AudioListener.pause = false;
                PausePanel.SetActive(false);
                BagPanel.SetActive(false);
                if (GameInfo.CurrentGame.isKeybroadAndMouseUsing == false)
                    TouchControls.SetActive(true);
                if (GameInfo.CurrentGame.isKeybroadAndMouseUsing == true)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            });
            MainMenuButton.onClick.AddListener(delegate ()
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
                GameInfo.TargetScene = 1;
                SceneManager.LoadScene(2);
            });
            RealSteps = footSteps;
        }
        public void ChangeFootStep(string str)
        {
            if (str == "Normal")
            {
                RealSteps = footSteps;
            }
            else if (str == "Metal")
            {
                RealSteps = footStepsMetal;
            }
        }
        float ShakingTimeRemaining = 0.0f;
        float ShakingAmplitude = 0.0f;
        bool isShaking = false;
        Animator cameraAni;
        IEnumerator shake()
        {
            isShaking = true;
            //float ShakingTemp = 0.0f;
            //bool A = false;
            //bool B = false;
            //bool C = false;
            //float ShakeLimit = 0.0f;
            if (ShakingTimeRemaining < 3)
            {
                ShakingTimeRemaining = 3.1f;
            }
            while (ShakingTimeRemaining > 0)
            {
                ShakingTimeRemaining -= Time.deltaTime;

                yield return null;
            }
            {
                cameraAni.SetTrigger("StopShake");
                //var lr = cameraTransform.localRotation;
                //var angle = lr.eulerAngles;
                //angle.z = 0;
                //lr.eulerAngles = angle;
                //cameraTransform.localRotation = lr;
            }
            isShaking = false;
            yield break;
        }
        public void ShakeHead(float duration)
        {
            ShakingTimeRemaining = duration;
            if (isShaking == false)
            {
                cameraAni.SetTrigger("StartShake");
                StartCoroutine(shake());

            }
            else
            {
                ShakingTimeRemaining = duration;
            }
        }
        private void Awake()
        {

            myTransform = transform;
            cameraTransform = transform.GetChild(0).GetChild(0).transform;
            controller = GetComponent<CharacterController>();
        }
        float baseHeadHeight = 0.3f;
        [HideInInspector]
        public bool ispaused = false;
        // Update is called once per frame
        void Update()
        {
            if(ConsoleText.text!= GameInfo.ConsoleText)
            {
                ConsoleText.text = GameInfo.ConsoleText;
            }
            if (SubTime > 0)
            {
                SubTime -= Time.deltaTime;
                if (Subtitle.gameObject.activeSelf == false)
                    Subtitle.gameObject.SetActive(true);
                if (SubTime <= 0)
                {
                    if (Subtitle.gameObject.activeSelf == true)
                        Subtitle.gameObject.SetActive(false);
                }
            }
            if (GameInfo.CurrentGame.isKeybroadAndMouseUsing == true)
            {
                if (OperateButton.gameObject.activeSelf)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        try
                        {
                            if (ispaused == false)
                                OperateButton.onClick.Invoke();
                        }
                        catch (Exception)
                        {

                        }
                    }
                }

                if (Input.GetButton("Blink"))
                {
                    TimeBlink = -0.1f;
                }
                if (Input.GetButtonDown("Cancel"))
                {
                    PausePanel.SetActive(true);
                    BagPanel.SetActive(false);
                    TouchControls.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0;
                    AudioListener.pause = true;
                    ispaused = true;
                }
                if (Input.GetButtonDown("Bag"))
                {
                    BagPanel.SetActive(true);
                    PausePanel.SetActive(false);
                    TouchControls.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0;
                    AudioListener.pause = true;
                    ispaused = true;
                }
                if (Input.GetButtonDown("Console"))
                {
                    Console.SetActive(true);
                    PausePanel.SetActive(false);
                    TouchControls.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0;
                    AudioListener.pause = true;
                    ispaused = true;
                }
            }

            TimeBlink -= Time.deltaTime * BlinkCutdownSpeed;
            BlinkBar.value = (int)TimeBlink;
            StaminaBar.value = (int)TimeStamina;
            if (TimeStamina < 13.9f)
            {
                TimeStamina += Time.deltaTime * StaminaRecoverSpeed;
            }
            if (TimeBlink < 0)
            {
                if (BlankCover.activeSelf == false)
                {
                    isBlinking = true;
                    BlankCover.SetActive(true);
                }
                if (TimeBlink < -0.4f)
                {
                    isBlinking = false;
                    BlankCover.SetActive(false);
                    TimeBlink = 13.9f;
                }
            }
            if (TCKInput.GetAction("Crouch", EActionEvent.Down))
            {
                Crouch = !Crouch;
            }
            if (TCKInput.GetAction("Blink", EActionEvent.Press))
            {
                TimeBlink = -0.01f;
            }
            if (Input.GetButtonDown("Crouch"))
            {
                Crouch = !Crouch;
            }
            //else
            //{
            //    Crouch = false;
            //}
            if (Crouch == true)
            {
                if (baseHeadHeight > -0.2f)
                {
                    baseHeadHeight -= Time.deltaTime * 0.7f;
                    if (baseHeadHeight <= -0.2f)
                    {
                        baseHeadHeight = -0.2f;
                    }
                }
            }
            else
            {
                if (baseHeadHeight < 0.35f)
                {
                    baseHeadHeight += Time.deltaTime * 3;
                    if (baseHeadHeight >= 0.35f)
                    {
                        baseHeadHeight = 0.35f;
                    }
                }
            }
            if (GameInfo.CurrentGame.isKeybroadAndMouseUsing == false)
            {

                Vector2 look = TCKInput.GetAxis("Touchpad0");
                PlayerRotation(look.x, look.y);
            }
            else
            {
                if (
                ispaused == false)
                    PlayerRotation(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            }
            {
                //Head-Bod
                //y=-x^2+1.
                var lp = Head.localPosition;
                lp.y = baseHeadHeight - ((float)Math.Pow(walkCycle - 0.5, 2) - 1f) / 3f;
                Head.localPosition = lp;
                if (walkCycle >= 1.0f)
                {
                    walkCycle = 0;
                    footsetpSrc.clip = RealSteps[fotstpIndex];
                    footsetpSrc.Play();
                    fotstpIndex++;
                    if (fotstpIndex == footSteps.Count)
                    {
                        fotstpIndex = 0;
                    }
                }
            }
            if (GameInfo.CurrentGame.isKeybroadAndMouseUsing == false)
            {
                Vector2 move = TCKInput.GetAxis("Joystick0"); // NEW func since ver 1.5.5
                PlayerMovement(move.x, move.y);
            }
            else
            {

                PlayerMovement((Running ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal") / 2), (Running ? Input.GetAxis("Vertical") : Input.GetAxis("Vertical") / 2));
            }
        }


        float walkCycle = 0.0f;
        // FixedUpdate
        void FixedUpdate()
        {
            /*float moveX = TCKInput.GetAxis( "Joystick", EAxisType.Horizontal );
            float moveY = TCKInput.GetAxis( "Joystick", EAxisType.Vertical );*/

            if (Input.GetButton("Run"))
            {
                Running = true;
            }
            else
            {
                Running = false;
            }
        }

        public void PlayerRotation(float horizontal, float vertical)
        {
            myTransform.Rotate(0f, horizontal * 12f, 0f);
            rotation += vertical * 12f;
            rotation = Mathf.Clamp(rotation, -60f, 60f);
            cameraTransform.localEulerAngles = new Vector3(-rotation, cameraTransform.localEulerAngles.y, 0f);
        }
        private void PlayerMovement(float horizontal, float vertical)
        {
            bool grounded = controller.isGrounded;
            Vector3 moveDirection;
            if (StaminaBar.value > 0)
            {

                moveDirection = myTransform.forward * (vertical == 0 ? 0 : (vertical > 0f ? (vertical <= 0.8f ? WalkSpeed : RunSpeed) : (vertical >= -0.8f ? -WalkSpeed : -RunSpeed)));
                moveDirection += myTransform.right * (horizontal == 0 ? 0 : (horizontal > 0f ? (horizontal <= 0.8f ? WalkSpeed : RunSpeed) : (horizontal >= -0.8f ? -WalkSpeed : -RunSpeed)));

            }
            else
            {

                moveDirection = myTransform.forward * (vertical == 0 ? 0 : (vertical > 0f ? 0.25f : -0.25f));
                moveDirection += myTransform.right * (horizontal == 0 ? 0 : (horizontal > 0f ? 0.25f : -0.25f));

            }
            //Debug.Log($"H:{horizontal},V:{vertical}");
            moveDirection.y = -10f;
            //Debug.Log(Vector3.Magnitude(moveDirection) + "");
            if (vertical != 0 | horizontal != 0)
            {

                walkCycle += Vector2.Distance(new Vector2(0, 0), new Vector2(vertical == 0 ? 0 : (vertical > 0f ? (vertical < 0.8f ? WalkSpeed : RunSpeed) : (vertical > -0.5f ? -WalkSpeed : -RunSpeed)), horizontal == 0 ? 0 : (horizontal > 0f ? (horizontal < 0.8f ? WalkSpeed : RunSpeed) : (horizontal > -0.8f ? -WalkSpeed : -RunSpeed))))
                    * Time.deltaTime * 5 * (Crouch ? 0.5f : 1);
            }
            if (vertical > 0.8f | horizontal > 0.8f)
            {
                TimeStamina -= Time.deltaTime * StaminaConsumeSpeed;
            }
            if (Crouch)
            {
                moveDirection /= 2;
            }
            else
            {

            }
            //if (jump)
            //{
            //    jump = false;
            //    moveDirection.y = 25f;
            //    isPorjectileCube = !isPorjectileCube;
            //}

            if (grounded)
                moveDirection *= 7f;

            controller.Move(moveDirection * Time.deltaTime);

            if (!prevGrounded && grounded)
                moveDirection.y = 0f;

            prevGrounded = grounded;
        }
    }
}