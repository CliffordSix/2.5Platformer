using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

    public void Quit()
    {
        Application.Quit();
    }
    public void LoadGame()
    {
        Application.LoadLevel(1);
    }

}
