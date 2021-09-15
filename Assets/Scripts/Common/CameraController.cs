using System;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

namespace Common
{
    internal class CameraController : MonoBehaviour
    {
        public CinemachineFreeLook vCam;
        
        // [SerializeField] private Vector2 speeds;
        // public string xAxisName = "MouseX";
        // public string yAxisName = "MouseY";

        private void Start()
        {
            PauseGameManager.OnPaused += OnPaused;
            PauseGameManager.OnResumed += OnResumed;
        }

        private void OnDestroy()
        {
            PauseGameManager.OnPaused -= OnPaused;
            PauseGameManager.OnResumed -= OnResumed;
        }

        private void OnPaused()
        {
            vCam.enabled = false;
        }

        private void OnResumed()
        {
            vCam.enabled = true;
        }

        private void Reset()
        {
            vCam = GetComponent<CinemachineFreeLook>();
        }

        // [Button] private void SetupSalwaXd() // utility for new projects, sets up stuff
        // {
        //     speeds = new Vector2(-2.5f, -0.04f);
        //     vCam.m_XAxis.m_MaxSpeed = speeds.x;
        //     vCam.m_YAxis.m_MaxSpeed = speeds.y;
        //     vCam.m_XAxis.m_SpeedMode = vCam.m_YAxis.m_SpeedMode = AxisState.SpeedMode.InputValueGain;
        //     vCam.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
        // }

    }
}
