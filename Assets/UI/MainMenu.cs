using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public DeckList deckList;

    public void Play()
    {
        GameManager.it.SetDeck(deckList.selected);
        SceneManager.LoadScene("Dungeon");
    }
}
