using TMPro;
using UnityEngine;

public class StandByPanel : MonoBehaviour, GameUISubscriber
{
    [SerializeField] private TextMeshProUGUI message;

    void Awake()
    {
        GameStateManager.Instance.Subscribe(this);
    }

    void OnDestroy()
    {
        GameStateManager.Instance.Unsubscribe(this);
    }

    void Start()
    {
        SetStandByMessage();
    }

    void Update()
    {
        if (
            (
                GameStateManager.Instance.state == GameStateManager.State.StandBy
                ||
                GameStateManager.Instance.state == GameStateManager.State.Finished
            )
            && Input.GetKeyUp(KeyCode.Space)
            )
        {
            GameStateManager.Instance.Play();
            gameObject.SetActive(false);
        }
    }

    void SetStandByMessage()
    {
        message.text = "Press\nSPACEBAR\nto start";
    }

    void SetFinishedMessage()
    {
        message.text = "Game ended!\n\nPress\nSPACEBAR\nto start";
    }

    public void ScoreChanged(int leftScore, int rightScore)
    {
        if (GameStateManager.Instance.state == GameStateManager.State.Finished)
        {
            SetFinishedMessage();
            gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
    }

    public void StopGame()
    {
        if (GameStateManager.Instance.state == GameStateManager.State.Finished)
            SetFinishedMessage();
        else
            SetStandByMessage();
        gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
    }
}
