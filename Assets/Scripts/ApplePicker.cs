using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePicker : MonoBehaviour
{
    [SerializeField] public GameObject basketPrefab;
    [SerializeField] private float spawnPositionForBasket = -4.75f;
    [SerializeField] private float basketSpacingY = 0.5f;
    private int numberOfBaskets = 3;
    // Start is called before the first frame update
    void Start()
    {
        SpawnBasketForPlayer();
    }

    private void SpawnBasketForPlayer()
    {
        Vector3 pos = Vector3.zero;
        pos.y = spawnPositionForBasket;
        for (int i = 0; i < numberOfBaskets; i++)
        {
            Instantiate(basketPrefab, pos, Quaternion.identity);
            pos.y += basketSpacingY;
        }
    }
}
