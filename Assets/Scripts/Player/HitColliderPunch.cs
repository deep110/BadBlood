using UnityEngine;
using System.Collections;

public class HitColliderPunch : MonoBehaviour {

	public float damageLeft;
	public float damageRight;

	public Fighter owner;

	void OnTriggerEnter(Collider other){
		Fighter enemy = other.gameObject.GetComponent<Fighter> ();
		if (owner.punchingLeft && damageLeft>0) {
			/*if (enemy != null && enemy != owner) {
				enemy.hurt (damage);
			}*/
			Debug.Log (damageLeft+"/"+damageRight);
		} 
		if (owner.punchingRight && damageRight > 0) {
			/*if (enemy != null && enemy != owner) {
				enemy.hurt (damage);
			}*/
			Debug.Log (damageLeft+"/"+damageRight);
		} 
	}
}
