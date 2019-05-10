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
    [SerializeField] private Text _actionText;  

    [SerializeField] private GameObject _deathPanel;

    void Start()
    {
        _battleManager.SetBattleEndCallback(OnBattleEnd);
        gameObject.SetActive(false);
        _battleManager.StartBattle(true);
    }


    private void OnBattleEnd(bool playerWin)
    {
        // TODO: Implement endbattle subscription with playerWin value in subscription messege

        _deathPanel.SetActive(true);

        gameObject.SetActive(true);
        if (playerWin)
        {
            _resultText.text = "You win!";
            _actionText.text = "Next Level";
        }
        else
        {
            _resultText.text = "You lost!";
            _actionText.text = "Retry";

        }


        //SceneManager.LoadScene(0);
    }
}
