using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AALoader : Singleton<AALoader>,ISingleton
{
    private Dictionary<string, UnityEngine.Object> cache;
    private List<AsyncOperationHandle> loadHandleCache;

    public void SingletonInit()
    {
        cache = new Dictionary<string, UnityEngine.Object>();
        loadHandleCache = new List<AsyncOperationHandle>();
    }


    public void LoadAssetAsync<T>(string addressName, Action<T> onComplete) where T : UnityEngine.Object
    {
        if (cache.ContainsKey(addressName))
        {
            onComplete?.Invoke(cache[addressName] as T);
            return;
        }

        AsyncOperationHandle loadHandle = Addressables.LoadAssetAsync<T>(addressName);
        loadHandleCache.Add(loadHandle);
        loadHandle.Completed += (t) =>
        {
            var obj = t.Result as T;
            obj.name = addressName;
            onComplete?.Invoke(obj);
            cache.Add(addressName, obj);
        };
    }

    public T LoadAssetAsync<T> (string addressName) where T : UnityEngine.Object
    {
        if (cache.ContainsKey(addressName))
        {
            return cache[addressName] as T;
        }

        AsyncOperationHandle loadHandle = Addressables.LoadAssetAsync<T>(addressName);
        loadHandle.WaitForCompletion();
        loadHandleCache.Add(loadHandle);

        var obj = loadHandle.Result as T;
        cache.Add(addressName, obj);
        return obj as T;
    }

    public void SingletonDestory()
    {
        foreach (var item in cache)
        {
            GameObject.Destroy(item.Value);
        }
        cache = null;
    }
}
