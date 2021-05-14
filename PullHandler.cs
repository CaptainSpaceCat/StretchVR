using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullHandler : Handle
{
    public ParticleSystem highlights;
    public float range = 1000f;
    public GameObject waypoint;

    public override void StoreNeededData() {
        //roomDeltaPos = roomObject.transform.position - grabber.transform.position;
    }

    public override void HandleGrab() {
        //playerObject.transform.position = waypoint.transform.position;
        Vector3 goal = waypoint.transform.position - playerObject.transform.position;
        playerObject.transform.Translate(goal * .03f);
    }

    public void SetHighlights(bool state) {
        if (state && highlights.isStopped) {
            highlights.Play();
        } else if (!state && highlights.isPlaying) {
            highlights.Stop();
        }
    }

    public override bool TrySetHighlighted(bool state, Vector3 pos, Vector3 hit) {
        if (!state || Vector3.Distance(pos, hit) > .8f) {
            SetHighlights(state);
            return true;
        }
        return false;
    }
}
