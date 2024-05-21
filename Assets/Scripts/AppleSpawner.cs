using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;
    [SerializeField] private float secsBetweenAppleDrop = 1f;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateAppleDrop", 1f, secsBetweenAppleDrop);
    }

    private void Update()
    {
        AssignSpawnPosition();
    }


    private void AssignSpawnPosition()
    {
        pos = transform.position;
        pos.y = 2.2f;
    }

    public void InstantiateAppleDrop()
    {
        Instantiate(applePrefab, pos, Quaternion.identity);
        FindObjectOfType<AudioManager>().PlayAudio("AppleSpawn");
        //FindObjectOfType<AudioManager>().PlayAudio("AppleDrop");
    }
}
