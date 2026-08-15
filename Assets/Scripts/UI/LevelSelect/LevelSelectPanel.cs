using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectPanel : MonoBehaviour
{

	// Arena 1: Simple, to warm up
	// Arena 2: Starting to get harder, light-medium-medium-light-medium-heavy
	// Arena 3: Tough and hard, light-hard-hard-light-hard-medium-hard-light-extreme

	[SerializeField] private SceneSO[] scenes;

	public void OpenPanel()
	{
		Pause();
		gameObject.SetActive(true);

	}

	public void ClosePanel()
	{
		Unpause();
		gameObject.SetActive(false);

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
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
