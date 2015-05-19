using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.IO;

public class Transaction<T> where T : class
{
    T response;
    string responseText;

    public Transaction()
    {
        
    }

    string SetParameters()
    {
        return null;
    }

    public IEnumerator HttpGetRequest(string requestUrl)
    {
        WWW www = new WWW(
            requestUrl
            );
        yield return www;
        responseText = www.text;
        //response = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(www.text);
        //T data;
        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.Default.GetBytes(responseText)))
        {
            response = CreateFromJsonStream<T>(stream);
        }
        //response = data;
    }

    public T GetResponse()
    {
        if (null != response)
        {
            return response;
        }
        else
        {
            Debug.LogError("No Response Data Present!");
            return default(T);
        }
    }

    public string GetText()
    {
        return responseText;
    }

    public static T CreateFromJsonStream<T>(Stream stream)
    {
        JsonSerializer serializer = new JsonSerializer();
        T data;
        using (StreamReader streamReader = new StreamReader(stream))
        {
            data = (T)serializer.Deserialize(streamReader, typeof(T));
        }
        return data;
    }
}
