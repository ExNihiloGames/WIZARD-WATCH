using UnityEngine;

public class Trap : MonoBehaviour
{
    public ElementalType elementalType;
    public GameObject trapEffect;
    public int manaCost;
    public float cooldown;
    public int damage;
    public bool isActive;
    private bool isOnCooldown;
    private bool inGame;

    private GameManager gameManager;
    private float nextManaCost;
    private float nextActivation;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        nextManaCost = gameManager.cycleDuration;
        nextActivation = 0;
    }

    private void Update()
    {
        if (inGame)
        {
            if (isOnCooldown)
            {
                nextActivation -= Time.deltaTime;
                if (nextActivation <= 0)
                {
                    isOnCooldown = false;
                    nextActivation = 0;
                }
            }
            if (isActive)
            {
                nextManaCost -= Time.deltaTime;
                if (nextManaCost <= 0)
                {
                    gameManager.CurrentMana -= manaCost;
                    nextManaCost = gameManager.cycleDuration;
                }
            }
        }        
    }

    public void OnGameStart()
    {
        inGame = true;
    }

    private void OnGamePaused()
    {
        inGame = false;
    }

    public void setActive()
    {
        if (isActive == false)
        {
            if (isOnCooldown)
            {
                Debug.Log("This trap room is on Cooldown !");
            }
            else
            {
                if (gameManager.CurrentMana >= manaCost)
                {
                    gameManager.CurrentMana -= manaCost;
                    trapEffect.SetActive(true);
                    isActive = true;
                    Debug.Log("Trap Room Activated !");
                    switch (elementalType)
                    {
                        case ElementalType.FIRE:
                            AudioManager.Instance.Play("Fire");
                            break;
                        case ElementalType.ICE:
                            AudioManager.Instance.Play("Ice");
                            break;
                        case ElementalType.EARTH:
                            AudioManager.Instance.Play("Earth");
                            break;
                        case ElementalType.SPIKES:
                            AudioManager.Instance.Play("Trap0");
                            break;
                        case ElementalType.HEAL:
                            AudioManager.Instance.Play("Heal");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.Log("Not enough Mana !");
                }                
            }            
        }
        else
        {
            isActive = false;
            trapEffect.SetActive(false);
            isOnCooldown = true;
            nextManaCost = gameManager.cycleDuration;
            nextActivation = cooldown;
            Debug.Log("Trap Room Deactivated !");
        }
    }

    private void OnEnable()
    {
        GameManager.onStart += OnGameStart;
        GameManager.onStop += OnGamePaused;
    }
}

public enum ElementalType
{
    FIRE,
    ICE,
    EARTH,
    SPIKES,
    HEAL
}
