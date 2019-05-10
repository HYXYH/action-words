using System;
using System.Collections;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;


namespace Battle
{
public class DamageDealer : MonoBehaviour
{
	[SerializeField] private CanvasGroup _boardCanvas;
	[SerializeField] private Character _attackTarget;
	[SerializeField] private float _alpha = 0.2f;
	[SerializeField] private int _damage;

	[CanBeNull] private Action _endTurnCallback;

	public void SetEndTurnCallback(Action callback)
    {
		_endTurnCallback += callback;
    }

	public void makeCanvasTransparent(){
		_boardCanvas.gameObject.SetActive(false);
		// _puzzleCanvas.alpha = _alpha;
	}

	public void makeCanvasVisible(){
		_boardCanvas.gameObject.SetActive(true);
		_endTurnCallback();
		// _puzzleCanvas.alpha = 1;
	}

	public void setDamage(int damage){
		_damage = damage;
	}


    public void DealDamageToTarget(){
    	_attackTarget.DealDamage(_damage);
    }
}
}