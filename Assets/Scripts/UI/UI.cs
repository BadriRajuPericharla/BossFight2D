
using System.Collections;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField]private GameObject MainMenu;
    [SerializeField]private GameObject GameOver;
    [SerializeField]private GameObject levelComplete;
    [SerializeField]private GameObject Settings;
    [SerializeField]private GameObject MobileControlPanel;
    [SerializeField]private GameObject KeyboardContorlsPanel;
    [SerializeField]private GameObject modesPanel;
    [SerializeField]private GameObject healthBars;
    [SerializeField]private GameObject pausePanel;
    [Header("Monobehaviour Scripts")]
    [SerializeField]private MonoBehaviour movement;
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private EnemyController enemyController;
    [SerializeField]private PlayerShoot bulletScript;
    [SerializeField]private Spikes spikesScript;  
    [Header("ParticleSystems")]
    [SerializeField]private ParticleSystem[] introParticleSystem;
    [Header("Text")]
    [SerializeField]private TextMeshProUGUI countDownTxt;
  
   

    static bool ShowModes=false;
    
    static bool SkipMenu=false;
    
    
    
    void Start()
    {  
        Time.timeScale = 0f;

        if (ShowModes)
        {
            Modes.instance.DefaultMode();
            ShowModes = false;

            MainMenu.SetActive(false);
            modesPanel.SetActive(true);

            playerMovement.enabled = false;
            enemyController.enabled = false;
            spikesScript.enabled = false;
            bulletScript.enabled = false;
            movement.enabled = false;

            return;
        }

        if (SkipMenu && MainMenu != null)
        {
            MainMenu.SetActive(false);
            Time.timeScale = 1f;

            foreach (ParticleSystem particleSystem in introParticleSystem)
            {
                particleSystem.gameObject.SetActive(false);
            }

            healthBars.SetActive(true);
            MobileControlPanel.SetActive(true);

            spikesScript.enabled = true;
            bulletScript.enabled = true;

            SkipMenu = false;
            movement.enabled = true;
        }
        else
        {
            if (MainMenu != null)
            {
                MainMenu.SetActive(true);
                Modes.instance.DefaultMode();
                playerMovement.enabled = false;
                enemyController.enabled = false;
                spikesScript.enabled = false;
                bulletScript.enabled = false;
                movement.enabled = false;
            }
        }
        
    }

    
    public void showMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
        SkipMenu=true;
        MainMenu.SetActive(false);
        modesPanel.SetActive(true);
        
       
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SkipMenu=true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
        Time.timeScale=0f;
    }
    public void ShowSettings()
    {
        if (MainMenu != null)
        {
            MainMenu.SetActive(false);
        }
        
        GameOver.SetActive(false);
        MobileControlPanel.SetActive(false);
        Settings.SetActive(true);
        Time.timeScale=0f;
    }
    public void Home()
    {
        showMainMenu();
        
    }
    public void NewGame()
    {
        ShowModes=true;
        SkipMenu=true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        StartCoroutine(ResumeTimer());
    }
    public void ShowGameOver()
    {
        GameOver.SetActive(true);
        MobileControlPanel.SetActive(false);
    }    
    public void ShowLevelComplete()
    {
        levelComplete.SetActive(true);
        MobileControlPanel.SetActive(false);
    }
    public void ShowControls()
    {
        KeyboardContorlsPanel.SetActive(true);
        Time.timeScale=0f;
    }
    public void CloseSettings()
    {
        Settings.SetActive(false);
        if (true)
        {
            MobileControlPanel.SetActive(true);
            KeyboardContorlsPanel.SetActive(false);
        }
        Time.timeScale=1f;
    }
    IEnumerator ResumeTimer()
    {
        Time.timeScale=0f;
        countDownTxt.gameObject.SetActive(true);
        countDownTxt.text="3";
        yield return new WaitForSecondsRealtime(1);
        countDownTxt.text="2";
        yield return new WaitForSecondsRealtime(1);
        countDownTxt.text="1";
        yield return new WaitForSecondsRealtime(1);
        countDownTxt.gameObject.SetActive(false);
        if (true)
        {
            MobileControlPanel.SetActive(true);
            KeyboardContorlsPanel.SetActive(false);
        }
        Time.timeScale=1f;
        
    }
}
