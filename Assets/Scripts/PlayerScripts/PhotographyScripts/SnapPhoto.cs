using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class SnapPhoto : MonoBehaviour
{


    // public RenderTexture renderTexture;
    public TextMeshProUGUI DebugText;
    public GameObject reticleCanvas;
    public GameObject nameCanvas;
    public TextMeshProUGUI objectName;
    public int maxPhotos;
    public RawImage[] photoGallery;
    public RenderTexture[] photoTextures;

    private int numPhotos;
    private int i;

    // variables that will hold specific items pulled from lists
    RawImage thePhoto;
    RenderTexture renderTexture;
    Canvas galleryCanvas;

    // Start is called before the first frame update
    private void Start()
    {
        numPhotos = 0;
        i = 0;
        galleryCanvas = GetComponent<Canvas>();
        galleryCanvas.enabled = false;
        DebugText.enabled = false;

        for(int j = 0; j < maxPhotos; j++)
        {
            photoGallery[j].enabled = false;
        }
    }


    IEnumerator SnapShot()
    {
        /*
         * This is the function that takes the photo. Turns the screenshot into
         * a texture that is then applied to the RawImage object.
         * NOTE! It is very important that thePhoto is disabled immediately after
         * because then this function will keep getting called as a coroutine and
         * it will basically turn into a horrible video feed lol
         * 
         * The texture object is destroyed when the game is turned off via the
         * OnDisable() method inside Player_Space_Ship_Movement.cs.
         * Again, very important unless you like memory leaks.
         */
        yield return new WaitForEndOfFrame();

        if (numPhotos == maxPhotos)
        {
            DebugText.text = "Out of Room!";
            //galleryCanvas.SetActive(false);
            galleryCanvas.enabled = false;
            reticleCanvas.SetActive(true);
            nameCanvas.SetActive(true);
            StopCoroutine(SnapShot());
            yield break;
        }

        thePhoto = photoGallery[i];
        thePhoto.enabled = true;
        renderTexture = photoTextures[i];

        renderTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.DefaultHDR);
        ScreenCapture.CaptureScreenshotIntoRenderTexture(renderTexture);
        thePhoto.texture = renderTexture;
        thePhoto.enabled = false;
        reticleCanvas.SetActive(true);
        nameCanvas.SetActive(true);

        i++;
        numPhotos++;

        galleryCanvas.enabled = false;
        //galleryCanvas.SetActive(false);

        /* 
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
        thePhoto.texture = texture;
        thePhoto.enabled = false;
        */
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // if (thePhoto.enabled == true)
        if (galleryCanvas.enabled == true)
        {
            reticleCanvas.SetActive(false);
            nameCanvas.SetActive(false);
            StartCoroutine(SnapShot());
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
