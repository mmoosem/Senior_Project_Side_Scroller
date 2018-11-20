using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuCanvas;
    public Text gunText;
    public Text DashText;
    public Text WallJumpText;
    public Text DoubleJumpText;
    public bool isPaused;
    public bool isOptions;

    public string MainMenuScene;


    // Update is called once per frame
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("player_character");
       
    }

    void Update()
    {
        if (isPaused)
        {
            
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (player.GetComponent<playerController>().hasGun == true)
            {
                gunText.color = Color.white;
            }
            if (player.GetComponent<playerController>().dashing == true)
            {
                DashText.color = Color.white;
            }
            if (player.GetComponent<playerController>().wallJumping == true)
            {
                WallJumpText.color = Color.white;
            }
            if (player.GetComponent<playerController>().doubleJumping == true)
            {
                DoubleJumpText.color = Color.white;
            }

        }
    }

    public void Resume()
    {
        isPaused = false;
    }
    public void Quit()
    {
        SceneManager.LoadScene(MainMenuScene);
    }
}