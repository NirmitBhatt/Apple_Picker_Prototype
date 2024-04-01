using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    BasketController basketController;

    void Start()
    {
        basketController = GameObject.FindGameObjectWithTag("Basket").GetComponent<BasketController>();
    }
    public void UpdateScoreOnUI()
    {
        //Debug.Log("From UIController");
        scoreText.text = basketController.score.ToString();
    }
}
