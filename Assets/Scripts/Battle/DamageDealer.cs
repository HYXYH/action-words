using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Battle
{
public class DamageDealer : MonoBehaviour
{

	[SerializeField] private Character _attackTarget;
	[SerializeField] private int _damage;


	public void setDamage(int damage){
		_damage = damage;
	}


    public void DealDamageToTarget(){
    	_attackTarget.DealDamage(_damage);
    }
}
}