using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class AppleTree : MonoBehaviour
{
    public GameObject applePrefab;
    public float speed = 1f;
    public float leftScreenEdge = -10f;
    public float rightScreenEdge = 10f;
    public float chanceToChangeDirections = 0.0001f;
    public float secsBetweenAppleDrop = 1f;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        InvokeRepeating("InstantiateAppleDrop", 1f, secsBetweenAppleDrop);
    }

    // Update is called once per frame
    void Update()
    {
        MoveAppleTree();
    }

    private void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirections)
        {
            speed *= -1;
        }
    }
    public void MoveAppleTree()
    {
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        ChangeTreeDirection();       
    }
    public void ChangeTreeDirection()
    {
        if (pos.x < leftScreenEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > rightScreenEdge)
        {
            speed = -Mathf.Abs(speed);
        }
    }
    public void InstantiateAppleDrop()
    {
        Instantiate(applePrefab, pos, Quaternion.identity);
    }
}
