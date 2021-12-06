using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    List<Dictionary<string, object>> questList;
    private void Awake()
    {
        questList = CSVReader.Read("questList");
    }





}
