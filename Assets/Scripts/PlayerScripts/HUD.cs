using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // public GameObject stationButtons;
    public Text scripReadout;
    public Slider fuelSlider;
    public Slider hullSlider;
    public GameObject stationButtons;
    public GameObject miniMap;

    /* AM is the object that holds the Player_Asset_Manager script, which
     * is responsible for tracking fuel, hull, and scrip across scenes.
     * Also holds methods for updating those values
     */
    public static GameObject AM;

    void Start()
    {
        if (AM == null)
        {
            AM = GameObject.Find("AssetManager");
        }
        scripReadout.text = "Scrip: " + AM.GetComponent<Player_Asset_Manager>().scrip.ToString();
        hullSlider.value = AM.GetComponent<Player_Asset_Manager>().currentHull;
        fuelSlider.value = AM.GetComponent<Player_Asset_Manager>().currentFuel;
    }

    public void LeaveStation()
    {
        Time.timeScale = 1f;
        stationButtons.SetActive(false);
        miniMap.SetActive(true);
    }


    public void refuelButton()
    {
        AM.GetComponent<Player_Asset_Manager>().Refuel();
        fuelSlider.value = AM.GetComponent<Player_Asset_Manager>().currentFuel;
        scripReadout.text = "Scrip: " + AM.GetComponent<Player_Asset_Manager>().scrip.ToString();
    }

    public void repairButton()
    {
        AM.GetComponent<Player_Asset_Manager>().RepairHull();
        hullSlider.value = AM.GetComponent<Player_Asset_Manager>().currentHull;
        scripReadout.text = "Scrip: " + AM.GetComponent<Player_Asset_Manager>().scrip.ToString();
    }


    private void FixedUpdate()
    {
        /*
         * Not the most graceful. Did this so we don't have to move where all
         * the methods for updating the hull and fuel values are.
         * The alternative is have the methods here, only update
         * the sliders in the method calls, and then update the current fuel/hull/scrip
         * info inside AM with each method call.
         * This is easier for now.
         */
        hullSlider.value = AM.GetComponent<Player_Asset_Manager>().currentHull;
        fuelSlider.value = AM.GetComponent<Player_Asset_Manager>().currentFuel;
        scripReadout.text = "Scrip: " + AM.GetComponent<Player_Asset_Manager>().scrip.ToString();
    }

}
