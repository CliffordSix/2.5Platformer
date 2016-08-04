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

    PlayerController Player;

    public Text GO;

    public Image A1, A2, A3, A4;

    public void Start()
    {
        GO.enabled = false;
        Player = Camera.main.GetComponent<PlayerController>();
        
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
				Player.LHand.abilityTwo ();
				StartCoroutine (CoolDown (cD2, A2));
			}

        }
        if (Input.GetKeyDown("3"))
        {
			if (A3.color == Color.white) {
				A3.color = Color.grey;
				if (Player.LHand.is2H)
					Player.LHand.abilityThree ();
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
					Player.LHand.abilityFour ();
				else
					Player.RHand.abilityTwo ();				
				StartCoroutine (CoolDown (cD4, A4));
			}
		
        }

    }

	IEnumerator CoolDown(float time, Image Ability)
    {
        yield return new WaitForSeconds(time);
        Ability.color = Color.white;    
    }
}
