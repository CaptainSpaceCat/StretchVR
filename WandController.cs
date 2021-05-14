using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
    private RoomController roomObject;
    public GameObject playerObject;

    public bool isLeftController;
    public GameObject ballMarker;

    private bool isHighlighting = false;
    private Handle highlightedHandle;
    private bool isGrabbing = false;
    private static Handle lGrabbedHandle;
    private static Handle rGrabbedHandle;
    public static int numGrabbed = 0;
    public static Vector3 deformScale;

    public int VisualizeNumGrabbed = 0;

    public bool debugToggle = false;

    public static Handle firstHandle;

    // Start is called before the first frame update
    void Start()
    {
        deformScale = Vector3.one;
        roomObject = GameObject.FindWithTag("Room").GetComponent<RoomController>();
    }

    // Update is called once per frame
    void Update()
    {
        VisualizeNumGrabbed = numGrabbed;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit)) {
            ballMarker.transform.position = hit.point;
            if (hit.collider.tag == "HandleCollider" && hit.collider.gameObject.GetComponentInParent<Handle>().TrySetHighlighted(true, transform.position, hit.point)) {
                isHighlighting = true;
                highlightedHandle = hit.collider.gameObject.GetComponentInParent<Handle>();
            } else {
                if (isHighlighting) {
                    highlightedHandle.TrySetHighlighted(false, transform.position, hit.point);
                }
                isHighlighting = false;
            }
        }

        bool isPushed = debugToggle || (isLeftController && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) || (!isLeftController && OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger));
        if (!isGrabbing && isHighlighting && isPushed) {
            if (highlightedHandle.OnGrab(this, numGrabbed == 0)) {
                if (numGrabbed == 0) {
                    firstHandle = highlightedHandle;
                }
                isGrabbing = true;
                if (isLeftController) {
                    lGrabbedHandle = highlightedHandle;
                } else {
                    rGrabbedHandle = highlightedHandle;
                }
                numGrabbed++;
            }
        } else if (isGrabbing && !isPushed) {
            isGrabbing = false;
            if (isLeftController) {
                lGrabbedHandle.OnRelease();
            } else {
                rGrabbedHandle.OnRelease();
            }
            numGrabbed--;
        }

    }
}
