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


}
