using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Reflection;
//using Newtonsoft.Json;
using ExcelDataReader.Log;
using UnityEditor;

public class ExcelToJson
{
    public static List<object> DoXlsxToJson(Type type,string path , string sheetName)
    {
        string dataPath = UnityEngine.Application.dataPath;
        // xlsx路径
        //string xlsxPath = string.Format("{0}/Xlsx/BaseData.xlsx",dataPath);// dataPath.Remove(dataPath.IndexOf("Assets")));
        string xlsxPath = path;// "C:\\Users\\Administrator\\Desktop\\渭北项目\\配置表\\园区简介配置表.xlsx";

        FileStream stream = null;
        try
        {
            stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read);
        }
        catch (IOException e)
        {
            UnityEngine.Debug.LogFormat("关闭xlsx文件后重试！");
            UnityEngine.Debug.LogError(e.Message);
        }
        if (stream == null)
            return null;
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        //excelReader.Close();
        // 读取第一张工资表
        List<object> ret = ReadSingleSheet(type, result.Tables[sheetName], string.Format("{0}/{1}.json", UnityEngine.Application.streamingAssetsPath, sheetName));
        stream.Close();
        return ret;
    }
    /// <summary>
    /// 读取一个工作表的数据
    /// </summary>
    /// <param name="type">要转换的struct或class类型</param>
    /// <param name="dataTable">读取的工作表数据</param>
    /// <param name="jsonPath">存储路径</param>
    private static List<object> ReadSingleSheet(Type type, DataTable dataTable, string jsonPath)
    {
        int rows = dataTable.Rows.Count;
        int Columns = dataTable.Columns.Count;
        // 工作表的行数据
        DataRowCollection collect = dataTable.Rows;
        // xlsx对应的数据字段，规定是第二行
        string[] jsonFileds = new string[Columns];
        // 要保存成Json的obj
        List<object> objsToSave = new List<object>();
        for (int i = 0; i < Columns; i++)
        {
            jsonFileds[i] = collect[1][i].ToString();
        }
        // 从第三行开始
        for (int i = 2; i < rows; i++)
        {
            // 生成一个实例
            object objIns = type.Assembly.CreateInstance(type.ToString());

            for (int j = 0; j < Columns; j++)
            {
                // 获取字段
                FieldInfo field = type.GetField(jsonFileds[j]);
                if (field != null)
                {
                    object value = null;
                    try // 赋值
                    {
                        value = Convert.ChangeType(collect[i][j], field.FieldType);
                    }
                    catch (InvalidCastException e) // 一般到这都是Int数组，当然还可以更细致的处理不同类型的数组
                    {
                        Console.WriteLine(e.Message);
                        string str = collect[i][j].ToString();
                        string[] strs = str.Split(',');
                        int[] ints = new int[strs.Length];
                        for (int k = 0; k < strs.Length; k++)
                        {
                            ints[k] = int.Parse(strs[k]);
                        }
                        value = ints;
                    }
                    field.SetValue(objIns, value);
                }
                else
                {
                    UnityEngine.Debug.LogFormat("有无法识别的字符串：{0}", jsonFileds[j]);
                }
            }
            objsToSave.Add(objIns);
        }
        // 保存为Json
        //string content = JsonConvert.SerializeObject(objsToSave);
        //SaveFile(content, jsonPath);
        return objsToSave;
    }

    private static void SaveFile(string content, string jsonPath)
    {
        StreamWriter streamWriter;
        FileStream fileStream;
        if (File.Exists(jsonPath))
        {
            File.Delete(jsonPath);
        }
        fileStream = new FileStream(jsonPath, FileMode.Create);
        streamWriter = new StreamWriter(fileStream);
        streamWriter.Write(content);
        streamWriter.Close();
        UnityEngine.Debug.Log("保存完成！");
    }

    public static List<List<string>> GetSheetInfoRowToRim(string path, string sheetName, int row = 0,int maxrow = -1)
    {
        if (maxrow == -1)
        {
            string xlsxPath = path;// "C:\\Users\\Administrator\\Desktop\\渭北项目\\配置表\\园区简介配置表.xlsx";

            FileStream stream = null;
            try
            {
                stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read);
            }
            catch (IOException e)
            {
                UnityEngine.Debug.LogFormat("关闭xlsx文件后重试！");
                UnityEngine.Debug.LogError(e.Message);
            }
            if (stream == null)
                return null;
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            maxrow = result.Tables[sheetName].Rows.Count;
            stream.Close();
        }
        List<List<string>> rt = new List<List<string>>();
        for (int x = row; x < maxrow; x++)
        {
            rt.Add(GetSheetInfoByRow(path, sheetName, x));
        }
        return rt;
    }

    /// <summary>
    /// 读取某个表的某一行数据内容
    /// </summary>
    /// <param name="path">表路径</param>
    /// <param name="sheetName">分页名称</param>
    /// <param name="row">第几行</param>
    /// <returns></returns>
    public static List<string> GetSheetInfoByRow(string path, string sheetName,int row = 0)
    {
        string xlsxPath = path;
        // xlsx工作表名
        Debug.Log(xlsxPath);
        FileStream stream = null;
        try
        {
            stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read);
        }
        catch (IOException e)
        {
            UnityEngine.Debug.LogFormat("关闭xlsx文件后重试！");
            UnityEngine.Debug.LogError(e.Message);
        }
        if (stream == null)
            return null;
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet dataSet = excelReader.AsDataSet();

        DataTable dataTable = dataSet.Tables[sheetName];

        int rows = dataTable.Rows.Count;
        int Columns = dataTable.Columns.Count;

        List<string> rt = new List<string>();
        for (int i = 0; i < Columns; i++)
        {
            rt.Add(dataTable.Rows[row][i].ToString());
        }
        stream.Close();
        return rt;
    }

    public static List<string> GetSheetInfoByCol(string path, string sheetName, int col, bool giveUpSpace = true)
    {
        string xlsxPath = path;
        FileStream stream = null;
        try
        {
            stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        if (stream == null)
        {
            return null;
        }
        IExcelDataReader dataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet dataSet = dataReader.AsDataSet();

        DataTable dataTable = dataSet.Tables[sheetName];
        int rows = dataTable.Rows.Count;
        List<string> rt = new List<string>();
        for (int i = 0; i < rows; i++)
        {
            if (!string.IsNullOrEmpty(dataTable.Rows[i][col].ToString()))
            {
                rt.Add(dataTable.Rows[i][col].ToString());
            }
        }
        stream.Close();
        return rt;
    }
    public static string GetSheetInfoByRowCol(string path, string sheetName, int Row, int col, bool giveUpSpace = true)
    {
        string xlsxPath = path;
        FileStream stream = null;
        try
        {
            stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        if (stream == null)
        {
            return null;
        }
        IExcelDataReader dataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet dataSet = dataReader.AsDataSet();

        DataTable dataTable = dataSet.Tables[sheetName];
        string rt = string.Empty;
        if (dataTable.Rows[Row].ItemArray.Length > col)
        {
            rt = dataTable.Rows[Row][col].ToString();
        }
        stream.Close();
        return rt;
    }
}
