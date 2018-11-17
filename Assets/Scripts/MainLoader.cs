using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLoader : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Scenes/Main");
    }
}
