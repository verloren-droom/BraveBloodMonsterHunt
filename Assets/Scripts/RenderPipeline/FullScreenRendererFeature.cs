using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace BraveBloodMonsterHunt.Rendering
{
    public class FullScreenRendererFeature : ScriptableRendererFeature
    {
        private FullScreenRenderer m_Renderer;

        [SerializeField] private Material passMaterial;
        [SerializeField] private RenderPassEvent passRenderPassEvent = RenderPassEvent.AfterRenderingPostProcessing; 

        public override void Create()
        {
            m_Renderer = new FullScreenRenderer
            {
                renderPassEvent = passRenderPassEvent,
                PassMaterial = passMaterial
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(m_Renderer);
        }

        private sealed class FullScreenRenderer : ScriptableRenderPass
        {
            private static readonly int blitTextureID = Shader.PropertyToID("_BlitTexture");

            private Material m_PassMaterial;
            public Material PassMaterial
            {
                get => m_PassMaterial;
                set => m_PassMaterial = value;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                if (m_PassMaterial == null) return;
    
                CommandBuffer cmd = CommandBufferPool.Get("FullScreenRender");

                using (new ProfilingScope(cmd, profilingSampler))
                {
                    int w = renderingData.cameraData.cameraTargetDescriptor.width;
                    int h = renderingData.cameraData.cameraTargetDescriptor.height;

                    cmd.GetTemporaryRT(blitTextureID, w, h, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        
                    cmd.SetRenderTarget(blitTextureID);
                    CoreUtils.DrawFullScreen(cmd, m_PassMaterial, blitTextureID, 0);

                    cmd.ReleaseTemporaryRT(blitTextureID);
                    context.ExecuteCommandBuffer(cmd);
                    CommandBufferPool.Release(cmd);
                }
            }
            
        }
    }
}
