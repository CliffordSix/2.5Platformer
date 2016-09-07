using UnityEngine;
using System.Collections;

public class GreatswordShockwave : Ability {

    public ParticleSystem Slash;

    public GameObject Shockwave;

    public override void CallAbility(GameObject projectile)
    {
        Shockwave = Instantiate(Shockwave, transform.position , Quaternion.identity) as GameObject;
        Shockwave.transform.localScale = new Vector3(PlayerController.it.transform.localScale.x,1,1);
        
        //Shockwave.transform.localPosition = Vector3.zero;
        Shockwave.SetActive(true);
        Shockwave.GetComponent<Rigidbody2D>().velocity = new Vector3(10, 0, 0) * PlayerController.it.transform.localScale.x;
    }

    void Update()
    {
      

    }

}
