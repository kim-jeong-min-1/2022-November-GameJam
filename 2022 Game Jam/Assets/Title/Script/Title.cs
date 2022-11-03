using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{

    public GameObject Helf;

    bool Click;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            transform.position = new Vector2(0, 100);
            Helf.transform.position = new Vector2(0, 0);
            Click = true;
        }

        if(Input.anyKeyDown && Click)
        {
            //·Îµå ¾À
        }
    }
}
