using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileSaver
{
    private string dataDirPath;
    private string dataFileName;

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
                        save = JsonUtility.FromJson<SaveState>(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured while loading data");
            }
        }

        return save;
    }

    public void Save(SaveState save)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Debug.Log(fullPath);
        try
        {
            Directory.CreateDirectory(dataDirPath);
            string data = JsonUtility.ToJson(save, false);
            using (FileStream stream = new FileStream(fullPath,FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }   
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured while saving data");
        }
    }
}
