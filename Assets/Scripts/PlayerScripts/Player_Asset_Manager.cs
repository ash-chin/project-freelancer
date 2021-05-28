using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Asset_Manager : MonoBehaviour
{
    /* Responsible for holding onto and updating player data, such as:
     *     - Current fuel, hull, and scrip values
     *     - Bounty completion statuses
     * 
     * MEMBERS:
     *   public float currentHull:
     *           This is the value of the player's current hull integrity
     *   public float currentFuel:
     *           This is the value of player's current remaining fuel
     *   public int scrip:
     *           This is the current value of the player's
     *   public List<bool> bountyBools:
     *           List of bools that tracks player's current completion status on bounties
     * 
     * GUI ELEMENTS OF DATA MEMBERS: inside HUD.cs script
     *   The visual representation of the hull/fuel/scrip data members are updated in the
     *   HUD.cs script, which is attached to the OuterHud object in the heirarchy.
     *   The visual representation of bounty completion statuses are updated in the
     *   BountyNetwork.cs script, attached to the PhotoBountyNetwork item in the heirarchy.
     * 
     * STATIC MEMBERS:
     *   public static GameObject PM:
     *      Reference to the object holding the player script.
     *      Used to access members and methods inside the script.
     *      This member is assigned at start as follows:
     *              if (PM == null)
     *                  {
     *                      PM = GameObject.Find("Player");
     *                  }
     *   
     * 
     * METHODS:
     *   public void HullDamage()
     *   public void VariableDamage(float damage)
     *   public void PayTheMan(int payment)
     *   public void MoneyPwease(int payment)
     *   public void Refuel()
     *   public void RepairHull()
     * 
     * 
     * -----HOW TO ACCESS MEMBERS OR METHODS--------------------------------------------------------
     * To reference this script, first find the GameObject this script is attached to like so:
     *      if (AM == null) { AM = GameObject.Find("AssetManager"); }
     * and then 
     *      AM.GetComponent<Player_Asset_Manager>().[ MEMBER OR METHOD CALL ]
     * EXAMPLE:
     *      hullSlider.value = AM.GetComponent<Player_Asset_Manager>().currentHull;
     * the above example can be found in HUD.cs on line 28
     * ---------------------------------------------------------------------------------------------
     */

    public float currentHull;
    public float currentFuel;
    public int scrip;

    public List<bool> bountyBools;


/*  
    These components are not perserved on load, so they will be missing if player
    loads into another scene. Will instead "find" the player object and access its
    variables and scripts as needed.
    // public Player_Space_Ship_Movement player;
    // public EndScript endingMechanisms; // the script we're going to use to call the end;
*/

    public static Player_Asset_Manager instance;    // the static instance of this, to get preservation
    public static GameObject PM;    // GameObject holding player transformation variables and scripts


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

        if (PM == null)
        {
            PM = GameObject.Find("Player");
        }

        // scripReadout.text = "Scrip: " + scrip.ToString();
        // fuelSlider.value = startingFuel;
        // hullSlider.value = startingHull;
    }


    public void HullDamage()
    {
        //currentHull -= 0.1f * (Mathf.Abs(player.movementCurrentXAxisSpeed) + Mathf.Abs(player.movementCurrentYAxisSpeed) + Mathf.Abs(player.movementCurrentZAxisSpeed));
        currentHull -= 0.1f * (Mathf.Abs(PM.GetComponent<Player_Space_Ship_Movement>().movementCurrentXAxisSpeed) +
            Mathf.Abs(PM.GetComponent<Player_Space_Ship_Movement>().movementCurrentYAxisSpeed) + 
            Mathf.Abs(PM.GetComponent<Player_Space_Ship_Movement>().movementCurrentZAxisSpeed));
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
        //currentFuel -= 0.001f * (Mathf.Abs(player.movementCurrentZAxisSpeed) + Mathf.Abs(player.movementCurrentYAxisSpeed) + Mathf.Abs(player.movementCurrentXAxisSpeed)) * Time.deltaTime;
        
        currentFuel -= 0.001f * PM.GetComponent<Player_Space_Ship_Movement>().boostFuelCost * (Mathf.Abs(PM.GetComponent<Player_Space_Ship_Movement>().movementCurrentZAxisSpeed) + 
            Mathf.Abs(PM.GetComponent<Player_Space_Ship_Movement>().movementCurrentYAxisSpeed) + 
            Mathf.Abs(PM.GetComponent<Player_Space_Ship_Movement>().movementCurrentXAxisSpeed)) * Time.deltaTime;

        // if the fuel slider is at 0 we call the end the of the game and stop attempting to do so by using a bool switch.
        if ((currentFuel <= 0)||(currentHull <= 0))
        {
            //endingMechanisms.EndGame();
            PM.GetComponent<EndScript>().EndGame();
        }
    }
}
