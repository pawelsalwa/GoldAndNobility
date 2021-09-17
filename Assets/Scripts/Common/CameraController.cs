using Cinemachine;
using Common.GameInput;
using UnityEngine;

namespace Common
{
    /// <summary> all it does is turn off camera orbitting when gameplay input is turned off too </summary>
    internal class CameraController : MonoBehaviour
    {
        public CinemachineFreeLook vCam;
        
        private void Start() => GameplayInput.OnEnabledChanged += OnInputChanged;

        private void OnDestroy() => GameplayInput.OnEnabledChanged -= OnInputChanged;

        private void OnInputChanged(bool enabled) => vCam.enabled = enabled;

        private void Reset() => vCam = GetComponent<CinemachineFreeLook>();

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
