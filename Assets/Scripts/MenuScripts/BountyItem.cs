using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class BountyItem : MonoBehaviour
{
    /* Class that can be used to create new Photo Bounty Requests
     * 
     * public string request: name of the request
     * public string description: ... the description....
     * public int reward: monetary reward of the request, AKA the bounty
     * public bool status: whether or not the request has already been fulfilled
     */
   
    //**DATA MEMBERS**//
    public string request; //name of the request
    [TextArea(3, 10)]
    public string description; //...description of the request...
    public string m_tag;    // member used to compare to object tag
    public int reward;    // monetary reward (bounty) of the request
    public bool isComplete;   // whether the request is complete
    
    public TextMeshProUGUI reqField;
    public TextMeshProUGUI descField;
    //public GameObject descHolder;
    public TextMeshProUGUI rewardField;
    public TextMeshProUGUI statusField;

    /*
    private void Start()
    {
        descHolder.SetActive(false);
    }
    */

    public void fillFields()
    {
        reqField.text = request;
        rewardField.text = "reward: " + reward.ToString();
        if (isComplete)
        {
            statusField.text = "Completed";
        } else
        {
            statusField.text = "Open Bounty";
        }
    }

    
    void OnMouseEnter()
    {
        descField.enabled = true;
        descField.text = description;
        //descHolder.SetActive(true);
        //descField.text = description;
    }
    

    public void OnMouseOver()
    {
        descField.enabled = true;
        descField.text = description;
        //descHolder.SetActive(true);
        //descField.text = description;
    }

    public void OnMouseExit()
    {
        descField.text = "";
        descField.enabled = false;
        //descHolder.SetActive(false);
        //descField.text = "";
    }

    
}
