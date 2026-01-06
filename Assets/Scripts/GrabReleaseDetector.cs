using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabReleaseDetector : MonoBehaviour
{
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
            Vector3 angularVelocity = rb.angularVelocity;

            Debug.Log($"Vitesse lin�aire : {velocity}, Vitesse angulaire : {angularVelocity}");
        }
    }
}
