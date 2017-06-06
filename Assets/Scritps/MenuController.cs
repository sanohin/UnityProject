using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public CustomButton playButton;
    void Start()
    {
        playButton.signalOnClick.AddListener(this.onPlayClick);
    }

    public void onPlayClick()
    {
        SceneManager.LoadScene("LevelSelect");
        Application.Quit();
    }

    public void onSettingsClick()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
