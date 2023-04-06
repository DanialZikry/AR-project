using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using Vuforia;
using UnityEngine.UI;

public class RadialProgress : MonoBehaviour
{
    public string url = "http://danialzikry.pythonanywhere.com/";
    public static string weatherApiResponseBody = null;
    public ParaData paraData = null;

    
    public Image CircleLoadingBar;
    public TMPro.TextMeshProUGUI powerText;


    private void Awake()
    {
        StartCoroutine(GetRequest());
    }

    private IEnumerator GetRequest()
    {
        while (true)
        {
            using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url))
            {
                yield return unityWebRequest.SendWebRequest();

                switch (unityWebRequest.result)
                {

                    case UnityWebRequest.Result.Success:
                        Debug.Log("Data received is ok!");
                        weatherApiResponseBody = unityWebRequest.downloadHandler.text;
                        paraData = JsonUtility.FromJson<ParaData>(weatherApiResponseBody);
                        Debug.Log(" usage of KWh: " + paraData.usage_of_kWh);


                        powerText.text = paraData.usage_of_kWh.ToString();
                        UpdateCircleProgress(paraData.usage_of_kWh);

                        break;

                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.ProtocolError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError("something is not okay");
                        break;


                }


                // Add a delay before the next API call
                yield return new WaitForSeconds(5);
            }
        }
    }

    void UpdateCircleProgress(float currentValue)
    {
        CircleLoadingBar.fillAmount = currentValue / 650;

        if (currentValue < 599)
        {
            CircleLoadingBar.color = Color.green;
        }
        else if (currentValue > 600 && currentValue < 640)
        {
            CircleLoadingBar.color = Color.yellow;
        }
        else
        {
            CircleLoadingBar.color = Color.red;
        }
    }
}
