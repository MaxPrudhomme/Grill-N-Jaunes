using System.Drawing;
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
    [SerializeField] private bool walking = true;
    private Animator anim;
    private float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        if (!signe && walking)
            Barks();
        else
        {
            if (Random.Range(0, 3) == 0)
                anim.Play("Walking");
        }

        if(!walking)
        {
            
            if(signe)
                anim.Play("Standing Torch Idle 01");
            else
            {
                anim.Play("Breathing Idle");
                Barks();
            }

        }
        speed = Random.Range(-0.05f, 0.05f);
        

        hair.material = hairMat[Random.Range(0,hairMat.Length)];
        pant.material = pantMat[Random.Range(0, pantMat.Length)];
        top.materials[0] = topMat[Random.Range(0, topMat.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (CommandeManager.instance.gameIsOver)
        {
            anim.speed = speed + 1f;
            if (signe)
                anim.Play("Standing Torch Idle 01");
            else
            {
                anim.Play("Breathing Idle");
                Barks();
            }
        }
        else
            anim.speed = speed + PickupMovement.instance.speed;


    }

    private void Barks()
    {
        StopAllCoroutines();
        StartCoroutine(IBarks(Random.Range(5f, 15f)));
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
