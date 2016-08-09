using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

    public Vector2 position = new Vector2(0, 0);
    public Vector2 size = new Vector2(0, 0);
    public Image HealthFull;
    public Image HealthEmpty;
    public float Health = 0;
    public float MaxHealth;
    public float cD1, cD2, cD3, cD4;

    public Image CardBack;
    public Text CardName, CardDiff;

	public Image LoadingScreen;
	public Image LoadingBarFull, LoadingBarEmpty;
	public Text LoadingText;

    PlayerController Player;

    public Text GO;

    public Image A1, A2, A3, A4;

    public void Start()
    {
        GO.enabled = false;
        Player = Camera.main.GetComponent<PlayerController>();
		Player.enabled = false;
    }

	void LoadBarInc(float i)
	{
	//	Debug.Log ("Loading" + i);
		LoadingBarFull.fillAmount += i;
		if (LoadingBarFull.fillAmount == 1) {
			LoadingScreen.enabled = false;
			LoadingBarEmpty.enabled = false;
			LoadingBarFull.enabled = false;
			LoadingText.enabled = false;
			Player.enabled = true;
			A1.enabled = true;
			A2.enabled = true;
			A3.enabled = true;
			A4.enabled = true;
			HealthFull.enabled = true;
			HealthEmpty.enabled = true;
		}
	}

    void Update () {

		MaxHealth = Player.MaxHealth;
      	HealthFull.fillAmount = Health / MaxHealth;

        Health = Player.Health;
        if(Health < 0)
        {
            GO.enabled = true;
        }

        if (Input.GetKeyDown("1"))
        {
			if (A1.color == Color.white) 
			{
				A1.color = Color.grey;
				Player.LHand.abilityOne (Player.Player.transform);
				StartCoroutine (CoolDown (cD1, A1));
			}

        }
        if (Input.GetKeyDown("2"))
        {
			if (A2.color == Color.white) {
							A2.color = Color.grey;
				Player.LHand.abilityTwo (Player.Player.transform);
				StartCoroutine (CoolDown (cD2, A2));
			}

        }
        if (Input.GetKeyDown("3"))
        {
			if (A3.color == Color.white) {
				A3.color = Color.grey;
				if (Player.LHand.is2H)
					Player.LHand.abilityThree (Player.Player.transform);
				else
					Player.RHand.abilityOne (Player.Player.transform);
				StartCoroutine (CoolDown (cD3, A3));
			}

        }
        if (Input.GetKeyDown("4"))
        {
			if (A4.color == Color.white) {
				A4.color = Color.grey;
				if (Player.LHand.is2H)
					Player.LHand.abilityFour (Player.Player.transform);
				else
					Player.RHand.abilityTwo (Player.Player.transform);				
				StartCoroutine (CoolDown (cD4, A4));
			}
		
        }

    }

    public void DisplayPickup(string n, int d)
    {
        Debug.Log("Display Pickup");
        CardName.text = n;
        switch (d)
        {
            case 1:
                CardDiff.text = "Easy";
                break;
            case 2:
                CardDiff.text = "Medium";
                break;
            case 3:
                CardDiff.text = "Hard";
                break;
            case 4:
                CardDiff.text = "Insane";
                break;
            case 5:
                CardDiff.text = "Boss";
                break;
        }

        CardBack.enabled = true;
        CardDiff.enabled = true;
        CardName.enabled = true;
        StartCoroutine("DisplayTimer");
    }

    IEnumerator DisplayTimer()
    {

        yield return new WaitForSeconds(2.0f);
        CardBack.enabled = false;
        CardDiff.enabled = false;
        CardName.enabled = false;
    }


    IEnumerator CoolDown(float time, Image Ability)
    {
        yield return new WaitForSeconds(time);
        Ability.color = Color.white;    
    }
}
