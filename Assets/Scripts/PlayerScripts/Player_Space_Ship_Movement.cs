using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CharacterController))]

public class Player_Space_Ship_Movement : MonoBehaviour
{
    // the boost bool
    // literally just adding a line because git says no changes for some reason...
    private bool boosted = false;
    public float boostQuantity;
    public float boostModifier = 1;
    public float boostFuelCost;

    // sound source
    public PlayerAudio playerAudioSource;

    // Camera assets
    public Camera mainCam;
    public Camera photoCam;
    public CameraScript photocamScript;

    // these are the buttons for refueling and whatnot
    // the timer is to ensure that the player doesn't get caught in the cylce endlessly
    public GameObject stationButtons;
    public GameObject miniMap;

    // player controls, and input assets
    private CharacterController controller;
    private Transform playerShipTransform;
    [SerializeField] private InputActionAsset controls;

    #region Serialized Fields
    // serialized fields for the input of controls
    [SerializeField] public float movementAccelerationZAxis;
    [SerializeField] public float movementAccelerationXAxis;
    [SerializeField] public float movementAccelerationYAxis;

    [SerializeField] public float movementMaxZAxisSpeed;
    [SerializeField] public float movementMaxXAxisSpeed;
    [SerializeField] public float movementMaxYAxisSpeed;

    [SerializeField] private float movementZAxis;
    [SerializeField] private float movementXAxis;
    [SerializeField] private float movementYAxis;

    [SerializeField] public float movementCurrentZAxisSpeed;
    [SerializeField] public float movementCurrentXAxisSpeed;
    [SerializeField] public float movementCurrentYAxisSpeed;

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

    // photoCam movement restricted to rotation w/ seperate accel & speeds

    #region Camera Serialized Fields
    [SerializeField] private float photoCam_rotationAccelerationZAxis;
    [SerializeField] private float photoCam_rotationAccelerationXAxis;
    [SerializeField] private float photoCam_rotationAccelerationYAxis;
    [SerializeField] private float photoCam_rotationMaxZAxisSpeed;
    [SerializeField] private float photoCam_rotationMaxXAxisSpeed;
    [SerializeField] private float photoCam_rotationMaxYAxisSpeed;
    public float rotationResetSpeed = 1.0f;
    #endregion

    // is Player in photo mode?
    bool photoMode;
    Quaternion lastRotation;
    

    private void Start()
    {
        // game does not start in photoMode
        mainCam.enabled = true;
        photoCam.enabled = false;
        photoMode = false;
        //playerGallery.enabled = false;
        //playerPhoto.SetActive(false);
        controls.FindActionMap("photoCam Controls").Disable();
    }

    private void Awake()
    {
        /* 
         * This if else block says if there is not an instance of the audio manager in the scene
         *  then instantiate one otherwise there already is one so delete this one
         */

        controller = gameObject.GetComponent<CharacterController>();

        playerShipTransform = gameObject.GetComponent<Transform>();

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
        controls.FindActionMap("Space Ship Controls").FindAction("Boost").performed += cntxt => Boost();



        controls.FindActionMap("photoCam Controls").Enable();
        controls.FindActionMap("photoCam Controls").FindAction("Rotation Z Axis").performed += cntxt => rotationZAxis = cntxt.ReadValue<float>();
        controls.FindActionMap("photoCam Controls").FindAction("Rotation Z Axis").canceled += cntxt => rotationZAxis = 0;
        controls.FindActionMap("photoCam Controls").FindAction("Rotation X Axis").performed += cntxt => rotationXAxis = cntxt.ReadValue<float>();
        controls.FindActionMap("photoCam Controls").FindAction("Rotation X Axis").canceled += cntxt => rotationXAxis = 0;
        controls.FindActionMap("photoCam Controls").FindAction("Rotation Y Axis").performed += cntxt => rotationYAxis = cntxt.ReadValue<float>();
        controls.FindActionMap("photoCam Controls").FindAction("Rotation Y Axis").canceled += cntxt => rotationYAxis = 0;
        controls.FindActionMap("photoCam Controls").FindAction("Camera Switch").performed += cntxt => SwitchCamera();
        controls.FindActionMap("photoCam Controls").FindAction("Take Photo").performed += cntxt => photocamScript.takePhoto();

    }


    void SwitchCamera()
    {
        /*
         * Method Responsible for engaging and disengaging "photoMode"
         * If photoMode is not true, then that means the camera is being switched
         * to engage photoMode. Otherwise, the camera is being switched to
         * exit photoMode.
         */

        // if photoMode is not true, then photoMode is being engaged
        if (!photoMode)
        {
            // save quaternion value to reset back to original rotation later
            lastRotation = transform.rotation;
            mainCam.enabled = false;
            photoCam.enabled = true;
            photoMode = true;

            // moved photoMode actions activation into start()

            controls.FindActionMap("Space Ship Controls").Disable();
            controls.FindActionMap("photoCam Controls").Enable();

        }
        else
        {
            // reset back to original rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lastRotation, Time.time * rotationResetSpeed);
            mainCam.enabled = true;
            photoCam.enabled = false;
            photoMode = false;
            controls.FindActionMap("Space Ship Controls").Enable();
            controls.FindActionMap("photoCam Controls").Disable();
        }
        
    }

    private void Boost()
    {
        if (!boosted)
        {
            boosted = true;
            movementMaxZAxisSpeed += boostQuantity;
            boostModifier = boostFuelCost;
        }
        else
        {
            boosted = false;
            movementMaxZAxisSpeed -= boostQuantity;
            boostModifier = 1;
        }
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
        controls.FindAction("Boost").Enable();
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

        // this will not cause a memory leak... You're welcome!
        // moved to OnDisable function in SnapPhoto.cs script

    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (!photoMode)
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

            movementCurrentZAxisSpeed = Mathf.Lerp(movementCurrentZAxisSpeed, movementZAxis * movementMaxZAxisSpeed, movementAccelerationZAxis * Time.deltaTime);

            controller.Move((transform.forward * movementCurrentZAxisSpeed * Time.deltaTime));

            playerShipTransform.Rotate(rotationCurrentZAxisSpeed * Time.deltaTime,
                           rotationCurrentXAxisSpeed * Time.deltaTime,
                           rotationCurrentYAxisSpeed * Time.deltaTime,
                           Space.Self);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Refueling Depot")
        {
            stationButtons.SetActive(true);
            miniMap.SetActive(false);
            Time.timeScale = 0.0f;
        }
    }

    // That's it. That's the Tweet.
}

