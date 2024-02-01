using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;

[System.Serializable]
public class AnimInfo
{
    [SpineAnimation]public string AnimationName;public bool isAnimLoop;public float animLoopTime;
}

