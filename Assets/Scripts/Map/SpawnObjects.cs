using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    private void Start()
    {
        foreach (var prop in GameObject.FindGameObjectsWithTag("Transformant"))
        {
            prop.SetActive(false);
            if (Random.value < 0.6f)
            {
                prop.SetActive(true);
            }
            if (Random.value < 0.6f)
            {
                prop.tag = "Untagged";
            }
        }
    }
}
