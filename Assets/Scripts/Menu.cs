using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Battle;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    [SerializeField] private BattleManager _battleManager;
    [SerializeField] private  Text _resultText;
    
    // Start is called before the first frame update
    void Start()
    {
        _battleManager.SetBattleEndCallback(OnBattleEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayClick()
    {
        gameObject.SetActive(false);
        _battleManager.StartBattle();
    }


    private void OnBattleEnd(bool playerWin)
    {
        gameObject.SetActive(true);
        if (playerWin)
        {
            _resultText.text = "Victory!";
        }
        else
        {
            _resultText.text = "Defeat!";
        }
    }
}
