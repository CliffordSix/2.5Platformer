using UnityEngine;
using System.Collections;

public class AtlasFist : MonoBehaviour {

    public enum TargetMode
    {
        SLAM,
        SWEEP
    }

    public AtlasController controller;
    public float maxHeight = 6.0f;
    public float minX = 0.0f;
    public float maxX = 0.0f;
    public float slamTime = 1.0f;

    public float sweepAccel = 0.01f;
    float sweepSpeed = 0.0f;

    TargetMode targetMode = TargetMode.SLAM;
    Damageable damageable;
    bool hasTarget = false;
    float progress = 1.0f;
    float minY = 0.0f;
    CubicBezier bezier;
    Vector3 sweepStart = Vector3.zero;
    Vector3 sweepTarget = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
        damageable = GetComponent<Damageable>();
        minY = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (progress >= 1.0f || !hasTarget)
            return;

        Vector3 pos = Vector3.zero;
        if(targetMode == TargetMode.SLAM)
        {
            progress += Time.deltaTime / slamTime;
            pos = bezier.Get(progress);
            if (pos.y < minY)
                pos.y = minY;
        }
        else
        {
            sweepSpeed += sweepAccel;
            progress += sweepSpeed;
            pos = Vector3.Lerp(sweepStart, sweepTarget, progress);
        }

        transform.position = pos;

        if (progress >= 1.0f)
            hasTarget = false;
    }

    public Vector3 GetMinTarget()
    {
        Vector3 parentPos = transform.parent.position;
        parentPos.x += minX;
        parentPos.y = minY;
        return parentPos;
    }

    public Vector3 GetMaxTarget()
    {
        Vector3 parentPos = transform.parent.position;
        parentPos.x += maxX;
        parentPos.y = minY;
        return parentPos;
    }

    public void Slam(Vector3 target)
    {
        if (hasTarget) return;

        targetMode = TargetMode.SLAM;
        Vector3 start = transform.position;
        start.x = Mathf.Clamp(start.x, transform.parent.position.x + minX, transform.parent.position.x + maxX);
        Vector3 end = target;
        end.x = Mathf.Clamp(end.x, transform.parent.position.x + minX, transform.parent.position.x + maxX);
        end.y = start.y;
        Vector3 b = new Vector3(start.x, start.y + maxHeight * 0.5f);
        Vector3 c = new Vector3(end.x, start.y + maxHeight);
        Vector3[] points = new Vector3[] { start, b, c, end };
        bezier = new CubicBezier(points);
        hasTarget = true;
        progress = 0.01f;
    }

    public void Sweep(Vector3 target)
    {
        targetMode = TargetMode.SWEEP;
        sweepStart = transform.position;
        sweepStart.y = minY;
        sweepTarget = target;
        sweepTarget.y = minY;
        hasTarget = true;
        progress = 0.01f;
    }

    public bool IsMoving()
    {
        return progress < 1.0f;
    }
}
