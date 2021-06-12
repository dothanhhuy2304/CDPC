using UnityEngine;

namespace CDPC.View
{
    public class PerformentSetting : MonoBehaviour
    {
        public int frameRate = 50;
        void Awake()
        {
            Application.targetFrameRate = frameRate;
        }
    }
}