using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class PickupMovement : MonoBehaviour
{
    //[SerializeField] private Transform[] track;
    //[SerializeField] private Transform map;
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float baseSpeed;

    public Vector3 velocity;

    private float t = 0f;
    private bool canMove = true;
    private float speed;

    private void Start()
    {
        speed = baseSpeed;
    }

    private void FixedUpdate()
    {
        if (canMove) Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<Rigidbody>(out var _)) return;

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
        if (!other.gameObject.TryGetComponent<Rigidbody>(out var _)) return;

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
