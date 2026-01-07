using UnityEngine;
using UnityEngine.UIElements;

public class PickupMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent.SetParent(null);
    }
}
