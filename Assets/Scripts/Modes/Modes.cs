using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
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
        survival,
        elimination,
        challenge
    }
    
    public modes currentMode;
    
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
