using UnityEngine;
using System.Collections.Generic;

public class CommandeManager: MonoBehaviour
{
    public CommandeManagerScriptableObject commandeManagerSO;
    public GameObject commandePrefab;
    public float difficulty;
    public int maxCommandes;

    private GameObject commande;
    private int nbCommandes = 0;
    private float maxAnger;
    private float anger;
    private Vector3 newSpawnPoint = Vector3.zero;
    private Vector2 newSpawnPoint2D = Vector2.zero;
    private List<(Vector2, bool)> spawnPoints = new List<(Vector2, bool)>();
    private List<Vector2> availableSpawnPoints = new List<Vector2>();


    private float timer = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxAnger = commandeManagerSO.maxHungerThreshold;
        foreach(Vector2 point in commandeManagerSO.spawnPoints)
        {
            spawnPoints.Add((point, false));
        }
        anger = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        timer+= Time.deltaTime;
        if(timer >= 0.5f)
        {
            timer = 0;
            if (nbCommandes<maxCommandes)
            {
                if (Random.Range(0, 20) >= difficulty)
                {
                    commande = Instantiate(commandePrefab);
                    
                    newSpawnPoint2D = spawnPoints[Random.Range(0, spawnPoints.Count - 1)].Item1;
                    newSpawnPoint.x = newSpawnPoint2D.x;
                    newSpawnPoint.y = 2;
                    newSpawnPoint.z = newSpawnPoint2D.y;
                    commande.transform.position = newSpawnPoint;
                    Debug.Log("Spawn");
                    nbCommandes++;
                }
            }
        }
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
