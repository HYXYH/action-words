using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Battle
{
public class DamageDealer : MonoBehaviour
{
	[SerializeField] private CanvasGroup _puzzleCanvas;
	[SerializeField] private Character _attackTarget;
	[SerializeField] private float _alpha = 0.2f;
	[SerializeField] private int _damage;

	public void makeCanvasTransparent(){
		_puzzleCanvas.gameObject.SetActive(false);
		// _puzzleCanvas.alpha = _alpha;
	}

	public void makeCanvasVisible(){
		_puzzleCanvas.gameObject.SetActive(true);
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