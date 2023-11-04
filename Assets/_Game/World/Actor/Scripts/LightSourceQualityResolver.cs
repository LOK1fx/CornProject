using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace LOK1game.Rendering
{
    [RequireComponent(typeof(HDAdditionalLightData))]
    public class LightSourceQualityResolver : MonoBehaviour
    {
        private const int QUALITY_VERY_LOW = 0;
        private const int QUALITY_LOW = 1;
        private const int QUALITY_MEDIUM = 2;
        private const int QUALITY_ULTRA = 3;

        private const int VERY_LOW_RES = 256;
        private const int LOW_RES = 512;
        private const int MEDIUM_RES = 512;
        private const int ULTRA_RES = 1024;

        private HDAdditionalLightData _light;

        private void Start()
        {
            _light = GetComponent<HDAdditionalLightData>();

            switch (QualitySettings.GetQualityLevel())
            {
                case QUALITY_VERY_LOW:
                    _light.SetShadowResolution(VERY_LOW_RES);
                    break;
                case QUALITY_LOW:
                    _light.SetShadowResolution(LOW_RES);
                    break;
                case QUALITY_MEDIUM:
                    _light.SetShadowResolution(MEDIUM_RES);
                    break;
                case QUALITY_ULTRA:
                    _light.SetShadowResolution(ULTRA_RES);
                    break;
            }
        }
    }
}