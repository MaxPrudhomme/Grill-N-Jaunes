using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Cookable : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] meshRenderer;
    [SerializeField] private Material[] cookedMaterials;
    [SerializeField] private Transform socket;

    private float cookPoint = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(SkinnedMeshRenderer renderer in meshRenderer)
            renderer.material = cookedMaterials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (cookPoint >= 10)
        {
            foreach (SkinnedMeshRenderer renderer in meshRenderer)
                renderer.material = cookedMaterials[2];
        }
        else if (cookPoint >= 5)
        {
            foreach (SkinnedMeshRenderer renderer in meshRenderer)
                renderer.material = cookedMaterials[1];
        }
    }

    public void CheckSocket()
    {
        Debug.Log("Check socket");
        if(cookPoint <10 && cookPoint >= 5)
        {
            Debug.Log("Check is cooked");
            Collider[] hits = Physics.OverlapSphere(transform.position, 0.1f);
            foreach (Collider hit in hits)
            {
                Debug.Log("Check socket"+hit.gameObject.name);
                if (hit.CompareTag("Socket"))
                {
                    Debug.Log( hit.gameObject.name+" is socket");
                    transform.position = hit.gameObject.transform.parent.GetChild(hit.gameObject.transform.childCount - 1).position;
                    transform.rotation = hit.gameObject.transform.parent.GetChild(hit.gameObject.transform.childCount - 1).rotation;
                    Destroy(transform.GetComponent<XRGrabInteractable>());
                    Destroy(transform.GetComponent<Rigidbody>());
                    transform.GetChild(hit.gameObject.transform.childCount - 2).gameObject.SetActive(false);
                    Debug.Log(hit.gameObject.name + " socketed");
                    return;
                }
            }
        }
        else
            Debug.Log("Check is not cooked");
    }

    public void IsCooked(float value)
    {
        //print(cookPoint);
        cookPoint += value;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
