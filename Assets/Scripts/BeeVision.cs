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
    public float _fadeTime = 1.0f;
    private bool _isFading = false;

    private float _lastActivationTime = 0.0f;
    private float _activationCooldown = 6.0f;

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
        if (targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool triggerValue) && triggerValue && !_isFading && Time.time - _lastActivationTime >= _activationCooldown)
        {
            Debug.Log("Y button is pressed.");

            _lastActivationTime = Time.time;

            StartCoroutine(FadeToBeeVision());
        }
    }

    IEnumerator FadeToBeeVision()
    {
        _isFading = true;
        float fadeInTime = 0.1f;  // Duration of the fade-in effect
        float fadeOutDelay = 1.0f; // Delay before starting the fade-out effect
        float fadeOutTime = 2.0f;  // Duration of the fade-out effect

        // Fade In (Instant Appearance)
        float elapsedTime = 0f;
        Color defaultColor = defaultVision.backgroundColor;
        Color targetColor = BeeVisionCam.backgroundColor;

        while (elapsedTime < fadeInTime)
        {
            defaultVision.backgroundColor = Color.Lerp(defaultColor, targetColor, elapsedTime / fadeInTime);
            BeeVisionCam.backgroundColor = Color.Lerp(targetColor, defaultColor, elapsedTime / fadeInTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        defaultVision.backgroundColor = targetColor;
        BeeVisionCam.backgroundColor = defaultColor;

        BeeVisionCam.enabled = true;
        yield return new WaitForSeconds(fadeOutDelay);

        // Fade Out (Slow and Smooth)
        elapsedTime = 0f;

        while (elapsedTime < fadeOutTime)
        {
            defaultVision.backgroundColor = Color.Lerp(targetColor, defaultColor, elapsedTime / fadeOutTime);
            BeeVisionCam.backgroundColor = Color.Lerp(defaultColor, targetColor, elapsedTime / fadeOutTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        defaultVision.backgroundColor = defaultColor;
        BeeVisionCam.backgroundColor = targetColor;

        BeeVisionCam.enabled = false;
        _isFading = false;
    }

}
