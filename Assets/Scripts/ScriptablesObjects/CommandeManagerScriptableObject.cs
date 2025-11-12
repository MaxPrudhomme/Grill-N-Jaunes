using UnityEngine;

[CreateAssetMenu(fileName = "CommandeManager", menuName = "Scriptable Objects/CommandeManager")]
public class CommandeManagerScriptableObject : ScriptableObject
{
    public float maxHungerThreshold; 
    public float difficultyScaling;
    public Vector2[] spawnPoints;

}
