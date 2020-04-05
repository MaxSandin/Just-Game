using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject player;

    public void ResetToDefault()
    {
        Vector3 defaultPos = new Vector3(0, 1, 0);
        player.transform.position = defaultPos;
    }
}
