using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadBody : MonoBehaviour
{
    public GameObject TouchControls;
    public Button MainMenu;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        AudioListener.pause = false;
        TouchControls.SetActive(false);
        MainMenu.onClick.AddListener(delegate () {
            Time.timeScale = 1;
            AudioListener.pause = false;
            SceneManager.LoadScene(0);
        });
    }
}
