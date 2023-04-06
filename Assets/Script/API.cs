using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.Events;

public class API : MonoBehaviour
{
    const string ENDPOINT = "https://covid19.who.int/WHO-COVID-19-global-data.csv";

   
    public void GetTimeData(UnityAction<List<TimeData>>callback)
    {
        StartCoroutine(GetTimeDataRoutine(callback));
    }

    IEnumerator GetTimeDataRoutine(UnityAction<List<TimeData>>callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(ENDPOINT);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("Network Error");
        } else
        {
            callback(ParseData(request.downloadHandler.text));
        }
    }

    List<TimeData> ParseData(string data)
    {   //State,Tests,positive,negative,pending,hospitalized,death,stateFips,grade,date
        //Date_reported,Country_code,Country,WHO_region,New_cases,Cumulative_cases,New_deaths,Cumulative_deaths
        List<string> lines = data.Split('\n').ToList();     // SPLIT THE DATA RECEIVED BY LINE CHARACTER
        lines.RemoveAt(0);                                  // REMOVED LINE FIRST LINE
        lines.RemoveAt(lines.Count - 1);                    //REMOCE LAST LINE --> line.Count = to counting until last line

        List<TimeData> dataList = new List<TimeData>();

        foreach (string line in lines)                      //PRINTING LINE 
        {
            List<string> lineData = line.Split(',').ToList();
            TimeData timeData = new TimeData
            {
               date = Convert.ToDateTime(lineData[0]),            //Allready solve this part .JSON has specifit format to read the date time
                                                                  //-->The format for sring to read is [2020-01-03]
                                                                  //--->DateTime.Parse() ---> Convert.ToDateTime()
                                                                  //---->n sql server 2005 database store date in yyyy-MM-dd formate
                New_deaths = int.Parse(lineData[6]),

                //date = Convert.ToDateTime(lineData[9]),          //ERROR OCCUR [COMPILING SUCCESS][RUN APPLICATION =FAIL]
                //Tests = int.Parse(lineData[1]),                  //FormatException: String was not recognized as a valid DateTime.
                // positive = int.Parse(lineData[2]),

            };
            dataList.Add(timeData);

            
        }

        return dataList;
    }
}
