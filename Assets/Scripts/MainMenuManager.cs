using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LoadRound ()
    {
        SceneManager.LoadScene(1);
    }

    public void GoVk ()
    {
        Application.OpenURL("https://vk.com/gameformonth");
    }
}
