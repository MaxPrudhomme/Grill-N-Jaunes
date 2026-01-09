using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class PickupMovement : MonoBehaviour
{
    public static PickupMovement instance = null;
    [SerializeField] private SplineContainer spline;
    public float baseSpeed;

    public Vector3 velocity;

    private Vector3 lastPosition;
    private float t = 0f;
    private bool canMove = true;
    public float speed;

    private void Awake()
    {
        instance = this;
    }

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
        velocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
        lastPosition = transform.position;

        Vector3 pos = spline.EvaluatePosition(t);
        Vector3 pos2 = spline.EvaluatePosition(t + 0.1f);
        t += Time.fixedDeltaTime * speed / 100;
        transform.position = pos;
        transform.LookAt(pos2);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
