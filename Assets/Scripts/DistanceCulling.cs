using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCulling : MonoBehaviour
{
    private void Start()
    {
        Camera cam = GetComponent<Camera>();
        float[] distances = new float[32];
        distances[12] = 4000;
        distances[13] = 2000;
        distances[14] = 8000;
        cam.layerCullDistances = distances;
    }
}
