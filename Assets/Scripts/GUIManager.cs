using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

    public static GUIManager it;

    public Vector2 position = new Vector2(0, 0);
    public Vector2 size = new Vector2(0, 0);
    public Image HealthFull;
    public Image HealthEmpty;
    public float Health = 0;
    public float MaxHealth;

    float[] abilityCds = new float[4];
    public Image[] abilityIcons = new Image[4];

    public Image CardBack;
    public Text CardName, CardDiff;

	public Image LoadingScreen;
	public Image LoadingBarFull, LoadingBarEmpty;
	public Text LoadingText;

    public Text GO;

    public Image A1, A2, A3, A4;

    public GameObject bossUI;
    public Image bossHealthBar;
    Damageable bossHealth;

    void Awake()
    {
        if (it != this)
        {
            it = this;
            Init();
        }
    }

    void Init()
    {
        GO.enabled = false;
    }

    public void StartBoss(string name, Damageable health)
    {
        bossUI.SetActive(true);
        bossHealthBar.fillAmount = 1;
        bossHealth = health;
        bossUI.transform.Find("Name").GetComponent<Text>().text = name;
    }

    public void StopBoss()
    {
        bossUI.SetActive(false);
    }

    void SetAbilities(Weapon weapon, int count, int start)
    {
        for(int i = 0; i < count; i++)
        {         
            Ability ability = weapon.abilities[i];
            abilityCds[start + i] = ability.CD;
            abilityIcons[start + i].sprite = ability.Icon;
        }
    }

    public void SetAbilities(Weapon mainHand, Weapon offhand = null)
    {
        if(offhand == null)
        {
            SetAbilities(mainHand, mainHand.abilities.Count, 0);
        }
        else
        {
            SetAbilities(mainHand, 2, 0);
            SetAbilities(offhand, 2, 2);
        }
    }

	public void LoadBarInc(float i)
	{
	//	Debug.Log ("Loading" + i);
		LoadingBarFull.fillAmount += i;
		if (LoadingBarFull.fillAmount == 1) {
			LoadingScreen.enabled = false;
			LoadingBarEmpty.enabled = false;
			LoadingBarFull.enabled = false;
			LoadingText.enabled = false;
			PlayerController.it.enabled = true;
			A1.enabled = true;
			A2.enabled = true;
			A3.enabled = true;
			A4.enabled = true;
			HealthFull.enabled = true;
			HealthEmpty.enabled = true;
            Camera.main.GetComponent<CameraController>().load = true;
		}
	}

    void Update () {
        float maxHealth = PlayerController.it.damageable.maxHealth;
        float health = PlayerController.it.damageable.GetHealth();
        HealthFull.fillAmount = health / maxHealth;
        if(Health < 0)
        {
            GO.enabled = true;
        }

        if (Input.GetKeyDown("1"))
        {
            if (A1.color == Color.white)
            {
                A1.color = Color.grey;
                PlayerController.it.mainHandWep.abilityOne(PlayerController.it.transform);
                StartCoroutine(CoolDown(abilityCds[0], A1));
            }

        }
        if (Input.GetKeyDown("2"))
        {
            if (A2.color == Color.white)
            {
                A2.color = Color.grey;
                PlayerController.it.mainHandWep.abilityTwo(PlayerController.it.transform);
                StartCoroutine(CoolDown(abilityCds[1], A2));
            }

        }
        if (Input.GetKeyDown("3"))
        {
            if (A3.color == Color.white)
            {
                A3.color = Color.grey;
                if (PlayerController.it.mainHandWep.is2H)
                    PlayerController.it.mainHandWep.abilityThree(PlayerController.it.transform);
                else
                    PlayerController.it.offhandWep.abilityOne(PlayerController.it.transform);
                StartCoroutine(CoolDown(abilityCds[2], A3));
            }

        }
        if (Input.GetKeyDown("4"))
        {
            if (A4.color == Color.white)
            {
                A4.color = Color.grey;
                if (PlayerController.it.mainHandWep.is2H)
                    PlayerController.it.mainHandWep.abilityFour(PlayerController.it.transform);
                else
                    PlayerController.it.offhandWep.abilityTwo(PlayerController.it.transform);
                StartCoroutine(CoolDown(abilityCds[3], A4));
            }

        }

        if (bossUI.activeSelf)
        {
            bossHealthBar.fillAmount = bossHealth.GetHealth() / bossHealth.maxHealth;
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

