using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;
using UnityEngine.UI;

public class API1 : MonoBehaviour
{
    public string url = "http://danialzikry.pythonanywhere.com/";
    public static string weatherApiResponseBody = null;
    public ParaData paraData = null;

  

    public TMPro.TextMeshProUGUI temprM1Text;
    public TMPro.TextMeshProUGUI temprM2Text;
    public TMPro.TextMeshProUGUI temprAvText;
    public TMPro.TextMeshProUGUI voltM1Text;
    public TMPro.TextMeshProUGUI voltM2Text;
    //public TMPro.TextMeshProUGUI powerText;
    //public TMPro.TextMeshProUGUI lgcurrentText;
    //public TMPro.TextMeshProUGUI ldcurrentText;




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
                    Debug.Log("Data received is ok!");                              //data receieve succesfull
                    weatherApiResponseBody = unityWebRequest.downloadHandler.text;
                    paraData = JsonUtility.FromJson<ParaData>(weatherApiResponseBody);
                        Debug.Log(" temperature M1: " + paraData.temperature_M1 +  " temperature M2: " + paraData.temperature_M2 + " averge tempe: " + paraData.average_temperature + " volt M1: " + paraData.volt_M1 + " volt M2: " + paraData.volt_M2);


                    temprM1Text.text = paraData.temperature_M1.ToString();              //int temperature data --> string temperature data = for displaying in text
                    temprM2Text.text = paraData.temperature_M2.ToString();
                    temprAvText.text = paraData.average_temperature.ToString();
                    voltM1Text.text = paraData.volt_M1.ToString();                      //int volt data --> string temperature data = for displaying in text
                    voltM2Text.text = paraData.volt_M2.ToString();
                    //powerText.text = paraData.usage_of_kWh.ToString();                //int power data --> string temperature data = for displaying in text
                    //lgcurrentText.text = paraData.lagging_current.ToString();
                    //ldcurrentText.text = paraData.leading_current.ToString();


                        break;

                    case UnityWebRequest.Result.ConnectionError:                    
                    case UnityWebRequest.Result.ProtocolError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError("something is not okay");                    //something wring with the connection /error
                        break;

                  
                }


                // Add a delay before the next API call
                yield return new WaitForSeconds(5);
            }
        }
    }
}
