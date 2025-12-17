using UnityEngine;

public class Cookable : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] meshRenderer;
    [SerializeField] private Material[] cookedMaterials;

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
        if(cookPoint <10 && cookPoint >= 5)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 0.1f);
            foreach (Collider hit in hits)
            {
                if(hit.CompareTag("Socket"))
                {
                    transform.position = hit.gameObject.transform.GetChild(hit.gameObject.transform.childCount - 1).position;
                    transform.rotation = hit.gameObject.transform.GetChild(hit.gameObject.transform.childCount - 1).rotation;
                    Destroy(transform.GetComponent<Rigidbody>());
                    hit.gameObject.transform.GetChild(hit.gameObject.transform.childCount - 2).gameObject.SetActive(false);
                }
            }
        }
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
