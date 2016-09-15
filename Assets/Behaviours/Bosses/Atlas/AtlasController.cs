using UnityEngine;
using System.Collections;

public class AtlasController : MonoBehaviour {

    enum Pattern
    {
        IDLE,
        TARGET,
        SWEEP,
        RAGE
    }

    public AtlasFist leftFist;
    public AtlasFist rightFist;
    public Damageable damageable;
    public Animator wallBreaker;
    public GameObject[] brokenBits;

    public int targetPatternCycles = 4;
    public int sweepPatternCycles = 1;

    public Trigger fightTrigger;

    Pattern pattern = Pattern.TARGET;
    Pattern lastPattern = Pattern.IDLE;

    bool fightStarted = false;
    float fightTime = 0.0f;
    
	void Start ()
    {
	}
	
	void Update ()
    {
        if (fightStarted)
            fightTime += Time.deltaTime;

        if (!fightStarted && fightTrigger != null && fightTrigger.IsActive())
        {
            fightStarted = true;
            wallBreaker.Play("Break");
            GUIManager.it.StartBoss("Atlas", damageable);
        }
        
        if(damageable.GetHealth() <= 0)
        {
            GUIManager.it.StopBoss();
        }
        
        AnimatorStateInfo anim = wallBreaker.GetCurrentAnimatorStateInfo(0);
        if (anim.IsName("Break") && anim.normalizedTime >= 1.0f)
        {
            wallBreaker.enabled = false;
            foreach(GameObject bit in brokenBits)
            {
                Destroy(bit);
            }
        }
    }

    void IdleUpdate()
    {

    }

    public float slamDelay = 1.0f;
    float untilSlam = 0.0f;

    int patternCycles = 0;

    void TargetPatternUpdate()
    {
        if (untilSlam > 0.0f)
            untilSlam -= Time.deltaTime;
        if (untilSlam <= 0.0f)
        {
            patternCycles++;
            if (PlayerController.it.transform.position.x <= transform.position.x)
                leftFist.Slam(PlayerController.it.transform.position);
            else
                rightFist.Slam(PlayerController.it.transform.position);
            untilSlam = slamDelay;
        }
    }

    void SweepPatternUpdate()
    {
        if (untilSlam > 0.0f)
            untilSlam -= Time.deltaTime;
        if (untilSlam <= 0.0f)
        {
            patternCycles++;
            leftFist.Sweep(leftFist.GetMaxTarget());
            rightFist.Sweep(rightFist.GetMinTarget());
            untilSlam = slamDelay;
        }
    }

    void RagePatternUpdate()
    {

    }

    void SetPattern(Pattern newPattern)
    {
        pattern = newPattern;
        patternCycles = 0;
        leftFist.Slam(leftFist.GetMinTarget());
        rightFist.Slam(rightFist.GetMaxTarget());
    }

    void FixedUpdate()
    {
        if (!fightStarted || leftFist.IsMoving() || rightFist.IsMoving())
            return;

        if(pattern == Pattern.TARGET && patternCycles == targetPatternCycles)
            SetPattern(Pattern.SWEEP);
        if (pattern == Pattern.SWEEP && patternCycles == sweepPatternCycles)
            SetPattern(Pattern.TARGET);
        if (damageable.GetHealth() / damageable.maxHealth < 0.3f)
            SetPattern(Pattern.RAGE);

        switch (pattern)
        {
            case Pattern.IDLE: IdleUpdate(); break;
            case Pattern.TARGET: TargetPatternUpdate(); break;
            case Pattern.SWEEP: SweepPatternUpdate(); break;
            case Pattern.RAGE: RagePatternUpdate(); break;
        }

        lastPattern = pattern;
    }
}
