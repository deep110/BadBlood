using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Filter to correct the joint locations and joint orientations to constraint to the range of viable human motion.
/// </summary>
//using Windows.Kinect;


public class BoneOrientationsConstraint
{
	public enum ConstraintAxis { X = 0, Y = 1, Z = 2 }
	
    // The Joint Constraints.  
    private readonly List<BoneOrientationConstraint> jointConstraints = new List<BoneOrientationConstraint>();

	//private GUIText debugText;
	

    /// Initializes a new instance of the BoneOrientationConstraints class.
    public BoneOrientationsConstraint()
    {
    }

	public void SetDebugText(GUIText debugText)
	{
		//this.debugText = debugText;
	}
	
	// find the bone constraint structure for given joint
	// returns the structure index in the list, or -1 if the bone structure is not found
	private int FindBoneOrientationConstraint(int thisJoint)
	{
		for(int i = 0; i < jointConstraints.Count; i++)
		{
			if(jointConstraints[i].thisJoint == thisJoint)
				return i;
		}
		
		// not found
		return -1;
	}

    // AddJointConstraint - Adds a joint constraint to the system.  
    public void AddBoneOrientationConstraint(int thisJoint, ConstraintAxis axis, float angleMin, float angleMax)
    {
		int index = FindBoneOrientationConstraint(thisJoint);
		
		BoneOrientationConstraint jc = index >= 0 ? jointConstraints[index] : new BoneOrientationConstraint(thisJoint);
		
		if(index < 0)
		{
			index = jointConstraints.Count;
			jointConstraints.Add(jc);
		}
		
		AxisOrientationConstraint constraint = new AxisOrientationConstraint(axis, angleMin, angleMax);
		jc.axisConstrainrs.Add(constraint);
		
		jointConstraints[index] = jc;
     }

