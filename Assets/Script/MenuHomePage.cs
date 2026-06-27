using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuHomePage : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Nama scene tujuan saat tombol Mulai Jelajah ditekan")]
    [SerializeField] private string firstRoomSceneName = "Lobby2";

    [Header("Sound Settings")]
    [SerializeField] private Image soundIcon;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    // Dipanggil oleh ButtonStart
    public void StartExplore()
    {
        Debug.Log("StartExplore dipanggil, loading scene: " + firstRoomSceneName);
        SceneManager.LoadScene(firstRoomSceneName);
    }

    // Dipanggil oleh ButtonExit
    public void ExitApp()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    // Dipanggil oleh ButtonSound
    public void ToggleSound()
    {
        if (BackgroundMusicManager.Instance != null)
        {
            BackgroundMusicManager.Instance.ToggleMute();
            UpdateSoundIcon(BackgroundMusicManager.Instance.IsMuted());
        }
    }

    private void UpdateSoundIcon(bool isMuted)
    {
        if (soundIcon == null) return;

        if (isMuted && soundOffSprite != null)
            soundIcon.sprite = soundOffSprite;
        else if (!isMuted && soundOnSprite != null)
            soundIcon.sprite = soundOnSprite;
    }
}