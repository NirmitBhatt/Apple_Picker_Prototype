using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private float timeBetweenLevelChange = 8f;
    [SerializeField] private int level = 1;


    private AppleTreeMovement appleTreeMovement;
    private AppleSpawner appleSpawner;
    private AudioManager audioManager;
    
    private int currentLevel = 1;
    private float levelTimer = 0;

    private Dictionary<int, LevelConfigData> levelConfigMap = new Dictionary<int, LevelConfigData>()
    {
        {1, new LevelConfigData(1, 7f, 0.01f, 0.8f, 1.2f, 0.8f, 0.7f) },
        {2, new LevelConfigData(2, 10f, 0.01f, 0.6f, 1.1f, 0.8f, 0.7f) },
        {3, new LevelConfigData(4, 15f, 0.02f, 0.4f, 1f, 1f, 0.8f) },
        {4, new LevelConfigData(5, 18f, 0.02f, 0.3f, 1f, 1.2f, 0.8f) },
        {5, new LevelConfigData(8, 25f, 0.03f, 0.2f, 0.8f, 1.5f, 1f) },
        {6, new LevelConfigData(10, 32f, 0.03f, 0.1f, 0.6f, 2f, 1f) },
    };

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

            if (appleTreeMovement == null && appleSpawner == null && audioManager == null && gameController == null)
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
        this.level = level;
        LevelConfigData configData = levelConfigMap[level];
        SetLevelData(configData);

        //return;
        //switch (level)
        //{
        //    case 1:
        //        appleTreeMovement.speed = 7f;
        //        appleTreeMovement.chanceToChangeDirections = 0.01f;
        //        appleSpawner.SetSecsBetweenAppleDrop(0.8f);
        //        appleSpawner.SetAppleRigidbodyValues(1.2f, 0.8f);
        //        audioManager.SetPitch("GameBackground", 0.7f);
        //        break;

        //    case 2:
        //        appleTreeMovement.speed = 10f;
        //        appleTreeMovement.chanceToChangeDirections = 0.01f;
        //        appleSpawner.SetSecsBetweenAppleDrop(0.6f);
        //        appleSpawner.SetAppleRigidbodyValues(1.1f, 0.8f);
        //        audioManager.SetPitch("GameBackground", 0.7f);
        //        break;

        //    case 3:
        //        appleTreeMovement.speed = 15f;
        //        appleTreeMovement.chanceToChangeDirections = 0.02f;
        //        appleSpawner.SetSecsBetweenAppleDrop(0.4f);
        //        appleSpawner.SetAppleRigidbodyValues(1f, 1f);
        //        audioManager.SetPitch("GameBackground", 0.8f);
        //        break;

        //    case 4:
        //        appleTreeMovement.speed = 18f;
        //        appleTreeMovement.chanceToChangeDirections = 0.02f;
        //        appleSpawner.SetSecsBetweenAppleDrop(0.3f); 
        //        appleSpawner.SetAppleRigidbodyValues(1f, 1.2f);
        //        audioManager.SetPitch("GameBackground", 0.8f);
        //        break;

        //    case 5:
        //        appleTreeMovement.speed = 25f;
        //        appleTreeMovement.chanceToChangeDirections = 0.03f;
        //        appleSpawner.SetSecsBetweenAppleDrop(0.2f);
        //        appleSpawner.SetAppleRigidbodyValues(0.8f, 1.5f);
        //        audioManager.SetPitch("GameBackground", 1f);
        //        break;

        //    case 6:
        //        appleTreeMovement.speed = 32f;
        //        appleTreeMovement.chanceToChangeDirections = 0.03f;
        //        appleSpawner.SetSecsBetweenAppleDrop(0.1f);
        //        appleSpawner.SetAppleRigidbodyValues(0.6f, 2f);
        //        audioManager.SetPitch("GameBackground", 1f);
        //        break;

        //    default:
        //        appleTreeMovement.speed = 10f;
        //        appleTreeMovement.chanceToChangeDirections = 0.01f;
        //        appleSpawner.SetSecsBetweenAppleDrop(0.6f);
        //        appleSpawner.SetAppleRigidbodyValues(1.2f, 0.8f);
        //        audioManager.SetPitch("GameBackground", 0.7f);
        //        Debug.Log("Default case.");
        //        break;
        //}
    }

    private void SetLevelData(LevelConfigData levelConfigData)
    {
        gameController.ScoreIncrementRate = levelConfigData.ScoreIncrementRate;
        appleTreeMovement.speed = levelConfigData.Speed;
        appleTreeMovement.chanceToChangeDirections = levelConfigData.ChanceToChangeDirections;
        appleSpawner.SetSecsBetweenAppleDrop(levelConfigData.SecsBetweenAppleDrop);
        appleSpawner.SetAppleRigidbodyValues(levelConfigData.LinearDrag, levelConfigData.GravítyScale);
        audioManager.SetPitch("GameBackground", levelConfigData.AudioPitch);
    }

    private class LevelConfigData
    {
        public LevelConfigData(int scoreIncrementRate, 
            float speed, 
            float chanceToChangeDirections, 
            float secsBetweenAppleDrop, 
            float linearDrag, 
            float gravítyScale, 
            float audioPitch)
        {
            ScoreIncrementRate = scoreIncrementRate;
            Speed = speed;
            ChanceToChangeDirections = chanceToChangeDirections;
            SecsBetweenAppleDrop = secsBetweenAppleDrop;
            LinearDrag = linearDrag;
            GravítyScale = gravítyScale;
            AudioPitch = audioPitch;
        }

        [field: SerializeField] public int ScoreIncrementRate {  get; private set; }
        [field: SerializeField] public float Speed {  get; private set; }
        [field: SerializeField] public float ChanceToChangeDirections { get; private set; }
        [field: SerializeField] public float SecsBetweenAppleDrop { get; private set; }
        [field: SerializeField] public float LinearDrag { get; private set; }
        [field: SerializeField] public float GravítyScale { get; private set; }
        [field: SerializeField] public float AudioPitch { get; private set; }

    }
}