    // AddDefaultConstraints - Adds a set of default joint constraints for normal human poses.  
    // This is a reasonable set of constraints for plausible human bio-mechanics.
    public void AddDefaultConstraints()
    {
        // Spine
		AddBoneOrientationConstraint((int)KinectInterop.JointType.SpineMid, ConstraintAxis.Z, -5f, 5f);
		AddBoneOrientationConstraint((int)KinectInterop.JointType.SpineMid, ConstraintAxis.X, -5f, 5f);
		AddBoneOrientationConstraint((int)KinectInterop.JointType.SpineMid, ConstraintAxis.Y, -5f, 5f);

		// SpineShoulder
		AddBoneOrientationConstraint((int)KinectInterop.JointType.SpineShoulder, ConstraintAxis.Z, -5f, 5f);
		AddBoneOrientationConstraint((int)KinectInterop.JointType.SpineShoulder, ConstraintAxis.X, -5f, 5f);
		AddBoneOrientationConstraint((int)KinectInterop.JointType.SpineShoulder, ConstraintAxis.Y, -5f, 5f);

		// Neck
		AddBoneOrientationConstraint((int)KinectInterop.JointType.Neck, ConstraintAxis.Z, -35f, 35f);  // lat
		AddBoneOrientationConstraint((int)KinectInterop.JointType.Neck, ConstraintAxis.X, -30f, 60f);  // sag
		AddBoneOrientationConstraint((int)KinectInterop.JointType.Neck, ConstraintAxis.Y, -60f, 60f);  // rot

		// ShoulderLeft, ShoulderRight
		AddBoneOrientationConstraint((int)KinectInterop.JointType.ShoulderLeft, ConstraintAxis.Z, -90f, 120f);  // lat
		//AddBoneOrientationConstraint((int)KinectInterop.JointType.ShoulderLeft, ConstraintAxis.X, -180f, 180f);  // rot
		AddBoneOrientationConstraint((int)KinectInterop.JointType.ShoulderLeft, ConstraintAxis.Y, -60f, 120f);  // sag
		AddBoneOrientationConstraint((int)KinectInterop.JointType.ShoulderRight, ConstraintAxis.Z, -90f, 120f);  // lat
		//AddBoneOrientationConstraint((int)KinectInterop.JointType.ShoulderRight, ConstraintAxis.X, -180f, 180f);  // rot
		AddBoneOrientationConstraint((int)KinectInterop.JointType.ShoulderRight, ConstraintAxis.Y, -120f, 60f);  // sag

		// ElbowLeft, ElbowRight
		//AddBoneOrientationConstraint((int)KinectInterop.JointType.ElbowLeft, ConstraintAxis.Z, -180f, 180f);  // lat
		AddBoneOrientationConstraint((int)KinectInterop.JointType.ElbowLeft, ConstraintAxis.X, -90f, 90f);  // rot
		AddBoneOrientationConstraint((int)KinectInterop.JointType.ElbowLeft, ConstraintAxis.Y, -60f, 130f);  // sag
		//AddBoneOrientationConstraint((int)KinectInterop.JointType.ElbowRight, ConstraintAxis.Z, -180f, 180f);  // lat
		AddBoneOrientationConstraint((int)KinectInterop.JointType.ElbowRight, ConstraintAxis.X, -90f, 90f);  // rot
		AddBoneOrientationConstraint((int)KinectInterop.JointType.ElbowRight, ConstraintAxis.Y, -130f, 60f);  // sag

		// WristLeft, WristRight
		AddBoneOrientationConstraint((int)KinectInterop.JointType.WristLeft, ConstraintAxis.Z, -60f, 60f);  // lat
		AddBoneOrientationConstraint((int)KinectInterop.JointType.WristLeft, ConstraintAxis.X, -90f, 90f);  // rot
		AddBoneOrientationConstraint((int)KinectInterop.JointType.WristLeft, ConstraintAxis.Y, -60f, 60f);  // sag*
		AddBoneOrientationConstraint((int)KinectInterop.JointType.WristRight, ConstraintAxis.Z, -60f, 60f);  // lat
		AddBoneOrientationConstraint((int)KinectInterop.JointType.WristRight, ConstraintAxis.X, -90f, 90f);  // rot
		AddBoneOrientationConstraint((int)KinectInterop.JointType.WristRight, ConstraintAxis.Y, -60f, 60f);  // sag*

		// HandLeft, HandRight
		AddBoneOrientationConstraint((int)KinectInterop.JointType.HandLeft, ConstraintAxis.Z, -30f, 90f);  // lat
		//AddBoneOrientationConstraint((int)KinectInterop.JointType.HandLeft, ConstraintAxis.X, -180f, 180f);  // rot
		AddBoneOrientationConstraint((int)KinectInterop.JointType.HandLeft, ConstraintAxis.Y, -30f, 30f);  // sag
		AddBoneOrientationConstraint((int)KinectInterop.JointType.HandRight, ConstraintAxis.Z, -90f, 30f);  // lat
		//AddBoneOrientationConstraint((int)KinectInterop.JointType.HandRight, ConstraintAxis.X, -180f, 180f);  // rot
		AddBoneOrientationConstraint((int)KinectInterop.JointType.HandRight, ConstraintAxis.Y, -30f, 30f);  // sag

		// HipLeft, HipRight
		AddBoneOrientationConstraint((int)KinectInterop.JointType.HipLeft, ConstraintAxis.Z, -160f, 20f);  // lat
		AddBoneOrientationConstraint((int)KinectInterop.JointType.HipLeft, ConstraintAxis.X, -110f, 30f);  // sag
		AddBoneOrientationConstraint((int)KinectInterop.JointType.HipLeft, ConstraintAxis.Y, 0f, 180f);  // rot
		AddBoneOrientationConstraint((int)KinectInterop.JointType.HipRight, ConstraintAxis.Z, -20f, 160f);  // lat
		AddBoneOrientationConstraint((int)KinectInterop.JointType.HipRight, ConstraintAxis.X, -110f, 30f);  // sag
		AddBoneOrientationConstraint((int)KinectInterop.JointType.HipRight, ConstraintAxis.Y, -180f, 0f);  // rot

		// KneeLeft, KneeRight
		AddBoneOrientationConstraint((int)KinectInterop.JointType.KneeLeft, ConstraintAxis.Z, 0f, 180f);  // lat
		AddBoneOrientationConstraint((int)KinectInterop.JointType.KneeLeft, ConstraintAxis.X, 0f, 90f);  // sag
		AddBoneOrientationConstraint((int)KinectInterop.JointType.KneeLeft, ConstraintAxis.Y, 0f, 180f);  // rot
		AddBoneOrientationConstraint((int)KinectInterop.JointType.KneeRight, ConstraintAxis.Z, -180f, 0f);  // lat
		AddBoneOrientationConstraint((int)KinectInterop.JointType.KneeRight, ConstraintAxis.X, 0f, 90f);  // sag
		AddBoneOrientationConstraint((int)KinectInterop.JointType.KneeRight, ConstraintAxis.Y, -180f, 0f);  // rot

		// AnkleLeft, AnkleRight
		AddBoneOrientationConstraint((int)KinectInterop.JointType.AnkleLeft, ConstraintAxis.Z, -5f, 5f);  // lat
		AddBoneOrientationConstraint((int)KinectInterop.JointType.AnkleLeft, ConstraintAxis.X, -10f, 10f);  // sag
		AddBoneOrientationConstraint((int)KinectInterop.JointType.AnkleLeft, ConstraintAxis.Y, -30f, 30f);  // rot
		AddBoneOrientationConstraint((int)KinectInterop.JointType.AnkleRight, ConstraintAxis.Z, -5f, 5f);  // lat
		AddBoneOrientationConstraint((int)KinectInterop.JointType.AnkleRight, ConstraintAxis.X, -10f, 10f);  // sag
		AddBoneOrientationConstraint((int)KinectInterop.JointType.AnkleRight, ConstraintAxis.Y, -30f, 30f);  // rot
	}

