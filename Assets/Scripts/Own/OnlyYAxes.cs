using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.Events;


public class OnlyYAxes : MonoBehaviour
{
    public XRNode inputSource;
    public float speed = 1;

    private XROrigin rig;
    private Vector2 inputAxis;
    private CharacterController character;
    private bool isFirstInputProcessed = false;

    public UnityEvent OnFirstInput;

    public bool InputInvoked = false;

    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        if (device.isValid && !InputInvoked && Mathf.Abs(inputAxis.y) > 0.01f)
        {
            isFirstInputProcessed = true;
            OnFirstInput.Invoke();
            InputInvoked = true;
        }
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);


        // Check if the first input has been processed

    }


    private void FixedUpdate()
    {
        // Set the height change based on the thumbstick's Y-axis input
        float heightChange = inputAxis.y * speed * Time.fixedDeltaTime;

        // Update the player's height (Y-axis) only
        Vector3 position = transform.position;
        position.y += heightChange;
        transform.position = position;
    }


}