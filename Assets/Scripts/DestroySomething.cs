using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySomething : MonoBehaviour
{
    public void DestroySomethingNow(GameObject input)
    {
        Destroy(input);
    }
}
