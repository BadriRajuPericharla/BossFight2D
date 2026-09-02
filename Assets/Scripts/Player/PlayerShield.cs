using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class PlayerShield : MonoBehaviour
{
    public static PlayerShield instance;
    void Awake()
    {
        if (instance == null)
        {
            instance=this;
        }
        else
            Destroy(gameObject);
    }
    [SerializeField]private GameObject playershield;
    [SerializeField]private Image image;
    public bool shieldActivated=false;
    private bool resetShield=false;
    void Start()
    {
        playershield.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !shieldActivated)
        {
            StartCoroutine(ActivateShield());
        }
    }
    public void ShieldButton()
    {
        if (!resetShield)
        {
            StartCoroutine(ActivateShield());
        }
    }
    IEnumerator ActivateShield()
    {
        image.enabled = true;
        playershield.SetActive(true);
        shieldActivated = true;

        float duration = 3f;
        float time = 0f;

        image.fillAmount = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            image.fillAmount = time / duration;

            yield return null;
        }

        image.fillAmount = 1f;

        playershield.SetActive(false);
        shieldActivated = false;

        StartCoroutine(ResetShield());
    }
    IEnumerator ResetShield()
    {
        image.enabled=true;
        float cooldown = -13f;
        float time = cooldown;
        resetShield=true;
        image.fillAmount = 1;

        while (time < 0f)
        {
            time += Time.deltaTime;
            image.fillAmount = time / cooldown;
            yield return null;
        }

        image.fillAmount = 0;
        resetShield=false;
        image.enabled=false;
    }
}
