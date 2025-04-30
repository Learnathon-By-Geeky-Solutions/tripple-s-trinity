using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectsOfType<BackgroundMusic>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
