using UnityEngine;
using System.Collections;

public class DropManger : MonoBehaviour {

    public GameObject Card;

    public void DropItem(Vector3 position)
    {
        //Drops a card. Cards don't do a great deal right now and this system still needs a tonne of work
        GameObject CardObj = Instantiate(Card, position, Quaternion.identity) as GameObject;
        CardObj.GetComponentInChildren<Pickup>().SetDifficulty(GetDifficulty());
    }

    int GetDifficulty()
    {
        return 1;
    }

}
