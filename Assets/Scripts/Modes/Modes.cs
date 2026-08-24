using UnityEngine;
using UnityEngine.SceneManagement;

public class Modes : MonoBehaviour
{
    public static Modes instance;
    void Awake()
    {
        if (instance == null)
        {
            instance=this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public enum modes
    {
        def,
        survival,
        elimination,
        challenge
    }
    
    public modes currentMode;
    
    public void DefaultMode()
    {
        currentMode=modes.def;
        PlayerPrefs.SetInt("GameMode",(int)currentMode);
        PlayerPrefs.Save();
    }
    public void SurvivalMode()
    {
        currentMode=modes.survival;
        PlayerPrefs.SetInt("GameMode",(int)currentMode);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EliminationMode()
    {
        currentMode=modes.elimination;
        PlayerPrefs.SetInt("GameMode",(int)currentMode);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ChallengeMode()
    {
        currentMode=modes.challenge;
        PlayerPrefs.SetInt("GameMode",(int)currentMode);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    

}
