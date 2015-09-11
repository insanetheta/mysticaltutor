using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;


public class LocalStorage
{
    public static void SaveFile(string fileText, string fileName)
    {
        Debug.Log(Application.persistentDataPath);
        File.WriteAllText(Application.persistentDataPath + @"/" + fileName, fileText, ASCIIEncoding.ASCII);
    }

    public static string LoadFileText(string fileName)
    {
        Debug.Log(Application.persistentDataPath);
        try
        {
            return File.ReadAllText(Application.persistentDataPath + @"/" + fileName, ASCIIEncoding.ASCII);
        }
        catch(Exception e)
        {
            Debug.LogWarning("Failed to Load: " + e.Message);
            return "";
        }
        
    }
}

