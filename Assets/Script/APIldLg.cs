using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using Vuforia;
using UnityEngine.UI;

public class APIldLg : MonoBehaviour
{
    public string url = "http://danialzikry.pythonanywhere.com/";
    public static string weatherApiResponseBody = null;
    public ParaData paraData = null;

    public Slider PowerBar;
    public Slider PowerBar1;
    public Image SliderBarColor;
    public Image SliderBar1Color;


    public TMPro.TextMeshProUGUI ldcurrentText;
    public TMPro.TextMeshProUGUI ldcurrent1Text;




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
                        Debug.Log(" leading current: " + paraData.leading_current);



                        ldcurrentText.text = paraData.leading_current.ToString();


                        UpdateLdBarProgress(paraData.leading_current);
                        UpdateLdBarProgress1(paraData.leading_current);

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

    void UpdateLdBarProgress(float currentValue)
    {
        PowerBar.value = currentValue;
        if (currentValue > 97)
        {
            SliderBarColor.color = Color.red;
        }
        else if (currentValue > 94 && currentValue < 96)
        {
            SliderBarColor.color = Color.yellow;
        }
        else if (currentValue > 90 && currentValue < 93)
        {
            SliderBarColor.color = Color.green;
        }
    }

    void UpdateLdBarProgress1(float currentValue)
    {
        PowerBar.value = currentValue;
        if (currentValue > 97)
        {
            SliderBar1Color.color = Color.red;
            ldcurrent1Text.text = ("high leading current");

        }
        else if (currentValue > 94 && currentValue < 96)
        {
            SliderBar1Color.color = Color.yellow;
            ldcurrent1Text.text = ("intermediate leading current");
        }
        else if (currentValue > 90 && currentValue < 93)
        {
            SliderBar1Color.color = Color.green;
            ldcurrent1Text.text = ("optimal leading current");
        }
    }
}
