using UnityEngine;

public class LevelSelectUI : MonoBehaviour
{
    public void CloseLevelSelect()
    {
        Unpause();
        gameObject.SetActive(false);
    }
    
    public void OpenLevelSelect()
    {
        Pause();
        gameObject.SetActive(true);
    }
    
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
    
    public void Unpause()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
