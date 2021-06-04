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
    public TextMeshProUGUI titleField;
    public TextMeshProUGUI descField;
    public TextMeshProUGUI rewardField;
    public TextMeshProUGUI statusField;
    public int m_index;

    public void fillFields()
    {
        reqField.text = request;
        rewardField.text = "reward: " + reward.ToString();
        updateStatus();
    }

    public void updateStatus()
    {
        /*
         * This is used to update the status text field in the Photo-Bounty Network menu
         * The actual completion status can simply be updated by typing:
         *     bountyVar.isComplete = true/false;
         */
        if (isComplete)
        {
            statusField.text = "Completed";
        }
        else
        {
            statusField.text = "Open Bounty";
        }
    }

    public void OnMouseOver()
    {
        descField.enabled = true;
        titleField.enabled = true;
        statusField.enabled = true;
        rewardField.enabled = true;
        rewardField.text = reward.ToString();
        descField.text = description;
        titleField.text = request;
        if (isComplete)
        {
            statusField.text = "Completed";
        }
        else
        {
            statusField.text = "Open Bounty";
        }
        reqField.color = Color.black;
        // descField.enabled = true;
        // descField.text = description;
        // reqField.color = Color.black;
        //descHolder.SetActive(true);
        //descField.text = description;
    }

    public void OnMouseDown()
    {
        descField.enabled = true;
        titleField.enabled = true;
        statusField.enabled = true;
        rewardField.enabled = true;
        rewardField.text = reward.ToString();
        descField.text = description;
        titleField.text = request;
        if (isComplete)
        {
            statusField.text = "Completed";
        }
        else
        {
            statusField.text = "Open Bounty";
        }
        reqField.color = Color.black;
    }

    public void OnMouseExit()
    {
        descField.text = "";
        descField.enabled = false;
        titleField.enabled = false;
        statusField.enabled = false;
        rewardField.enabled = false;
        reqField.color = Color.white;
        //descHolder.SetActive(false);
        //descField.text = "";
    }


}
