using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using System;
using UnityEngine.Animations;
using Unity.VisualScripting;

[ExecuteInEditMode]
public class SpineAnimationControl : MonoBehaviour
{
    [HideInInspector]public SkeletonAnimation skeletonAnimation;
    private SkeletonDataAsset skeletonData;

    [HideInInspector]public Text timerText;
    [HideInInspector]public Slider slider;
    [HideInInspector]public GameObject playButton;
    [HideInInspector]public GameObject pauseButton;

    [SerializeField][Header("动画片段")]private List<AnimInfo> animInfo = new List<AnimInfo>();

    private float timer;
    private string animName;
    
    private float totleTime;
    private float currentTime;

    private bool isPlaying = false;


    [Header("动画对象")]public SkeletonDataAsset skeletonDataAsset;

    private void Start() 
    {
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
        skeletonData = skeletonAnimation.skeletonDataAsset;
        
        
        //获得机器所有动画的总时长
        totleTime = 0;
        for(int i = 0;i < animInfo.Count;i++)
        {
            if(animInfo[i].animLoopTime > 0)
            {
                totleTime += animInfo[i].animLoopTime;
            }
            else
            {
                var anim = skeletonData.GetSkeletonData(true).FindAnimation(animInfo[i].AnimationName);
                totleTime += anim.Duration;
            }
        }
        currentTime = totleTime;
    }

    private void Update() 
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer == 0)
            {
                return;
            }
        }

        //将循环时间显示在text上
        string animTime = Math.Round(timer).ToString();
        timerText.text = string.Format("动画片段：{0}\n动画时长：{1}",animName,animTime);

        //进度计时
        if(isPlaying)
        {
            currentTime -= Time.deltaTime;
            slider.value = Mathf.Clamp(currentTime/totleTime,0,1);
        }
    }

    public void OnPlayButtonClick()
    {   
        //更改按钮文本
        playButton.GetComponentInChildren<Text>().text = "重新播放";
        //显示暂停按钮
        pauseButton.SetActive(true);
        pauseButton.GetComponentInParent<HorizontalLayoutGroup>().padding.left += -100;

        isPlaying = true;
        currentTime = totleTime;

        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        /*
        skeletonAnimation.AnimationState.SetAnimation(0,animInfo[0].AnimationName,animInfo[0].isAnimLoop);
        //获取动画名称
        animName = animInfo[0].AnimationName;
        //获取动画播放时长
        var anim = skeletonData.GetSkeletonData(true).FindAnimation(animInfo[0].AnimationName);
        timer = anim.Duration;
        //等待第一段动画播放
        yield return new WaitForSeconds(anim.Duration);
        */

        for(int i = 0;i < animInfo.Count;i++)
        {
            if(animInfo[i].animLoopTime > 0)
            {
                yield return StartCoroutine(LoopTime(i)); 
            }
            else
            {
                skeletonAnimation.AnimationState.AddAnimation(0,animInfo[i].AnimationName,animInfo[i].isAnimLoop,0);
                //获取动画名称
                animName = animInfo[i].AnimationName;
                //获取动画播放时长
                var anim = skeletonData.GetSkeletonData(true).FindAnimation(animInfo[i].AnimationName);
                timer = anim.Duration;
                yield return new WaitForSeconds(anim.Duration);
            }
        }
    }

    private IEnumerator LoopTime(int i)
    {
        skeletonAnimation.AnimationState.AddAnimation(0,animInfo[i].AnimationName,animInfo[i].isAnimLoop,0);
        //获取动画名称
        animName = animInfo[i].AnimationName;
        //将当前循环时间给到timer
        timer = animInfo[i].animLoopTime;
        yield return new WaitForSeconds(animInfo[i].animLoopTime);
    }

    public void ChangeSkeletonDateAsset()
    {
        this.GetComponentInChildren<SkeletonAnimation>().skeletonDataAsset = skeletonDataAsset;
    }
}
