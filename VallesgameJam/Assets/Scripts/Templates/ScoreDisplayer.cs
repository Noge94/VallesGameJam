using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayer : MonoBehaviour
{
    public static ScoreDisplayer Instance;
    
    private Text _uiText;
    public Text _dinnerText;
    public int _score = -30;
    public int _dinners = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _uiText = GetComponent<Text>();
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddScore(int add)
    {
        _score += add;
        _uiText.text = "SCORE\n" + _score;
    }

    public void AddDinner(int a) {
        _dinners += a;
        _dinnerText.text = _dinners.ToString();
    }

    public int GetScore()
    {
        return _score;
    }
}