using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public int level;
    void OnTriggerEnter2D(Collider2D collider)
    {
        SceneManager.LoadScene("Level" + level);
    }
}

