using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI muteButtonText;

    [Header("Scene Settings")]
    [SerializeField] private string homepageSceneName = "Homepage";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (popupPanel != null)
            popupPanel.SetActive(false);
    }

    // Dipanggil oleh ButtonMenu (titik 3)
    public void TogglePopup()
    {
        bool isActive = popupPanel.activeSelf;
        popupPanel.SetActive(!isActive);

        if (!isActive)
            RefreshMuteButtonLabel();
    }

    // Dipanggil oleh ButtonClosePopup
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    // Dipanggil oleh ButtonMuteToggle
    public void ToggleMute()
    {
        if (BackgroundMusicManager.Instance != null)
        {
            BackgroundMusicManager.Instance.ToggleMute();
            RefreshMuteButtonLabel();
        }
    }

    private void RefreshMuteButtonLabel()
    {
        if (muteButtonText == null || BackgroundMusicManager.Instance == null) return;

        bool isMuted = BackgroundMusicManager.Instance.IsMuted();
        muteButtonText.text = isMuted ? "  + Unmute Sound" : "  + Mute Sound";
    }

    // Dipanggil oleh ButtonBackToHome
    public void BackToHomepage()
    {
        popupPanel.SetActive(false);
        SceneManager.LoadScene(homepageSceneName);
    }

    // Dipanggil oleh ButtonExitApp
    public void ExitApp()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }
}