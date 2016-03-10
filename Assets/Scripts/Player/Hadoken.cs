using UnityEngine;
using System.Collections;

public class Hadoken : MonoBehaviour {
	public Fighter caster;

	private Rigidbody body;
	private float creationTime;
	
	void Start () {
		creationTime = Time.time;
		body = GetComponent<Rigidbody> ();
		body.AddRelativeForce (new Vector3 (200, 0, 0));
	}

	void Update () {
		if (Time.time - creationTime > 2.4) {
			Destroy(gameObject);
		}
	}
}
