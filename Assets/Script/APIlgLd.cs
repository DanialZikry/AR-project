using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using Vuforia;
using UnityEngine.UI;

public class APIlgLd : MonoBehaviour
{
    public string url = "http://danialzikry.pythonanywhere.com/";
    public static string weatherApiResponseBody = null;
    public ParaData paraData = null;

    public Slider PowerBar;
    public Slider PowerBar1;
    public Image SliderBarColor;
    public Image SliderBar1Color;


    public TMPro.TextMeshProUGUI lgcurrentText;
    public TMPro.TextMeshProUGUI lgcurrent1Text;




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
                        Debug.Log(" lagging current: " + paraData.lagging_current);


                   
                        lgcurrentText.text = paraData.lagging_current.ToString();
                        //lgcurrent1Text.text = paraData.lagging_current.ToString();


                        UpdateLgBarProgress(paraData.lagging_current);
                        UpdateLgBarProgress1(paraData.lagging_current);

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

    void UpdateLgBarProgress(float currentValue)
    {
        PowerBar.value = currentValue;
        if (currentValue > 90)
        {
            SliderBarColor.color = Color.red;
        }
        else if (currentValue > 80 && currentValue < 89)
        {
            SliderBarColor.color = Color.yellow;
        }
        else if (currentValue > 70 && currentValue < 79)
        {
            SliderBarColor.color = Color.green;
        }
    }

    void UpdateLgBarProgress1(float currentValue)
    {
        PowerBar.value = currentValue;
        if (currentValue > 90)
        {
            SliderBar1Color.color = Color.red;
            lgcurrent1Text.text = ("high lagging current");

        }
        else if (currentValue > 80 && currentValue < 89)
        {
            SliderBar1Color.color = Color.yellow;
            lgcurrent1Text.text = ("intermediate lagging current");
        }
        else if (currentValue > 70 && currentValue < 79)
        {
            SliderBar1Color.color = Color.green;
            lgcurrent1Text.text = ("optimal lagging current");
        }
    }
}
