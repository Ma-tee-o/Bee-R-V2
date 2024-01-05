using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputDeviceCharacteristics inputDeviceCharacteristics;
    public InputDevice targetDevice;
    public Transform head;
    public float spawnDistance = 2;

    private float _lastActivationTime = 0.0f;
    private float _activationCooldown = 0.6f;

    void Start()
    {
        menu.SetActive(false);
    }

    void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        foreach (var item in devices)
        {
            targetDevice = item;
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool triggerValue) && triggerValue && Time.time - _lastActivationTime >= _activationCooldown)
        {
            _lastActivationTime = Time.time;
            Debug.Log("Menu button pressed.");
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }
}
