using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;

    //private XRGrabInteractable simple;
    private XRSimpleInteractable simple;
    private Collider[] myColliders;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //simple = GetComponent<XRGrabInteractable>();
        simple = GetComponent<XRSimpleInteractable>();
        simple.selectEntered.AddListener(OnSelect);
        myColliders = GetComponentsInChildren<Collider>();
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

    public GameObject SpawnObject()
    {
        return Instantiate(
            prefabToSpawn,
            transform.position,
            transform.rotation
        );
    }
}
