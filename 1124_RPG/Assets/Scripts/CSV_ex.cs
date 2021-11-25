using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSV_ex : MonoBehaviour
{

    void Awake()
    {

        List<Dictionary<string, object>> data = CSVReader.Read("example");

            string[] scripts = ((string)data[0]["Dialogue"]).Split('$');
        for (var i = 0; i < data.Count; i++)
        {

            print(data[i]["Index"] + " " +
                  data[i]["Name"] + " " +
                  data[i]["Dialogue"] + " " +
                  data[i]["EventNum"] + " " +
                  data[i]["SkipLine"]);
        }

    }
}