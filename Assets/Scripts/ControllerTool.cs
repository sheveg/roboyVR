﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTool : MonoBehaviour {

    public SteamVR_Controller.Device Controller { get { return m_SteamVRDevice; } }
    public SteamVR_TrackedController ControllerEventListener { get { return m_SteamVRTrackedController; } }

    protected SteamVR_Controller.Device m_SteamVRDevice;
    protected SteamVR_TrackedObject m_SteamVRController;
    protected SteamVR_TrackedController m_SteamVRTrackedController;

    // Use this for initialization
    void Awake()
    {
        // Find Selector tool and the corresponding controller
        m_SteamVRController = GetComponentInParent<SteamVR_TrackedObject>();
        // Find the controller and initialize the values to default
        m_SteamVRDevice = SteamVR_Controller.Input((int)m_SteamVRController.index);
        m_SteamVRDevice.Update();

        m_SteamVRTrackedController = GetComponentInParent<SteamVR_TrackedController>();
    }

    void Update()
    {
        m_SteamVRDevice.Update();
    }

    public void Vibrate()
    {
        StartCoroutine(vibrateController());
    }

    private IEnumerator vibrateController()
    {
        float duration = 0.25f;
        float currDuration = 0f;
        float vibrationStrength = 250f;

        while (currDuration < duration)
        {
            float sinValue = Mathf.Sin(currDuration / duration * Mathf.PI) * vibrationStrength;
            SteamVR_Controller.Input((int)m_SteamVRController.index).TriggerHapticPulse((ushort)sinValue);
            currDuration += Time.fixedDeltaTime;
            yield return Time.fixedDeltaTime;
        }
    }

}