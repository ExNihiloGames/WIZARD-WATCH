using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HeroIDScroll
{
    public string firstName;
    public string lastName;
    public int age;
    public string title;
    public ElementalType heroType;
    public string faction;
    public Sprite portrait;
    public Sprite factionLogo;
}


[System.Serializable]
public class HeroStats
{
    public int maxHP;
    public float maxSpeed;
    public int manaWhenSlain;
    public int goldWhenSlain;
}


public class Hero : MonoBehaviour
{
    public HeroIDScroll idScroll;
    public HeroStats stats;

    private GameManager gameManager;
    private Room currentRoom;
    public Room CurrentRoom 
    { 
        get { return currentRoom; } 
        set { currentRoom = value; } 
    }

    private HeroID heroIDRef;
    public HeroID HeroIDRef
    {
        get { return heroIDRef; }
        set { heroIDRef = value; }
    }

    private int currentHP;
    private float currentSpeed;
    private float nextMove;
    private bool inGame;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentHP = stats.maxHP;
        currentSpeed = stats.maxSpeed;
        inGame = true;
        UpdateTimeToNextMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            if (nextMove > 0)
            {
                nextMove -= Time.deltaTime;
                if (nextMove <= 0)
                {
                    MoveToNextRoom(ChooseNextRoom());
                    UpdateTimeToNextMove();
                }
            }
        }        
    }

    public void ReceiveDmgOfType(int damageRaw, ElementalType damageType)
    {
        int receivedDmg = damageRaw;

        switch(damageType)
        {
            case ElementalType.FIRE:
                if (idScroll.heroType == ElementalType.FIRE)
                {
                    Debug.Log($"{idScroll.firstName} is IMUNE to {damageType} and takes 0 damage !!");
                    receivedDmg = 0;
                }
                else if (idScroll.heroType == ElementalType.ICE)
                {
                    Debug.Log($"{idScroll.firstName} is RESISTANT to {damageType} and takes half damage !!");
                    receivedDmg = Mathf.RoundToInt(damageRaw / 2);
                }
                else
                {
                    Debug.Log($"{idScroll.firstName} is VULNERABLE to {damageType} and takes double damage !!");
                    receivedDmg = 2 * damageRaw;
                }
                break;
            case ElementalType.ICE:
                if (idScroll.heroType == ElementalType.FIRE)
                {
                    Debug.Log($"{idScroll.firstName} is VULNERABLE to {damageType} and takes double damage !!");
                    receivedDmg = 2 * damageRaw;
                }
                else if (idScroll.heroType == ElementalType.ICE)
                {
                    Debug.Log($"{idScroll.firstName} is IMUNE to {damageType} and takes 0 damage !!");
                    receivedDmg = 0;
                }
                else
                {
                    Debug.Log($"{idScroll.firstName} is RESISTANT to {damageType} and takes half damage !!");
                    receivedDmg = Mathf.RoundToInt(damageRaw / 2);
                }
                break;
            case ElementalType.EARTH:
                if (idScroll.heroType == ElementalType.FIRE)
                {
                    Debug.Log($"{idScroll.firstName} is RESISTANT to {damageType} and takes half damage !!");
                    receivedDmg = Mathf.RoundToInt(damageRaw / 2);
                }
                else if (idScroll.heroType == ElementalType.ICE)
                {
                    Debug.Log($"{idScroll.firstName} is VULNERABLE to {damageType} and takes double damage !!");
                    receivedDmg = 2 * damageRaw;
                }
                else
                {
                    Debug.Log($"{idScroll.firstName} is IMUNE to {damageType} and takes 0 damage !!");
                    receivedDmg = 0;
                }
                break;
            case ElementalType.SPIKES:
                receivedDmg = damageRaw;
                break;
            case ElementalType.HEAL:
                receivedDmg += damageRaw;
                break;

        }
        Logger.Log(heroIDRef, $"{idScroll.firstName} takes {receivedDmg} damage ", $"in {currentRoom.roomName} !", GameManager.Instance.displayParams);
        currentHP -= receivedDmg;

        if (currentHP <= 0) 
        {
            Die();
        }
    }

    private void OnGamePaused()
    {
        inGame = false;
    }

    private void UpdateTimeToNextMove()
    {
        nextMove = (currentRoom.cyclesToCross * gameManager.cycleDuration) / currentSpeed;
    }

    private Room ChooseNextRoom()
    {
        if (currentRoom.gates.Count != 0)
        {
            Debug.Log("Now choosing next room");
            List<Room> openRooms = new List<Room>();

            foreach (Gate gate in currentRoom.gates)
            {
                if (gate.isClosed == false)
                {
                    openRooms.Add(gate.linkedRoom);
                }
            }

            int randIndex = Random.Range(0, openRooms.Count - 1);
            return openRooms[randIndex];
        }

        Debug.Log("No gates in room");
        return null;
    }

    private void MoveToNextRoom(Room targetRoom)
    {
        if (targetRoom)
        {
            Debug.Log("Now moving to next room");
            currentRoom.RemoveHero(this);
            transform.position = targetRoom.Pos;
            currentRoom = targetRoom;
            currentRoom.AddHero(this);
            if (currentRoom.healsHeroes)
            {
                currentHP += 3;
            }
        }
        else
        {
            Debug.Log("Could not move this cycle");
        }        
    }

    private void Die()
    {
        Debug.Log($"The {idScroll.title} {idScroll.firstName} {idScroll.lastName} Has been slain ! The Dunjoen feasts on the fresh soul and grants you {stats.manaWhenSlain} !");
        gameManager.CurrentMana += stats.manaWhenSlain;
        gameManager.CurrentGold += stats.goldWhenSlain;
        gameManager.HeroesSlainedCount += 1;
        currentRoom.RemoveHero(this);
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        GameManager.onStop += OnGamePaused;
    }
}
