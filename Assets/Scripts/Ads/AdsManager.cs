using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using TMPro;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;
    [SerializeField] private UI uI;
    [SerializeField] private TextMeshProUGUI countDownTxt;
    private InterstitialAd interstitial;
    private RewardedInterstitialAd rewarded;
    private static int retryCount;
    private string interstitialId = "ca-app-pub-9565881819222312/3046886573";
    private string rewardedId = "ca-app-pub-3940256099942544/5354046379";
    private bool isShowingRewardedAd;
    private bool rewardEarned;
    private Coroutine continueCountdown;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        MobileAds.Initialize(initStatus =>
        {
            LoadInterstitial();
            LoadRewarded();
        });
    }
    private void LoadInterstitial()
    {
        if (interstitial != null)
        {
            interstitial.Destroy();
            interstitial = null;
        }
        InterstitialAd.Load(interstitialId,new AdRequest(),(ad, error) =>
            {
                if (error != null)
                {
                    return;
                }

                if (ad == null)
                {
                    return;
                }
                interstitial = ad;
                ad.OnAdFullScreenContentClosed +=HandleInterstitialClosed;
                ad.OnAdFullScreenContentFailed +=HandleInterstitialFailed;
            }
        );
    }


    private void HandleInterstitialClosed()
    {
        LoadInterstitial();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void HandleInterstitialFailed(AdError error)
    {
        LoadInterstitial();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ShowRetryAd()
    {
        retryCount++;
        if (retryCount>= 3 &&interstitial != null &&interstitial.CanShowAd())
        {
            retryCount = 0;
            interstitial.Show();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void LoadRewarded()
    {
        if (rewarded != null)
        {
            rewarded.Destroy();
            rewarded = null;
        }
        RewardedInterstitialAd.Load(rewardedId,new AdRequest(),(ad, error) =>
            {
                if (error != null)
                {
                    return;
                }

                if (ad == null)
                {
                    return;
                }
                rewarded = ad;
                ad.OnAdFullScreenContentClosed +=HandleRewardedClosed;
                ad.OnAdFullScreenContentFailed +=HandleRewardedFailed;
            }
        );
    }
    public void ShowRewardedAd()
    {
        if (isShowingRewardedAd)
        {

            return;
        }
        if (rewarded != null &&rewarded.CanShowAd())
        {
            uI.continuePanel.SetActive(true);    
            if (continueCountdown != null)
            {
                StopCoroutine(continueCountdown);
            }
            continueCountdown =StartCoroutine(ContinuePanelCountDown());
        }
        else
        {
            uI.continuePanel.SetActive(false);
            LoadRewarded();
        }
    }
    public void PlayRewardedAd()
    {
        if (continueCountdown != null)
        {
            StopCoroutine(continueCountdown);
            continueCountdown = null;
        }
        if (isShowingRewardedAd)
        {
            return;
        }

        if (rewarded == null || !rewarded.CanShowAd())
        {
            uI.continuePanel.SetActive(false);
            uI.ShowGameOver();
            LoadRewarded();
            return;
        }
        isShowingRewardedAd = true;
        rewardEarned = false;
        RewardedInterstitialAd currentAd = rewarded;
        rewarded = null;
        uI.continuePanel.SetActive(false);
        currentAd.Show((Reward reward) =>{rewardEarned = true;});
    }
    private void HandleRewardedClosed()
    {
        isShowingRewardedAd = false;
        if (rewardEarned)
        {
            uI.StartCoroutine(uI.ResumeTimer());
        }
        else
        {
            uI.ShowGameOver();
        }
        rewardEarned = false;
        LoadRewarded();
    }
    private void HandleRewardedFailed(
        AdError error
    )
    {
        isShowingRewardedAd = false;
        rewardEarned = false;
        uI.continuePanel.SetActive(false);
        uI.ShowGameOver();
        LoadRewarded();
    }

    private IEnumerator ContinuePanelCountDown()
    {
        countDownTxt.text = "5";
        yield return new WaitForSecondsRealtime(1f);
        countDownTxt.text = "4";
        yield return new WaitForSecondsRealtime(1f);
        countDownTxt.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        countDownTxt.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        countDownTxt.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        continueCountdown = null;
        if (uI.continuePanel.activeSelf)
        {
            uI.CloseContinuePanel();
        }
    }
}