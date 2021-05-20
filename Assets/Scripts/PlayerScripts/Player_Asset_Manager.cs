using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Asset_Manager : MonoBehaviour
{
    // sliders we will use to track the value of hull and 
    public float startingHull;
    public float startingFuel;
    public Slider fuelSlider;
    public Slider hullSlider;
    // this is the canvas for station buttons
    public GameObject stationButtons;
    // the player's controller script. We will use this to slowly decrement the speed
    // when fuel runs out, then call the end script
    public Player_Space_Ship_Movement player;
    // the amount of money the player has, and the associated text readout
    public int scrip;
    public Text scripReadout;

    // the script we're going to use to call the end;
    public EndScript endingMechanisms;

    // the static instance of this, to get preservation
    public static Player_Asset_Manager instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);


        scripReadout.text = "Scrip: " + scrip.ToString();
        fuelSlider.value = startingFuel;
        hullSlider.value = startingHull;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Refueling Depot")
        {
            Time.timeScale = 0.1f;
            stationButtons.SetActive(true);
        }
    }

    public void LeaveStation()
    {
        Time.timeScale = 1f;
        stationButtons.SetActive(false);
    }

    public void HullDamage()
    {
        hullSlider.value -= 0.1f * (Mathf.Abs(player.movementCurrentXAxisSpeed) + Mathf.Abs(player.movementCurrentYAxisSpeed) + Mathf.Abs(player.movementCurrentZAxisSpeed));
    }

    public void VariableDamage(float damage)
    {
        hullSlider.value -= damage;
    }

    public void PayTheMan(int payment)
    {
        scrip -= payment;
        scripReadout.text = "Scrip: " + scrip.ToString();
    }

    public void MoneyPwease(int payment)
    {
        scrip += payment;
        scripReadout.text = "Scrip: " + scrip.ToString();
    }

    public void Refuel()
    {
        if (fuelSlider.value < 100 && 50 <= scrip)
        {
            fuelSlider.value = Mathf.Min(fuelSlider.value + 40, 100);
            scrip -= 50;
        }
    }

    public void RepairHull()
    {
        if (hullSlider.value < 100 && scrip >= 50)
        {
            scrip -= 50;
            hullSlider.value += Mathf.Min(hullSlider.value + 30, 100);
        }
    }

    // now we deal with everything that needs to be managed in fixed upadate

    private void FixedUpdate()
    {
        // first we deal with decrmeneting fuel
        fuelSlider.value -= 0.001f * (Mathf.Abs(player.movementCurrentZAxisSpeed) + Mathf.Abs(player.movementCurrentYAxisSpeed) + Mathf.Abs(player.movementCurrentXAxisSpeed)) * Time.deltaTime;

        // if the fuel slider is at 0 we call the end the of the game and stop attempting to do so by using a bool switch.
        if (fuelSlider.value <= 0)
        {
            endingMechanisms.EndGame();
        }
    }
}
