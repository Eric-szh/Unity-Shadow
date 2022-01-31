using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public string _nextScene;
    public VideoPlayer _vp;
    void Update()
    {
        if ((_vp.frame) > 0 && (_vp.isPlaying == false))
        {
            int pnum = PlayerPrefs.GetInt("LevelProgress");
            PlayerPrefs.SetInt("LevelProgress", pnum + 1);
            SceneManager.LoadScene(_nextScene);
        }
    }
}
