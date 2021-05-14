using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomParentController : MonoBehaviour
{
    public GameObject leftWand, rightWand, roomObject;
    private bool isGripping = false;
    private bool isLeft;

    public bool debugToggle;

    private void Update() {
        bool left = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || debugToggle;
        bool right = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);
        if (!isGripping && (left || right)) {
            isGripping = true;
            if (left) {
                transform.LookAt(leftWand.transform);
            } else {
                transform.LookAt(rightWand.transform);
            }
            isLeft = left;
            roomObject.transform.SetParent(transform, true);
            roomObject.GetComponent<RoomController>().HackFix(gameObject);
        } else if (isGripping && (!left && !right)) {
            isGripping = false;
            roomObject.transform.SetParent(null, true);
            roomObject.GetComponent<RoomController>().HackFix(null);
        }

        if (isGripping) {
            if (isLeft) {
                transform.LookAt(leftWand.transform);
            } else {
                transform.LookAt(rightWand.transform);
            }
        }
        
    }
}
