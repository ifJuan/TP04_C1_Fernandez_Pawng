using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject creditsPanel;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (GameStateManager.Instance.state != GameStateManager.State.MainMenu)
            {
                GameStateManager.Instance.Pause();
                menu.SetActive(GameStateManager.Instance.state == GameStateManager.State.Paused);
            }
            if (GameStateManager.Instance.state == GameStateManager.State.Playing)
            {
                settingPanel.SetActive(false);
                creditsPanel.SetActive(false);
            }
        }
    }
}
