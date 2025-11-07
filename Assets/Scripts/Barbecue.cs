using UnityEngine;

public class Barbecue : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Cookable"))
        {
            collision.transform.parent.GetComponent<Cookable>().IsCooked(Time.deltaTime);
        }
    }
}
