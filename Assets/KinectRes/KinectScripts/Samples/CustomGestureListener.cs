using UnityEngine;
using System;
using System.Collections;



public class CustomGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface{

	private bool progressDisplayed;
	private float progressGestureTime;
	
	public void UserDetected(long userId, int userIndex){
		KinectManager kinectManager = KinectManager.Instance;
		if(!kinectManager|| (userId != kinectManager.GetPrimaryUserID()))
			return;

		kinectManager.DetectGesture(userId, KinectGestures.Gestures.PunchLeft);
		kinectManager.DetectGesture(userId, KinectGestures.Gestures.PunchRight);
		kinectManager.DetectGesture(userId,KinectGestures.Gestures.Jump);
		kinectManager.DetectGesture(userId,KinectGestures.Gestures.Squat);
		kinectManager.DetectGesture (userId,KinectGestures.Gestures.Defend);
		kinectManager.DetectGesture (userId,KinectGestures.Gestures.KickHitLeft);
		kinectManager.DetectGesture (userId,KinectGestures.Gestures.KickHitRight);
		kinectManager.DetectGesture (userId,KinectGestures.Gestures.WalkForward);
		kinectManager.DetectGesture (userId,KinectGestures.Gestures.WalkBackward);

		//kinectManager.DetectGesture (userId,KinectGestures.Gestures.Push);

		//Debug.Log("User detected in CustomGestureListener.");
	}
	
	public void UserLost(long userId, int userIndex){
		Debug.Log("User lost detected in CustomGestureListener.");
	}
	
	public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectInterop.JointType joint, Vector3 screenPos){
			progressGestureTime = Time.realtimeSinceStartup;
		  //  Debug.Log(gesture);
	}

	
	public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture, 
	                             KinectInterop.JointType joint, Vector3 screenPos){

		//if(progressDisplayed)return true;
		string sGestureText = gesture + " detected";
		Debug.Log(sGestureText);
		return true;
	}
	
	public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, 
	                             KinectInterop.JointType joint)
	{
		if(progressDisplayed)
		{
			progressDisplayed = false;
		}
		
		return true;
	}

	public void Update(){
		if(progressDisplayed && ((Time.realtimeSinceStartup - progressGestureTime) > 2f)){
			progressDisplayed = false;
			Debug.Log("Forced progress to end.");
		}
	}
}

