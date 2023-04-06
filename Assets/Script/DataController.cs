using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class DataController : MonoBehaviour
{
    public static string API_URL = "https://api.open-meteo.com/v1/forecast?latitude=5.40&longitude=100.59&hourly=temperature_2m,relativehumidity_2m";
    public static string weatherApiResponseBody = null;
    public WeatherData weatherData = null;

    public TMPro.TextMeshProUGUI timeText;
    public TMPro.TextMeshProUGUI temprText;
    public TMPro.TextMeshProUGUI humText;

    private void Awake()
    {
        StartCoroutine(GetRequest());
    }

    private IEnumerator GetRequest()
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(API_URL))
        {
            yield return unityWebRequest.SendWebRequest();

            switch (unityWebRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log("Data received is ok!");
                    weatherApiResponseBody = unityWebRequest.downloadHandler.text;
                    weatherData = JsonUtility.FromJson<WeatherData>(weatherApiResponseBody);
                    Debug.Log("First element :" + weatherData.hourly.time[0] + ": tempr." + weatherData.hourly.temperature_2m[0] + ": Hum." + weatherData.hourly.relativehumidity_2m[0]);
                    
                    timeText.text += weatherData.hourly.time[0];
                    temprText.text += weatherData.hourly.temperature_2m[0];
                    humText.text += weatherData.hourly.relativehumidity_2m[0];
                    break;

                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("something is not okay");
                    break;
            }
        }

    }
}
