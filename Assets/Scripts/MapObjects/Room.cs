using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{
    public List<Gate> gates = new List<Gate>();
    public Trap linkedTrap;
    public int cyclesToCross;
    public string roomName;
    public bool logHeroEntry;
    public bool isFinalRoom;

    private GameManager gameManager;
    private BoxCollider bCollider;
    private List<Hero> heroes = new List<Hero>();
    private Vector3 pos;
    public Vector3 Pos { get { return pos; } }
    private bool inGame;
    private float nextDmgProc;
    public bool healsHeroes;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        bCollider = gameObject.GetComponent<BoxCollider>();
        pos = transform.position;
        nextDmgProc = gameManager.cycleDuration;
    }

    public void Update()
    {
        if (linkedTrap)
        {
            if (linkedTrap.isActive && heroes.Count > 0)
            {
                if (nextDmgProc > 0)
                {
                    nextDmgProc -= Time.deltaTime;
                }
                else
                {
                    DamageHeroesInRoom();
                    nextDmgProc = gameManager.cycleDuration;
                }
            }            
        }
    }
    private void OnGameStart()
    {
        if (bCollider && (linkedTrap != null))
        {
            bCollider.enabled = true;
        }
        inGame = true;
    }

    private void OnGamePaused()
    {
        if (bCollider)
        {
            bCollider.enabled = false;
        }
        inGame = false;
    }

    public void AddHero(Hero hero)
    {
        if (!heroes.Contains(hero)) 
        { 
            heroes.Add(hero);
            AudioManager.Instance.Play("Footstep");
            if (logHeroEntry)
            {
                Logger.Log(hero.HeroIDRef, $"has entered", $"the {roomName} Room!", GameManager.Instance.displayParams);
            }            
        }
        if (isFinalRoom)
        {
            gameManager.GameOver(heroes[0]);
        }
    }

    public void RemoveHero(Hero hero)
    {
        if (heroes.Contains(hero))
        {
            heroes.Remove(hero);
        }
    }

    private void DamageHeroesInRoom()
    {
        foreach (Hero hero in heroes)
        {
            hero.ReceiveDmgOfType(linkedTrap.damage, linkedTrap.elementalType);
        }
    }

    public void OnClick()
    {
        if (linkedTrap != null)
        {
            linkedTrap.setActive();
            if (linkedTrap.isActive)
            {
                DamageHeroesInRoom();
            }
        }        
    }

    public void onMouseHoover()
    {

    }

    private void OnEnable()
    {
        GameManager.onStart += OnGameStart;
        GameManager.onStop += OnGamePaused;
    }
}
