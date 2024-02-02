using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.UI;

public class RecorderControl : MonoBehaviour
{
    private RecorderControllerSettings controllerSettings;
    private RecorderController recorderController;
    private MovieRecorderSettings videoRecorder;

    private int num;
    private float recordStartTime;
    private float recordEndTime;

    public Button recorderButton;


    private void Start() 
    {
        controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        recorderController = new RecorderController(controllerSettings);
        videoRecorder = ScriptableObject.CreateInstance<MovieRecorderSettings>();
        
        videoRecorder.name = "Spine Animation Recorder";
        videoRecorder.Enabled = true;
        //videoRecorder.VideoBitRateMode = VideoBitrateMode.High;
        videoRecorder.ImageInputSettings = new GameViewInputSettings
        {
            OutputWidth = 640 * 3,
            OutputHeight = 480 * 3
        };
        videoRecorder.AudioInputSettings.PreserveAudio = true;
        videoRecorder.OutputFile = "Assets/Recordings/SpineAnim.mp4";

        controllerSettings.AddRecorderSettings(videoRecorder);
        controllerSettings.FrameRate = 60;
        //controllerSettings.SetRecordModeToFrameInterval(0,600);

        RecorderOptions.VerboseMode = false;
    }

    public void OnRecorderButtonClick()
    {
        if(num % 2 != 0)
        {
            num += 1;

            recorderButton.GetComponent<Image>().color = new Color(0,0.85f,0.2f);

            recorderController.PrepareRecording();
            recorderController.StartRecording();

            recordStartTime = Time.time;
        }
        else
        {
            num += 1;

            recorderButton.GetComponent<Image>().color = new Color(0.85f,0,0.2f);

            recorderController.StopRecording();

            recordEndTime = Time.time;

            int frameCount = Mathf.RoundToInt((recordEndTime - recordStartTime) * controllerSettings.FrameRate);
            controllerSettings.SetRecordModeToFrameInterval(0,frameCount - 1);
        }
    }
}
