using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine;

public class Placing : MonoBehaviour, IInputClickHandler
{
    private BoxCollider boxCollider;

    private bool IsPlacing = true;

    private const float raycastDistance = 30f;

    private int oldLayer;

    // Use this for initialization
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlacing)
            Move();
    }

    private void Move()
    {
        int layer = SpatialMappingManager.Instance.LayerMask;

        // Get the largest scale (z axis is ignored)
        var scale = transform.localScale.x > transform.localScale.y ? transform.localScale.x : transform.localScale.y;

        // Use the scale to calculate the distance needed to fit the object (https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html)
        var distance = scale / Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);

        // Move this object in front of the camera, if no surface is hit
        this.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;

        // Rotate this object's parent object to face the user.
        Quaternion toQuat = Camera.main.transform.localRotation;
        toQuat.x = 0;
        toQuat.z = 0;
        this.transform.rotation = toQuat;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        IsPlacing = false;

        // Enable the CreateBallScript after the user has chosen a position
        var ballScript = GetComponent<CreateBallScript>();
        if (ballScript != null)
            ballScript.enabled = true;

        boxCollider.enabled = false;
    }
}