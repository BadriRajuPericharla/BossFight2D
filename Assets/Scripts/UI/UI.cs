
using System.Collections;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField]private GameObject informationPanel;
    [SerializeField]private GameObject survivalInformation;
    [SerializeField]private GameObject challengeInformation;
    [SerializeField]private GameObject eliminationInformation;
    
    public GameObject continuePanel;
    [Header("Monobehaviour Scripts")]
    [SerializeField]private MonoBehaviour movement;
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private EnemyController enemyController;
    [SerializeField]private PlayerShoot bulletScript;
    [SerializeField]private PlayerHealth playerHealth;
    [SerializeField]private PlayerShield playerShield;
    [SerializeField]private ChildEnemyController[] chilEnemyController;
    [SerializeField]private ModesManager modesManager;
    [SerializeField]private Spikes spikesScript;
    [Header("Text")]
    [SerializeField]private TextMeshProUGUI countDownTxt;

  
    private static int continueCounter=0;

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
    public void SurvivalWarning()
    {
        modesPanel.SetActive(false);
        informationPanel.SetActive(true);
        survivalInformation.SetActive(true);
    }
    public void CloseSurvivalWarning()
    {
        informationPanel.SetActive(false);
        survivalInformation.SetActive(false);
    }
    public void ChallengeWarning()
    {
        modesPanel.SetActive(false);
        informationPanel.SetActive(true);
        challengeInformation.SetActive(true);
    }
    public void CloseChallengeWarning()
    {

        informationPanel.SetActive(false);
        challengeInformation.SetActive(false);
    }
    public void EliminationWarning()
    {
        modesPanel.SetActive(false);
        informationPanel.SetActive(true);
        eliminationInformation.SetActive(true);
    }
    public void CloseEliminationWarning()
    {
        informationPanel.SetActive(false);
        eliminationInformation.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
        Time.timeScale=0f;
    }
    public void ShowSettings()
    {
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
        SkipMenu=true;
        AdsManager.Instance.ShowRetryAd();
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        StartCoroutine(ResumeTimer());
    }
    public void ShowGameOver()
    {
        continueCounter++;
        if (continueCounter >= 2)
        {
            continueCounter=0;
            AdsManager.Instance.ShowRewardedAd();
        }
        else
        {
            GameOver.SetActive(true);
            MobileControlPanel.SetActive(false);
            healthBars.SetActive(false);
            foreach(ChildEnemyController childEnemyController in chilEnemyController)
            {
                childEnemyController.enabled=false;
            }
        }
        
        
    }
    public void CloseContinuePanel()
    {
        continuePanel.SetActive(false);
        GameOver.SetActive(true);
        MobileControlPanel.SetActive(false);
        healthBars.SetActive(false);
        foreach(ChildEnemyController childEnemyController in chilEnemyController)
        {
            childEnemyController.enabled=false;
        }
    }
    public void ContinueButton()
    {
        continuePanel.SetActive(false);
        AdsManager.Instance.PlayRewardedAd();
    }
    public void ShowLevelComplete()
    {
        levelComplete.SetActive(true);
        MobileControlPanel.SetActive(false);
        healthBars.SetActive(false);
        foreach(ChildEnemyController childEnemyController in chilEnemyController)
        {
            childEnemyController.enabled=false;
        }
    }
    public void ShowControls()
    {
        KeyboardContorlsPanel.SetActive(true);
        Time.timeScale=0f;
    }
    public void CloseSettings()
    {
        Settings.SetActive(false);
        if (!MainMenu.activeInHierarchy && true)
        {
            MobileControlPanel.SetActive(true);
            KeyboardContorlsPanel.SetActive(false);
        }
        Time.timeScale=1f;
    }
    public IEnumerator ResumeTimer()
    {
        GameOver.SetActive(false);
        continuePanel.SetActive(false);
        playerHealth.CurrentHealth = 70f;
        playerHealth.healthSlider.value = 70f;
        playerHealth.FillArea.SetActive(true);
        playerHealth.IsDead = false;
        movement.gameObject.SetActive(true);
        playerMovement.enabled = false;
        bulletScript.enabled = false;
        playerShield.enabled = false;
        enemyController.enabled = false;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 0f;
        countDownTxt.gameObject.SetActive(true);
        countDownTxt.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        countDownTxt.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        countDownTxt.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        countDownTxt.gameObject.SetActive(false);
        MobileControlPanel.SetActive(true);
        KeyboardContorlsPanel.SetActive(false);
        enemyController.enabled = true;
        playerMovement.enabled = true;
        bulletScript.enabled = true;
        playerShield.enabled = true;
        Time.timeScale = 1f;
    }
}
