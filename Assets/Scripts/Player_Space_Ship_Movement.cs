using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class Player_Space_Ship_Movement : MonoBehaviour
{
    public Camera mainCam;
    public Camera noseCam;

    private CharacterController controller;
    private Transform playerShipTransform;
    [SerializeField] private InputActionAsset controls;

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


    private void Start()
    {
        mainCam.enabled = true;
        noseCam.enabled = false;
    }

    private void Awake()
    {
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
    }

    void SwitchCamera()
    {
        mainCam.enabled = !mainCam.enabled;
        noseCam.enabled = !noseCam.enabled;
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
    }

    // Update is called once per frame
    void Update()
    {

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
}