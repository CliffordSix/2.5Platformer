using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

   public GUIManager GUI;
    string CardName = "imp";
    int difficulty;

    void Start()
    {
        GUI = GameObject.FindObjectOfType<GUIManager>();
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            GUI.DisplayPickup(CardName, difficulty);
            Debug.Log("Card Picked Up");
            Destroy(gameObject);
        }
    }

    public void SetDifficulty(int d)
    {
        difficulty = d;
    }
}
