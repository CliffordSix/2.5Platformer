using UnityEngine;
using System.Collections;

public class AtlasFist : MonoBehaviour {

    public float maxHeight = 6.0f;
    public float minX = 0.0f;
    public float maxX = 0.0f;
    public float bounceTime = 1.0f;

    bool hasTarget = false;
    float progress = 1.0f;
    float minY = 0.0f;
    CubicBezier bezier;

	// Use this for initialization
	void Start ()
    {
        minY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (progress >= 1.0f || !hasTarget)
            return;
        
        progress += Time.deltaTime / bounceTime;
        Vector3 pos = bezier.Get(progress);
        if (pos.y < minY)
            pos.y = minY;

        transform.position = pos;

        if (progress >= 1.0f)
            Slam(null);
    }

    public void Slam(Transform target)
    {
        if (target != null)
        {
            Vector3 start = transform.position;
            start.x = Mathf.Clamp(start.x, minX, maxX);
            Vector3 end = target.position;
            end.x = Mathf.Clamp(end.x, minX, maxX);
            end.y = start.y;
            Vector3 b = new Vector3(start.x, start.y + maxHeight * 0.5f);
            Vector3 c = new Vector3(end.x, start.y + maxHeight);
            Vector3[] points = new Vector3[] { start, b, c, end };
            bezier = new CubicBezier(points);
            hasTarget = true;
            progress = 0.01f;
        }
        else
            hasTarget = false;
    }

    public bool IsMoving()
    {
        return progress < 1.0f;
    }
}
