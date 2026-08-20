using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Rendering;

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
    void Start()
    {
        playershield.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ActivateShield());
        }
    }
    public void ShieldButton()
    {
        StartCoroutine(ActivateShield());
        StartCoroutine(ResetShield());
    }
    IEnumerator ActivateShield()
    {
        playershield.SetActive(true);
        shieldActivated=true;
        yield return new WaitForSeconds(3);
        playershield.SetActive(false);
        shieldActivated=false;

        

    }
    IEnumerator ResetShield()
    {
        image.enabled=true;
        float cooldown = -13f;
        float time = cooldown;

        image.fillAmount = 1;

        while (time < 0f)
        {
            time += Time.deltaTime;
            image.fillAmount = time / cooldown;
            yield return null;
        }

        image.fillAmount = 0;
        image.enabled=false;
    }
}
