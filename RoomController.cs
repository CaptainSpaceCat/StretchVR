using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject parentObject;
    private bool hackFix2 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector3 deformScale = Vector3.one;

    private void Update() {
        if (!hackFix2 && parentObject != null) {
            parentObject.transform.localScale = deformScale;
            transform.localScale = Vector3.one;
        }
    }

    public Vector3 GetInverseScale() {
        if (parentObject != null) {
            Debug.Log("parent");
            Debug.Log(parentObject.transform.localScale);
            return parentObject.transform.localScale;
        }
        Debug.Log("self");
        Debug.Log(transform.localScale);
        return transform.localScale;
    }

    public void HackFix(GameObject p) {
        parentObject = p;
        if (p != null) {
            hackFix2 = true;
        } else {
            hackFix2 = false;
        }
    }

    public GameObject SetTempParent(Vector3 pos) {
        GameObject temp = new GameObject();
        parentObject = Instantiate(temp, pos, transform.rotation);
        transform.SetParent(parentObject.transform);
        //parentObject.transform.localScale = deformScale;
        //transform.localScale = Vector3.one;
        return parentObject;
    }

    public void ReleaseTempParent() {
        //transform.localScale = 
        //transform.SetParent(null);
    }
}
