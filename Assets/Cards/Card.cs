using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

    public Transform model;

    public void SetCardDetail(CardDefinition details)
    {
        model.Find("name_text").GetComponent<TextMesh>().text = details.name;
        model.Find("flavour_text").GetComponent<TextMesh>().text = details.flavour_text;
    }
}
