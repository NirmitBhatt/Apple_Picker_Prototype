using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject basketPrefab;
    [SerializeField] private float spawnPositionForBasket = -4.75f;
    [SerializeField] private float basketSpacingY = 0.5f;
    private int numberOfBaskets = 3;
    List<GameObject> basketList;
    //public TextMeshProUGUI scxoreText;

    void Start()
    {
        basketList = new List<GameObject>();
        SpawnBasketForPlayer();
        FindObjectOfType<Death>().OnAppleDropped += EliminateBasket;
        FindObjectOfType<Death>().OnAppleDropped += DestroyAllApplesOnScreen;

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

    public void EliminateBasket()
    {
        //int basketIndex = basketList.Count - 1;
        GameObject tBasketGO = basketList[0];
        basketList.RemoveAt(0);
        Destroy(tBasketGO);
        if (basketList.Count == 0)
        {
            Debug.Log("Game Over");
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

}
