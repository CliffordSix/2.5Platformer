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

    GUIManager GUIMan;
    public float Health = 100;

    bool jumping = false;

    void Start()
    {
    
        if(!(GUIMan = GetComponent<GUIManager>()))
        {
            Debug.Log("No Gui Manager attached to player controller");
        }
        else
        {
            //init guiMan
            GUIMan.setMaxHP(MaxHealth);
        }
    }


    void Update()
    {
        if(Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        GUIMan.Health = Health;
        //Read Inputs here
        if (Input.GetButtonDown("Fire1") && isGrounded())
        {
            jumping = true;
        }
        
    }
	
	void FixedUpdate () {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
  
        //Set the position of the camera to follow the player
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + CameraYoffset , -5);

		if (jumping)
        {
            Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            jumping = false;
            if (Physics2D.Raycast (transform.position, Vector2.up, 5.0f, platforms)){
				Physics2D.IgnoreLayerCollision (10, 9,true); 
				StartCoroutine(turnOnCollision (0.8f));
			
			}
        }
        //Allows player to jump up from under platforms
	    if (V < 0) {
			Physics2D.IgnoreLayerCollision (10, 9,true); 
			StartCoroutine(turnOnCollision (0.5f));
		}

        if(H != 0 )
        {
            Vector2 Vel = Player.GetComponent<Rigidbody2D>().velocity;
            Vel.x = 0;
            
            Player.GetComponent<Rigidbody2D>().velocity = Vel;

            Player.GetComponent<Rigidbody2D>().AddForce(new Vector2((H * speed), Vel.y) - Player.GetComponent<Rigidbody2D>().velocity, ForceMode2D.Force);
            
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
