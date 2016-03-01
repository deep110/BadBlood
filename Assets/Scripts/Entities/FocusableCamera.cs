using UnityEngine;
using System.Collections;

public class FocusableCamera : MonoBehaviour {
	public Transform leftTarget;
	public Transform rightTarget;

	public float minDistance;

	private Vector3 newPosition;

	void Start(){
		newPosition = new Vector3 ();
	}
	
	void Update () {
		float distanceBetweenTargets = Mathf.Abs (leftTarget.position.x - rightTarget.position.x);
		float centerPosition = (leftTarget.position.x + rightTarget.position.x)/ 2;

		newPosition.Set (
			centerPosition, transform.position.y,
			distanceBetweenTargets > minDistance? -distanceBetweenTargets: -minDistance);

		transform.position = newPosition;
	}
}
