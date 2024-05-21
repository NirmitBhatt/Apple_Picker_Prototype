using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using EZCameraShake;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject basketPrefab;
    [SerializeField] private float spawnPositionForBasket = -4.75f;
    [SerializeField] private float basketSpacingY = 0.5f;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] private ParticleSystem basketDestroyParticles = default;
    private int numberOfBaskets = 3;
    List<GameObject> basketList;

    private void Awake()
    {
        basketList = new List<GameObject>();
        SpawnBasketForPlayer();
        gameOverPanel.SetActive(false);
        FindObjectOfType<AudioManager>().PlayAudio("GameBackground");
    }
    void Start()
    {
        FindObjectOfType<Death>().OnAppleDropped += EliminateBasket;
        FindObjectOfType<Death>().OnAppleDropped += DestroyAllApplesOnScreen;

    }

    public void EliminateBasket()
    {
        GameObject tBasketGO = basketList[0];
        basketList.RemoveAt(0);
        FindObjectOfType<AudioManager>().PlayAudio("BasketBreak");
        Destroy(tBasketGO);
        CameraShaker.Instance.ShakeOnce(4f, 15f, .3f, 1f);
        basketDestroyParticles.transform.position = tBasketGO.transform.position;
        basketDestroyParticles.Play();
        if (basketList.Count == 0)
        {
            Time.timeScale = 0f;
            FindObjectOfType<AudioManager>().StopAudio("GameBackground");
            gameOverPanel.SetActive(true);
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
            GameObject tBasketGO = Instantiate(basketPrefab, pos, Quaternion.identity);
            pos.y += basketSpacingY;
            basketList.Add(tBasketGO);
        }
    }
}
