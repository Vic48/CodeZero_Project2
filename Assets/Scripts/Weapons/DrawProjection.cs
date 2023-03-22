using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProjection : MonoBehaviour
{
    Throwing throwScript;
    LineRenderer lineRenderer;

    //number points on line
    public int numPoints = 50;

    //distance between points on line
    public float timeBetweenPoints = 0.1f;

    //physics layer that causes line to stop drawing
    public LayerMask collidableLayers;

    public bool projectLine = false;

    // Start is called before the first frame update
    void Start()
    {
        throwScript = GetComponent<Throwing>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (projectLine == true)
        {
            lineRenderer.positionCount = (int)numPoints;
            List<Vector3> points = new List<Vector3>();
            Vector3 startPos = throwScript.attackPoint.position;
            Vector3 startVelocity = throwScript.attackPoint.up * throwScript.throwForce;

            for (float t = 0; t < numPoints; t += timeBetweenPoints)
            {
                Vector3 newPoint = startPos + t * startVelocity;
                newPoint.y = startPos.y + startVelocity.y * t + Physics.gravity.y / 2f * t * t;
                points.Add(newPoint);

                if (Physics.OverlapSphere(newPoint, 2, collidableLayers).Length > 0)
                {
                    lineRenderer.positionCount = points.Count;
                    break;
                }
            }

            lineRenderer.SetPositions(points.ToArray());
        }
    }
}
