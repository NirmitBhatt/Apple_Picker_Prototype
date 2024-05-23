using TMPro;
using UnityEngine;

public class UIScoreController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    public void UpdateScoreOnUI()
    {
        //Debug.Log("From UIController");
        scoreText.text = BasketController.score.ToString();
    }
}
