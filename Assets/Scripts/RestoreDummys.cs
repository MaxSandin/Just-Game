using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreDummys : MonoBehaviour
{
    public GameObject listDummys;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (Transform child in listDummys.transform)
                child.gameObject.SetActive(true);
        }
    }
}
