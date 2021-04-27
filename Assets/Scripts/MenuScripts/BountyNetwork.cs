using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UI;


public class BountyNetwork : MonoBehaviour
{
    public BountyItem[] bounties;
    // public TextMeshProUGUI[] titles;
    //public GameObject descriptionField;

    private void Start()
    {
        //descriptionField.SetActive(false);

        foreach (BountyItem b in bounties)
        {
            b.fillFields();
        }
    }

    /*
    void Start()
    {
        //TODO
        int t = 0;

        foreach(BountyItem b in bounties)
        {
            titles[t++].text = b.request;
            b.isComplete = false;
        }

    }
    */

}
