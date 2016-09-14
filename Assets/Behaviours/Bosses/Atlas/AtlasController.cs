using UnityEngine;
using System.Collections;

public class AtlasController : MonoBehaviour {

    enum Pattern
    {
        IDLE,
        TARGET,
        SWEEP,
        BOUNCE,
        RAGE
    }

    public AtlasFist leftFist;
    public AtlasFist rightFist;

    public float maxBounceHeight = 6.0f;
    public float minFistSpeed = 0.0f;
    public float maxFistSpeed = 0.0f;

    public Trigger fightTrigger;

    Pattern pattern = Pattern.TARGET;

    bool fightStarted = false;
    float fightTime = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //if (!fightStarted && fightTrigger.IsActive())
        //    fightStarted = true;
        //if (fightStarted)
        //    fightTime += Time.deltaTime;
	}

    void IdleUpdate()
    {

    }

    public float bounceDelay = 1.0f;
    float untilBounce = 0.0f;

    void TargetPatternUpdate()
    {
        if(!leftFist.IsMoving() && !rightFist.IsMoving())
        {
            if (untilBounce > 0.0f)
                untilBounce -= Time.deltaTime;
            if (untilBounce <= 0.0f)
            {
                if (PlayerController.it.transform.position.x <= transform.position.x)
                    leftFist.Slam(PlayerController.it.transform);
                else
                    rightFist.Slam(PlayerController.it.transform);
                untilBounce = bounceDelay;
            }
        }
    }

    void SweepPatternUpdate()
    {

    }

    void BouncePatternUpdate()
    {

    }

    void RagePatternUpdate()
    {

    }

    void FixedUpdate()
    {
        switch (pattern)
        {
            case Pattern.IDLE: IdleUpdate(); break;
            case Pattern.TARGET: TargetPatternUpdate(); break;
            case Pattern.SWEEP: SweepPatternUpdate(); break;
            case Pattern.BOUNCE: BouncePatternUpdate(); break;
            case Pattern.RAGE: RagePatternUpdate(); break;
        }
    }
}
