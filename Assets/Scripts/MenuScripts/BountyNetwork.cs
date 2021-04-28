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

    public void bountyCheck(string objTag)
    {
        foreach(BountyItem b in bounties)
        {
            if (b.isComplete) { continue; }

            if(b.m_tag == objTag)
            {
                b.isComplete = true;
                b.updateStatus();
            }
        }
    }
}
