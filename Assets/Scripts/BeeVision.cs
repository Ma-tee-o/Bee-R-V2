using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class BeeVision : MonoBehaviour
{
    public Camera defaultVision;
    public Camera BeeVisionCam;
    public InputDeviceCharacteristics inputDeviceCharacteristics;
    
    public InputDevice targetDevice;
    

    // Start is called before the first frame update
    void Start()
    {
        BeeVisionCam.enabled = false;
    }


    void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
            Debug.Log(devices[0].name + devices[0].characteristics);
            targetDevice = devices[0];
        }
    }

    private void FixedUpdate()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool triggerValue) && triggerValue)
        {
            Debug.Log("Y button is pressed.");

            defaultVision.enabled = !defaultVision.enabled;
            BeeVisionCam.enabled = !BeeVisionCam.enabled;

        }
    }
}
    
    

      
    