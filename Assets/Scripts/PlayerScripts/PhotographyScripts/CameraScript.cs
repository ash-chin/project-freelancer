using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraScript : MonoBehaviour
{
    public TextMeshProUGUI ObjectName;    // Textfied to readout object tag
    public GameObject ThisCanvas;    // Canvas the textfield lives in
    public GameObject OuterHud;    // Canvas that hoilds hull, fuel gauges
    public GameObject blackReticle;
    public GameObject redReticle;

    Camera photoCam;


    void Start()
    {
        photoCam = GetComponent<Camera>();
        ThisCanvas.SetActive(false);
        OuterHud.SetActive(true);
        blackReticle.SetActive(false);
        redReticle.SetActive(false);
    }

    void Update()
    {

        // PhotoMode Enabled
        if (photoCam.enabled == true)
        {
            OuterHud.SetActive(false);
            blackReticle.SetActive(true);
            Ray ray = photoCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;

            // if the camera spots an object
            if (Physics.Raycast(ray, out hit))
            {
                ThisCanvas.SetActive(true);
                redReticle.SetActive(true);
                ObjectName.text = hit.transform.tag.ToString();
            }
            else
            {
                ThisCanvas.SetActive(false);
                // blackReticle.SetActive(false);
                redReticle.SetActive(false);
            }
        }
        else    // PhotoMode Disabled
        {
            OuterHud.SetActive(true);
            ThisCanvas.SetActive(false);
            blackReticle.SetActive(false);
            redReticle.SetActive(false);
        }

        /*
            print("I'm looking at " + hit.transform.name);
        else
            print("I'm looking at nothing!");
    }
        */
    }

}