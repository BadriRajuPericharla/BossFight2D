using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using TMPro;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    [SerializeField]private GameObject checkInternet;
    [SerializeField]private UI uI;
    [SerializeField]private TextMeshProUGUI countDownTxt;

    InterstitialAd interstitial;
    RewardedInterstitialAd rewarded;

    private static int retryCount;

    string interstitialId = "ca-app-pub-9565881819222312/3046886573";
    string rewardedId = "ca-app-pub-9565881819222312/3792107556";

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

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
            if (error != null)
            {
                return;
            }

            if (ad == null)
            {
                return;
            }


            interstitial = ad;

            ad.OnAdFullScreenContentClosed += () =>
            {
                LoadInterstitial();
                SceneManager.LoadScene(
                    SceneManager.GetActiveScene().buildIndex
                );
            };
        });
    }

    public void ShowRetryAd()
    {
        retryCount++;
        Debug.Log(retryCount);
        if (retryCount % 3 == 0 && interstitial?.CanShowAd() == true)
        {
            interstitial.Show();
            retryCount=0;
        }
            
        else
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
    }


    void LoadRewarded()
    {
        rewarded?.Destroy();

        RewardedInterstitialAd.Load(rewardedId, new AdRequest(), (ad, error) =>
        {
            if (error != null || ad == null)
                return;

            rewarded = ad;

            ad.OnAdFullScreenContentClosed += LoadRewarded;
        });
    }

    public void ShowRewardedAd()
    {
        if (rewarded?.CanShowAd() == true)
        {
            uI.continuePanel.SetActive(true);
            StartCoroutine(ContinuePanelCountDown());
        }
        else
        {
            uI.ShowGameOver();
        }
    }

    public void PlayRewardedAd()
    {
        if (rewarded?.CanShowAd() != true)
        {
            uI.continuePanel.SetActive(false);
            uI.ShowGameOver();
            return;
        }

        bool rewardEarned = false;

        rewarded.OnAdFullScreenContentClosed += () =>
        {
            if (rewardEarned)
            {
                uI.StartCoroutine(uI.ResumeTimer());
            }

            LoadRewarded();
        };

        rewarded.Show((Reward reward) =>
        {
            rewardEarned = true;
        });
    }


    IEnumerator CheckInternet()
    {
        checkInternet.SetActive(true);

        yield return new WaitForSeconds(1f);

        checkInternet.SetActive(false);
    }
    IEnumerator ContinuePanelCountDown()
    {
        countDownTxt.text="5";
        yield return new WaitForSeconds(1f);
        countDownTxt.text="4";
        yield return new WaitForSeconds(1f);
        countDownTxt.text="3";
        yield return new WaitForSeconds(1f);
        countDownTxt.text="2";
        yield return new WaitForSeconds(1f);
        countDownTxt.text="1";
        yield return new WaitForSeconds(1f);
        uI.CloseContinuePanel();
    }
}