using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

   public GUIManager GUI;
    string CardName = "imp";
    int difficulty;

    public ParticleSystem diffGlow;

    void Start()
    {
        GUI = GameObject.FindObjectOfType<GUIManager>();
        Debug.Log(difficulty);
        switch(difficulty)
        {
            case 1:
                diffGlow.startColor = Color.white;
                break;
            case 2:
                diffGlow.startColor = Color.green;
                break;
            case 3:
                diffGlow.startColor = Color.blue;
                break;
            case 4:
                diffGlow.startColor = new Color(238, 130, 238);
                break;
            case 5:
                diffGlow.startColor = new Color(255, 215, 0);
                break;
        }
        Debug.Log(diffGlow.startColor);
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            GUI.DisplayPickup(CardName, difficulty);
            Debug.Log("Card Picked Up");
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public void SetDifficulty(int d)
    {
        difficulty = d;
    }
}
