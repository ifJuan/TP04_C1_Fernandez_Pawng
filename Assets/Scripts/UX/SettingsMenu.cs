using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Slider heightSlider;
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button colorBtn;
    [SerializeField] private TextMeshProUGUI settingsText;
    [SerializeField] private TextMeshProUGUI speedModifierText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI heightModifierText;
    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TextMeshProUGUI colorBtnText;

    [Header("Players")]
    [SerializeField] private Movement player1;
    [SerializeField] private Movement player2;

    void Start()
    {
        settingsText.text = "Settings";
        speedModifierText.text = "Speed modifier (1-5)";
        heightModifierText.text = "Height modifier\n(Screen %)";
        colorBtnText.text = "RNG Ball Color";
    }


    void Awake()
    {
        speedSlider.onValueChanged.AddListener(OnSpeedMove);
        if (heightSlider != null) {
            heightSlider.onValueChanged.AddListener(OnHeightMove);
        }
        closeBtn.onClick.AddListener(OnCloseTap);
        colorBtn.onClick.AddListener(OnColorTap);
    }

    void OnSpeedMove(float value)
    {
        speedText.text = value.ToString("F2");
        GameStateManager.Instance.SetSpeedModifier(value);
    }

    void OnHeightMove(float value)
    {
        heightText.text = value.ToString("F2") + "%";
        GameStateManager.Instance.SetHeightPercentage(value);
    }

    private void OnDestroy()
    {
        speedSlider.onValueChanged.RemoveAllListeners();
        heightSlider.onValueChanged.RemoveAllListeners();
        closeBtn.onClick.RemoveAllListeners();
        colorBtn.onClick.RemoveAllListeners();
    }

    private void OnCloseTap()
    {
        gameObject.SetActive(false);
    }

    private void OnColorTap()
    {
        Color color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
        GameStateManager.Instance.SetBallColor(color);
    }
}