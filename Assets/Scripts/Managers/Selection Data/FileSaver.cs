using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileSaver
{
    private string dataDirPath;
    private string dataFileName;
    private string encryptionString = "Extravagance";

    public FileSaver(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public SaveState Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        SaveState save = null;
        if (File.Exists(fullPath))
        {
            try
            {
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        save = JsonUtility.FromJson<SaveState>(decrypt(reader.ReadToEnd()));
                    }
                }
            }
            catch (Exception ignoredException)
            {
                Debug.LogError("Error occured while loading data");
            }
        }

        return save;
    }

    public void Save(SaveState save)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(dataDirPath);
            string data = JsonUtility.ToJson(save, false);
            using (FileStream stream = new FileStream(fullPath,FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(encrypt(data));
                }   
            }
        }
        catch (Exception ignoredException)
        {
            Debug.LogError("Error occured while saving data");
        }
    }

    private string encrypt(string s)
    {
        for (int j = 0; j < encryptionString.Length; j++)
        {
            string d = "";
            for (int i = 0; i < s.Length; i++)
            {
                d += (char)(s[i] ^ encryptionString[j]);
            }

            s = d;
        }

        return s;
    }

    private string decrypt(string s)
    {
        for (int j = encryptionString.Length-1; j >= 0; j--)
        {
            string d = "";
            for (int i = 0; i < s.Length; i++)
            {
                d += (char)(s[i] ^ encryptionString[j]);
            }

            s = d;
        }

        return s;
    }
}
