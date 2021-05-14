using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleScaler : MonoBehaviour
{
    private RoomController roomObject;

    // Start is called before the first frame update
    void Start()
    {
        roomObject = GameObject.FindWithTag("Room").GetComponent<RoomController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sc = roomObject.GetInverseScale();
        transform.localScale = new Vector3(1 / sc.x, 1 / sc.y, 1 / sc.z);
    }
}
