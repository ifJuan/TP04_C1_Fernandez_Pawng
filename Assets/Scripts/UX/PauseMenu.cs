using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnContinue;
    [SerializeField] private TextMeshProUGUI continueText;
    [SerializeField] private Button btnSettings;
    [SerializeField] private TextMeshProUGUI settingsText;
    [SerializeField] private Button btnCredits;
    [SerializeField] private TextMeshProUGUI creditsText;
    [SerializeField] private Button btnExit;
    [SerializeField] private TextMeshProUGUI exitText;

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    private void Awake()
    {
        btnContinue.onClick.AddListener(BtnContinueClicked);
        btnSettings.onClick.AddListener(BtnSettingsClicked);
        btnCredits.onClick.AddListener(BtnCreditsClicked);
        btnExit.onClick.AddListener(BtnExitClicked);
    }

    private void OnDestroy()
    {
        btnContinue.onClick.RemoveAllListeners();
        btnSettings.onClick.RemoveAllListeners();
        btnCredits.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
    }

    void Start()
    {
        continueText.text = "Continue";
        settingsText.text = "Settings";
        creditsText.text = "Credits";
        exitText.text = "Exit";
    }

    private void BtnContinueClicked()
    {
        GameStateManager.Instance.Play();
        gameObject.SetActive(false);
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