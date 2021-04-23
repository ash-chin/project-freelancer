using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /* FEEL FREE TO MESS WITH VALUES - This is for learning (but becareful if other ppl in scene)
         * 
         * Layer12: Inbetween - things that are kinda big, but not big enough to be viewed from 4k distance
         * Layer13: Near - not big enough to be viewed from a far
         * Layer14: Distant and large - obsolete, don't really need because default distance is 4k
         * 
         * Set the camera far distance clipping plane to the max
         * distance we want it to render. 
         * Things that we want to render with a SMALLER distance clipping
         * plane, we put on a specific layer# and then manually set the
         * clipping distance for that layer.
         * Any layer that does not have a clipping distance set will use
         * the default clipping distance.
         * THE LAYER CLIPPING DISTANCE CANNOT BE SET FOR SOMETHING BEYOND
         * THE CAMERA'S DEFAULT FAR CLIPPPING DISTANCE
         * For this reason, we want objects that should only be visibile
         * within <some range smaller than max> on a specific layer,
         * and set that clipping distance with this script.
         * 
         * SO - things that should not be visible from far away should go on layer 13, "near"
         */

        Camera camera = GetComponent<Camera>();
        float[] distances = new float[32];
        distances[13] = 700;    // near, not large enough to be viewed from very far away

        // Something that is big, but not THAT BIG
        // so should be viewable at some distance, but not max distance.
        distances[12] = 2000;

        // distances[14] = 4000;    // far and large

        camera.layerCullDistances = distances;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
