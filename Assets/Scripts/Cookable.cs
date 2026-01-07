using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public enum Cuisson
{
    Crue,
    Cuite,
    Brulee,
}
public class Cookable : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] meshRenderer;
    [SerializeField] private Material[] cookedMaterials;
    [SerializeField] private Transform socket;

    public bool check = false;
    public float cookPoint = 0;
    public Cuisson cuisson = Cuisson.Crue;

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
            cuisson = Cuisson.Brulee;
        }
        else if (cookPoint >= 5)
        {
            cuisson = Cuisson.Cuite;
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
