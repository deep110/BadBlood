using UnityEngine;
using System.Collections;

public class HitColliderKick : MonoBehaviour {

	public float damage;
	public Fighter owner;
	
	void OnTriggerEnter(Collider other){
		Fighter enemy = other.gameObject.GetComponent<Fighter> ();

		if (owner.kicking) {
			if (enemy != null && enemy != owner) {
				enemy.hurt (damage);
			}
		} 
	}
}
