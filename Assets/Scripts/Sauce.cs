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

            if(Physics.SphereCast(transform.position, 0.1f, transform.up, out RaycastHit hit, 1f))
            {
                if(hit.collider.CompareTag("Socket"))
                {
                    Debug.Log(hit.collider.gameObject.name + "/" + hit.collider.transform.parent.gameObject.name);
                    hit.collider.transform.parent.GetComponent<Consumable>().Sauce(sauce);
                }
            }
        }
        else
        {
            part.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 1f);
    }
}
