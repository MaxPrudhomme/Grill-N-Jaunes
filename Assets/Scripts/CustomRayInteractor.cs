using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CustomRayInteractor : XRRayInteractor
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!args.interactableObject.transform.gameObject.TryGetComponent(out Spawner spawner)) base.OnSelectEntered(args);

        GameObject newObj = spawner.SpawnObject();

        XRGrabInteractable grab = newObj.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            SelectEnterEventArgs newArgs = args;
            newArgs.interactableObject = grab;
            base.OnSelectEntered(newArgs);
        }
    }
}
