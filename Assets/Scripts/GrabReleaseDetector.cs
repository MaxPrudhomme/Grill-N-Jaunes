using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabReleaseDetector : MonoBehaviour
{
    //[SerializeField] private float threshold = 2;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            Vector3 velocity = rb.linearVelocity;

            //Debug.Log("dot product: " + Vector3.Dot(velocity.normalized, Vector3.up));
            //if (Vector3.Dot(velocity.normalized, Vector3.up) > 0 && velocity.magnitude < threshold)
            //{
            //    Debug.Log("application de l'aide à la vélocité");
            //    Debug.Log("--- old velocity: " + velocity);
            //    rb.linearVelocity = velocity * (threshold / velocity.magnitude);
            //    Debug.Log("--- new velocity: " + velocity);
            //}

            if (transform.parent != null && transform.parent.TryGetComponent<PickupMovement>(out var pickupMovement))
            {
                Debug.Log("test");
                Debug.Log("velocity before: " + rb.linearVelocity);
                //rb.linearVelocity -= pickupMovement.velocity;
                transform.SetParent(null);
                Debug.Log("velocity after: " + rb.linearVelocity);
            }
            //if (transform.parent.TryGetComponent<Rigidbody>(out var rigidbody))
            //{
            //    Debug.Log("test");
            //    Debug.Log("velocity before: " + rb.linearVelocity);
            //    rb.linearVelocity -= rigidbody.linearVelocity;
            //    Debug.Log("velocity after: " + rb.linearVelocity);
            //}
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            if (transform.parent != null && transform.parent.TryGetComponent<PickupMovement>(out var pickupMovement))
            {
                transform.SetParent(null);
            }
        }
    }
}
