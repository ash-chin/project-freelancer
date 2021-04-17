using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraScript : MonoBehaviour
{
    public TextMeshProUGUI ObjectName;
    public GameObject Canvas;
    public GameObject blackReticle;
    public GameObject redReticle;

    Camera cam;


    void Start()
    {
        cam = GetComponent<Camera>();
        Canvas.SetActive(false);
        blackReticle.SetActive(false);
        redReticle.SetActive(false);
    }

    void Update()
    {
        if (cam.enabled == true)
        {
            blackReticle.SetActive(true);
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Canvas.SetActive(true);
                // blackReticle.SetActive(true);
                redReticle.SetActive(true);
                ObjectName.text = hit.transform.tag.ToString();
            }
            else
            {
                Canvas.SetActive(false);
                // blackReticle.SetActive(false);
                redReticle.SetActive(false);
            }
        }
        else
        {
            Canvas.SetActive(false);
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