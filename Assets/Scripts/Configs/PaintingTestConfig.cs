using System;
using System.Collections;
using System.Collections.Generic;
using AutoGenConfig;
using FlatBuffers;
using UnityEngine;

public class PaintingTestDefine
{
    public int ID;
    public string data1;
    public int data2;
}

public class PaintingTestConfig : MonoSingleton<PaintingTestConfig>,IMonoSingleton
{
    Dictionary<int, PaintingTestDefine> _allTestDef;

    public void SingletonInit()
    {
        string path = RSPathUtil.Config("TestConfig");
        ResourceManager.Instance.LoadAndProcTextAsset(path, ProcFB);
    }

    private void ProcFB(byte[] bytes)
    {
        ByteBuffer bb = new ByteBuffer(bytes);

        if (!TestConfig.TestConfigBufferHasIdentifier(bb))
        {
            throw new Exception("Identifier text failed,testconfig");
        }

        TestConfig config = TestConfig.GetRootAsTestConfig(bb);
        Debug.Log("Loaded TestConfig data count"+config.DataLength);

        _allTestDef = new Dictionary<int, PaintingTestDefine>();
        for (int i = 0; i < config.DataLength; i++)
        {
            SingleTestConfigData? data = config.Data(i);
            if (data != null)
            {
                PaintingTestDefine def = new PaintingTestDefine();
                def.ID = data.Value.ID;
                def.data1 = data.Value.Data1;
                def.data2 = data.Value.Data2;

                _allTestDef.Add(def.ID, def);
            }
        }
    }

    public PaintingTestDefine Get(int id)
    {
        PaintingTestDefine def = null;
        _allTestDef.TryGetValue(id, out def);
        return def;
    }
}
