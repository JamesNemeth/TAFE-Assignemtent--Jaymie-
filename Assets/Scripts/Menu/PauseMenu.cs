using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public GameObject _pauseMenu;
    // Update is called once per frame
    void Start()
    {
        _pauseMenu = GameObject.FindGameObjectWithTag("Pausemenu");
        isPaused = false;//not pasued 
        Time.timeScale = 1;//start time
        Cursor.lockState = CursorLockMode.Locked;//lock cursor to cenetr of screen
        Cursor.visible = false;//hide cursor
        _pauseMenu.SetActive(false);//show pause menu

    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))//press escape
        {
            TogglePause();//runs pause function
        }
    }

    public void TogglePause()
    {
        if (isPaused)//is true if it is active
        {
            _pauseMenu.SetActive(false);//show pause menu
            isPaused = false;//not pasued 
            Time.timeScale = 1;//start time
            Cursor.lockState = CursorLockMode.Locked;//lock cursor to cenetr of screen
            Cursor.visible = false;//hide cursor
        }
        else//is false if it is active
        {

            _pauseMenu.SetActive(true);//hide pause menu
            isPaused = true;//we are pasued
            Time.timeScale = 0;//stop time
            Cursor.lockState = CursorLockMode.None;//allow cursor movement
            Cursor.visible = true;//show cursor
        }
    }
}
