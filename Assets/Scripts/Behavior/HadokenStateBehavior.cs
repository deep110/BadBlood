using UnityEngine;
using System.Collections;

public class HadokenStateBehavior : FighterStateBehavior {

	public GameObject obj;

	override public void OnStateEnter(Animator animator, 
	                                  AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);

		float fighterX = fighter.transform.position.x;

		GameObject instance = Object.Instantiate (
			obj,
			new Vector3 (fighterX, 0.1f, fighter.transform.position.z), 
			Quaternion.Euler (0, 0, 0)
			) as GameObject;

		Hadoken hadoken = instance.GetComponent<Hadoken> ();
		hadoken.caster = fighter;
	}
}
