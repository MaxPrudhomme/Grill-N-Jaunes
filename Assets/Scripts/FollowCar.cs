using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FollowCar : MonoBehaviour
{
    public Transform followTarget; // main ou véhicule
    private Rigidbody rb;
    private bool following = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // désactive physique pendant le suivi
    }

    void FixedUpdate()
    {
        if (following && followTarget != null)
        {
            rb.MovePosition(rb.position + followTarget.GetComponent<Rigidbody>().linearVelocity * Time.fixedDeltaTime);
            //rb.MoveRotation(followTarget.rotation);
        }
    }

    public void Release()
    {
        following = false;
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void StartFollowing(Transform target)
    {
        followTarget = target;
        following = true;
        rb.isKinematic = true;
    }
}
