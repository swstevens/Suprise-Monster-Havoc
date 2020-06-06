using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
	private string currentlevel;

	void Start()
	{

        if (SceneManager.GetActiveScene().name != "hub_world") {

            currentlevel = MapManager.instance.currentlevel;
        }
	}

    void Update() {
        
        if (Input.GetKeyDown(KeyCode.Escape)) {

        	if (GameIsPaused) {

        		Resume();

        	} else {

        		Pause();
        	}
        }
    }

    public void Resume() {

    	pauseMenuUI.SetActive(false);
    	Time.timeScale = 1f;
    	GameIsPaused = false;
        Cursor.visible = false;
    }

    void Pause() {

    	pauseMenuUI.SetActive(true);
    	Time.timeScale = 0f;
    	GameIsPaused = true;
    }

    public void Reload() {

    	Time.timeScale = 1f;
    	GameIsPaused = false;
    	Scene scene = SceneManager.GetActiveScene();
    	SceneManager.LoadScene(currentlevel);
    }

    public void MainMenu() {

    	Time.timeScale = 1f;
    	pauseMenuUI.SetActive(false);
    	GameIsPaused = false;
        SceneManager.LoadScene("main_menu");

    }

    public void QuitGame() {

    	Debug.Log("Quitting game...");
    	Application.Quit();
    }
}
