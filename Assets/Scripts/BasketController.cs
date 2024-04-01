using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class BasketController : MonoBehaviour
{
    [SerializeField] private float leftScreenEdgeForBasket = -10.1f;
    [SerializeField] private float rightScreenEdgeForBasket = 10.1f;
    public UnityEvent AppleCollect;
    private float clampedMouseXPos3D;
    public int score { get; private set; }

    // Update is called once per frame
    void Start()
    {
        score = 0;
        AppleCollect.AddListener(GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>().UpdateScoreOnUI);
    }


    void Update()
    {
        MoveBasketWithMouseCursor();

    }

    private void MoveBasketWithMouseCursor()
    {
        Vector3 mousePosd2D = Input.mousePosition;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePosd2D);
        clampedMouseXPos3D = Mathf.Clamp(mousePos3D.x, leftScreenEdgeForBasket, rightScreenEdgeForBasket);
        transform.position = new Vector3(clampedMouseXPos3D, transform.position.y, transform.position.z);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Apple")
        {
            Destroy(collision.gameObject);
            score += 10;
            Debug.Log(score);
            AppleCollect.Invoke();
        }
    }
}
