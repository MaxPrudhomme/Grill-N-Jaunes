using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabReleaseDetector : MonoBehaviour
{
    [SerializeField] private float threshold = 2;
    [SerializeField] private float coefSpeed = 5;
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

            if (velocity.magnitude < threshold)
            {
                rb.linearVelocity = velocity * coefSpeed;
            }
        }
    }
}
