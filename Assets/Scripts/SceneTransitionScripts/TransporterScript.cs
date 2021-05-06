using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransporterScript : MonoBehaviour
{
    // it's kinda dumb how easy this actually is
    public string targetScene;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
