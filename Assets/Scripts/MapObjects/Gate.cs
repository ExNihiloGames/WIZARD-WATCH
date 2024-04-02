using UnityEngine;

public class Gate : MonoBehaviour
{
    public Room linkedRoom;
    public Transform leftDoor;
    public Transform rightDoor;
    public int manaCost;
    public bool isClosed = false;
    public bool inGame;

    private GameManager gameManager;
    private BoxCollider bCollider;
    private float nextManaCost;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        bCollider = gameObject.GetComponent<BoxCollider>();
        nextManaCost = gameManager.cycleDuration;
    }

    private void Update()
    {
        if (inGame)
        {
            if (isClosed)
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

    private void OnGameStart()
    {
        if (bCollider)
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

    public void setClosed()
    {
        if (isClosed == false)
        {
            if (gameManager.CurrentMana >= manaCost)
            {
                gameManager.CurrentMana -= manaCost;
                leftDoor.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                rightDoor.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                isClosed = true;
                Debug.Log("Door is closed !");
                AudioManager.Instance.Play("Door");
            }
            else
            {
                Debug.Log("Not enough Mana !");
            }            
        }
        else
        {
            leftDoor.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
            rightDoor.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
            isClosed = false;
            nextManaCost = gameManager.cycleDuration;
            Debug.Log("Door is open !");
            AudioManager.Instance.Play("Door");
        }
    }

    public void OnClick()
    {
        setClosed();
    }

    public void OnMouseHoover()
    {
        
    }

    private void OnEnable()
    {
        GameManager.onStart += OnGameStart;
        GameManager.onStop += OnGamePaused;
    }
}
