using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

    public Vector2 position = new Vector2(0, 0);
    public Vector2 size = new Vector2(0, 0);
    public Image HealthFull;
    public Image HealthEmpty;
    public float Health = 0;
    float MaxHealth;
    public float cD1, cD2, cD3, cD4; 

    public Image A1, A2, A3, A4;

    public void setMaxHP(float hp)
    {
        MaxHealth = hp;
    }

    void Update () {


       HealthFull.fillAmount = Health / MaxHealth;
        

        if (Input.GetKeyDown("1"))
        {
            A1.color = Color.grey;
            StartCoroutine(CoolDown(cD1, A1));
        }
        if (Input.GetKeyDown("2"))
        {
            A2.color = Color.grey;
            StartCoroutine(CoolDown(cD2, A2));
        }
        if (Input.GetKeyDown("3"))
        {
            A3.color = Color.grey;
            StartCoroutine(CoolDown(cD3, A3));
        }
        if (Input.GetKeyDown("4"))
        {
            A4.color = Color.grey;
            StartCoroutine(CoolDown(cD4, A4));
        }

    }

    IEnumerator CoolDown(float time, Image Ability)
    {
        yield return new WaitForSeconds(time);
        Ability.color = Color.white;
        
    }
}
