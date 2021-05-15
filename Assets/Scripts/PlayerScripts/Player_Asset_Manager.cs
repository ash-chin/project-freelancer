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

    // this is the bool we're going to use to track the game ending having been called;
    private bool endCalled;
    // the script we're going to use to call the end;
    public EndScript endingMechanisms;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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

    public void Refuel(int cost, float fuel)
    {
        if (fuelSlider.value < 100 && cost <= scrip)
        {
            fuelSlider.value = Mathf.Min(fuelSlider.value + fuel, 100);
            scrip -= cost;
        }
    }

    public void RepairHull(int cost, float repairValue)
    {
        if (hullSlider.value < 100 && cost <= scrip)
        {
            scrip -= cost;
            hullSlider.value += Mathf.Min(hullSlider.value + repairValue, 100);
        }
    }

    // now we deal with everything that needs to be managed in fixed upadate

    private void FixedUpdate()
    {
        // first we deal with decrmeneting fuel
        fuelSlider.value -= 0.00005f * (Mathf.Abs(player.movementCurrentZAxisSpeed) + Mathf.Abs(player.movementCurrentYAxisSpeed) + Mathf.Abs(player.movementCurrentXAxisSpeed)) * Time.deltaTime;

        // if the fuel slider is at 0 we call the end the of the game and stop attempting to do so by using a bool switch.
        if (fuelSlider.value <= 0)
        {
            if (!endCalled)
            {
                endCalled = true;
                endingMechanisms.EndGame();
            }

            // if the player is out of fuel we slowly pull them to a stop.
            player.movementMaxXAxisSpeed = Mathf.Lerp(player.movementCurrentXAxisSpeed, 0, player.movementAccelerationXAxis * Time.deltaTime);
            player.movementMaxYAxisSpeed = Mathf.Lerp(player.movementCurrentYAxisSpeed, 0, player.movementAccelerationYAxis);
            player.movementMaxZAxisSpeed = Mathf.Lerp(player.movementCurrentZAxisSpeed, 0, player.movementAccelerationZAxis);
        }
    }
}
