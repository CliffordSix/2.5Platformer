using UnityEngine;
using System.Collections;

public class GreatswordShockwave : Ability {

    public ParticleSystem Slash;

    public GameObject Shockwave;

    public override void CallAbility(GameObject projectile)
    {
        Shockwave = Instantiate(Shockwave, transform.position , Quaternion.identity) as GameObject;
        Shockwave.transform.localScale = Vector3.one;
        //Shockwave.transform.localPosition = Vector3.zero;
        Shockwave.SetActive(true);
        Shockwave.GetComponent<Rigidbody2D>().velocity = new Vector3(10, 0, 0);
    }

    void Update()
    {
      

    }

}
