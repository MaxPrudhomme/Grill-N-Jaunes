using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabReleaseDetector : MonoBehaviour
{
    [SerializeField] private float threshold = 2;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void OnDisable()
    {
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            Vector3 velocity = rb.linearVelocity;

            Debug.Log("dot product: " + Vector3.Dot(velocity.normalized, Vector3.up));
            if (Vector3.Dot(velocity.normalized, Vector3.up) > 0 && velocity.magnitude < threshold)
            {
                Debug.Log("application de l'aide à la vélocité");
                Debug.Log("--- old velocity: " + velocity);
                rb.linearVelocity = velocity * (threshold / velocity.magnitude);
                Debug.Log("--- new velocity: " + velocity);
            }
        }
    }
}
