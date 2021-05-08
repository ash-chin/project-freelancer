using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AlphaDemoControls : MonoBehaviour
{
    #region Main Control Serialized Fields
    [SerializeField] private float movementAccelerationZAxis;
    [SerializeField] private float movementAccelerationXAxis;
    [SerializeField] private float movementAccelerationYAxis;

    [SerializeField] private float movementMaxZAxisSpeed;
    [SerializeField] private float movementMaxXAxisSpeed;
    [SerializeField] private float movementMaxYAxisSpeed;

    [SerializeField] private float movementZAxis;
    [SerializeField] private float movementXAxis;
    [SerializeField] private float movementYAxis;

    [SerializeField] private float movementCurrentZAxisSpeed;
    [SerializeField] private float movementCurrentXAxisSpeed;
    [SerializeField] private float movementCurrentYAxisSpeed;

    [SerializeField] private float rotationAccelerationZAxis;
    [SerializeField] private float rotationAccelerationXAxis;
    [SerializeField] private float rotationAccelerationYAxis;

    [SerializeField] private float rotationMaxZAxisSpeed;
    [SerializeField] private float rotationMaxXAxisSpeed;
    [SerializeField] private float rotationMaxYAxisSpeed;

    [SerializeField] private float rotationZAxis;
    [SerializeField] private float rotationXAxis;
    [SerializeField] private float rotationYAxis;

    [SerializeField] private float rotationCurrentZAxisSpeed;
    [SerializeField] private float rotationCurrentXAxisSpeed;
    [SerializeField] private float rotationCurrentYAxisSpeed;
    #endregion

    #region Photo Camera Serialized Fields
    [SerializeField] private float photoCam_rotationAccelerationZAxis;
    [SerializeField] private float photoCam_rotationAccelerationXAxis;
    [SerializeField] private float photoCam_rotationAccelerationYAxis;
    [SerializeField] private float photoCam_rotationMaxZAxisSpeed;
    [SerializeField] private float photoCam_rotationMaxXAxisSpeed;
    [SerializeField] private float photoCam_rotationMaxYAxisSpeed;
    #endregion

    #region Camera Essentials
    private bool isPhotoMode = false;
    private Quaternion lastRotation;

    public Camera mainCamera;
    public Camera photographyCamera;
    public Canvas playerGallery;

    public float rotationResetSpeed = 1.0f;

    public CameraScript photocamScript;
    #endregion

    #region UI Essentials
    public Slider hullSlider;
    public Slider fuelSlider;
    public float sceneHull;
    public float sceneFuel;

    public Text scripTracker;
    public int scrip;


    #endregion

    #region Ship Controls Essentials
    private CharacterController controller;
    private Transform playerShipTransform;
    [SerializeField] private InputActionAsset controls;
    #endregion

    private void Start()
    {
        // set our desired bool values
        mainCamera.enabled = true;
        photographyCamera.enabled = false;
        playerGallery.enabled = false;

        // deactivate the Photography Camera Controls
        controls.FindActionMap("photoCam Controls").Disable();

        // set our readout values correctly
        fuelSlider.value = sceneFuel;
        hullSlider.value = sceneHull;
        scripTracker.text = "Script: " + scrip.ToString();
    }

    private void Awake()
    {
        // grab our necessary controller and starting transform
        controller = gameObject.GetComponent<CharacterController>();
        playerShipTransform = gameObject.GetComponent<Transform>();

        #region Player Controller Setup
        controls.FindActionMap("Space Ship Controls").FindAction("Movement Z Axis").performed += cntxt => movementZAxis = cntxt.ReadValue<float>();
        controls.FindActionMap("Space Ship Controls").FindAction("Movement Z Axis").canceled += cntxt => movementZAxis = 0;

        controls.FindActionMap("Space Ship Controls").FindAction("Movement X Axis").performed += cntxt => movementXAxis = cntxt.ReadValue<float>();
        controls.FindActionMap("Space Ship Controls").FindAction("Movement X Axis").canceled += cntxt => movementXAxis = 0;

        controls.FindActionMap("Space Ship Controls").FindAction("Movement Y Axis").performed += cntxt => movementYAxis = cntxt.ReadValue<float>();
        controls.FindActionMap("Space Ship Controls").FindAction("Movement Y Axis").canceled += cntxt => movementYAxis = 0;

        controls.FindActionMap("Space Ship Controls").FindAction("Rotation Z Axis").performed += cntxt => rotationZAxis = cntxt.ReadValue<float>();
        controls.FindActionMap("Space Ship Controls").FindAction("Rotation Z Axis").canceled += cntxt => rotationZAxis = 0;

        controls.FindActionMap("Space Ship Controls").FindAction("Rotation X Axis").performed += cntxt => rotationXAxis = cntxt.ReadValue<float>();
        controls.FindActionMap("Space Ship Controls").FindAction("Rotation X Axis").canceled += cntxt => rotationXAxis = 0;

        controls.FindActionMap("Space Ship Controls").FindAction("Rotation Y Axis").performed += cntxt => rotationYAxis = cntxt.ReadValue<float>();
        controls.FindActionMap("Space Ship Controls").FindAction("Rotation Y Axis").canceled += cntxt => rotationYAxis = 0;

        controls.FindActionMap("Space Ship Controls").FindAction("Camera Switch").performed += cntxt => SwitchCamera();
        #endregion


        #region Photography Camera Controller Setup
        controls.FindActionMap("photoCam Controls").Enable();
        controls.FindActionMap("photoCam Controls").FindAction("Rotation Z Axis").performed += cntxt => rotationZAxis = cntxt.ReadValue<float>();
        controls.FindActionMap("photoCam Controls").FindAction("Rotation Z Axis").canceled += cntxt => rotationZAxis = 0;
        controls.FindActionMap("photoCam Controls").FindAction("Rotation X Axis").performed += cntxt => rotationXAxis = cntxt.ReadValue<float>();
        controls.FindActionMap("photoCam Controls").FindAction("Rotation X Axis").canceled += cntxt => rotationXAxis = 0;
        controls.FindActionMap("photoCam Controls").FindAction("Rotation Y Axis").performed += cntxt => rotationYAxis = cntxt.ReadValue<float>();
        controls.FindActionMap("photoCam Controls").FindAction("Rotation Y Axis").canceled += cntxt => rotationYAxis = 0;
        controls.FindActionMap("photoCam Controls").FindAction("Camera Switch").performed += cntxt => SwitchCamera();
        controls.FindActionMap("photoCam Controls").FindAction("Take Photo").performed += cntxt => photocamScript.takePhoto();
        #endregion
    }

    private void SwitchCamera()
    {
        /*
        * Method Responsible for engaging and disengaging "photoMode"
        * If photoMode is not true, then that means the camera is being switched
        * to engage photoMode. Otherwise, the camera is being switched to
        * exit photoMode.
        */

        // if photoMode is not true, then photoMode is being engaged
        if (!isPhotoMode)
        {
            // save quaternion value to reset back to original rotation later
            lastRotation = transform.rotation;
            mainCamera.enabled = false;
            photographyCamera.enabled = true;
            isPhotoMode = true;

            // moved photoMode actions activation into start()

            controls.FindActionMap("Space Ship Controls").Disable();
            controls.FindActionMap("photoCam Controls").Enable();

        }
        else
        {
            // reset back to original rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lastRotation, Time.time * rotationResetSpeed);
            mainCamera.enabled = true;
            photographyCamera.enabled = false;
            isPhotoMode = false;
            controls.FindActionMap("Space Ship Controls").Enable();
            controls.FindActionMap("photoCam Controls").Disable();
        }
    }

/*    private void TakePhoto()
    {
        photocamScript.verifyBounty();


        //ASH ORIGINAL STUFF
        *//*
         * Literally just sets the playerPhoto object to enabled when the player
         * hits the 'P' key. Then inside the SnapPhoto.cs script (on the playerPhoto object),
         * LateUpdate() checks to see if it's enabled. That is the script responsible for
         * taking photos. Please look at it for context. Thank you for listening to my TED Talk.
         * 
         * P.S
         * Look, I know this is stupid, and I know I could consolidate the code into
         * this script, but also... kinda wanna try to not pile everything into one script.
         *//*

        playerGallery.enabled = true;
        //playerPhoto.SetActive(true);
    }*/

    public void OnCollisionEnter(Collision collision)
    {
        // if the player collides with another object they take damage equal to the speed they were traveling.
        // this isn't sophisticated, so you take as much damage traveling away from the object as you do towards it
        // but we're playing in webGL here, so I don't want to do too much math.
        hullSlider.value -= 0.1f * (Mathf.Abs(movementCurrentYAxisSpeed) + Mathf.Abs(movementCurrentZAxisSpeed) + Mathf.Abs(movementCurrentXAxisSpeed));
    }

    private void OnEnable()
    {
        controls.FindAction("Movement Z Axis").Enable();
        controls.FindAction("Movement X Axis").Enable();
        controls.FindAction("Movement Y Axis").Enable();
        controls.FindAction("Rotation Z Axis").Enable();
        controls.FindAction("Rotation X Axis").Enable();
        controls.FindAction("Rotation Y Axis").Enable();
        controls.FindAction("Camera Switch").Enable();
        controls.FindAction("Take Photo").Enable();
    }

    private void OnDisable()
    {
        controls.FindAction("Movement Z Axis").Disable();
        controls.FindAction("Movement X Axis").Disable();
        controls.FindAction("Movement Y Axis").Disable();
        controls.FindAction("Rotation Z Axis").Disable();
        controls.FindAction("Rotation X Axis").Disable();
        controls.FindAction("Rotation Y Axis").Disable();
        controls.FindAction("Camera Switch").Disable();
        controls.FindAction("Take Photo").Disable();
    }

    private void FixedUpdate()
    {
        if (!isPhotoMode)
        {
            if (fuelSlider.value != 0)
            {
                /*
          * PHOTO MODE DISENGAGED
          * resume normal flight movement
          */

                // controls the movement speed during acceleration and deceleration.
                movementCurrentZAxisSpeed = Mathf.Lerp(movementCurrentZAxisSpeed, movementZAxis * movementMaxZAxisSpeed, movementAccelerationZAxis * Time.deltaTime);
                movementCurrentXAxisSpeed = Mathf.Lerp(movementCurrentXAxisSpeed, movementXAxis * movementMaxXAxisSpeed, movementAccelerationXAxis * Time.deltaTime);
                movementCurrentYAxisSpeed = Mathf.Lerp(movementCurrentYAxisSpeed, movementYAxis * movementMaxYAxisSpeed, movementAccelerationYAxis * Time.deltaTime);

                // controls rotation speed during acceleration and deceleration.
                rotationCurrentZAxisSpeed = Mathf.Lerp(rotationCurrentZAxisSpeed, rotationZAxis * rotationMaxZAxisSpeed, rotationAccelerationZAxis * Time.deltaTime);
                rotationCurrentXAxisSpeed = Mathf.Lerp(rotationCurrentXAxisSpeed, rotationXAxis * rotationMaxXAxisSpeed, rotationAccelerationXAxis * Time.deltaTime);
                rotationCurrentYAxisSpeed = Mathf.Lerp(rotationCurrentYAxisSpeed, rotationYAxis * rotationMaxYAxisSpeed, rotationAccelerationYAxis * Time.deltaTime);

                controller.Move((transform.forward * movementCurrentZAxisSpeed * Time.deltaTime) +
                                (transform.right * movementCurrentXAxisSpeed * Time.deltaTime) +
                                (transform.up * movementCurrentYAxisSpeed * Time.deltaTime));

                playerShipTransform.Rotate(rotationCurrentZAxisSpeed * Time.deltaTime,
                                           rotationCurrentXAxisSpeed * Time.deltaTime,
                                           rotationCurrentYAxisSpeed * Time.deltaTime,
                                           Space.Self);

                //now we decrement the fuel in relation to absolute movement.
                fuelSlider.value -= 0.0001f * (Mathf.Abs(movementCurrentZAxisSpeed) + Mathf.Abs(movementCurrentYAxisSpeed) + Mathf.Abs(movementCurrentXAxisSpeed));
            } 
            else
            {
                movementCurrentZAxisSpeed = Mathf.Lerp(movementCurrentZAxisSpeed, 0, movementAccelerationZAxis * Time.deltaTime);
                movementCurrentXAxisSpeed = Mathf.Lerp(movementCurrentXAxisSpeed, 0, movementAccelerationXAxis * Time.deltaTime);
                movementCurrentYAxisSpeed = Mathf.Lerp(movementCurrentYAxisSpeed, 0, movementAccelerationYAxis * Time.deltaTime);

                rotationCurrentZAxisSpeed = Mathf.Lerp(rotationCurrentZAxisSpeed, 0, rotationAccelerationZAxis * Time.deltaTime);
                rotationCurrentXAxisSpeed = Mathf.Lerp(rotationCurrentXAxisSpeed, 0, rotationAccelerationXAxis * Time.deltaTime);
                rotationCurrentYAxisSpeed = Mathf.Lerp(rotationCurrentYAxisSpeed, 0, rotationAccelerationYAxis * Time.deltaTime);

                controller.Move((transform.forward * movementCurrentZAxisSpeed * Time.deltaTime) 
                    + (transform.right * movementCurrentXAxisSpeed * Time.deltaTime)
                    + (transform.up * movementCurrentYAxisSpeed * Time.deltaTime));
            }
        } 
        else
        {
            /*
             *  PHOTO MODE ENGAGED
             *  Can only rotate on the axes in photo mode, no movement.
             *  Movement code is included so ship can finish whatever movement it was previously doing,
             *  otherwise it will force the ship to stop suddenyl.
             */
            rotationCurrentZAxisSpeed = Mathf.Lerp(rotationCurrentZAxisSpeed, rotationZAxis * photoCam_rotationMaxZAxisSpeed, photoCam_rotationAccelerationZAxis * Time.deltaTime);
            rotationCurrentXAxisSpeed = Mathf.Lerp(rotationCurrentXAxisSpeed, rotationXAxis * photoCam_rotationMaxXAxisSpeed, photoCam_rotationAccelerationXAxis * Time.deltaTime);
            rotationCurrentYAxisSpeed = Mathf.Lerp(rotationCurrentYAxisSpeed, rotationYAxis * photoCam_rotationMaxYAxisSpeed, photoCam_rotationAccelerationYAxis * Time.deltaTime);

            /*
            controller.Move((transform.forward * movementCurrentZAxisSpeed * Time.deltaTime) +
                (transform.right * movementCurrentXAxisSpeed * Time.deltaTime) +
                (transform.up * movementCurrentYAxisSpeed * Time.deltaTime));
            */

            movementCurrentZAxisSpeed = Mathf.Lerp(movementCurrentZAxisSpeed, movementZAxis * movementMaxZAxisSpeed, movementAccelerationZAxis * Time.deltaTime);

            controller.Move((transform.forward * movementCurrentZAxisSpeed * Time.deltaTime));

            playerShipTransform.Rotate(rotationCurrentZAxisSpeed * Time.deltaTime,
                           rotationCurrentXAxisSpeed * Time.deltaTime,
                           rotationCurrentYAxisSpeed * Time.deltaTime,
                           Space.Self);
        }
    }
}
