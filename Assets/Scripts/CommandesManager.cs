using UnityEngine;
using System.Collections.Generic;

public class CommandeManager: MonoBehaviour
{
    public CommandeManagerScriptableObject commandeManagerSO;
    public GameObject commandePrefab;
    public float difficulty;
    public int maxCommandes;
    public GameObject player;
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
            //Augmentation de la colère
            //On le fait avant pour pas qu'une commande qui vient de spawn l'augmente direct
            foreach (var spawnPoint in spawnPoints)
            {
                //Si la commande est active, on augmente la colère
                if (spawnPoint.Item2)
                {
                    //Faire en sorte que la colère/sec augmente de façon log ? 
                    anger++;
                }
            }

            //Spawn des commandes
            timer = 0;
            //Check si on peut faire spawn plus de commandes
            if (nbCommandes<maxCommandes)
            {
                //Check si on fait bien spawn une commande
                if (Random.Range(0, 20) <= difficulty)
                {
                    commande = Instantiate(commandePrefab);
                    //On génere un index aléatoire jusqu'a trouver un emplacement libre
                    do
                    {
                        spawnPointsIndex = Random.Range(0, spawnPoints.Count);

                    } while (spawnPoints[spawnPointsIndex].Item2);
                    //Dernier check (peut être à dégager)
                    if (!spawnPoints[spawnPointsIndex].Item2)
                    {
                        //Setup de la commande
                        spawnPoints[spawnPointsIndex] = (spawnPoints[spawnPointsIndex].Item1, true);

                        newSpawnPoint2D = spawnPoints[spawnPointsIndex].Item1;
                        newSpawnPoint.x = newSpawnPoint2D.x;
                        newSpawnPoint.y = 2;
                        newSpawnPoint.z = newSpawnPoint2D.y;

                        //TODO:
                        //Génerer des ints aléatoires pour la sauce demandée.


                        commande.transform.position = newSpawnPoint;
                        //On lie l'Action de la commande
                        commande.GetComponent<NPC>().isCompleted += orderCompleted;
                        commande.GetComponent<NPC>().index = spawnPointsIndex;

                        //Rotation de la commande vers le joueur
                        commande.transform.LookAt(new Vector3(player.transform.position.x,commande.transform.position.y,player.transform.position.z));
                        commande.transform.Rotate(new Vector3(0, 90, 0));
                        Debug.Log("Spawn");
                        nbCommandes++;
                    }
                    else{
                        Debug.Log("Cant spawn");
                    }
                    
                }
            }

            Debug.Log(anger);
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
        //Modification de l'emplacement
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
