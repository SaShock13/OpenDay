using Assets._Game.SCRIPTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FireSpreading : MonoBehaviour
{
    private List<Flamable> flamablesList;
    private Flamable flamable = null;

    private void Awake()
    {
        flamablesList = new List<Flamable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"In Trigger : {other.name}");
        if (other.TryGetComponent<Flamable>(out flamable))
        {
            if (!flamable.isOnFire)
            {
                if (!flamablesList.Contains(flamable))
                {
                    flamable.heatingSpeed /= 2;
                    flamablesList.Add(flamable);
                    Debug.Log($"Added to list : {flamable.name}");
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (flamablesList.Count > 0)
        {
            foreach (var flamable in flamablesList)
            {
                flamable.Heating();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Flamable>(out flamable))
        {            
            if (flamablesList.Contains(flamable))
            {
                flamablesList.Remove(flamable);
                Debug.Log($"Removed from list : {flamable.name}");
            }
        }
    }
}
