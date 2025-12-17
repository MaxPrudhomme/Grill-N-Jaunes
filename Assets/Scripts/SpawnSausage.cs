using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SpawnSausage : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;

    private XRGrabInteractable simple;
    private Collider[] myColliders;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        simple = GetComponent<XRGrabInteractable>();
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

        IXRSelectInteractor interactor = args.interactorObject;
        var grab = spawned.GetComponent<XRGrabInteractable>();

        if (interactor != null && grab != null)
        {
            grab.interactionManager.SelectEnter(interactor, grab);
        }
    }
}
