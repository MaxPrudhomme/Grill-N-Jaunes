using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SwitchOnGrab : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;

    private XRGrabInteractable grab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
    }
    void OnDestroy()
    {
        grab.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (prefabToSpawn == null) return;

        GameObject spawned = Instantiate(
            prefabToSpawn,
            transform.position,
            transform.rotation
        );

        XRGrabInteractable newGrab = spawned.GetComponent<XRGrabInteractable>();

        if (newGrab == null)
            return;

        var interactor = args.interactorObject;
        var interactable = newGrab;

        if (interactor != null && interactable != null)
        {
            grab.interactionManager.SelectEnter(interactor, interactable);
        }
    }
}
