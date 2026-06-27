using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        // Kalau sudah ada instance lain, hancurkan yang baru (cegah duplikat)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // <- kunci utamanya, GameObject ini gak ikut hancur tiap ganti scene
    }

    public void ToggleMute()
    {
        audioSource.mute = !audioSource.mute;
    }

    public bool IsMuted()
    {
        return audioSource.mute;
    }
}