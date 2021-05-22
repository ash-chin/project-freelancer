using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    public static GalleryManager instance;
    public Canvas gallery;
    public int maxPhotos;
    // photo count and index are updated by CameraScript
    public int photoCount;
    public int index;
    public RawImage[] photoGallery;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        gallery.enabled = false;
    }

}
