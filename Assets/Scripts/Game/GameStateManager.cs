using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public interface PlayerSubscriber
{
    void HeightChanged(float percentage) { /* Default */ }
    void SpeedModifierChanged(float speedModifier) { /* Default */ }
    void MoveToInitialPosition() {  /* Default */ }
}

public interface BallSubscriber
{
    void ColorChanged(UnityEngine.Color color);
    void RemoveFromField();
}

public interface GameUISubscriber
{
    void ScoreChanged(int leftScore, int rightScore);
    void StartGame();
    void StopGame() { /* Default */ }
    void ResumeGame();
    void UpdateTimer(float time) { /* Default */ }
} 

public class GameStateManager: MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private GameStateDefaultValuesSo defaultValues;
    [Header("Prefabs")]
    [SerializeField] private GameObject ballPrefab;

    // Singleton
    public static GameStateManager Instance;

    // Subscribers 
    private List<PlayerSubscriber> playersSubscribers;
    private List<BallSubscriber> ballsSubscribers;
    private List<GameUISubscriber> gameUISubscribers;

    // Players
    private float characterPercentageHeight;
    private float characterSpeedModifier;
    private UnityEngine.Color ballColor;

    // Game
    private int goalsAmount;
    private int gameDuration; 
    private int leftSideGoals = 0;
    private int rightSideGoals = 0;

    // Time management
    private float timeCount;

    private void Update()
    {
        if (state == State.Playing)
        {
            timeCount -= Time.deltaTime;
            if (timeCount >= 0)
            {
                UpdateUITime(timeCount);
            } else
            {
                Vector2 position = Camera.main.WorldToViewportPoint(ballPrefab.transform.position);
                if (position.x < 0.5f)
                    GoalFor(Goal.Side.Left);
                else
                    GoalFor(Goal.Side.Right);
            }
        }
    }

    // State 
    public State state;
    public enum State
    {
        MainMenu,
        Paused,
        Playing,
        StandBy,
        Finished
    };

    // Init
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            state = defaultValues.state;
            characterPercentageHeight = defaultValues.characterPercentageHeight;
            characterSpeedModifier = defaultValues.characterSpeedModifier;
            ballColor = defaultValues.ballColor;
            goalsAmount = defaultValues.goalsAmount;
            gameDuration = defaultValues.gameDuration;
            playersSubscribers = new();
            ballsSubscribers = new();
            gameUISubscribers = new();
            timeCount = gameDuration;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    //State changes
    public void Play()
    {
        if (state == State.Finished)
        {
            SetPlayersToInitialPosition();
            timeCount = gameDuration;
            leftSideGoals = 0;
            rightSideGoals = 0;
        }
        if (state == State.StandBy)
        {
            SetPlayersToInitialPosition();
            timeCount = gameDuration;
            UpdateUITime(timeCount);
        }
        if (state != State.Playing)
        {
            timeCount = gameDuration;
            state = State.Playing;
            Instantiate(ballPrefab, new Vector2(0, 0), Quaternion.identity);
            Time.timeScale = 1f;
        }
    }

    public void StandBy()
    {
        Time.timeScale = 0f;
        state = State.StandBy;
    }

    public void End()
    {
        Time.timeScale = 0f;
        state = State.Finished;
    }

    public void Pause()
    {
        switch (state)
        {
            case State.Playing:
                state = State.Paused;
                Time.timeScale = 0f;
                break;
            case State.Paused:
                state = State.Playing;
                Time.timeScale = 1f;
                break;
            default:
                break;
        }
    }

    public void Exit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                            Application.Quit();
        #endif
    }

    // Players subscribers
    public void Subscribe(PlayerSubscriber observer)
    {
        if (!playersSubscribers.Contains(observer))
            playersSubscribers.Add(observer);
    }

    public void Unsubscribe(PlayerSubscriber observer)
    {
        if (playersSubscribers.Contains(observer))
            playersSubscribers.Remove(observer);
    }

    private void SetPlayersToInitialPosition()
    {
        foreach (var player in playersSubscribers)
        {
            player.MoveToInitialPosition();
        }
    }

    // Balls subscribers
    public void Subscribe(BallSubscriber observer)
    {
        if (!ballsSubscribers.Contains(observer))
            ballsSubscribers.Add(observer);
    }

    public void Unsubscribe(BallSubscriber observer)
    {
        if (ballsSubscribers.Contains(observer))
            ballsSubscribers.Remove(observer);
    }

    // GameUI subscribers
    public void Subscribe(GameUISubscriber observer)
    {
        if (!gameUISubscribers.Contains(observer))
            gameUISubscribers.Add(observer);
    }

    public void Unsubscribe(GameUISubscriber observer)
    {
        if (gameUISubscribers.Contains(observer))
            gameUISubscribers.Remove(observer);
    }

    private void RemoveBallsFromField()
    {
        foreach (var ball in ballsSubscribers)
            ball.RemoveFromField();
    }

    // Setters
    public void SetBallColor(UnityEngine.Color color)
    {
        ballColor = color;
        foreach (var observer in ballsSubscribers)
        {
            observer.ColorChanged(ballColor);
        }
    }

    public void SetHeightPercentage(float percentage)
    {
        characterPercentageHeight = percentage;
        foreach (var observer in playersSubscribers)
        {
            observer.HeightChanged(characterPercentageHeight);
        }
    }

    public void SetSpeedModifier(float speedModifier)
    {
        characterSpeedModifier = speedModifier;
        foreach (var observer in playersSubscribers)
        {
            observer.SpeedModifierChanged(characterSpeedModifier);
        }
    }

    // Getters
    public UnityEngine.Color GetBallColor()
    {
        return ballColor;
    }

    public float GetHeightPercentage()
    {
        return characterPercentageHeight;
    }

    public float GetSpeedModifier()
    {
        return characterSpeedModifier;
    }

    // Goals
    public void GoalFor(Goal.Side side)
    {
        if (side == Goal.Side.Left) 
            leftSideGoals++;
        else 
            rightSideGoals++;
        if (leftSideGoals >= goalsAmount || rightSideGoals >= goalsAmount)
            End();
        else
            StandBy();

        RemoveBallsFromField();

        foreach (var gameUI in gameUISubscribers)
        {
            gameUI.ScoreChanged(leftSideGoals, rightSideGoals);
            gameUI.StopGame(); 
        }
    }

    // Time changes
    private void UpdateUITime(float time)
    {
        foreach (var gameUI in gameUISubscribers)
        {
            gameUI.UpdateTimer(timeCount);
        }
    }
}