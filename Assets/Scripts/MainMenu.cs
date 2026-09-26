using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private TextMeshProUGUI playText;
    [SerializeField] private Button btnSettings;
    [SerializeField] private TextMeshProUGUI settingsText;
    [SerializeField] private Button btnCredits;
    [SerializeField] private TextMeshProUGUI creditsText;
    [SerializeField] private Button btnExit;
    [SerializeField] private TextMeshProUGUI exitText;

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    private string gameScene = "GameScene";

    private void Awake()
    {
        btnPlay.onClick.AddListener(BtnPlayClicked);
        btnSettings.onClick.AddListener(BtnSettingsClicked);
        btnCredits.onClick.AddListener(BtnCreditsClicked);
        btnExit.onClick.AddListener(BtnExitClicked);
    }

    private void OnDestroy()
    {
        btnPlay.onClick.RemoveAllListeners();
        btnSettings.onClick.RemoveAllListeners();
        btnCredits.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
    }

    void Start()
    {
        playText.text = "Play";
        settingsText.text = "Settings";
        creditsText.text = "Credits";
        exitText.text = "Exit";
    }

    private void BtnPlayClicked()
    {
        GameStateManager.Instance.Play();
        SceneManager.LoadScene(gameScene, LoadSceneMode.Single);
    }

    private void BtnSettingsClicked()
    {
        settingsPanel.SetActive(true);
    }

    private void BtnCreditsClicked()
    {
        creditsPanel.SetActive(true);
    }

    private void BtnExitClicked()
    {
        GameStateManager.Instance.Exit();
    }
}
