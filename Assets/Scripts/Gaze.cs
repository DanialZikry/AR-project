using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Gaze : MonoBehaviour
{
    List<InfoBehaviourScript> infos = new List<InfoBehaviourScript>();

    void Start()
    {
        infos = FindObjectsOfType<InfoBehaviourScript>().ToList();
    }
     void Update()
    {
        if (Physics.Raycast(transform.position,transform.forward, out RaycastHit hit))
        {
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("hasInfo"))
            {
                print("HERE");
            }
        }
    }
}
