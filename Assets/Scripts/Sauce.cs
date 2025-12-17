using UnityEngine;

public class Sauce : MonoBehaviour
{

    [SerializeField] private ParticleSystem part;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(-Vector3.Dot(transform.up, Vector3.up) >0.5f)
        {
            part.gameObject.SetActive(true);
        }
        else
        {
            part.gameObject.SetActive(false);
        }
    }
}
