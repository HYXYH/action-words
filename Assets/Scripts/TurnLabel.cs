using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TurnLabel : MonoBehaviour
{

	[SerializeField] private Text _turnText;
	[SerializeField] private Sprite _playerIco;
	[SerializeField] private Sprite _bossIco;
	[SerializeField] private Image _holderImage;


	public void setTurn(bool isPlayerTurn){
		if (isPlayerTurn){
			_holderImage.sprite = _playerIco;
			_turnText.text = "Player Turn";
		}
		else {
			_holderImage.sprite = _bossIco;
			_turnText.text = "Boss Turn";
		}
	}
}
