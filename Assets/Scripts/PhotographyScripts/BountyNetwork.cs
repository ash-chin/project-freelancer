using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;
using UnityEngine.UI;


public class BountyNetwork : MonoBehaviour
{
    public BountyItem[] bounties;
    public Player_Asset_Manager assetManager;
    public GameObject BountyNotification;
    public Text notificationText;

    public static GameObject AM;

    private void Start()
    {
        if (AM == null)
        {
            AM = GameObject.Find("AssetManager");
        }

        BountyNotification.SetActive(false);
        notificationText.text = "";

        foreach (BountyItem b in bounties)
        {
            b.isComplete = AM.GetComponent<Player_Asset_Manager>().bountyBools[b.m_index];
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
                AM.GetComponent<Player_Asset_Manager>().bountyBools[b.m_index] = true;
                b.updateStatus();
                notificationText.text = "PhotoBounty Completed! " + b.request;
                StartCoroutine(showCompleted());
                assetManager.MoneyPwease(b.reward);
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
