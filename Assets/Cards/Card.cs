using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

    public Transform model;

	// Use this for initialization
	void Start () {
	
	}

    public void SetCardDetail(CardDetail details)
    {
        model.GetComponent<TextMesh>();
        model.Find("name_text").GetComponent<TextMesh>().text = details.name;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
