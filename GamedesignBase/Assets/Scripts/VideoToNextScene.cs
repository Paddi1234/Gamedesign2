using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class VideoToNextScene : MonoBehaviour
{
    public string nextSceneName = "NextGameScene";
    private bool videoFinished = false;

    void Start()
    {
        var player = GetComponent<VideoPlayer>();
        player.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        videoFinished = true;
    }

    void Update()
    {
        if (videoFinished && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}