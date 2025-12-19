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
    private int spawnPointsIndex;
    private const float TIME_INTERVAL = 2f;

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
        if(timer >= TIME_INTERVAL)
        {
            timer = 0;
            if (nbCommandes<maxCommandes)
            {
                if (Random.Range(0, 20) >= difficulty)
                {
                    commande = Instantiate(commandePrefab);
                    spawnPointsIndex = Random.Range(0, spawnPoints.Count);
                    if (!spawnPoints[spawnPointsIndex].Item2)
                    {
                        spawnPoints[spawnPointsIndex] = (spawnPoints[spawnPointsIndex].Item1, true);

                        newSpawnPoint2D = spawnPoints[spawnPointsIndex].Item1;
                        newSpawnPoint.x = newSpawnPoint2D.x;
                        newSpawnPoint.y = 2;
                        newSpawnPoint.z = newSpawnPoint2D.y;


                        commande.transform.position = newSpawnPoint;
                        commande.GetComponent<NPC>().isCompleted += orderCompleted;
                        commande.GetComponent<NPC>().index = spawnPointsIndex;

                        commande.transform.rotation = Quaternion.LookRotation(commande.transform.position - new Vector3(-10, commande.transform.position.y, 0));

                        Debug.Log("Spawn");
                        nbCommandes++;
                    }
                    else{
                        Debug.Log("Cant spawn");
                    }
                    
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

    private void orderCompleted(int index)
    {
        spawnPoints[index] = (spawnPoints[index].Item1, false);
        nbCommandes--;
    }

    public void OnDrawGizmos()
    {
        foreach ((Vector2,bool) point in spawnPoints) 
        {
            Gizmos.DrawIcon(new Vector3(point.Item1.x,2, point.Item1.y),"d");
        }
    }
}
