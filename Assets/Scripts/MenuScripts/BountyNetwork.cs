using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;
using UnityEngine.UI;


public class BountyNetwork : MonoBehaviour
{
    public BountyItem[] bounties;
    public Player_Space_Ship_Movement freelancer;
    public GameObject BountyNotification;
    public Text notificationText;

    private void Start()
    {
        //descriptionField.SetActive(false);
        BountyNotification.SetActive(false);
        notificationText.text = "";
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
                notificationText.text = "PhotoBounty Completed! " + b.request;
                StartCoroutine(showCompleted());
                freelancer.MoneyPweaaaaase(b.reward);
            }
        }
    }

    IEnumerator showCompleted()
    {
        //stuff
        BountyNotification.SetActive(true);
        yield return new WaitForSeconds(5);
        BountyNotification.SetActive(false);

    }
}
