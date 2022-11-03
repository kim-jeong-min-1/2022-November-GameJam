using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            instance = FindObjectOfType<T>();

            if(instance == null)
            {
                instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
