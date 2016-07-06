using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    public bool Up ,Down,Left ,Right;

    public bool collided;

    public Doorway[] GetExits()
    {
        Doorway[] doorways = GetComponentsInChildren<Doorway>();
        return doorways;
    }

    void Start()
    {
       // StartCoroutine(CheckForCollision());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Room")
            collided = true;
    }

    IEnumerator CheckForCollision()
    {
        yield return new WaitForSeconds(1.5f);

        Debug.Log(collided);
        if(collided)
        {
            Debug.Log("Rrmoving Room");
            Destroy(this.gameObject);
        }

    }

   

}
