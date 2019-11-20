using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    [SerializeField]
    private string name_scene;

    public void LoadChosenScene()
    {
        SceneManager.LoadScene(name_scene);
    }
}