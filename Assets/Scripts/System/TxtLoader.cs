using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TxtLoader
{
    public static string[] ReadTxt(string filename)
    {
        TextAsset txt = Resources.Load(filename) as TextAsset;

        string[] result = txt.text.Split('\n');

        return result;
    }

}
