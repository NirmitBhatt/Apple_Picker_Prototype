using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using System;

public class GameController : MonoBehaviour
{
    public static event Action<int> ScoreChanged;
    public static event Action OnGameStart;
    public static event Action OnBasketBreak;

    public static Action OnGameOver;

    private int scoreIncrementRate;
    private int numberOfBaskets = 3;
    private List<BasketController> basketList;

    [SerializeField] private BasketController basketPrefab;
    [SerializeField] private Death death;
    [SerializeField] private float spawnPositionForBasket = -4.75f;
    [SerializeField] private float basketSpacingY = 0.5f;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private ParticleSystem basketDestroyParticles = default;

    public int ScoreIncrementRate
    {
        get
        {
            return scoreIncrementRate;
        }
        set
        {
            scoreIncrementRate = value;
        }
    }

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

    private void OnEnable()
    {
        BasketController.AppleCollect += IncrementScore;
        death.OnAppleDropped += EliminateBasket;
        death.OnAppleDropped += DestroyAllApplesOnScreen;
        OnGameStart?.Invoke();
    }

    private void OnDisable()
    {
        BasketController.AppleCollect -= IncrementScore;
        death.OnAppleDropped -= EliminateBasket;
        death.OnAppleDropped -= DestroyAllApplesOnScreen;
    }

    private void IncrementScore()
    {
        Score += scoreIncrementRate;
    }

    public void EliminateBasket()
    {
        BasketController basketController = basketList[0];
        basketList.RemoveAt(0);
        OnBasketBreak?.Invoke();
        Destroy(basketController.gameObject);
        CameraShaker.Instance.ShakeOnce(4f, 15f, .3f, 1f);
        SpawnBasketBreakParticles(basketController);  
        if (basketList.Count == 0)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
            OnGameOver?.Invoke();
        }
    }

    private void SpawnBasketBreakParticles(BasketController basketController)
    {
        basketDestroyParticles.transform.position = basketController.transform.position;
        basketDestroyParticles.Play();
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
