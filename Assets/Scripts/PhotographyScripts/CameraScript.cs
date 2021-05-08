using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraScript : MonoBehaviour
{
    public TextMeshProUGUI ObjectName;    // Textfield to readout object tag
    public GameObject ObjectReadOut;    // Canvas the textfield lives in
    public GameObject OuterHud;    // Canvas that hoilds hull, fuel gauges
    public GameObject reticleCanvas;
    public GameObject blackReticle;
    public GameObject redReticle;
    public BountyNetwork bountyNetwork;
    // new members
    public GameObject tutorialCanvas;
    public Player_Space_Ship_Movement freelancer;
    public PlayerAudio playerAudioSource;
    public Canvas galleryCanvas;
    public int maxPhotos;
    public RawImage[] photoGallery;
    public RenderTexture[] photoTextures;

    private int numPhotos;
    private int i;

    Camera photoCam;
    string objTag;
    // new variables
    RawImage thePhoto;
    RenderTexture renderTexture;

    void Start()
    {
        photoCam = GetComponent<Camera>();
        ObjectReadOut.SetActive(false);
        OuterHud.SetActive(true);
        blackReticle.SetActive(false);
        redReticle.SetActive(false);

        numPhotos = 0;
        galleryCanvas.enabled = false;
        for (int j = 0; j < maxPhotos; j++)
        {
            photoGallery[j].enabled = false;
        }
    }


    public void takePhoto()
    {
        playerAudioSource.ShutterNoise();
        if (numPhotos == maxPhotos)
        {
            //galleryCanvas.enabled = false;
            reticleCanvas.SetActive(true);
            ObjectReadOut.SetActive(true);
            return;
        }

        if (tutorialCanvas)
        {
            tutorialCanvas.SetActive(false);
        }

        StartCoroutine(capturePhoto());
        if (objTag != "Untagged")
        {
            bountyNetwork.bountyCheck(objTag);
        }
        
    }

/*    public void verifyBounty()
    {
        *//*
         * Lol this is absurd, I am absurd
         *//*
        bountyNetwork.bountyCheck(objTag);
    }*/

    IEnumerator capturePhoto()
    {
        //blah
        yield return new WaitForEndOfFrame();
        tutorialCanvas.SetActive(false);
        thePhoto = photoGallery[i];
        thePhoto.enabled = true;
        renderTexture = photoTextures[i];

        renderTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.DefaultHDR);
        ScreenCapture.CaptureScreenshotIntoRenderTexture(renderTexture);
        thePhoto.texture = renderTexture;
        thePhoto.enabled = false;
        reticleCanvas.SetActive(true);
        ObjectReadOut.SetActive(true);

        i++;
        numPhotos++;
        //galleryCanvas.enabled = false;
        tutorialCanvas.SetActive(true);
        StopCoroutine(capturePhoto());
    }

    public void Update()
    {

        // PhotoMode Enabled
        if (photoCam.enabled == true)
        {
            OuterHud.SetActive(false);
            blackReticle.SetActive(true);
            Ray ray = photoCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            // Ray ray = new Ray(transform.position, transform.forward);
            // Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;

            // if the camera spots an object
            if (Physics.Raycast(ray, out hit, 8000))
            {
                ObjectReadOut.SetActive(true);
                redReticle.SetActive(true);
                ObjectName.text = hit.transform.tag.ToString();
                objTag = hit.transform.tag.ToString();
            }
            else
            {
                ObjectReadOut.SetActive(false);
                // blackReticle.SetActive(false);
                redReticle.SetActive(false);
            }
        }
        else    // PhotoMode Disabled
        {
            OuterHud.SetActive(true);
            ObjectReadOut.SetActive(false);
            blackReticle.SetActive(false);
            redReticle.SetActive(false);
        }
    }

    private void OnDisable()
    {
        // to do
        // Object.Destroy(playerPhoto.texture);
        for (i = 0; i < numPhotos; i++)
        {
            //stuff
            thePhoto = photoGallery[i];
            Object.Destroy(thePhoto.texture);
        }
    }

}