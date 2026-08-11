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
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private TextMeshProUGUI timerText;
    [SerializeField]private UI uI;
    private float currentTime;
    private float duration=100f;
    private modes currentMode;
    
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
        playerMovement.canAttack=true;

    }
    public void ChallengeMode()
    {
        currentMode=modes.challenge;
        PlayerPrefs.SetInt("GameMode",(int)currentMode);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerMovement.canAttack=true;

    }
    public IEnumerator Timer()
    {
        currentTime = duration;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);

            timerText.text = $"{minutes:00}:{seconds:00}";

            yield return null;
        }
        uI.ShowLevelComplete();
        timerText.text = "00:00";
    }

}
