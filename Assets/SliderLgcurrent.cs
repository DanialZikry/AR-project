using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using Vuforia;
using UnityEngine.UI;

public class SliderLgcurrent : MonoBehaviour
{
    public string url = "http://danialzikry.pythonanywhere.com/";
    public static string weatherApiResponseBody = null;
    public ParaData paraData = null;

    public Slider PowerBar;
    public Image SliderBarColor;
    //public TMPro.TextMeshProUGUI lgcurrentText;



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
                        Debug.Log(" lagging current: " + paraData.lagging_current + " leading current: " + paraData.leading_current);

                
                        UpdateLgBarProgress(paraData.lagging_current);
                        UpdateLdBarProgress(paraData.leading_current);

                        break;

                  
                }
                // Add a delay before the next API call
                yield return new WaitForSeconds(5);
            }
        }
    }
    void UpdateLgBarProgress(float currentValue)
    {
        PowerBar.value = currentValue;
        if (currentValue > 90 )
        {
            SliderBarColor.color = Color.red;
        }
        else if (currentValue > 80 && currentValue < 89)
        {
            SliderBarColor.color = Color.green;
        }
        else if (currentValue > 70 && currentValue < 79)
        {
            SliderBarColor.color = Color.yellow;
        }
    }

    void UpdateLdBarProgress(float currentValue)
    {
        PowerBar.value = currentValue;
        if (currentValue > 95)
        {
            SliderBarColor.color = Color.red;
        }
        else if (currentValue > 90 && currentValue < 94)
        {
            SliderBarColor.color = Color.green;
        }
        
    }
}