    // ApplyBoneOrientationConstraints and constrain rotations.
	public void Constrain(ref KinectInterop.BodyData bodyData)
    {
		KinectManager manager = KinectManager.Instance;

        for (int i = 0; i < this.jointConstraints.Count; i++)
        {
            BoneOrientationConstraint jc = this.jointConstraints[i];

			if (jc.thisJoint == (int)KinectInterop.JointType.SpineBase || bodyData.joint[jc.thisJoint].normalRotation == Quaternion.identity)
                continue;
			if (bodyData.joint[jc.thisJoint].trackingState == KinectInterop.TrackingState.NotTracked)
				continue;

			int prevJoint = (int)manager.GetParentJoint((KinectInterop.JointType)jc.thisJoint);
			if (bodyData.joint[prevJoint].trackingState == KinectInterop.TrackingState.NotTracked) 
				continue;

			Quaternion rotJointN = bodyData.joint[jc.thisJoint].normalRotation;
			Quaternion rotParentN = bodyData.joint[prevJoint].normalRotation;

			Quaternion rotLocalN = Quaternion.Inverse(rotParentN) * rotJointN;
			Vector3 eulerAnglesN = rotLocalN.eulerAngles;
			
			Quaternion rotJointM = bodyData.joint[jc.thisJoint].mirroredRotation;
			Quaternion rotParentM = bodyData.joint[prevJoint].mirroredRotation;
			
			Quaternion rotLocalM = Quaternion.Inverse(rotParentM) * rotJointM;
			Vector3 eulerAnglesM = rotLocalM.eulerAngles;

//			if(debugText != null && jc.thisJoint == (int)KinectInterop.JointType.SpineShoulder)
//			{
//				debugText.guiText.text = string.Format("{0} - ({1:000}, {2:000}, {3:000})", ((KinectInterop.JointType)jc.thisJoint).ToString(), 
//				                                       eulerAnglesN.x, eulerAnglesN.y, eulerAnglesN.z);
//			}
			
			bool isConstrained = false;

			for(int a = 0; a < jc.axisConstrainrs.Count; a++)
			{
				AxisOrientationConstraint ac = jc.axisConstrainrs[a];
				
				Quaternion axisRotation = Quaternion.AngleAxis(eulerAnglesN[ac.axis], ac.rotateAround);
				float angleFromMin = Quaternion.Angle(axisRotation, ac.minQuaternion);
				float angleFromMax = Quaternion.Angle(axisRotation, ac.maxQuaternion);
				 
				if(!(angleFromMin <= ac.angleRange && angleFromMax <= ac.angleRange))
				{
					// correct the axis that has fallen out of range.
					if(angleFromMin > angleFromMax)
					{
						eulerAnglesN[ac.axis] = ac.angleMax;
					}
					else
					{
						eulerAnglesN[ac.axis] = ac.angleMin;
					}

					// fix mirrored rotation as well
					if(ac.axis == 0)
					{
						eulerAnglesM[ac.axis] = eulerAnglesN[ac.axis];
					}
					else
					{
						eulerAnglesM[ac.axis] = -eulerAnglesN[ac.axis];
					}
					
					isConstrained = true;
				}
			}
			
			if(isConstrained)
			{
				rotLocalN = Quaternion.Euler(eulerAnglesN);
				rotJointN = rotParentN * rotLocalN;

				rotLocalM = Quaternion.Euler(eulerAnglesM);
				rotJointM = rotParentM * rotLocalM;
				
				// Put it back into the bone directions
				bodyData.joint[jc.thisJoint].normalRotation = rotJointN;
				bodyData.joint[jc.thisJoint].mirroredRotation = rotJointM;
//				dirJoint = constrainedRotation * dirParent;
//				bodyData.joint[jc.thisJoint].direction = dirJoint;
			}
			
        }
    }


    private struct BoneOrientationConstraint
    {
		public int thisJoint;
		public List<AxisOrientationConstraint> axisConstrainrs;
		
		
		public BoneOrientationConstraint(int thisJoint)
        {
			this.thisJoint = thisJoint;
			axisConstrainrs = new List<AxisOrientationConstraint>();
        }
    }
	
	
	private struct AxisOrientationConstraint
	{
		// the axis to rotate around
		public int axis;
		public Vector3 rotateAround;
				
		// min, max and range of allowed angle
		public float angleMin;
		public float angleMax;
		
		public Quaternion minQuaternion;
		public Quaternion maxQuaternion;
		public float angleRange;
				
		
		public AxisOrientationConstraint(ConstraintAxis axis, float angleMin, float angleMax)
		{
			// Set the axis that we will rotate around
			this.axis = (int)axis;
			
			switch(axis)
			{
				case ConstraintAxis.X:
					this.rotateAround = Vector3.right;
					break;
				 
				case ConstraintAxis.Y:
					this.rotateAround = Vector3.up;
					break;
				 
				case ConstraintAxis.Z:
					this.rotateAround = Vector3.forward;
					break;
			
				default:
					this.rotateAround = Vector3.zero;
					break;
			}
			
			// Set the min and max rotations in degrees
			this.angleMin = angleMin;
			this.angleMax = angleMax;
			
				 
			// Set the min and max rotations in quaternion space
			this.minQuaternion = Quaternion.AngleAxis(angleMin, this.rotateAround);
			this.maxQuaternion = Quaternion.AngleAxis(angleMax, this.rotateAround);
			this.angleRange = angleMax - angleMin;
		}
	}
	
}