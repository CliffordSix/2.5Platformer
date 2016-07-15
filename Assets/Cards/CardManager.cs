using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public struct CardDetail
{
    public int id;
    public string name;
    public string flavour_text;
}

public class CardManager : MonoBehaviour
{
    public string card_definition_folder_ = "Cards";
    public List<CardDetail> cards_;

    void Start()
    {
        string[] card_folders = Directory.GetDirectories("Assets/Resources/Cards");
        foreach (string card_folder in card_folders)
        {
            //Hacky? Could do with a native way
            string[] parts = card_folder.Split(Path.DirectorySeparatorChar);
            try
            {
                CardDetail card_detail = Load(parts[parts.Length - 1]);
                card_detail.id = cards_.Count;
                cards_.Add(card_detail);
                //TOOD: Load model too, create new gameObject, add model, create card script, set model/details, add to gameObject
            }
            catch(System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }

    CardDetail Load(string card_directory)
    {
        Debug.Log("Loading Card from: " + card_directory);
        string path = card_definition_folder_ + "/" + card_directory + "/" + "details";

        TextAsset card_detail_text = Resources.Load(path) as TextAsset;
        if(card_detail_text == null)
        {
            throw new System.Exception("Loading failed");
        }

        return JsonUtility.FromJson<CardDetail>(card_detail_text.text);
    }
}
