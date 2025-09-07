using UnityEngine;
using UnityEngine.Rendering;

namespace BlurShader
{
    [System.Serializable, VolumeComponentMenu("Blur")]
    public class BlurSettings : VolumeComponent, IPostProcessComponent
    {
        [Tooltip("Standard Deviation (Spread of blur)")]
        public ClampedFloatParameter strength = new ClampedFloatParameter(0.0f, 0.0f, 15.0f);

        public bool IsActive()
        {
            // active
            return (strength.value > 0.0f) && active;
        }
    }
}
