using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHandler : Handle
{
    public ParticleSystem highlights;
    public float range = 2f;
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
