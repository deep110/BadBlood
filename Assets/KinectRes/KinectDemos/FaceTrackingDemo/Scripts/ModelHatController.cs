using UnityEngine;
using System.Collections;

public class ModelHatController : MonoBehaviour 
{
	[Tooltip("Vertical offset of the hat above the head position (in meters).")]
	public float verticalOffset = 0f;

	[Tooltip("Smooth factor used for hat-model movement and rotation.")]
	public float smoothFactor = 10f;

	//private KinectManager kinectManager;
	private FacetrackingManager faceManager;
	private Quaternion initialRotation;
	


	void Start () 
	{
		//kinectManager = KinectManager.Instance;
		initialRotation = transform.rotation;
	}
	
	void Update () 
	{
		// get the face-tracking manager instance
		if(faceManager == null)
		{
			faceManager = FacetrackingManager.Instance;
		}
		
		if(faceManager && faceManager.IsTrackingFace())
		{
			// head rotation
			Quaternion newRotation = initialRotation * faceManager.GetHeadRotation(true);
			
			if(smoothFactor != 0f)
				transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, smoothFactor * Time.deltaTime);
			else
				transform.rotation = newRotation;

			// head position
			Vector3 newPosition = faceManager.GetHeadPosition(true);

			if(verticalOffset != 0f)
			{
				Vector3 dirHead = new Vector3(0, verticalOffset, 0);
				dirHead = transform.InverseTransformDirection(dirHead);
				newPosition += dirHead;
			}
			
//			if(smoothFactor != 0f)
//				transform.position = Vector3.Lerp(transform.position, newPosition, smoothFactor * Time.deltaTime);
//			else
				transform.position = newPosition;
		}

	}
}
