using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using System;

public class GameController : MonoBehaviour
{
    public static event Action<int> ScoreChanged;
    public static event Action OnGameOver;
    [SerializeField] private BasketController basketPrefab;
    [SerializeField] private float spawnPositionForBasket = -4.75f;
    [SerializeField] private float basketSpacingY = 0.5f;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] private ParticleSystem basketDestroyParticles = default;
    private int numberOfBaskets = 3;
    List<BasketController> basketList;

    public int Score 
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            ScoreChanged?.Invoke(score);
        }
    }
    private int score = 0;

    private void Awake()
    {
        basketList = new List<BasketController>();
        SpawnBasketForPlayer();
        gameOverPanel.SetActive(false);
    }
    void Start()
    {
        //FindObjectOfType<AudioManager>().PlayAudio("GameBackground");
        
        FindObjectOfType<Death>().OnAppleDropped += EliminateBasket;
        FindObjectOfType<Death>().OnAppleDropped += DestroyAllApplesOnScreen;
        PlayBackgroundAudio();

    }

    private void OnEnable()
    {
        BasketController.AppleCollect += IncrementScore;
    }

    private void OnDisable()
    {
        BasketController.AppleCollect -= IncrementScore;
    }

    private void IncrementScore()
    {
        Score = Score + 10;
    }

    private void PlayBackgroundAudio()
    {
        FindObjectOfType<AudioManager>().PlayAudio("GameBackground");
    }

    public void EliminateBasket()
    {
        BasketController basketController = basketList[0];
        basketList.RemoveAt(0);
        FindObjectOfType<AudioManager>().PlayAudio("BasketBreak");
        //FindObjectOfType<AudioManager>().PlayAudio("GameBackground");
        Destroy(basketController.gameObject);
        CameraShaker.Instance.ShakeOnce(4f, 15f, .3f, 1f);
        basketDestroyParticles.transform.position = basketController.transform.position;
        basketDestroyParticles.Play();
        if (basketList.Count == 0)
        {
            Time.timeScale = 0f;
            FindObjectOfType<AudioManager>().StopAudio("GameBackground");
            gameOverPanel.SetActive(true);
            OnGameOver?.Invoke();
            FindObjectOfType<Death>().OnAppleDropped -= EliminateBasket;
            FindObjectOfType<Death>().OnAppleDropped -= DestroyAllApplesOnScreen;
        }
    }

    public void DestroyAllApplesOnScreen()
    {
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tApple in tAppleArray)
        {
            Destroy(tApple);
        }
    }

    private void SpawnBasketForPlayer()
    {
        Vector3 pos = Vector3.zero;
        pos.y = spawnPositionForBasket;
        for (int i = 0; i < numberOfBaskets; i++)
        {
            BasketController basketController = Instantiate(basketPrefab, pos, Quaternion.identity);
            pos.y += basketSpacingY;
            basketList.Add(basketController);
        }
    }
}
