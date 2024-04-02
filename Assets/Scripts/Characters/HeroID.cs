using UnityEngine;

[CreateAssetMenu(fileName = "HID_New", menuName = "ScriptableObjects/HeroID", order = 3)]
public class HeroID : ScriptableObject
{
    public string firstName;
    public string lastName;
    public string title;
    public Sprite portrait;
    public int age;
    public ElementalType heroType;
    public Faction faction;
}
