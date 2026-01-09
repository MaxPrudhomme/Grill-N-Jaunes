using UnityEngine;
using System.Collections.Generic;
using System;

public class CommandeManager: MonoBehaviour
{
    public CommandeManagerScriptableObject commandeManagerSO;
    public GameObject commandePrefab;
    public float difficulty;
    public int maxCommandes;
    public GameObject player;
    public Transform commandeParent;
    private GameObject commande;
    private int nbCommandes = 0;
    private float maxAnger;
    private float anger;
    private Vector3 newSpawnPoint = Vector3.zero;
    private Vector2 newSpawnPoint2D = Vector2.zero;
    private List<(Vector2, bool)> spawnPoints = new List<(Vector2, bool)>();
    private List<Vector2> availableSpawnPoints = new List<Vector2>();
    private int spawnPointsIndex;
    private float beerAngerAmount;
    private float diffScaling;
    private int maxOrderCountDown = 0;
    private int nbCommandesScaling;
    private PickupMovement pickupMovementScript;

    private const float TIME_INTERVAL = 2f;

    private float timer = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxAnger = commandeManagerSO.maxHungerThreshold;
        beerAngerAmount = commandeManagerSO.beerAngerAmount;
        diffScaling = commandeManagerSO.difficultyScaling;
        nbCommandesScaling = commandeManagerSO.nbCommandesScaling;
        foreach(Vector2 point in commandeManagerSO.spawnPoints)
        {
            spawnPoints.Add((point, false));
        }
        anger = 0f;

        commandeParent.TryGetComponent(out pickupMovementScript);
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
                    anger+=1;
                    Debug.Log(anger);
                    pickupMovementScript.SetSpeed(Mathf.Abs(anger - maxAnger) / (2 * maxAnger));
                    if (anger >= maxAnger)
                    {
                        Debug.Log("GameOver");
                        gameOver();
                    }
                }
            }

            //Spawn des commandes
            timer = 0;
            //Check si on peut faire spawn plus de commandes
            if (nbCommandes<maxCommandes)
            {
                //Check si on fait bien spawn une commande
                if (UnityEngine.Random.Range(0, 20) <= difficulty)
                {
                    commande = Instantiate(commandePrefab);
                    //On génere un index aléatoire. Si jamais il est déjà pris, pas de spawn
                    
                    spawnPointsIndex = UnityEngine.Random.Range(0, spawnPoints.Count);

                    //Dernier check (peut être à dégager)
                    if (!spawnPoints[spawnPointsIndex].Item2)
                    {
                        //Setup de la commande
                        spawnPoints[spawnPointsIndex] = (spawnPoints[spawnPointsIndex].Item1, true);

                        newSpawnPoint2D = spawnPoints[spawnPointsIndex].Item1;
                        newSpawnPoint.x = newSpawnPoint2D.x + player.transform.position.x;
                        newSpawnPoint.y = 2;
                        newSpawnPoint.z = newSpawnPoint2D.y + player.transform.position.z; ;

                       


                        commande.transform.position = newSpawnPoint;
                        commande.transform.SetParent(commandeParent);
                        //On lie l'Action de la commande
                        commande.GetComponent<Commande>().isCompleted += orderCompleted;
                        commande.GetComponent<Commande>().index = spawnPointsIndex;

                        //Rotation de la commande vers le joueur
                        commande.transform.LookAt(new Vector3(player.transform.position.x,commande.transform.position.y,player.transform.position.z));
                        commande.transform.Rotate(new Vector3(0, 90, 0));

                        

                        commande.GetComponent<Commande>().anger = anger;
                        //Debug.Log("Spawn");
                        nbCommandes++;
                        Debug.Log(nbCommandes);

                    }
                    else{
                        //Debug.Log("Cant spawn");
                    }
                    
                }
            }

            //Augmentation de la difficulté
            difficulty += diffScaling;
            //Debug.Log("Diff : " + difficulty);
            //Toutes les minutes, on augmente le nb max de commandes
            maxOrderCountDown++;
            if(maxOrderCountDown >= 30)
            {
                maxOrderCountDown = 0;
                maxCommandes += nbCommandesScaling;
            }
        }



    }

   

    private void orderCompleted(int index, bool wasBeer)
    {
        //Modification de l'emplacement
        spawnPoints[index] = (spawnPoints[index].Item1, false);
        nbCommandes--;
        //Si ce n'est pas une bière, la colère diminue
        if (!wasBeer)
        {

            anger -= 5;
            if (anger < 0) anger = 0;
        }else
        {
            anger += 5;
        }

    }

    private void gameOver()
    {
        Debug.Log("GameOver");
        Destroy(gameObject);

        pickupMovementScript.SetSpeed(0);
    }

    public void OnDrawGizmos()
    {
        foreach ((Vector2,bool) point in spawnPoints) 
        {
            Gizmos.DrawIcon(new Vector3(point.Item1.x + player.transform.position.x,2, point.Item1.y+ player.transform.position.z),"d");
        }
    }
}
