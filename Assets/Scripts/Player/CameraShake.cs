//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Cinemachine;

//namespace CDPC.Player
//{
//    public class CameraShake : MonoBehaviour
//    {
//        public static CameraShake Instance { get; private set; }

//        private CinemachineVirtualCamera cinemachineVirtualCamera;
//        private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
//        public float shakeTime;

//        private void Awake()
//        {
//            Instance = this;
//            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
//        }

//        public void Shake(float intensity, float time)
//        {
//            cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
//            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
//            shakeTime = time;
//        }

//        private void Update()
//        {
//            if (shakeTime > 0)
//            {
//                shakeTime -= Time.deltaTime;
//                if (shakeTime <= 0)
//                {
//                    cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
//                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
//                }
//            }
//        }
//    }
//}