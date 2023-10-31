using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControlaCenas : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;
    private bool isMuted;
    public void CarregarCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    public void PauseTime()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            pauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            isPaused = true;
            pauseMenu.SetActive(true);
        }          

    }

    public void MuteGame()
    {
        if(isMuted)
        {
            isMuted = false;
            AudioListener.volume = 0f;
        }
        else
        {
            isMuted = true;
            AudioListener.volume = 1f;
        }
    }
}
