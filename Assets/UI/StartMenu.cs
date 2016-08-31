using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartMenu : MonoBehaviour {

    public void Play()
    {
        SceneManager.LoadScene("Dungeon");
    }
}
