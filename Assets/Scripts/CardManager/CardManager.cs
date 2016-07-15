using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public struct CardData {
	public uint id;
	public string name;
	public string flavour_text;
}
	
public class CardManager : MonoBehaviour
{
	public string card_definition_folder_ = "card_definitions";
	public List<CardData> cards_;

	void Start() {
		Load ();
	}

	public void Load() {
		Object[] card_resources = Resources.LoadAll(card_definition_folder_);
		if (card_resources == null) {
			Debug.Log ("Loading cards from folder failed");
			return;
		}
		foreach (TextAsset card_resource in card_resources) {
			CardData data = JsonUtility.FromJson<CardData>(card_resource.text);
			cards_.Add (data);
		}
	}
}
