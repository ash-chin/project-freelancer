using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Asset_Manager : MonoBehaviour
{

    /* OLD STUFF
    public float startingHull;
    public float startingFuel;
    public Slider fuelSlider;
    public Slider hullSlider;
    */


    // GUI elements updated in HUD script attached to OutHud
    public float currentHull;
    public float currentFuel;
    public int scrip;    // amount of money player has

    public List<bool> bountyBools;    // track bounty completion status

    // Use player controller scrip to slowly decrement the speed
    // when fuel runs out, then call the end script
    public Player_Space_Ship_Movement player;

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

        // scripReadout.text = "Scrip: " + scrip.ToString();
        // fuelSlider.value = startingFuel;
        // hullSlider.value = startingHull;
    }


    public void HullDamage()
    {
        currentHull -= 0.1f * (Mathf.Abs(player.movementCurrentXAxisSpeed) + Mathf.Abs(player.movementCurrentYAxisSpeed) + Mathf.Abs(player.movementCurrentZAxisSpeed));
    }

    public void VariableDamage(float damage)
    {
        currentHull -= damage;
    }

    public void PayTheMan(int payment)
    {
        scrip -= payment;
        // scripReadout.text = "Scrip: " + scrip.ToString();
    }

    public void MoneyPwease(int payment)
    {
        scrip += payment;
        // scripReadout.text = "Scrip: " + scrip.ToString();
    }

    public void Refuel()
    {
        if (currentFuel < 100 && 50 <= scrip)
        {
            currentFuel = Mathf.Min(currentFuel + 40, 100);
            scrip -= 50;
        }
    }

    public void RepairHull()
    {
        if (currentHull < 100 && scrip >= 50)
        {
            scrip -= 50;
            currentHull += Mathf.Min(currentHull + 30, 100);
        }
    }

    // now we deal with everything that needs to be managed in fixed upadate

    private void FixedUpdate()
    {
        // first we deal with decrmeneting fuel
        currentFuel -= 0.001f * (Mathf.Abs(player.movementCurrentZAxisSpeed) + Mathf.Abs(player.movementCurrentYAxisSpeed) + Mathf.Abs(player.movementCurrentXAxisSpeed)) * Time.deltaTime;

        // if the fuel slider is at 0 we call the end the of the game and stop attempting to do so by using a bool switch.
        if (currentFuel <= 0)
        {
            endingMechanisms.EndGame();
        }
    }
}
