using System;
using System.IO;
using System.Collections.Generic;
using FlatBuffers;
using AutoGenConfig;

public class TestConfigOperator {

    public class Data {
        public int ID;
        public string data1;
        public int data2;
    }

    public void Save(List<Data> datas, string path) {
        FlatBufferBuilder fbb = new FlatBufferBuilder(1);
        int count = datas.Count;
        Offset<SingleTestConfigData>[] offsets = new Offset<SingleTestConfigData>[count];
        for (int n = 0; n < count; ++n) {
            Data data = datas[n];
            offsets[n] = SingleTestConfigData.CreateSingleTestConfigData(fbb,
            data.ID,
            fbb.CreateString(data.data1),
            data.data2);
        }
        VectorOffset dataOff = TestConfig.CreateDataVector(fbb, offsets);
        var configOff = TestConfig.CreateTestConfig(fbb, dataOff);
        TestConfig.FinishTestConfigBuffer(fbb, configOff);
        using (var ms = new MemoryStream(fbb.DataBuffer.Data, fbb.DataBuffer.Position, fbb.Offset)) {
            File.WriteAllBytes(path, ms.ToArray());
        }
    }
}
