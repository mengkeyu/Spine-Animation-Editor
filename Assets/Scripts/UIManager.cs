using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public SpineAnimationControl spineAnimationControl;

    public Slider speedSlider;
    public GameObject infoPanel;

    private int num;

    private void Start() 
    {
        num = 1;    
    }

    private void Update() 
    {
        ChangePlaySpeed();
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
        speedSlider.GetComponentInChildren<Text>().text = string.Format("倍速：{0}",speedSlider.value + 1.0f);
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
