using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager it;

    public float cardDelay = Mathf.Infinity;

    Deck deck;
    float untilNextCard = 0.0f;

    void Awake()
    {
        if (it == null)
        {
            it = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else if (it != this)
        {
            Destroy(gameObject);
        }
    }
    
	void Init () {
        Random.InitState(System.Environment.TickCount);
	}

    public void SetDeck(Deck deck)
    {
        this.deck = deck;
        this.deck.Shuffle();
    }

    public Deck GetDeck()
    {
        return deck;
    }

    void CastCard()
    {
        Room room = PlayerController.it.GetComponent<RoomTracker>().current;
        Card card = deck.Draw();
        if (card == null || room == null) return;

        GameObject monster = Instantiate(card.monster);
        monster.transform.rotation = Quaternion.identity;

        SpawnArea[] spawns = new SpawnArea[0];
        if (monster.GetComponent<Behaviours.GroundFollow>() != null)
            spawns = room.groundSpawns;
        else if (monster.GetComponent<Behaviours.FlyingFollow>() != null)
            spawns = room.flyingSpawns;

        if (spawns.Length <= 0) return;

        int index = Random.Range(0, spawns.Length - 1);
        Vector3 position = spawns[index].GetRandomPosition();
        position.z = 0;

        monster.transform.position = position;
    }

	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name != "Dungeon" || deck.cards == null)
            return;

        untilNextCard -= Time.deltaTime;

	    if(untilNextCard <= 0.0f)
        {
            CastCard();
            untilNextCard = cardDelay;
        }
	}
}
