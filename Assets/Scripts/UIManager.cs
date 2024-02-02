using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector]public SpineAnimationControl spineAnimationControl;
    [HideInInspector]public Camera mainCamera;
    [HideInInspector]public GameObject infoPanel;

    [Header("播放速度滑动器")]public Slider speedSlider;
    [Header("镜头大小滑动器")]public Slider cameraSlider;

    private int num;

    private void Start() 
    {
        num = 1;    
    }

    private void Update() 
    {
        ChangePlaySpeed();
        ChangeCameraSize();
    }

    public void OnPauseButtonClick()
    {
        if(num % 2 == 0)
        {
            num += 1;
            Time.timeScale = 1;
            spineAnimationControl.pauseButton.GetComponentInChildren<Text>().text = "暂停";

            //播放按钮状态变更
            spineAnimationControl.playButton.GetComponent<Button>().interactable = true;
            spineAnimationControl.playButton.GetComponent<Image>().color = new Color(1.0f,1.0f,1.0f);
        }
        else
        {
            num += 1;
            Time.timeScale = 0;
            spineAnimationControl.pauseButton.GetComponentInChildren<Text>().text = "继续";

            //播放按钮状态变更
            spineAnimationControl.playButton.GetComponent<Button>().interactable = false;
            spineAnimationControl.playButton.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
        }
    }

    private void ChangePlaySpeed()
    {
        spineAnimationControl.skeletonAnimation.state.TimeScale = speedSlider.value + 1.0f;
        speedSlider.GetComponentInChildren<Text>().text = string.Format("倍速x{0}",Mathf.RoundToInt(speedSlider.value + 1.0f));
    }

    private void ChangeCameraSize()
    {
        mainCamera.orthographicSize = (cameraSlider.value * -1) + 12;
        cameraSlider.GetComponentInChildren<Text>().text = string.Format("缩放x{0}",Mathf.RoundToInt(cameraSlider.value));
    }

    public void OnInfoButtonClick()
    {
        infoPanel.SetActive(true);
    }

    public void OnInfoBackButtonClick()
    {
        infoPanel.SetActive(false);
    }
}
