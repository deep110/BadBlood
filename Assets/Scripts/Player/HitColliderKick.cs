using UnityEngine;
using System.Collections;

public class HitColliderKick : MonoBehaviour {

	public float damageRight;
	public Fighter owner;
	
	void OnTriggerEnter(Collider other){
		Fighter enemy = other.gameObject.GetComponent<Fighter> ();

		if (owner.kicking && damageRight > 0) {
			if (enemy != null && enemy != owner) {
				//enemy.hurt (damage);
				Debug.Log (damageRight);
			}
		} 
	}
}
