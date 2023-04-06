using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GraphController : MonoBehaviour
{
    public float WaitTime;

    [SerializeField]
    API api;

    [SerializeField]
    TextMeshPro title;

    void Start()
    {
        api.GetTimeData(OnDataRecieved);
    }

    void OnDataRecieved(List<TimeData>dataList)
    {
        StartCoroutine(CycleDataRoutine(dataList));
    }

    IEnumerator CycleDataRoutine(List<TimeData> dataList)
    {
        while (true)
        {
            foreach (TimeData data in dataList)
            {
                title.text = data.date.ToString("MMMM dd, yyyy");

                yield return new WaitForSeconds(WaitTime);
            }
            yield return new WaitForEndOfFrame();

        }
    }
}
