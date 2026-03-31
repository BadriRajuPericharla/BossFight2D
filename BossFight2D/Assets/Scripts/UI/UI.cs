
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]GameObject MainMenu;
    [SerializeField]GameObject GameOver;
    [SerializeField]GameObject Settings;
    [SerializeField]MonoBehaviour movement;
    [SerializeField]PlayerMovement playerMovement;
    [SerializeField]EnemyController enemyController;
    [SerializeField]private GameObject healthBars;
    [SerializeField]GameObject score;
    [SerializeField]GameObject settingIcon;
   

    
    
    static bool SkipMenu=false;
    
    void Start()
    {
        

        if (SkipMenu && MainMenu!=null)
        {
            MainMenu.SetActive(false);
            Time.timeScale=1;
            settingIcon.SetActive(true);
            healthBars.SetActive(true);
            SkipMenu=false;
            movement.enabled=true;
        }
        else
        {
            if (MainMenu != null)
            {
                MainMenu.SetActive(true);
                playerMovement.enabled=false;
                enemyController.enabled=false;
                movement.enabled=false;
            }
            
        }
    }

    
    void Update()
    {
        
    }
    public void showMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
        SkipMenu=true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
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
    public void ShowSettings()
    {
        if (MainMenu != null)
        {
            MainMenu.SetActive(false);
        }
        
        GameOver.SetActive(false);
        Settings.SetActive(true);
        Time.timeScale=0f;
    }
    public void Home()
    {
        showMainMenu();
    }
    public void Next()
    {
        EnemyHealth.MaxHealth+=200;
        SkipMenu=true;
        SceneManager.LoadScene(0);
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        Settings.SetActive(false);
        Time.timeScale=1f;
        
    }
    


}
