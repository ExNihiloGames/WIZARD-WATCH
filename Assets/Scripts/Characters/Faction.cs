using UnityEngine;

[CreateAssetMenu(fileName = "New Faction", menuName = "ScriptableObjects/Faction", order = 1)]
public class Faction : ScriptableObject
{
    public string factionName;
    public Sprite factionLogo;
}
