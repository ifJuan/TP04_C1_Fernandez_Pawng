using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public interface PlayerSubscriber
{
    void HeightChanged(float percentage) { /* Default */ }
    void SpeedModifierChanged(float speedModifier) { /* Default */ }
}

public interface BallSubscriber
{
    void ColorChanged(UnityEngine.Color color);
}

public class GameStateManager: MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private GameStateDefaultValuesSo defaultValues;

    // Singleton
    public static GameStateManager Instance;

    // Subscribers 
    private List<PlayerSubscriber> playersSubscribers;
    private List<BallSubscriber> ballsSubscribers;

    // Players
    private float characterPercentageHeight;
    private float characterSpeedModifier;
    private UnityEngine.Color ballColor;

    // State 
    public State state;
    public enum State
    {
        MainMenu,
        Paused,
        Playing
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
            playersSubscribers = new();
            ballsSubscribers = new();
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    //State changes
    public void Play()
    {
        if (state != State.Playing) {
            state = State.Playing;
            Time.timeScale = 1f;
        }
    }

    public void Pause()
    {
        if (state == State.MainMenu)
            return;
        if (state == State.Playing)
        {
            state = State.Paused;
            Time.timeScale = 0f;
        } else
        {
            state = State.Playing;
            Time.timeScale = 1f;
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
        if (!playersSubscribers.Contains(observer))
            playersSubscribers.Remove(observer);
    }

    // Balls subscribers
    public void Subscribe(BallSubscriber observer)
    {
        if (!ballsSubscribers.Contains(observer))
            ballsSubscribers.Add(observer);
    }

    public void Unsubscribe(BallSubscriber observer)
    {
        if (!ballsSubscribers.Contains(observer))
            ballsSubscribers.Remove(observer);
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
}