using UnityEngine;

public class LightmapTool : MonoBehaviour
{
    public Texture2D[] LightmapFars;
    public Texture2D[] LightmapNears;

    void Start()
    {
        this.UpdateLightmap();
    }

    [ContextMenu("Execute")]
    public void UpdateLightmap()
    {
        int length = Mathf.Max(this.LightmapFars.Length,this.LightmapNears.Length);
        LightmapData[] lightmaps = new LightmapData[length];
        for (int i = 0; i < length; i++)
        {
            LightmapData lightmapData = new LightmapData(); ;
            if (i < this.LightmapFars.Length)
            {
                lightmapData.lightmapFar = this.LightmapFars[i];
            }
            if (i < this.LightmapNears.Length)
            {
                lightmapData.lightmapNear = this.LightmapNears[i];
            }
            lightmaps[i] = lightmapData;
        }
        LightmapSettings.lightmaps = lightmaps;
    }
}
