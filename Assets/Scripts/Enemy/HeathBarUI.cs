using UnityEngine;
using UnityEngine.UI;

namespace CDPC.Enemy
{
    public class HeathBarUI : MonoBehaviour
    {
        [Header("Health Bar")]
        public Slider slider;
        public Vector3 offset;
        public Image fill;
        public Gradient gradient;
        Camera cam;
        //public Material Hight;
        //public Material low;
        private void Start()
        {
            cam = Camera.main;
        }

        public void SetHealth(float health, float maxHealth)
        {
            slider.gameObject.SetActive(health < maxHealth);
            slider.value = health;
            slider.maxValue = maxHealth;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            //slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low.color, Hight.color, slider.normalizedValue);
        }

        void LateUpdate()
        {
            slider.transform.position = cam.WorldToScreenPoint(transform.parent.position + offset);
        }
    }
}