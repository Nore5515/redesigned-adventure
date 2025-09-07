using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace BlurShader
{
    public class BlurRenderPass : ScriptableRenderPass
    {
        private Material material;
        private BlurSettings blurSettings;
        
        private RenderTargetIdentifier source;
        private RTHandle blurTex;
        private int blurTexID;

        [Obsolete("Obsolete")]
        public bool Setup(ScriptableRenderer renderer)
        {
            source = renderer.cameraColorTarget;
            blurSettings = VolumeManager.instance.stack.GetComponent<BlurSettings>();
            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

            if (blurSettings is not null && blurSettings.IsActive())
            {
                material = new Material(Shader.Find("PostProcessing/Blur"));
                return true;
            }

            return false;
        }
    }
}
