using UnityEngine;
using System.Collections;

public enum PathMode
{
    LOOP,
    FLIPFLOP
}

public class PathFollow : MonoBehaviour {

    public float speed = 0.0f;
    public PathMode pathMode = PathMode.FLIPFLOP;
    public Transform[] path = new Transform[2];

    int targetIndex = 1;
    Vector3 fromPoint;
    Vector3 toPoint;
    float progress = 0.0f;
    int direction = 1;

	// Use this for initialization
	void Start () {
        fromPoint = path[0].position;
        toPoint = path[1].position;
	}
	
    public float GetLength()
    {
        float result = 0.0f;
        for(int i = 0; i < path.Length - 1; i++)
        {
            result += (path[i + 1].position - path[i].position).magnitude;
        }
        result += (path[0].position - path[path.Length - 1].position).magnitude;
        return result;
    }

	void FixedUpdate()
    {
        float segmentLength = (toPoint - fromPoint).magnitude;
        float distancePercent = speed / segmentLength;

        progress += distancePercent;

        transform.position = Vector3.Lerp(fromPoint, toPoint, progress);

        if(progress >= 1.0f)
        {
            progress = 0.0f;
            targetIndex += direction;
            fromPoint = toPoint;

            if (pathMode == PathMode.LOOP && targetIndex >= path.Length)
            {
                targetIndex = 0;
            }
            else if(pathMode == PathMode.FLIPFLOP && (targetIndex < 0 || targetIndex >= path.Length))
            {
                direction *= -1;
                if (targetIndex < 0)
                    targetIndex = 1;
                else if (targetIndex >= path.Length)
                    targetIndex = path.Length - 2;
            }

            toPoint = path[targetIndex].position;
        }
    }
}
