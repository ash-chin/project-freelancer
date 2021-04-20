using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class SnapPhoto : MonoBehaviour
{
    // public Camera photoRender;
    public RenderTexture renderTexture;
    RawImage thePhoto;

    // Start is called before the first frame update
    void Start()
    {
        // this is the object the script is on
        thePhoto = GetComponent<RawImage>();
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

        renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        ScreenCapture.CaptureScreenshotIntoRenderTexture(renderTexture);
        thePhoto.texture = renderTexture;
        thePhoto.enabled = false;

        /*
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
        thePhoto.texture = texture;
        thePhoto.enabled = false;
        */
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (thePhoto.enabled == true)
        {
            StartCoroutine(SnapShot());
        }
    }
}
