using UnityEngine;
using System.Collections;


//Attach to the Camera. Should be put on a prefab.
public class PlayerController : MonoBehaviour {

    public GameObject Player;

    public float MaxHealth = 100;

    public float jumpSpeed = 100;
    public float speed = 1.0f;

    public float CameraYoffset = 3;

	public Transform tLeft;
	public Transform bRight;
	public LayerMask groundLayers;
	public LayerMask platforms;
	public LayerMask PlayerMask;

    GameObject PlayerWeaponL;
    GameObject PlayerWeaponR;

    public float CameraDistance = -10;

    public float Health = 100;

    public Weapon LHand;
    public Weapon RHand;
    public Armour Arm;
    bool jumping = false;

    public void Start()
    {
        //Initilise Armour
        MaxHealth += Arm.extraHP;
        Health += Arm.extraHP;
        Player.GetComponent<Rigidbody2D>().mass += Arm.weight;
        Physics2D.IgnoreLayerCollision(10, 13, true);
        
        //Initiliase Weapons
        PlayerWeaponL = Player.transform.Find("Weapon").gameObject;
        PlayerWeaponL.GetComponent<MeshFilter>().mesh = LHand.Model;
        PlayerWeaponL.GetComponent<MeshRenderer>().material = LHand.weaponMat;
        if(!LHand.is2H)
        {
            PlayerWeaponR = Player.transform.Find("WeaponDual").gameObject;
            PlayerWeaponR.GetComponent<MeshFilter>().mesh = RHand.Model;
            PlayerWeaponR.GetComponent<MeshRenderer>().material = RHand.weaponMat;
        }

        if(LHand.isRanged)
        {
            PlayerWeaponL.GetComponent<BoxCollider2D>().enabled = false;
        }
        if(RHand != null && RHand.isRanged)
        {
            PlayerWeaponR.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
     void Update()
    {

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }     
        //Read Inputs here
        if (Input.GetButtonDown("Fire1") && isGrounded())
        {
            jumping = true;
			Vector3 rayStart = transform.position;
			rayStart.y -= 1;
            if (Physics2D.Raycast(rayStart, Vector2.up, 2.0f, platforms))    
			{
				Debug.Log ("Hit a Platform");
                Physics2D.IgnoreLayerCollision(10, 9, true);
                StartCoroutine(turnOnCollision(0.8f));

            }
        }
        
    }

    void LateUpdate()
    {
        //Set the position of the camera to follow the player
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + CameraYoffset, CameraDistance);
    }

	void FixedUpdate () {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
  
		if (jumping)
        {
            Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            jumping = false;
          
        }
        //Allows player to jump up from under platforms
	    if (V < 0) {
			Physics2D.IgnoreLayerCollision (10, 9,true); 
			StartCoroutine(turnOnCollision (0.5f));
		}

        if(H != 0 )
        {
			if (H < 0)
				Player.transform.localScale = new Vector3 (-1, transform.localScale.y, 1);
			if(H > 0)
				Player.transform.localScale = new Vector3 (1, transform.localScale.y, 1);
			
            Vector2 Vel = Player.GetComponent<Rigidbody2D>().velocity;
            Vel.x = 0;
            
            Player.GetComponent<Rigidbody2D>().velocity = Vel;

            Player.GetComponent<Rigidbody2D>().AddForce(new Vector2((H * speed), Vel.y) - Player.GetComponent<Rigidbody2D>().velocity, ForceMode2D.Force);
           // Vel.x = (H * speed);
          //  Vel.y = Vel.y;
            Vel.x = Mathf.Clamp(Vel.x, 0.0f, speed);
            Player.GetComponent<Rigidbody2D>().velocity = Vel;
        }
 
    }

	bool isGrounded()
	{
		return Physics2D.OverlapArea (tLeft.position, bRight.position, groundLayers);
	}

	IEnumerator turnOnCollision(float time)
	{
		yield return new WaitForSeconds (time);
		Physics2D.IgnoreLayerCollision (10, 9,false); 
	}
}
