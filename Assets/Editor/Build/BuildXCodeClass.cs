using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BuildXCodeClass : System.IDisposable {

    private string _filePath;
    private List<string> _allLines;

    public BuildXCodeClass(string fPath) {
        _filePath = fPath;
        if (!System.IO.File.Exists(_filePath)) {
            Debug.LogError(_filePath + "路径下文件不存在");
            return;
        }

        _allLines = new List<string>();
        StreamReader streamReader = new StreamReader(_filePath);
        while (!streamReader.EndOfStream) {
            _allLines.Add(streamReader.ReadLine());
        }
        streamReader.Close();
    }

    public void WriteBefore(string befor, string[] text)
    {
        int index = findIndex(befor);
        if (index == -1)
        {
            Debug.LogError(_filePath + "write 中没有找到标志" + befor);
            return;
        }

        int willIndex = index - 1;
        _allLines.InsertRange(index, text);
    }

    public void WriteBelow(string below, string text) {
        int index = findIndex(below);
        if (index == -1) {
            Debug.LogError(_filePath + "write 中没有找到标志" + below);
            return;
        }

        //if(findIndex(text) != -1) {
        //    Debug.Log(_filePath + "write 中没已经有了相同的内容" + text);
        //    return;
        //}

        int willIndex = index + 1;
        if(!isEnd(index) && _allLines[willIndex] == "{") {
            willIndex += 1;
        }

        _allLines.Insert(willIndex, text);
    }

    public void Replace(string below, string newText) {
        int index = findIndex(below);
        if (index == -1) {
            Debug.LogError(_filePath + "replace 中没有找到标志" + below);
            return;
        }

        if (findIndex(newText) != -1) {
            Debug.Log(_filePath + "replace 中没已经有了相同的内容" + newText);
            return;
        }

        _allLines[index] = newText;
    }

    public void Delete(string text) {
        int index = findIndex(text);
        if (index == -1)
        {
            Debug.LogError(_filePath + "delete 中没有找到标志" + text);
            return;
        }

        _allLines.RemoveAt(index);
    }

    /// <summary>
    /// 将转换好的字符写回文件
    /// </summary>
    public void WriteBack() {
        StreamWriter streamWriter = new StreamWriter(_filePath);
        for(int n = 0; n < _allLines.Count; ++n) {
            streamWriter.WriteLine(_allLines[n]);
        }
        streamWriter.Close();
    }

    public void Dispose() {

    }

    private int findIndex(string content) {
        for (int n = 0; n < _allLines.Count; ++n) {
            if (_allLines[n].Contains(content)) {
                return n;
            }
        }
        return -1;
    }

    private bool isEnd(int index) {
        if (index >= _allLines.Count - 1) {
            return true;
        }

        return false;
    }
}