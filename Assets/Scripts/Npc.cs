using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer hair;
    [SerializeField] private Material[] hairMat;
    [SerializeField] private bool signe = false;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        if (!signe)
            Barks();

        anim.speed = Random.Range(0.9f, 1.1f);
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
