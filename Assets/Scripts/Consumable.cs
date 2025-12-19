using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Consumable : MonoBehaviour
{
    [SerializeField] private string objectName;
    [SerializeField] private GameObject kSauce;
    [SerializeField] private GameObject mSauce;
    private bool filled = false;
    private bool k = false;
    private bool m = false;


    public string getObjectName()
    {
        return objectName;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckSocket()
    {
        if (TryGetComponent<Cookable>(out Cookable c))
        {


            Debug.Log("Check socket");
            if (c.cookPoint < 10 && c.cookPoint >= 5)
            {
                Debug.Log("Check is cooked");
                Collider[] hits = Physics.OverlapSphere(transform.position, 0.1f);
                foreach (Collider hit in hits)
                {
                    Debug.Log("Check socket" + hit.gameObject.name);
                    if (hit.CompareTag("Socket"))
                    {
                        if (!hit.GetComponent<Consumable>().filled)
                        { 
                            Debug.Log(hit.gameObject.name + " is socket");
                            Destroy(transform.GetComponent<XRGrabInteractable>());
                            Destroy(transform.GetComponent<Rigidbody>());
                            transform.GetChild(transform.childCount - 2).gameObject.SetActive(false);
                            transform.parent = hit.gameObject.transform.parent.GetChild(hit.gameObject.transform.parent.childCount - 1);
                            transform.position = hit.gameObject.transform.parent.GetChild(hit.gameObject.transform.parent.childCount - 1).position;
                            transform.localRotation = Quaternion.identity;
                            filled = true;
                            Debug.Log(hit.gameObject.name + " socketed");
                            objectName = "HotDog";
                            return;
                        }
                    }
                }
            }
            else
                Debug.Log("Check is not cooked");
        }
    }

    public void Sauce(string s)
    {
        if(objectName == "HotDog")
        {
            if(s == "k")
            {
                k = true;
                kSauce.SetActive(true);
            }
            else
            {
                m = true;
                mSauce.SetActive(true);
            }
        }
    }
}
