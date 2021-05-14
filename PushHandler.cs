using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushHandler : Handle
{
    public Vector3 scalingAxis;
    private Vector3 roomDeltaPos;
    private Vector3 grabberDeltaPos;
    public ParticleSystem highlights;
    public float range = 3f;
    private float baseDistance;
    private Vector3 baseScale;
    private Handle otherHandle;

    private GameObject roomParentObject;
    private bool prepped = false;

    public ScaleSoundManager ssm;

    public override void StoreNeededData() {
        roomObject = GameObject.FindWithTag("Room");
        roomDeltaPos = roomObject.transform.position - grabber.transform.position;
        grabberDeltaPos = transform.position - grabber.transform.position;

        //if we're the second handle, see if this is a valid axis of scaling
        if (!isLeader) {
            /*RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit) && hit.collider.gameObject.tag == "HandleCollider") {
                otherHandle = hit.collider.gameObject.GetComponentInParent<PushHandler>();
                baseDistance = Vector3.Distance(transform.position, otherHandle.transform.position);
            } else {
                isLeader = true;
            }*/
            otherHandle = WandController.firstHandle;
            baseDistance = Vector3.Distance(transform.position, otherHandle.transform.position);
            baseScale = roomObject.GetComponent<RoomController>().deformScale;
            ssm = GameObject.FindWithTag("ssm").GetComponent<ScaleSoundManager>();
            ssm.SetPlaying(true);
        } else {
            Debug.Log("setting temp parent");
            roomParentObject = roomObject.GetComponent<RoomController>().SetTempParent(grabber.transform.position); //grabber.transform.position
            //roomDeltaPos = roomParentObject.transform.position - grabber.transform.position;
        }

        prepped = true;
    }

    public override void HandleGrab() {
        if (prepped) {
            if (isLeader) {
                //Vector3 trueDelta = Vector3.Scale(roomDeltaPos, WandController.deformScale);
                //roomObject.transform.position = grabber.transform.position + trueDelta;
                roomParentObject.transform.position = grabber.transform.position;// + Vector3.Scale(roomDeltaPos, roomObject.GetComponent<RoomController>().GetInverseScale());

            } else {
                float newDist = Mathf.Abs(Vector3.Magnitude(grabber.transform.position + grabberDeltaPos - otherHandle.transform.position));
                //Debug.Log(otherHandle.transform.position);
                //Debug.Log(grabber.transform.position + grabberDeltaPos);
                //Debug.Log(newDist);
                //Debug.Log(baseDistance);
                Vector3 scaler = (Vector3.one - scalingAxis) + (scalingAxis * (newDist / baseDistance));
                Debug.Log(scaler);
                Debug.Log(baseScale);
                Debug.Log(Vector3.Scale(baseScale, scaler));
                roomObject.GetComponent<RoomController>().deformScale = Vector3.Scale(baseScale, scaler);
                ssm.SetPitch(baseDistance / newDist);
            }
        }
    }

    public override void HandleRelease() {
        if (isLeader) {
            roomObject.GetComponent<RoomController>().ReleaseTempParent();
        } else {
            ssm.SetPlaying(false);
        }
        prepped = false;
    }

    public void SetHighlights(bool state) {
        if (state && highlights.isStopped) {
            highlights.Play();
        } else if (!state && highlights.isPlaying) {
            highlights.Stop();
        }
    }

    public override bool TrySetHighlighted(bool state, Vector3 pos, Vector3 hit) {
        if (!state || Vector3.Distance(pos, hit) < range) {
            SetHighlights(state);
            return true;
        }
        return false;
    }
}
