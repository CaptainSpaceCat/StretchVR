using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    private bool isGrabbed = false;
    public bool isLeader;
    public WandController grabber;
    private Vector3 relativePos;

    public GameObject roomObject;
    public GameObject playerObject;
    private GameObject parentHolder;

    private void Start() {
        roomObject = GameObject.FindWithTag("Room");
        playerObject = GameObject.FindWithTag("Player");
        GameObject temp = new GameObject();
        parentHolder = Instantiate(temp, transform.position, roomObject.transform.rotation, roomObject.transform);
        transform.SetParent(parentHolder.transform);
        parentHolder.AddComponent<HandleScaler>();
    }

    private void Update() {
        //Vector3 parentScale = transform.parent.localScale;
        //transform.localScale = new Vector3(1/parentScale.x, 1/parentScale.y, 1/parentScale.z);
    }

    private void FixedUpdate() {
        if (isGrabbed) {
            HandleGrab();
        }
    }

    public virtual void HandleGrab() { }
    public virtual void HandleRelease() { }
    public virtual void StoreNeededData() { }
    public virtual bool TrySetHighlighted(bool state, Vector3 pos, Vector3 hit) { return false; }

    public bool OnGrab(WandController wand, bool main) {
        if (!isGrabbed) {
            isGrabbed = true;
            isLeader = main;
            grabber = wand;
            StoreNeededData();
            return true;
        }
        return false;
    }

    public void OnRelease() {
        isGrabbed = false;
        HandleRelease();
    }

    public void RepositionToScale(Vector3 scale) {
        transform.position = Vector3.Scale(relativePos, scale);
    }

    public void SetRelativePos(Vector3 pos) {
        relativePos = pos;
    }
}
