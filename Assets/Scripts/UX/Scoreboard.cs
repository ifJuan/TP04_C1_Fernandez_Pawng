using System.Threading;
using TMPro;
using UnityEngine;

public class ScoreboardP : MonoBehaviour, GameUISubscriber
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI leftScore;
    [SerializeField] private TextMeshProUGUI rightScore;

    void Start()
    {
        SetInitialState();
    }

    void Awake()
    {
        GameStateManager.Instance.Subscribe(this);
    }

    void OnDestroy()
    {
        GameStateManager.Instance.Unsubscribe(this);
    }
    
    private void SetInitialState()
    {
        timer.text = "20";
        leftScore.text = "0";
        rightScore.text = "0";
    }

    public void ScoreChanged(int leftScore, int rightScore)
    {
        this.leftScore.text = leftScore.ToString();
        this.rightScore.text = rightScore.ToString();
    }

    public void StartGame()
    {
        SetInitialState();
    }

    public void UpdateTimer(float timeCount)
    {
        string roundedTime = timeCount.ToString("F2");
        timer.text = roundedTime;
    }

    public void ResumeGame()
    {
        leftScore.text = "0";
        rightScore.text = "0";
    }
}
