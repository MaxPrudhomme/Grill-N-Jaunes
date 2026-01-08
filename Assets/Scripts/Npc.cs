using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer hair;
    [SerializeField] private Material[] hairMat;
    [SerializeField] private SkinnedMeshRenderer pant;
    [SerializeField] private Material[] pantMat;
    [SerializeField] private SkinnedMeshRenderer top;
    [SerializeField] private Material[] topMat;
    [SerializeField] private bool signe = false;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        if (!signe)
            Barks();

        anim.speed = Random.Range(0.9f, 1.1f);

        hair.material = hairMat[Random.Range(0,hairMat.Length)];
        pant.material = pantMat[Random.Range(0, pantMat.Length)];
        top.materials[0] = topMat[Random.Range(0, topMat.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Barks()
    {
        StopAllCoroutines();
        StartCoroutine(IBarks(Random.Range(2f, 10f)));
    }

    private System.Collections.IEnumerator IBarks(float t)
    {
        int a = Random.Range(0, 2);

        switch(a)
        {
            case 0:
                anim.Play("Yelling",1);
                break;
            case 1:
                anim.Play("Angry Point",1);
                break;
            case 2:
                anim.Play("Angry",1);
                break;
        }

        yield return new WaitForSeconds(t);
        Barks();
    }
}
