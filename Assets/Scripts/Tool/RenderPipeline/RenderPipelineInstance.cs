using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RenderPipelineInstance : MonoBehaviour
{
    public bool beginCameraRender;
    public bool endCameraRender;
    public bool beginFrameRender;
    public bool endFrameRender;

    private void OnEnable()
    {
        if (beginCameraRender)
        {
            RenderPipelineManager.beginCameraRendering += Render;
        }
        if (endCameraRender)
        {
            RenderPipelineManager.endCameraRendering += Render;
        }
        if (beginFrameRender)
        {
            RenderPipelineManager.beginFrameRendering += Render;
        }
        if (endFrameRender)
        {
            RenderPipelineManager.endFrameRendering += Render;
        }
    }

    protected void Render(ScriptableRenderContext context, Camera cameras)
    {
        var cmd = new CommandBuffer();
        cmd.ClearRenderTarget(true, true, Color.red);
        context.ExecuteCommandBuffer(cmd);
        cmd.Release();

        context.Submit();
        Debug.Log("Render");
    }

    protected void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        var cmd = new CommandBuffer();
        cmd.ClearRenderTarget(true, true, Color.green);
        context.ExecuteCommandBuffer(cmd);
        cmd.Release();

        context.Submit();
        Debug.Log("Render");
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= Render;
        RenderPipelineManager.endCameraRendering -= Render;
        RenderPipelineManager.beginFrameRendering -= Render;
        RenderPipelineManager.endFrameRendering -= Render;
    }
}
