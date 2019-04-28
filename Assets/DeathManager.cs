using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathManager : MonoBehaviour
{
    public void StartNewBattle()
    {
        //gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
