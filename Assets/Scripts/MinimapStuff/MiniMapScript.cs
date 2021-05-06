/* 
 * If you are having trouble with the follow cam showing the direction arrows that are only for the miniMap
 * you need to create a new layer in your scene and assign those direction arrows to that new layer
 * and finally go to the follow cam inside its culling mask drop down deselect the new layer you made if its not already
 * also be sure to go into the minimap camera and do the opposite so it can see the arrows :D
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
