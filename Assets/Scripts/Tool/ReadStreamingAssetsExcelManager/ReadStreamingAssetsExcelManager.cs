//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;
//using NPOI;
//using NPOI.XSSF.UserModel;
//using NPOI.SS.UserModel;
//using System.Text.RegularExpressions;
//using System.Reflection;
//using System.CodeDom.Compiler;
//using UnityEngine.Diagnostics;
//using System;
public class ReadStreamingAssetsExcelManager : MonoSingleton<ReadStreamingAssetsExcelManager>, IMonoSingleton
{
    public void SingletonInit()
    {
    }

    public void CreateAllData()
    {

    }
}
//public class ReadStreamingAssetsExcelManager :MonoSingleton<ReadStreamingAssetsExcelManager>, IMonoSingleton
//{
//    private List<ExcelData> _excelDatas;


//    private static Regex _regex = new Regex(@"\A(?<desc>.+?)(?<flag>\|{1,3})(?<name>.+?)\((?<type>.+?)(:(?<constraint>.*?))?\)$");
//    public static string ConfigSaveFileEx = "Operator";

//    private Dictionary<string, string> _types;

//    public void SingletonInit()
//    {
//        _excelDatas = new List<ExcelData>() {
//        };
//        _types = new Dictionary<string, string>();
//        _types.Add("float", "float");
//        _types.Add("string", "string");
//        _types.Add("bool", "bool");
//        _types.Add("int", "int");
//        _types.Add("enum", "enum");
//    }

//    public void CreateAllData()
//    {

//        Debug.LogError(Application.dataPath + "=========");
//        ReadExcelNPOI();
//        GenField();
//        GenCode();
//        GenDataFiles();
//    }

//    /// <summary>
//    /// 读取表
//    /// </summary>
//    private void ReadExcelNPOI()
//    {
//        _excelDatas.Clear();
//        string[] files = Directory.GetFiles(Application.streamingAssetsPath, "*.xlsx");

//        foreach (string file in files)
//        {
//            string excelName = Path.GetFileNameWithoutExtension(file);

//            if (excelName.StartsWith("~$"))
//            {
//                Debug.LogWarning("frile is opening!");
//                continue;
//            }

//            XSSFWorkbook wk = new XSSFWorkbook(file);

//            int count = wk.Count;
//            for (int n = 0; n < count; n++)
//            {
//                ISheet sheet = wk[n];
//                string sheetName = sheet.SheetName;

//                if (!sheetName.EndsWith("Config"))
//                {
//                    Debug.LogWarningFormat("Config [{0}] is not endswith 'Config'", excelName + "/" + sheetName);
//                    continue;
//                }

//                ExcelData data = new ExcelData();
//                data.excelName = excelName;
//                data.sheetName = sheetName;
//                data.sheet = sheet;

//                _excelDatas.Add(data);
//            }

//        }
//    }

//    /// <summary>
//    /// ==================================获取表头信息
//    /// </summary>
//    private void GenField()
//    {
//        Debug.Log("-------------生成配置表头信息-------------------------");
//        if (_excelDatas == null || _excelDatas.Count<=0)
//        {
//            Debug.LogError("Excel Datas is Null");
//            return;
//        }
//        foreach (ExcelData data in _excelDatas)
//        {
//            //读取表的第一行
//            IRow header = data.sheet.GetRow(0);
//            if (header == null)
//            {
//                continue;
//            }
//            for (int i = 0; i < header.LastCellNum; i++)
//            {
//                string field = "";
//                ICell cell = header.GetCell(i);
//                if (cell == null)
//                {
//                    Debug.LogErrorFormat("Excel {0} Sheet {1} have null colume", data.excelName,data.sheetName);
//                    continue;
//                }

//                field = cell.ToString();

//                //没有正确的格式此列跳过
//                if (!field.Contains("|"))
//                {
//                    continue;
//                }

//                ExcelFieldInfo info = GetFieldInfo(data, field);
//                if (info == null)
//                {
//                    continue;
//                }
//                info.index = i;

//                data.AddFieldInfosInfo(info);
//            }
//        }
//    }
//    private void GenCode()
//    {
//        Debug.Log("-------------生成代码-------------------------");


//    }

//    private void GenDataFiles()
//    {
//        Debug.Log("-------------生成数据文件-------------------------");
//        foreach (ExcelData data in _excelDatas)
//        {
//            if (data.fieldInfos.Count == 0)
//                continue;

