using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleporter : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    public void TeleportToScene()
    {
        if (InfoHotspot.IsPopupOpen) return;
        SceneManager.LoadScene(sceneToLoad);
    }
}