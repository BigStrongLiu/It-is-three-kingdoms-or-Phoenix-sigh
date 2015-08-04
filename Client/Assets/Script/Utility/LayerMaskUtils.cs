using UnityEngine;
using System.Collections;

public class LayerMaskUtils
{
    public static bool CompareLayer(int ly,string flag)
    {
        int layer = LayerMask.NameToLayer(flag);
        if (ly == layer)
            return true;
        return false;
    }

	public static int GetRaycastLayer(string flag)
	{
        int layer = LayerMask.NameToLayer(flag);
        return 1 << layer;
	}

    public static int GetRaycastLayer(params string[] flags)
    {
        if (flags == null || flags.Length == 0)
            return 0;
		int layer = 0;
		for(int i = 0;i < flags.Length;i++){
            string mask = flags[i];
			int temp = LayerMask.NameToLayer(mask);
			layer += 1 << temp;
		}
		return layer;
	}
   
}
