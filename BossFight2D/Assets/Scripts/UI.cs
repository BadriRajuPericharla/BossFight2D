
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]GameObject MainMenu;
    [SerializeField]GameObject GameOver;
    [SerializeField]GameObject Settings;
    [SerializeField]MonoBehaviour movement;
    [SerializeField]GameObject score;
    [SerializeField]GameObject settingIcon;

    
    
    static bool SkipMenu=false;
    
    void Start()
    {

        if (SkipMenu && MainMenu!=null)
        {
            MainMenu.SetActive(false);
            SkipMenu=false;
            movement.enabled=true;
        }
        else
        {
            if (MainMenu != null)
            {
                MainMenu.SetActive(true);
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
    }
    public void Home()
    {
        showMainMenu();
    }
    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        Settings.SetActive(false);
        
    }
    


}
