using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

// TODO: Integrate this with the other input system?
public class TeriyakiBottle : MonoBehaviour
{
    private bool lastButtonState = false;
    private List<InputDevice> devicesWithPrimaryButton;

    public ParticleSystem particles;

    public GameObject projectile;
    public Transform spawnPoint;

    AudioSource audioSource;

    public float projectileForce = 50f;

    public float maxFireRate = 0.1f;
    private float fireResetTimer = 0.1f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        devicesWithPrimaryButton = new List<InputDevice>();
    }

    void OnEnable()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices)
            InputDevices_deviceConnected(device);

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        devicesWithPrimaryButton.Clear();
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        bool discardedValue;
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out discardedValue))
        {
            devicesWithPrimaryButton.Add(device); // Add any devices that have a primary button.
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (devicesWithPrimaryButton.Contains(device))
            devicesWithPrimaryButton.Remove(device);
    }

    void Update()
    {
        fireResetTimer -= Time.deltaTime;

        bool tempState = false;
        foreach (var device in devicesWithPrimaryButton)
        {
            bool primaryButtonState = false;
            tempState = device.TryGetFeatureValue(CommonUsages.triggerButton, out primaryButtonState) // did get a value
                        && primaryButtonState // the value we got
                        || tempState; // cumulative result from other controllers
        }

        if (tempState != lastButtonState) // Button state changed since last frame
        {
            FireBottle();
            lastButtonState = tempState;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            FireBottle();
        }
    }

    public void FireBottle()
    {
        if(fireResetTimer < 0f)
        {
            audioSource.Play();
            particles.Play();
            Rigidbody p = Instantiate(projectile, spawnPoint.position, transform.rotation).GetComponent<Rigidbody>();
            p.AddForce(transform.forward * projectileForce);

            fireResetTimer = maxFireRate;
        }
    }
}