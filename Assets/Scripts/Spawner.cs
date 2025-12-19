using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;

    //private XRGrabInteractable simple;
    private XRSimpleInteractable simple;
    private Collider[] myColliders;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //simple = GetComponent<XRGrabInteractable>();
        simple = GetComponent<XRSimpleInteractable>();
        myColliders = GetComponentsInChildren<Collider>();
        simple.selectEntered.AddListener(OnSelect);
    }
    void OnDestroy()
    {
        simple.selectEntered.RemoveListener(OnSelect);
    }

    private void OnSelect(SelectEnterEventArgs args)
    {
        GameObject spawned = Instantiate(
            prefabToSpawn,
            transform.position,
            transform.rotation
        );

        Collider[] spawnedColliders = spawned.GetComponentsInChildren<Collider>();
        foreach (var a in myColliders)
        {
            foreach (var b in spawnedColliders)
            {
                Physics.IgnoreCollision(a, b, true);
            }
        }

        XRBaseInteractor baseInteractor = args.interactorObject as XRBaseInteractor;
        XRGrabInteractable grab = spawned.GetComponent<XRGrabInteractable>();

        if (baseInteractor != null && grab != null)
        {
            baseInteractor.interactionManager.SelectExit(baseInteractor, args.interactableObject);
            baseInteractor.interactionManager.SelectEnter(baseInteractor, (IXRSelectInteractable)grab);
        }
    }


    //private void OnSelect(SelectEnterEventArgs args)
    //{
    //    GameObject spawned = Instantiate(
    //        prefabToSpawn,
    //        transform.position,
    //        transform.rotation
    //    );

    //    Collider[] spawnedColliders = spawned.GetComponentsInChildren<Collider>();

    //    foreach (var a in myColliders)
    //    {
    //        foreach (var b in spawnedColliders)
    //        {
    //            Physics.IgnoreCollision(a, b, true);
    //        }
    //    }

    //    IXRSelectInteractor interactor = args.interactorObject;
    //    var grab = spawned.GetComponent<XRGrabInteractable>();

    //    if (interactor != null && grab != null)
    //    {
    //        grab.interactionManager.SelectEnter(interactor, grab);
    //    }

    //    //if (prefabToSpawn == null) return;

    //    //GameObject spawned = Instantiate(
    //    //    prefabToSpawn,
    //    //    transform.position,
    //    //    transform.rotation
    //    //);

    //    //Collider[] spawnedColliders = spawned.GetComponentsInChildren<Collider>();

    //    //foreach (var a in myColliders)
    //    //{
    //    //    foreach (var b in spawnedColliders)
    //    //    {
    //    //        Physics.IgnoreCollision(a, b, true);
    //    //    }
    //    //}

    //    //XRGrabInteractable newGrab = spawned.GetComponent<XRGrabInteractable>();

    //    //if (newGrab == null)
    //    //    return;

    //    //var interactor = args.interactorObject;
    //    //var interactable = newGrab;

    //    //if (interactor != null && interactable != null)
    //    //{
    //    //    simple.interactionManager.SelectEnter(interactor, interactable);
    //    //}
    //}
}
