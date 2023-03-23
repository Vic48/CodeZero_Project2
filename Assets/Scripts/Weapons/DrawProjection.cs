using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawProjection : MonoBehaviour
{
    //Throwing throwScript;
    //LineRenderer lineRenderer;

    ////number points on line
    //public int numPoints = 50;

    ////distance between points on line
    //public float timeBetweenPoints = 0.1f;

    ////physics layer that causes line to stop drawing
    //public LayerMask collidableLayers;

    //public bool projectLine = false;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    throwScript = GetComponent<Throwing>();
    //    lineRenderer = GetComponent<LineRenderer>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (projectLine == true)
    //    {
    //        lineRenderer.positionCount = (int)numPoints;
    //        List<Vector3> points = new List<Vector3>();
    //        Vector3 startPos = throwScript.attackPoint.position;
    //        Vector3 startVelocity = throwScript.attackPoint.up * throwScript.throwForce;

    //        for (float t = 0; t < numPoints; t += timeBetweenPoints)
    //        {
    //            Vector3 newPoint = startPos + t * startVelocity;
    //            newPoint.y = startPos.y + startVelocity.y * t + Physics.gravity.y / 2f * t * t;
    //            points.Add(newPoint);

    //            if (Physics.OverlapSphere(newPoint, 2, collidableLayers).Length > 0)
    //            {
    //                lineRenderer.positionCount = points.Count;
    //                break;
    //            }
    //        }

    //        lineRenderer.SetPositions(points.ToArray());
    //    }
    //}

    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    [Range(3, 30)]
    private int _lineSegmentCount = 20;

    private List<Vector3> _linePoints = new List<Vector3>();

    public static DrawProjection Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startPos)
    {
        Vector3 veloctiy = (forceVector / rigidBody.mass) * Time.fixedDeltaTime;

        float FlightDuration = (2 * veloctiy.y) / Physics.gravity.y;

        float stepTime = FlightDuration / _lineSegmentCount;

        _linePoints.Clear();

        for (int i = 0; i < _lineSegmentCount; i++)
        {
            //change in time
            float stepTimePassed = stepTime * i;

            Vector3 movementVec = new Vector3
            (
                veloctiy.x * stepTimePassed, 
                veloctiy.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                veloctiy.z * stepTimePassed
            );

            RaycastHit hit;

            if (Physics.Raycast(startPos, -movementVec, out hit, movementVec.magnitude))
            {
                break;
            }
            _linePoints.Add(-movementVec + startPos);
        }

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }

    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }
}
