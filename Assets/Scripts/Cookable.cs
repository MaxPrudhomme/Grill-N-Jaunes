using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Cookable : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] meshRenderer;
    [SerializeField] private Material[] cookedMaterials;
    [SerializeField] private Transform socket;

    public bool check = false;
    public float cookPoint = 0;

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
