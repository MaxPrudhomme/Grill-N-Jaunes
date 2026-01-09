using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class PickupMovement : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float baseSpeed;

    public Vector3 velocity;

    private Vector3 lastPosition;
    private float t = 0f;
    private bool canMove = true;
    private float speed;

    private void Start()
    {
        lastPosition = transform.position;
        speed = baseSpeed;
    }

    private void FixedUpdate()
    {
        if (canMove) Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("----- test enter");
        if (!other.transform.parent.TryGetComponent<Rigidbody>(out var _)) return;
        Debug.Log("----- test enter 2");

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
        Debug.Log("----- test enter");
        if (!other.transform.parent.TryGetComponent<Rigidbody>(out var _)) return;
        Debug.Log("----- test enter 2");

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

        Vector3 pos = spline.EvaluatePosition(t);
        Vector3 pos2 = spline.EvaluatePosition(t + 0.1f);
        t += Time.deltaTime * speed / 100;
        transform.position = pos;
        transform.LookAt(pos2);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
