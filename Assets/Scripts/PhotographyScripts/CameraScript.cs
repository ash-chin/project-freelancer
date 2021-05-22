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
    public BountyNetwork bountyNetwork;    // BountyManager
    public Player_Space_Ship_Movement freelancer;
    public PlayerAudio playerAudioSource;
    public Canvas previewCanvas;    // Preview Pic
    public RawImage previewPic;

    // photos and photo data (max, count, current index) stored in GalleryManager
    public GalleryManager GM;

    Camera photoCam;    // the photography camera
    string objTag;    // used to verify if bounty was photographed

    RawImage thePhoto;    // temp hold RawImage objects
    RenderTexture tempRender;    // temp to render camera into for photos
    Texture2D photoTexture;    // this plus thePhoto make up the actual photos

    void Start()
    {
        //BountyNotification.SetActive(false);
        photoCam = GetComponent<Camera>();
        ObjectReadOut.SetActive(false);
        OuterHud.SetActive(true);
        blackReticle.SetActive(false);
        redReticle.SetActive(false);

        // numPhotos = 0;
        previewCanvas.enabled = false;
    }


    public void takePhoto()
    {
        playerAudioSource.ShutterNoise();
        if (GM.index == GM.maxPhotos)
        {
            GM.index = 0; // start saving over old pictures
            //return;
        }

        reticleCanvas.SetActive(false);
        // creating temp texture for photo
        int sqr = 1024;
        int width = Screen.width;
        int height = Screen.height;
        tempRender = new RenderTexture(width, height, 24);

        // temporarily render camera to this texture
        photoCam.targetTexture = tempRender;
        photoCam.Render();
        RenderTexture.active = tempRender;

        // grab empty photo from gallery
        thePhoto = GM.photoGallery[GM.index];

        // read pixels from screen into photoTexture
        // scales down size and flips so not upside down
        photoTexture = new Texture2D(sqr, sqr, TextureFormat.RGB24, false);
        photoTexture.ReadPixels(new Rect(width / 2 - sqr / 2, height / 2 - sqr / 2, sqr, sqr), 0, 0);
        photoTexture.Apply();

        RenderTexture.active = null;    // helps avoid errors
        photoCam.targetTexture = null;
        
        thePhoto.texture = photoTexture;    // "save" pic to gallery
        reticleCanvas.SetActive(true);    // turn reticle back on

        // increase photo count
        GM.index++;
        GM.photoCount++;

        // show the player a preview of the photo they just took
        previewPic.texture = photoTexture;
        StartCoroutine(showPhoto());

        // check if a bounty was completed
        if (objTag != "Untagged")
        {
            bountyNetwork.bountyCheck(objTag);
        }
        
    }

    IEnumerator showPhoto()
    {
        //stuff
        previewCanvas.enabled = true;
        yield return new WaitForSeconds(2);
        previewCanvas.enabled = false;
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
                //ObjectReadOut.SetActive(true);
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
        Destroy(tempRender);

        if(GM.maxPhotos < GM.photoCount)
        {
            GM.photoCount = GM.maxPhotos; // if we took more pics than max
        }

        for (int i = 0; i < GM.photoCount; i++)
        {
            //stuff
            thePhoto = GM.photoGallery[i];
            Object.Destroy(thePhoto.texture);
        }
    }

}