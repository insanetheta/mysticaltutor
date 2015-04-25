using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public static class StringMutator
{
    public static string ScrollViewCardNameTextWrap(string text)
    {
        string tmp = text;
        if (tmp.Length > 13)
        {
           tmp = tmp.Insert(13, "\n");
        }
        return tmp;
    }
}
