using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Cookable : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] meshRenderer;
    [SerializeField] private Material[] cookedMaterials;
    [SerializeField] private Transform socket;

    public bool check = false;
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
        /*
        if (Input.GetKeyDown(KeyCode.Space) && check)
        {
            cookPoint = 6;
            CheckSocket();
        }
        */
            
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
                    Destroy(transform.GetComponent<XRGrabInteractable>());
                    Destroy(transform.GetComponent<Rigidbody>());
                    transform.GetChild(transform.childCount - 2).gameObject.SetActive(false);
                    transform.parent = hit.gameObject.transform.parent.GetChild(hit.gameObject.transform.parent.childCount - 1);
                    transform.position = hit.gameObject.transform.parent.GetChild(hit.gameObject.transform.parent.childCount - 1).position;
                    transform.localRotation = Quaternion.identity;
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
