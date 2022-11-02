using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    float currtime;
    public GameObject square;
    GameObject ForDestroy;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        currtime += Time.deltaTime;
        if (currtime > 0.7)
        {
            float newX = Random.Range(-8f, 8f), newY = Random.Range(-5f, 5f);

            GameObject monstor = Instantiate(square);

            monstor.transform.position= new Vector2(newX, newY);

            currtime = 0;

        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(ray.origin, Vector2.zero);

            if(hit.transform.gameObject != null)
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
