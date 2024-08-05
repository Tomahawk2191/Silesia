using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    private void Start()
    { 
        VideoPlayer videoPlayer =
        GameObject.Find("Canvas").transform.Find("MainMenuLoop").GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"MainMenuLoop.mp4");
        videoPlayer.Play();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
