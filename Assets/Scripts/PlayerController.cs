using UnityEngine;
using System.Collections;


//Attach to the Camera. Should be put on a prefab.
public class PlayerController : MonoBehaviour {

    public GameObject Player;

    public float jumpSpeed = 100;
    public float speed = 1.0f;
    Vector3 MoveDirection = new Vector3();
    public float CameraYoffset = 3;

	public Transform tLeft;
	public Transform bRight;
	public LayerMask groundLayers;
	public LayerMask platforms;
	public LayerMask PlayerMask;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        MoveDirection.x = H / 100;
       // MoveDirection.x *= speed;
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + CameraYoffset , -5);
		if (Input.GetButtonDown("Fire1") && isGrounded())
        {
            Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed));
			if(Physics2D.Raycast (transform.position, Vector2.up, 1.0f, platforms)){
				Physics2D.IgnoreLayerCollision (10, 9,true); 
				StartCoroutine(turnOnCollision (0.5f));
			
			}
        }

		if (V < 0) {
			Physics2D.IgnoreLayerCollision (10, 9,true); 
			StartCoroutine(turnOnCollision (0.5f));
		}

        Player.transform.position += (new Vector3((speed * H), 0, 0));
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
