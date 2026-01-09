using UnityEngine;

public class Crs : MonoBehaviour
{
    [SerializeField]   private bool walking = false;

    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.SetBool("Walk", walking);
    }

    private void OnEnable()
    {
        if(walking)
        {
            anim.Play("Sword And Shield Walk");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
