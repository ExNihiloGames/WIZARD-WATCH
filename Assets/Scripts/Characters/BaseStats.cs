using UnityEngine;

[CreateAssetMenu(fileName = "New BaseStats", menuName = "ScriptableObjects/BaseStats", order = 2)]
public class BaseStats : ScriptableObject
{
    public int maxHP;
    public float speed;
    public int manaWhenSlained;
    public int goldWhenSlained;
}
