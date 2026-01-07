using UnityEngine;
using UnityEngine.UIElements;

public class PickupMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;

    public Vector3 velocity;

    private Vector3 lastPosition;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        transform.Translate(Vector3.forward * Time.deltaTime * speed); 
        //rb.MovePosition(rb.position + Vector3.forward * speed * Time.deltaTime);
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
}
