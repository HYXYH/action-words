using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Battle;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BattleManager _battleManager;
    [SerializeField] private Text _resultText;

    void Start()
    {
        _battleManager.SetBattleEndCallback(OnBattleEnd);
        gameObject.SetActive(false);
        _battleManager.StartBattle();
    }


    private void OnBattleEnd(bool playerWin)
    {
        // TODO: Implement endbattle subscription with playerWin value in subscription messege
        /*
        gameObject.SetActive(true);
        if (playerWin)
        {
            _resultText.text = "Victory!";
        }
        else
        {
            _resultText.text = "Defeat!";
        }
        */

        SceneManager.LoadScene(0);
    }
}
