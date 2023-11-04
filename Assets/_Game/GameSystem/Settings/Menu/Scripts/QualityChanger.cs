using UnityEngine;
using TMPro;

namespace LOK1game
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class QualityChanger : MonoBehaviour
    {
        private TMP_Dropdown _dropdown;

        private void Start()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
        }

        public void SetQuality(int index)
        {
            QualitySettings.SetQualityLevel(index);
        }
    }
}