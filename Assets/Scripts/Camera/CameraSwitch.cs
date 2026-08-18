using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]private GameObject followCam;
    [SerializeField]private GameObject constCam;
    [SerializeField]private PlayerMovement playerMovement;

    void Update()
    {
        if (playerMovement.isjumping)
        {
            constCam.SetActive(true);
            followCam.SetActive(false);
        }
        else
        {
            followCam.SetActive(true);
            constCam.SetActive(false);
        }
    }
}
