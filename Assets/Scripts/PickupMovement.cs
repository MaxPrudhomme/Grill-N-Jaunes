using UnityEngine;
using UnityEngine.UIElements;

public class PickupMovement : MonoBehaviour
{
    [SerializeField] private Transform[] track;
    [SerializeField] private Transform map;

    public Vector3 velocity;

    private Vector3 lastPosition;
    private Rigidbody rb;
    private int currentTrackTargetIndex;
    private bool canMove = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTrackTargetIndex = 0;
        transform.LookAt(track[0]);
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Vector3 v = track[currentTrackTargetIndex].position - transform.position;
        //if (v.magnitude < 0.1)
        //{
        //    // Next track target
        //    currentTrackTargetIndex++;
        //    transform.LookAt(track[currentTrackTargetIndex]);
        //    if (currentTrackTargetIndex == track.Length - 1) canMove = false;
        //}

        if (canMove) Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform parent = other.transform.parent;
        if (parent)
        {
            parent.SetParent(transform);
        }
        else
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Transform parent = other.transform.parent;
        if (parent)
        {
            parent.SetParent(null);
        }
        else
        {
            other.transform.SetParent(null);
        }
    }

    private void Move()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;

        //Debug.Log("---- currentIndex: " + currentTrackTargetIndex);
        //Debug.Log("---- track pos: " + track[currentTrackTargetIndex].position);
        //Vector3 dir = (track[currentTrackTargetIndex].position - transform.position).normalized;
        Vector3 dir = transform.forward;
        transform.Translate(dir * Time.fixedDeltaTime);

        //map.Translate(-dir * Time.fixedDeltaTime);

        //rb.MovePosition(rb.position + Vector3.forward * speed * Time.deltaTime);
    }
}