//            string classTypeName = data.sheetName + ConfigSaveFileEx;
//            //反射生成类中类要用+连接
//            string dataTypeName = data.sheetName + ConfigSaveFileEx + "+Data";

//            //生成C#代码
//            Assembly assembly = GetAssembly();
//            object datas = CreateGeneric(typeof(List<>), assembly.GetType(dataTypeName));

//            for (int i = 1; i < data.sheet.LastRowNum; i++)
//            {
//                IRow dataRow = data.sheet.GetRow(i);
//                if (dataRow == null)
//                {
//                    Debug.LogError(data.excelName + "/" + data.sheetName + "第" + i + "为空");
//                    continue;
//                }
//                ICell first = dataRow.GetCell(0);
//                //第一个字段为空认为表结束
//                if (first == null || "" == first.ToString().Trim())
//                {
//                    break;
//                }

//                object dataInst = assembly.CreateInstance(dataTypeName);
//                FieldInfo[] fields = dataInst.GetType().GetFields();
//                foreach (FieldInfo pi in fields)
//                {
//                    ExcelFieldInfo fieldInfo = null;
//                    string infoValue = null;
//                    object value = null;

//                    fieldInfo = data.fieldInfos[pi.Name];

//                    ICell cell = dataRow.GetCell(fieldInfo.index);

//                    if (cell == null)
//                    {
//                        infoValue = "";
//                    }
//                    else
//                    {
//                        if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
//                        {
//                            infoValue = cell.DateCellValue.ToString();
//                        }
//                        else
//                        {
//                            infoValue = cell.ToString();
//                        }
//                    }
//                    value = GetFieldValue(data, fieldInfo, infoValue);
//                    pi.SetValue(dataInst, value);
//                }
//                datas.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, datas, new object[] { dataInst });
//            }

//            object classInst = assembly.CreateInstance(classTypeName);
//            object[] parameters = new object[2];
//            parameters[0] = datas;
//            parameters[1] = Application.dataPath+ "/Scripts/Tool/ReadStreamingAssetsExcelManager/TestFolder" + data.sheetName + ".bytes";
//            CallMethod(classInst, "Save", parameters);
//        }
//    }

//    private void GenFBS()
//    {
//        Debug.Log("-------------生成FBS文件-------------------------");

//    }

//    private Assembly GetAssembly()
//    {
//        Debug.Log("-------------生成C#代码-------------------------");
//        Assembly rt = null;

//        string codePath = Application.dataPath + "/Scripts/Tool/ReadStreamingAssetsExcelManager/TestFolder/CodePath";
//        string[] files = Directory.GetFiles(codePath, "*.cs");

//        string flatRefPath = Application.dataPath+ "/Plugins/CompileRef" + "FlatBuffers.dll";
//        return CompileCS(flatRefPath, null, null, files);

//        return rt;
//    }

//    private ExcelFieldInfo GetFieldInfo(ExcelData data, string field)
//    {
//        ExcelFieldInfo ret = null;

//        field = field.Replace("\r", "").Replace("\n", "");

//        // \A、^从字符串开始出匹配;\Z、$从字符串结束处匹配
//        //一个(为一个组
//        //new Regex(@"\A(?<desc>.+?)(?<flag>\|{1,3})(?<name>.+?)\((?<type>.+?)(:(?<constraint>.*?))?\)$");
//        //(@"\A  (?<desc>.+?)  (?<flag>\|{1,3})  (?<name>.+?)  \(  (?<type>.+?)   (:  (?<constraint>.*?)  )  ?  \)$");
//        //                                                                        (:  (?<constraint>.*?)  ) 
//        Match match = _regex.Match(field);
//        if (match.Success)
//        {
//            string desc = match.Groups["desc"].Value;
//            string flag = match.Groups["flag"].Value;
//            string name = match.Groups["name"].Value;
//            string type = match.Groups["type"].Value;
//            string constraint = match.Groups["constraint"].Value;

//            if (_types.ContainsKey(type))
//            {
//                ret = new ExcelFieldInfo();
//                ret.name = name;
//                ret.type = type;
//                ret.desc = desc;
//                ret.constraint = constraint;
//            }
//            else
//            {
//                Debug.LogErrorFormat("Excel {0} Sheet {1} LineName {2} Type {3} is Error:[float、string、bool、int、enum]", data.excelName, data.sheetName, name, type);
//            }
//        }
//        else
//        {
//            Debug.LogErrorFormat("Excel {0} Sheet {1} LineNameStyle {2}  is Error", data.excelName, data.sheetName, field);
//        }

