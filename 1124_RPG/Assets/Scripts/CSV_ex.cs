using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSV_ex : MonoBehaviour
{

    void Awake()
    {

        List<Dictionary<string, object>> data = CSVReader.Read("example");

        for (var i = 0; i < data.Count; i++)
        {
            print(data[i]["ID"] + " " +
                  data[i]["�ι��̸�"] + " " +
                  data[i]["���"] + " " +
                  data[i]["�̺�Ʈ��ȣ"]);
        }

    }
}