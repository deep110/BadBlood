using UnityEngine;
using System.Collections;

public class HitColliderPunch : MonoBehaviour {
	public float damage;
	public Fighter owner;

	void OnTriggerEnter(Collider other){
		Fighter enemy = other.gameObject.GetComponent<Fighter> ();

		if (owner.punching) {
			if (enemy != null && enemy != owner) {
				enemy.hurt (damage);
			}
		} 
	}


}
