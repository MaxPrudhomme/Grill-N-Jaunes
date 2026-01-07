using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;

public enum Aliment
{
    Saucisse,
    HotDog
}
public enum SauceAliment
{
    Moutarde,
    Ketchup,
    Nature
}
public class Commande : MonoBehaviour
{
    [SerializeField] private TextMeshPro commandText;
    

    public CommandeManager manager;
    private (Aliment, SauceAliment) demande;
    private float timer = 0;
    public int index;
    public MeshRenderer meshRenderer;
    public event Action<int,bool> isCompleted;

  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AskFood();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AskFood()
    {
        int aliment = UnityEngine.Random.Range(0, 2);

        demande.Item1 = (Aliment)aliment;
        //Si l'aliment demandé est une saucisse, aucune sauce n'est demandée
        demande.Item2 = aliment == 0 ? SauceAliment.Nature : (SauceAliment)UnityEngine.Random.Range(0, 3);


        // TODO: activeBulle
        string commandeString = demande.Item1 + " " + demande.Item2 + " !";
        commandText.text = commandeString;

    }

    

    //A refaire

    //Si collide : 
    //  Si collider = consumable :
    //      Si bière : 
    //          Augmenter la colère de ~10
    //          Compléter commande
    //      Check les ingédients
    //      Si correspond : 
    //          
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide");
        if(other.transform.parent.GetComponent<Beer>() != null)
        {
            isCompleted.Invoke(index, true);
        }
        if (other.transform.parent.TryGetComponent<Consumable>(out Consumable c))
        {
            //On check le type d'aliment
            if (demande.Item1.ToString() == c.objectName)
            {
                if (demande.Item1 == Aliment.Saucisse)
                {
                    if ((other.transform.parent.TryGetComponent<Cookable>(out Cookable cookable)))
                    {
                        if(cookable.cuisson == Cuisson.Cuite)
                        {
                            Debug.Log(cookable.cuisson);
                            completeCommande(index, false);
                            Destroy(cookable.gameObject);

                        }
                    }
                }
                else
                {
                    switch (demande.Item2)
                    {
                        case SauceAliment.Nature:
                            if (!c.m && !c.k)
                            {

                                completeCommande(index, false);
                                Destroy(c.gameObject);
                            }
                            break;
                        case SauceAliment.Moutarde:
                            if (c.m)
                            {
                                completeCommande(index, false);
                                Destroy(c.gameObject);

                            }
                            break;
                        case SauceAliment.Ketchup:
                            if (c.k)
                            {
                                completeCommande(index, false);
                                Destroy(c.gameObject);

                            }
                            break;
                    }
                }
                    
            }
        }
    }

    private void completeCommande(int index, bool isBeer) 
    { 
        isCompleted.Invoke(index, isBeer);
        Destroy(gameObject);
    }
}
