using System;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HeroScriptableObjects
{
    public HeroIDList heroIDs;
    public FactionList factions;
    public BaseStatsList baseStats;
}


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public static event Action onStart;
    public static event Action onStop;

    public UIMainGame MainGameUI;
    public Logger.LogDisplayParams displayParams;
    public HeroScriptableObjects heroesSO;
    public Dictionary<HeroDifficulty, float> difficultyBuffs;    
    public Transform heroStartRoom;
    public GameObject heroPrefab;

    public float startupTime;
    public float cycleDuration;
    public float initTimeBetweenHeroes;
    public float minTimeBetweenHeroes;
    public float timeDecreasePerHero;
    public int startingMana;

    private float startTime;
    private float timeBetweenHeroes;
    private float nextHeroSpawn;
    private int currentMana;
    public int CurrentMana 
    { 
        get { return currentMana; } 
        set { currentMana = value; } 
    }
    private int currentGold;
    public int CurrentGold 
    { 
        get { return currentGold; } 
        set { currentGold = value; } 
    }
    private int heroesSlainedCount;
    public int HeroesSlainedCount
    {
        get { return heroesSlainedCount; }
        set { heroesSlainedCount = value; }
    }
    private bool inGame;
    private bool onPause;
    

    private void Awake()
    {
        if (instance != null & instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = startupTime;
        timeBetweenHeroes = initTimeBetweenHeroes;
        currentMana = startingMana;
        difficultyBuffs= new Dictionary<HeroDifficulty, float>() 
        {
            {HeroDifficulty.RECRUIT, 1f},
            {HeroDifficulty.SEASONED, 1.5f},
            {HeroDifficulty.VETERAN, 2f},
            {HeroDifficulty.LEGENDARY, 3f}
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame == false)
        {
            if (startTime > 0)
            {
                startTime -= Time.deltaTime;
            }
            else
            {
                GameStart();
            }
        }
        else
        {
            if (onPause == false)
            {
                MainGameUI.UpdateCurrentMana(currentMana, startingMana);
                if (nextHeroSpawn > 0)
                {
                    nextHeroSpawn -= Time.deltaTime;
                }
                else
                {
                    SpawnHeroes();
                }
            }            
        }
    }
    
    public Hero GenerateHero(HeroDifficulty heroDifficulty)
    {
        int randIndex = UnityEngine.Random.Range(0, heroesSO.heroIDs.list.Count - 1);
        int randIndex2 = UnityEngine.Random.Range(0, heroesSO.baseStats.list.Count - 1);

        HeroID heroID = heroesSO.heroIDs.list[randIndex];
        Debug.Log($"Generated Hero from {randIndex}");
        BaseStats baseStat = heroesSO.baseStats.list[randIndex2];

        GameObject heroGO = Instantiate(heroPrefab, heroStartRoom.position, heroStartRoom.rotation);
        Hero hero = heroGO.GetComponent<Hero>();
        hero.HeroIDRef = heroID;
        HeroIDScroll heroIDScroll = hero.idScroll;
        HeroStats heroStats = hero.stats;

        heroIDScroll.firstName = heroID.firstName;
        heroIDScroll.lastName = heroID.lastName;
        heroIDScroll.title = heroID.title;
        heroIDScroll.age = heroID.age;
        heroIDScroll.portrait = heroID.portrait;
        heroIDScroll.faction = heroID.faction.name;
        heroIDScroll.factionLogo = heroID.faction.factionLogo;

        heroStats.maxHP = Mathf.RoundToInt(baseStat.maxHP * difficultyBuffs[heroDifficulty]);
        heroStats.maxSpeed = baseStat.speed * difficultyBuffs[heroDifficulty];
        heroStats.manaWhenSlain = Mathf.RoundToInt(baseStat.manaWhenSlained * difficultyBuffs[heroDifficulty]);
        heroStats.goldWhenSlain = Mathf.RoundToInt(baseStat.goldWhenSlained * difficultyBuffs[heroDifficulty]);

        return hero;
    }

    private void SpawnHeroes()
    {
        Hero currentHero = GenerateHero(HeroDifficulty.RECRUIT);
        currentHero.CurrentRoom = heroStartRoom.GetComponent<Room>();
        heroStartRoom.GetComponent<Room>().AddHero(currentHero);
        timeBetweenHeroes = (timeBetweenHeroes - timeDecreasePerHero >= minTimeBetweenHeroes) ? timeBetweenHeroes - timeDecreasePerHero : minTimeBetweenHeroes;
        nextHeroSpawn = timeBetweenHeroes;
        Debug.Log($"Hero spawned. Next hero in {nextHeroSpawn}");
    }

    private void GameStart()
    {
        onStart?.Invoke();
        Debug.Log("GAME STARTS");
        SpawnHeroes();
        inGame = true;
    }

    public void GameOver(Hero hero)
    {
        Debug.Log($"{hero.HeroIDRef.title} {hero.HeroIDRef.firstName} {hero.HeroIDRef.lastName} as reach the control room and slained you !");
        Debug.Log("GAME OVER");
        onPause = true;
        onStop?.Invoke();
    }
}

public enum HeroDifficulty
{
    RECRUIT,
    SEASONED,
    VETERAN,
    LEGENDARY
}
