using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraScript : MonoBehaviour
{
    public TextMeshProUGUI ObjectName;
    public GameObject Canvas;

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        Canvas.SetActive(false);
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Canvas.SetActive(true);
            //ObjectName.text = hit.transform.name.ToString();
            ObjectName.text = hit.transform.tag.ToString();
        }
        else
        {
            Canvas.SetActive(false);
        }
        /*
            print("I'm looking at " + hit.transform.name);
        else
            print("I'm looking at nothing!");
    }
        */
    }
}