//        return ret;
//    }

//    /// <summary>
//    /// 动态编译C#
//    /// </summary>
//    /// <returns></returns>
//    private Assembly CompileCS(string refer,string output ,string options,params string[] code)
//    {
//        Debug.Log("-------------动态编译C#-------------------------");
//        CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
//        CompilerParameters compilerParameters = new CompilerParameters();
//        compilerParameters.GenerateExecutable = false;
//        compilerParameters.GenerateInMemory = true;
//        compilerParameters.ReferencedAssemblies.Add(Application.dataPath + "/Plugins/CompileRef" + "System.dll");
//        if (!string.IsNullOrEmpty(refer))
//        {
//            compilerParameters.ReferencedAssemblies.Add(refer);
//        }
//        if (!string.IsNullOrEmpty(output))
//        {
//            compilerParameters.OutputAssembly = output;
//        }
//        if (!string.IsNullOrEmpty(options))
//        {
//            compilerParameters.CompilerOptions = options;
//        }
//        CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromFile(compilerParameters, code);

//        if (compilerResults.Errors.Count > 0)
//        {
//            Debug.LogError("compile error");
//            foreach (var item in compilerResults.Errors)
//            {
//                Debug.LogError(item.ToString());
//            }
//        }
//        return compilerResults.CompiledAssembly;
//    }

//    /// <summary>
//    /// 动态创建泛型类型
//    /// </summary>
//    public static object CreateGeneric(Type generic, Type innerType, params object[] args)
//    {
//        Type specificType = generic.MakeGenericType(new System.Type[] { innerType });
//        return Activator.CreateInstance(specificType, args);
//    }
//    public static object GetFieldValue(ExcelData data, ExcelFieldInfo info, string value)
//    {
//        object ret = null;
//        try
//        {
//            switch (info.type)
//            {
//                case "float":
//                    if (string.IsNullOrEmpty(value))
//                    {
//                        ret = 0f;
//                    }
//                    else
//                    {
//                        ret = float.Parse(value);
//                    }
//                    break;
//                case "int32":
//                    if (string.IsNullOrEmpty(value))
//                    {
//                        ret = 0;
//                    }
//                    else
//                    {
//                        ret = int.Parse(value);
//                    }
//                    break;
//                case "bool":
//                    if (string.IsNullOrEmpty(value))
//                    {
//                        ret = false;
//                    }
//                    else
//                    {
//                        ret = (value.ToLower() == "true" || value == "1");
//                    }
//                    break;
//                case "string":
//                    if (string.IsNullOrEmpty(value))
//                    {
//                        ret = "";
//                    }
//                    else
//                    {
//                        ret = value;
//                    }
//                    break;
//                    //default:            // 枚举,转换成int类型
//                    //    Type t = _commonAssmbly.GetType("msg." + info.constraint);
//                    //    ret = Convert.ToInt32(Enum.Parse(t, value));
//                    //    break;
//            }
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(data.excelName + "中的" + data.sheetName + "中的：" + info.name + " " + info.type + " " + value + " " + e.Message);
//        }

//        return ret;
//    }

//    /// <summary>
//    /// 动态调用方法
//    /// </summary>
//    public static void CallMethod(object obj, string method, object[] parameters)
//    {
//        MethodInfo methodInfo = obj.GetType().GetMethod(method);
//        methodInfo.Invoke(obj, parameters);
//    }

//    public void SingletonDestory()
//    {

//    }
//}

//public class ExcelData
//{
//    public string excelName;
//    public string sheetName;
//    public ISheet sheet;

//    public Dictionary<string, ExcelFieldInfo> fieldInfos;

//    public void AddFieldInfosInfo(ExcelFieldInfo info)
//    {
//        if (fieldInfos == null)
//        {
//            fieldInfos = new Dictionary<string, ExcelFieldInfo>();
//        }
//        if (!fieldInfos.ContainsKey(info.name))
//        {
//            fieldInfos.Add(info.name, null);
//        }
//        else
//        {
//            Debug.LogErrorFormat("Excel {0} Sheet {1} Exist Equal Key {2}", excelName, sheetName, info.name);
//            return;
//        }
//        fieldInfos[info.name] = info;
//    }
//}

//public class ExcelFieldInfo
//{

//    public int index;
//    public string name;
//    public string type;
//    public string desc;
//    public string constraint;
//}