using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    [SerializeField] GameObject checkInternet;

    InterstitialAd interstitial;
    RewardedInterstitialAd rewarded;

    int retryCount;

    string interstitialId = "ca-app-pub-9565881819222312/3046886573";
    string rewardedId = "ca-app-pub-9565881819222312/3792107556";

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        MobileAds.Initialize(_ =>
        {
            LoadInterstitial();
            LoadRewarded();
        });
    }

    void LoadInterstitial()
    {
        interstitial?.Destroy();

        InterstitialAd.Load(interstitialId, new AdRequest(), (ad, error) =>
        {
            if (error != null || ad == null) return;

            interstitial = ad;
            ad.OnAdFullScreenContentClosed += () =>
            {
                LoadInterstitial();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            };
        });
    }

    public void ShowRetryAd()
    {
        retryCount++;

        if (retryCount % 3 == 0 && interstitial?.CanShowAd() == true)
            interstitial.Show();
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadRewarded()
    {
        rewarded?.Destroy();

        RewardedInterstitialAd.Load(rewardedId, new AdRequest(), (ad, error) =>
        {
            if (error != null || ad == null) return;

            rewarded = ad;
            ad.OnAdFullScreenContentClosed += LoadRewarded;
        });
    }

    public void ShowRewardedAd()
    {
        if (rewarded?.CanShowAd() == true)
        {
            rewarded.Show(_ => Debug.Log("Reward Earned!"));
        }
        else
        {
            StartCoroutine(CheckInternet());
        }
    }

    IEnumerator CheckInternet()
    {
        checkInternet.SetActive(true);
        yield return new WaitForSeconds(1);
        checkInternet.SetActive(false);
    }
}