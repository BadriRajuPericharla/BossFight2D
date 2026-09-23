using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using TMPro;
using CrazyGames;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;
    [SerializeField] private UI uI;
    [SerializeField] private TextMeshProUGUI countDownTxt;
    private InterstitialAd interstitial;
    private RewardedInterstitialAd rewarded;
    private static int retryCount;
    private string interstitialId = "ca-app-pub-9565881819222312/3046886573";
    private string rewardedId = "ca-app-pub-9565881819222312/3792107556";
    private bool isShowingRewardedAd;
    private bool rewardEarned;
    private bool crazyGamesInitialized;
    private Coroutine continueCountdown;

    private bool IsCrazyGames()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return CrazySDK.IsAvailable;
#else
        return false;
#endif
        
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (IsCrazyGames())
        {
            CrazySDK.Init(() =>
            {
                crazyGamesInitialized = true;
                Debug.Log("CrazyGames SDK initialized.");
            });
            return;
        }

        MobileAds.Initialize(initStatus =>
        {
            LoadInterstitial();
            LoadRewarded();
        });
    }

    private void LoadInterstitial()
    {
        if (IsCrazyGames())
            return;

        if (interstitial != null)
        {
            interstitial.Destroy();
            interstitial = null;
        }

        InterstitialAd.Load(interstitialId, new AdRequest(), (ad, error) =>
        {
            if (error != null || ad == null)
                return;

            interstitial = ad;
            ad.OnAdFullScreenContentClosed += HandleInterstitialClosed;
            ad.OnAdFullScreenContentFailed += HandleInterstitialFailed;
        });
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

        if (retryCount < 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        retryCount = 0;

        if (IsCrazyGames())
        {
            if (!crazyGamesInitialized)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }

            CrazySDK.Ad.RequestAd(
                CrazyAdType.Midgame,
                () => { },
                error =>
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                },
                () =>
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            );

            return;
        }

        if (interstitial != null && interstitial.CanShowAd())
        {
            interstitial.Show();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void LoadRewarded()
    {
        if (IsCrazyGames())
            return;

        if (rewarded != null)
        {
            rewarded.Destroy();
            rewarded = null;
        }

        RewardedInterstitialAd.Load(rewardedId, new AdRequest(), (ad, error) =>
        {
            if (error != null || ad == null)
                return;

            rewarded = ad;
            ad.OnAdFullScreenContentClosed += HandleRewardedClosed;
            ad.OnAdFullScreenContentFailed += HandleRewardedFailed;
        });
    }

    public void ShowRewardedAd()
    {
        if (isShowingRewardedAd)
            return;

        if (IsCrazyGames())
        {
            if (!crazyGamesInitialized)
            {
                Debug.Log("CrazyGames SDK is not initialized yet.");
                return;
            }

            uI.continuePanel.SetActive(true);

            if (continueCountdown != null)
                StopCoroutine(continueCountdown);

            continueCountdown = StartCoroutine(ContinuePanelCountDown());
            return;
        }

        if (rewarded != null && rewarded.CanShowAd())
        {
            uI.continuePanel.SetActive(true);

            if (continueCountdown != null)
                StopCoroutine(continueCountdown);

            continueCountdown = StartCoroutine(ContinuePanelCountDown());
        }
        else
        {
            uI.ShowGameOver();
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
            return;

        if (IsCrazyGames())
        {
            if (!crazyGamesInitialized)
            {
                Debug.Log("CrazyGames SDK is not initialized yet.");
                return;
            }

            isShowingRewardedAd = true;
            rewardEarned = false;
            uI.continuePanel.SetActive(false);

            CrazySDK.Ad.RequestAd(
                CrazyAdType.Rewarded,
                () =>
                {
                    Debug.Log("CrazyGames Rewarded Ad Started.");
                },
                error =>
                {
                    Debug.Log("CrazyGames Rewarded Ad Error: " + error);
                    isShowingRewardedAd = false;
                    rewardEarned = false;
                    uI.continuePanel.SetActive(false);
                    uI.ShowGameOver();
                },
                () =>
                {
                    Debug.Log("CrazyGames Rewarded Ad Finished.");
                    isShowingRewardedAd = false;
                    rewardEarned = true;
                    uI.StartCoroutine(uI.ResumeTimer());
                    rewardEarned = false;
                }
            );

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

        currentAd.Show((Reward reward) =>
        {
            rewardEarned = true;
        });
    }

    private void HandleRewardedClosed()
    {
        isShowingRewardedAd = false;

        if (rewardEarned)
            uI.StartCoroutine(uI.ResumeTimer());
        else
            uI.ShowGameOver();

        rewardEarned = false;
        LoadRewarded();
    }

    private void HandleRewardedFailed(AdError error)
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
            uI.CloseContinuePanel();
    }
}