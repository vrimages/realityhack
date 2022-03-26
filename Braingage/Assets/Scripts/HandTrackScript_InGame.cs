using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class HandTrackScript_InGame : MonoBehaviour
{
    public enum HandPoses { Ok, Finger, Thumb, OpenHand, Fist, NoPose };
    public enum HandPoses2 { Ok, Finger, Thumb, OpenHand, Fist, NoPose };

    public HandPoses pose = HandPoses.NoPose;
    public HandPoses2 pose2 = HandPoses2.NoPose;

    public Vector3[] pos;
    public GameObject sphereThumb, sphereIndex, sphereMiddle, sphereWrist, sphereRightIndex, sphereRightThumb, sphereRightMiddle;
    //sphereIndex2

    public GameObject testUI;

    private MLHandTracking.HandKeyPose[] _gestures;
    private MLHandTracking.HandKeyPose[] _gestures2;

    public AudioSource audio;

    bool audioReady = true;

    public GameObject particleObj;

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

        _gestures2 = new MLHandTracking.HandKeyPose[5];
        _gestures2[0] = MLHandTracking.HandKeyPose.Ok;
        _gestures2[1] = MLHandTracking.HandKeyPose.Finger;
        _gestures2[2] = MLHandTracking.HandKeyPose.OpenHand;
        _gestures2[3] = MLHandTracking.HandKeyPose.Fist;
        _gestures2[4] = MLHandTracking.HandKeyPose.Thumb;
        MLHandTracking.KeyPoseManager.EnableKeyPoses(_gestures2, true, false);

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
            if(particleObj.activeSelf == false){
                testUI.SetActive(true);
            }
            
            if(audioReady == true){
                audioReady = false;
                //audio.Play();
            }
        }
        else if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Fist))
        {
            pose = HandPoses.Fist;
        }
        else if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Thumb))
        {
            pose = HandPoses.Thumb;
        }else
        {
            pose = HandPoses.NoPose;
            audioReady = true;
        }

        
        if (GetGesture2(MLHandTracking.Right, MLHandTracking.HandKeyPose.Ok))
        {
            pose2 = HandPoses2.Ok;
        }else if (GetGesture2(MLHandTracking.Right, MLHandTracking.HandKeyPose.Finger))
        {
            pose2 = HandPoses2.Finger;
        }else if (GetGesture2(MLHandTracking.Right, MLHandTracking.HandKeyPose.OpenHand))
        {
            pose2 = HandPoses2.OpenHand;
            if(particleObj.activeSelf == false){
                testUI.SetActive(true);
            }
            //audio.Play();
        }else if (GetGesture2(MLHandTracking.Right, MLHandTracking.HandKeyPose.Fist))
        {
            pose2 = HandPoses2.Fist;
        }
        else if (GetGesture2(MLHandTracking.Right, MLHandTracking.HandKeyPose.Thumb))
        {
            pose2 = HandPoses2.Thumb;
        }
        else
        {
            pose2 = HandPoses2.NoPose;
        }

        if (pose != HandPoses.NoPose || pose2 != HandPoses2.NoPose) ShowPoints();

        if (pose != HandPoses.OpenHand && pose2 != HandPoses2.OpenHand && pose2 == HandPoses2.NoPose && pose == HandPoses.NoPose){
            testUI.SetActive(false);
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

        if(MLHandTracking.Right.Index.KeyPoints[2].Position != null){
            pos[5] = MLHandTracking.Right.Index.KeyPoints[2].Position;
            sphereRightIndex.transform.position = pos[5];
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

    private bool GetGesture2(MLHandTracking.Hand hand, MLHandTracking.HandKeyPose type)
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
