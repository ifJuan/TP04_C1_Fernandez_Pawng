using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : MonoBehaviour
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private TextMeshProUGUI creditsText;

    void Awake()
    {
        closeBtn.onClick.AddListener(CloseBtnClicked);
    }

    void Start()
    {
        creditsText.text = "Credits\r\n\r\nGame designed by: \r\n - Me\r\n\r\nAssets:\r\n - Also me\r\n - Ansimuz\r\n - Pay Artists Studios\r\n - Google (couldn't find the artist)";
    }

    private void OnDestroy()
    {
        closeBtn.onClick.RemoveAllListeners();
    }

    void CloseBtnClicked()
    {
        gameObject.SetActive(false);
    }
}
