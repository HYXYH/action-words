using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class HashTable
{
    private const int HASH_KEY  = 42;

    private List<string>[]  _data;
    private int             _size;


    public HashTable (int size, string sourcePath)
    {
        _size = size;

        _data = new List<string>[size];
        for (int i = 0; i < size; i++)
            _data[i] = new List<string>();

        if (_data == null) Debug.Log("_data not created at all!");
        if (_data[0] == null) Debug.Log("_data[0] not created.");

        Load (sourcePath);
    }
    
    
    public HashTable (int size, TextAsset textAsset)
    {
        _size = size;

        _data = new List<string>[size];
        for (int i = 0; i < size; i++)
            _data[i] = new List<string>();

        if (_data == null) Debug.Log("_data not created at all!");
        if (_data[0] == null) Debug.Log("_data[0] not created.");

        Load (textAsset);
    }


    private int GetHash(string word)
    {
        uint pos = 0;
        uint key = 1;
        int n = word.Length;
        
        for (int i = 0; i < n; i++)
        {
            pos = pos + word[i] * key;
            key = key * HASH_KEY;
        }

        return Mathf.Abs((int)pos % _size);
    }

    private void Add (string word)
    {
        int hash = GetHash(word);
        _data[hash].Add(word);
    }



    public bool Check (string word)
    {
        int hash = GetHash(word);
        int pos = _data[hash].BinarySearch(word);

        return (pos >= 0);
    }

    public void Load(string path)
    {
        StreamReader stream = new StreamReader(path);

        string tmpStr;

        if (stream == null)
            Debug.LogError("Couldn't find file " + path);

        while (!stream.EndOfStream)
        {
            tmpStr = stream.ReadLine();
            Add(tmpStr);
        }

        for (int i = 0; i < _size; i++)
        {
            _data[i].Sort();
        }

        stream.Close();
    }
    
    
    public void Load(TextAsset textAsset)
    {
        StringReader stream = new StringReader(textAsset.text);
        string tmpStr = stream.ReadLine();
        
        while (tmpStr != null)
        {
            Add(tmpStr);
            tmpStr = stream.ReadLine();
        }

        for (int i = 0; i < _size; i++)
        {
            _data[i].Sort();
        }

    }
}


