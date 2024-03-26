using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public bool MenuScene;

    private void Start()
    {
        if (MenuScene)
        {
            SoundManager.Instance.StartMenuMusic();
        }
        else
        {
            SoundManager.Instance.StartGameMusic();
        }
    }
}
