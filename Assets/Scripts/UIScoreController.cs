using TMPro;
using UnityEngine;

public class UIScoreController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        GameController.ScoreChanged += UpdateScoreOnUI;
    }

    private void OnDisable()
    {
        GameController.ScoreChanged -= UpdateScoreOnUI;
    }

    public void UpdateScoreOnUI(int score)
    {
        scoreText.SetText(score.ToString());
    }
}
