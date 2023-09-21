using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DeployAAResourceManager : ResourceManager
{
    private Hashtable objPool;

    public override void Init()
    {
        base.Init();
        objPool = new Hashtable();
    }

    public override void LoadAsync(string asset_name, System.Action<object> action)
    {
        if (objPool.ContainsKey(asset_name))
        {
            action(objPool[asset_name] as object);
            return;
        }
        object rt = null;
        var obj = Addressables.LoadAssetAsync<object>(asset_name);
        obj.Completed += (result) => {
            rt = result.Result;
            if (rt != null && !objPool.ContainsKey(asset_name))
            {
                objPool.Add(asset_name, rt);
            }
            action(rt);
        };
    }

    public override void Unload(string asset_path)
    {
        base.Unload(asset_path);
    }
}
