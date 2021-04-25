using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class BountyNetwork : MonoBehaviour
{
    public BountyItem[] bounties;
    public TextMeshProUGUI[] titles;

    /*
     * 
     *  Just toying with some stuff don't mind me....
     */

    // Start is called before the first frame update
    void Start()
    {
        //TODO
        int t = 0;

        foreach(BountyItem b in bounties)
        {
            titles[t++].text = b.request;
            b.status = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //TODO
    }
}
