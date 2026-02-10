using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneName;
    //public static 

    bool isOpen = false;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOpen == true)
        {
            LoadScene();
        }
    }
}
