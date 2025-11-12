using UnityEngine;

public class CommandeManager: MonoBehaviour
{
    public CommandeManagerScriptableObject commandeManagerSO;
    public GameObject commandePrefab;
    private GameObject commande;

    private float maxAnger;
    private float anger;

    private float timer = 0f;

    private GameObject[] commandesActives;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxAnger = commandeManagerSO.maxHungerThreshold;
        anger = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void increaseAnger(float amount)
    {
        anger += amount;
        if(anger >= maxAnger)
        {
            Debug.Log("GameOver");
        }
    }
}
