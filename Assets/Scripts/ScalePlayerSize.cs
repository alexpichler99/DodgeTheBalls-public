using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

public class ScalePlayerSize : MonoBehaviour
{
    private float oldHeight = 0;

	// Use this for initialization
	void Start ()
    {
		
	}

    private void FixedUpdate()
    {
    }

    private void LateUpdate()
    {
        float height = GetPlayerHeight();
        // adjust the height of the playerbox
        transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);

        // get the y position
        float y = height / 2;

        // place the box at the calculated height and put it in front of the camera
        // the box is put in front of the camera because if it would be at the position of the camera, the hit would be delayed with the hit the player sees
        // y is ignored when moving the box forward because it would cause the box to be at a heigher/lower position when the user looks up/down
        transform.position = new Vector3(Camera.main.transform.position.x + Camera.main.transform.forward.x * 0.5f,
            Camera.main.transform.position.y - y,Camera.main.transform.position.z + Camera.main.transform.forward.z * 0.5f);
    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The height if the floor has been found, the previous height if the floor hasn't been found</returns>
    private float GetPlayerHeight()
    {
        RaycastHit hitInfo;

        // Raycast to the bottom to get the size of the player
        if (Physics.Raycast(Camera.main.transform.position, Vector3.down, out hitInfo, 5.0f, SpatialMappingManager.Instance.LayerMask))
        {
            oldHeight = hitInfo.distance;
            return hitInfo.distance;
        }
        return oldHeight;
    }
}
