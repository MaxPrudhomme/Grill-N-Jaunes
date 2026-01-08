using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class PickupMovement : MonoBehaviour
{
    //[SerializeField] private Transform[] track;
    //[SerializeField] private Transform map;
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float speed;

    public Vector3 velocity;

    private Vector3 lastPosition;
    private Rigidbody rb;
    private int currentTrackTargetIndex;
    private bool canMove = true;
    
    private float t = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTrackTargetIndex = 0;
        //transform.LookAt(track[0]);
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        Vector3 pos = spline.EvaluatePosition(t);
        Vector3 pos2 = spline.EvaluatePosition(t + 0.1f);
        ////Vector3 rot = spline.EvaluateTangent(t);
        //rot.x = 0f;
        //rot.z = 0f;
        t += Time.deltaTime * speed / 100;
        transform.position = pos;
        transform.LookAt(pos2);
        //transform.rotation = Quaternion.Euler(rot);

        //if (canMove) Move();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Transform parent = other.transform.parent;
    //    if (parent)
    //    {
    //        parent.SetParent(transform);
    //    }
    //    else
    //    {
    //        other.transform.SetParent(transform);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    Transform parent = other.transform.parent;
    //    if (parent)
    //    {
    //        parent.SetParent(null);
    //    }
    //    else
    //    {
    //        other.transform.SetParent(null);
    //    }
    //}

    //private void Move()
    //{
    //    Vector3 v = track[currentTrackTargetIndex].position - transform.position;
    //    if (v.magnitude < 0.1)
    //    {
    //        // Next track target
    //        currentTrackTargetIndex++;
    //        if (currentTrackTargetIndex == track.Length)
    //        {

    //            canMove = false;
    //        }
    //        else
    //        {
    //            transform.LookAt(track[currentTrackTargetIndex]);
    //        }
    //    }

    //    velocity = (transform.position - lastPosition) / Time.deltaTime;
    //    lastPosition = transform.position;

    //    //Debug.Log("---- currentIndex: " + currentTrackTargetIndex);
    //    //Debug.Log("---- track pos: " + track[currentTrackTargetIndex].position);
    //    Vector3 dir = (track[currentTrackTargetIndex].position - transform.position).normalized;
    //    //Vector3 dir = transform.forward;
    //    transform.Translate(dir * Time.fixedDeltaTime * 10, Space.World);

    //    //map.Translate(-dir * Time.fixedDeltaTime, Space.World);

    //    //rb.MovePosition(rb.position + Vector3.forward * speed * Time.deltaTime);
    //}
}
