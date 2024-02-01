using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(SpineAnimationControl))]
public class InspectorManager : Editor
{
    public override void OnInspectorGUI()
    {
        SpineAnimationControl spineControl = (SpineAnimationControl)target;

        DrawDefaultInspector();

        if(GUILayout.Button("替换动画"))
        {
            spineControl.ChangeSkeletonDateAsset();
        }
    }
}
