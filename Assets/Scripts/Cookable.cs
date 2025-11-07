using UnityEngine;

public class Cookable : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material[] cookedMaterials;

    private float cookPoint = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer.material = cookedMaterials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (cookPoint >= 10)
        {
            meshRenderer.material = cookedMaterials[2];
        }
        else if (cookPoint >= 5)
        {
            meshRenderer.material = cookedMaterials[1];
        }
    }

    public void IsCooked(float value)
    {
        print(cookPoint);
        cookPoint += value;
    }
}
