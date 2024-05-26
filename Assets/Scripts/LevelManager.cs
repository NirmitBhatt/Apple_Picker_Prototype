using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private AppleTreeMovement appleTreeMovement;
    private AppleSpawner appleSpawner;
    private AudioManager audioManager;

    private int currentLevel = 1;
    [SerializeField] private float timeBetweenLevelChange = 8f;
    private float levelTimer = 0;

    private void Start()
    {
        AssignComponentsForLevelChange();
    }

    private void Update()
    {
        levelTimer += Time.deltaTime;
        if(levelTimer >= timeBetweenLevelChange)
        {
            currentLevel++;
            ChangeLevel(currentLevel);
            levelTimer = 0;
        }
    }

    private void AssignComponentsForLevelChange()
    {
        GameObject appleTree = GameObject.FindGameObjectWithTag("Apple Tree");
        if (appleTree != null)
        {
            appleTreeMovement = appleTree.GetComponent<AppleTreeMovement>();
            appleSpawner = appleTree.GetComponent<AppleSpawner>();
            audioManager = AudioManager.instance;

            if (appleTreeMovement == null && appleSpawner == null && audioManager == null)
            {
                Debug.LogError("Component and/or object type not assigned!");
                return;
            }

            ChangeLevel(1);
        }
        else
        {
            Debug.LogError("Cannot find GameObject with *Apple Tree* Tag");
        }
    }

    public void ChangeLevel(int level)
    {
        switch (level)
        {
            case 1:
                appleTreeMovement.speed = 7f;
                appleTreeMovement.chanceToChangeDirections = 0.01f;
                appleSpawner.SetSecsBetweenAppleDrop(0.8f);
                appleSpawner.SetAppleRigidbodyValues(1.2f, 0.8f);
                audioManager.SetPitch("GameBackground", 0.7f);
                break;

            case 2:
                appleTreeMovement.speed = 10f;
                appleTreeMovement.chanceToChangeDirections = 0.01f;
                appleSpawner.SetSecsBetweenAppleDrop(0.6f);
                appleSpawner.SetAppleRigidbodyValues(1.1f, 0.8f);
                audioManager.SetPitch("GameBackground", 0.7f);
                break;

            case 3:
                appleTreeMovement.speed = 15f;
                appleTreeMovement.chanceToChangeDirections = 0.02f;
                appleSpawner.SetSecsBetweenAppleDrop(0.4f);
                appleSpawner.SetAppleRigidbodyValues(1f, 1f);
                audioManager.SetPitch("GameBackground", 0.8f);
                break;

            case 4:
                appleTreeMovement.speed = 18f;
                appleTreeMovement.chanceToChangeDirections = 0.02f;
                appleSpawner.SetSecsBetweenAppleDrop(0.3f); 
                appleSpawner.SetAppleRigidbodyValues(1f, 1.2f);
                audioManager.SetPitch("GameBackground", 0.8f);
                break;

            case 5:
                appleTreeMovement.speed = 25f;
                appleTreeMovement.chanceToChangeDirections = 0.03f;
                appleSpawner.SetSecsBetweenAppleDrop(0.2f);
                appleSpawner.SetAppleRigidbodyValues(0.8f, 1.5f);
                audioManager.SetPitch("GameBackground", 1f);
                break;

            case 6:
                appleTreeMovement.speed = 32f;
                appleTreeMovement.chanceToChangeDirections = 0.03f;
                appleSpawner.SetSecsBetweenAppleDrop(0.1f);
                appleSpawner.SetAppleRigidbodyValues(0.6f, 2f);
                audioManager.SetPitch("GameBackground", 1f);
                break;

            default:
                appleTreeMovement.speed = 10f;
                appleTreeMovement.chanceToChangeDirections = 0.01f;
                appleSpawner.SetSecsBetweenAppleDrop(0.6f);
                appleSpawner.SetAppleRigidbodyValues(1.2f, 0.8f);
                audioManager.SetPitch("GameBackground", 0.7f);
                Debug.Log("Default case.");
                break;
        }
    }
}
