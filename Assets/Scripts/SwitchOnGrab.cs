using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SwitchOnGrab : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;

    private XRGrabInteractable grab;
    private bool alreadySpawned = false;

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

        var interactor = args.interactorObject;
        XRGrabInteractable newGrab = spawned.GetComponent<XRGrabInteractable>();

        grab.interactionManager.SelectExit(interactor, grab);

        if (alreadySpawned || prefabToSpawn == null)
            return;

        alreadySpawned = true;

        if (interactor != null && newGrab != null)
        {
            grab.interactionManager.SelectEnter(interactor, newGrab);
        }
    }
}
