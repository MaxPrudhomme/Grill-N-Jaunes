using UnityEngine;

public class Sauce : MonoBehaviour
{

    [SerializeField] private ParticleSystem part;
    [SerializeField] private string sauce;
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

            if(Physics.SphereCast(transform.position+ transform.up*0.5f, 0.1f, Vector3.up, out RaycastHit hit, 10f))
            {
                if(hit.collider.CompareTag("Socket") || hit.collider.CompareTag("Cookable"))
                {
                    hit.transform.GetComponent<Consumable>().Sauce(sauce);
                }
            }
        }
        else
        {
            part.gameObject.SetActive(false);
        }
    }
}
