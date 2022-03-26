using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class HandTrackScript_Menu : MonoBehaviour
{
    public enum HandPoses { Ok, Finger, Thumb, OpenHand, Fist, NoPose };
    public HandPoses pose = HandPoses.NoPose;
    public Vector3[] pos;
    public GameObject sphereThumb, sphereIndex, sphereMiddle, sphereWrist, sphereRightIndex, sphereRightThumb, sphereRightMiddle;
    //sphereIndex2

    public GameObject testUI;

    private MLHandTracking.HandKeyPose[] _gestures;

    //public AudioSource audio;

    private void Start()
    {
        MLHandTracking.Start();
        _gestures = new MLHandTracking.HandKeyPose[5];
        _gestures[0] = MLHandTracking.HandKeyPose.Ok;
        _gestures[1] = MLHandTracking.HandKeyPose.Finger;
        _gestures[2] = MLHandTracking.HandKeyPose.OpenHand;
        _gestures[3] = MLHandTracking.HandKeyPose.Fist;
        _gestures[4] = MLHandTracking.HandKeyPose.Thumb;
        MLHandTracking.KeyPoseManager.EnableKeyPoses(_gestures, true, false);
        pos = new Vector3[7];
    }
    private void OnDestroy()
    {
        MLHandTracking.Stop();
    }


    private void Update()
    {
        if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Ok))
        {
            pose = HandPoses.Ok;
        }
        else if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Finger))
        {
            pose = HandPoses.Finger;
        }
        else if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.OpenHand))
        {
            pose = HandPoses.OpenHand;
            //testUI.SetActive(true);
            //audio.Play();

        }
        else if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Fist))
        {
            pose = HandPoses.Fist;
        }
        else if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Thumb))
        {
            pose = HandPoses.Thumb;
        }else if (GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.Ok))
        {
            pose = HandPoses.Ok;
        }else if (GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.Finger))
        {
            pose = HandPoses.Finger;
        }else if (GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.OpenHand))
        {
            pose = HandPoses.OpenHand;
            //testUI.SetActive(true);
            //audio.Play();
        }else if (GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.Fist))
        {
            pose = HandPoses.Fist;
        }
        else if (GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.Thumb))
        {
            pose = HandPoses.Thumb;
        }
        else
        {
            pose = HandPoses.NoPose;
        }

        if (pose != HandPoses.NoPose) ShowPoints();

        if (pose != HandPoses.OpenHand){
            //testUI.SetActive(false);
        }
    }

    private void ShowPoints()
    {
        
        // Left Hand Thumb tip
        if(MLHandTracking.Left.Thumb.KeyPoints[2].Position != null){
            pos[0] = MLHandTracking.Left.Thumb.KeyPoints[2].Position;
            sphereThumb.transform.position = pos[0];
        }
        
        

        
        // Left Hand Index finger tip 
        if(MLHandTracking.Left.Index.KeyPoints[2].Position != null){
            pos[1] = MLHandTracking.Left.Index.KeyPoints[2].Position;
            sphereIndex.transform.position = pos[1];
        } 
        
        
        if(MLHandTracking.Right.Index.KeyPoints[2].Position != null){
            pos[5] = MLHandTracking.Right.Index.KeyPoints[2].Position;
            sphereRightIndex.transform.position = pos[5];
        }

        
        if(MLHandTracking.Left.Middle.KeyPoints[2].Position != null){
            pos[2] = MLHandTracking.Left.Middle.KeyPoints[2].Position;
            sphereMiddle.transform.position = pos[2];
        }

        if(MLHandTracking.Left.Wrist.KeyPoints[0].Position != null){
            pos[3] = MLHandTracking.Left.Wrist.KeyPoints[0].Position;
            sphereWrist.transform.position = pos[3];
        }

        if(MLHandTracking.Right.Index.KeyPoints[2].Position != null){
            pos[4] = MLHandTracking.Right.Index.KeyPoints[2].Position;
            sphereRightThumb.transform.position = pos[4];
        }

        

        if(MLHandTracking.Right.Middle.KeyPoints[2].Position != null){
            pos[6] = MLHandTracking.Right.Middle.KeyPoints[2].Position;
            sphereRightMiddle.transform.position = pos[6];
        }
        

        
        //sphereRightIndex.transform.position = pos[3];

        //pos[4] = MLHandTracking.Left.Index.KeyPoints[1].Position;
        //sphereIndex2.transform.position = pos[4];
        
        
    }

    private bool GetGesture(MLHandTracking.Hand hand, MLHandTracking.HandKeyPose type)
    {
        if (hand != null)
        {
            if (hand.KeyPose == type)
            {
                if (hand.HandKeyPoseConfidence > 0.9f)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
