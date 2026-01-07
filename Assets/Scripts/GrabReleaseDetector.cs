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

            if (Vector3.Dot(velocity, Vector3.up) > 0 && velocity.magnitude < threshold)
            {
                rb.linearVelocity = velocity * (threshold / velocity.magnitude);
            }
        }
    }
}
