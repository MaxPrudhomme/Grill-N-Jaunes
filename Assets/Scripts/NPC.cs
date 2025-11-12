using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;


public class NPC : MonoBehaviour
{
    [SerializeField] private List<Consumable> consumablesAsked;
    [SerializeField] private TextMeshPro commandText;

    public CommandeManager manager;
    private List<string> command;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        commandText.gameObject.SetActive(false);
        AskFood();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AskFood()
    {
        Debug.Log("NPC ask food");
        commandText.gameObject.SetActive(true);
        command = new();
        command.Add(consumablesAsked[0].getObjectName());
        // TODO: activeBulle
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide");
        if (other.transform.parent.TryGetComponent<Consumable>(out Consumable c))
        {
            if (command.Contains(c.getObjectName()))
            {
                command.Remove(c.getObjectName());
                if (command.Count == 0)
                {
                    commandText.gameObject.SetActive(false);
                    Debug.Log("command finished");
                }
                Destroy(other.gameObject);
            }
        }
    }
}
