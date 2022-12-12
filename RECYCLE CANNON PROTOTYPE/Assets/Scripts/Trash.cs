using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] float _lerpSpeed = 10f;
    public MaterialType Type;
    public int Size = 1;
    public bool IsBeingHeld = false;

    private void OnTriggerEnter(Collider other)
    {
        if (IsBeingHeld) return;
        if (!other.CompareTag("Player")) return;

        var player = other.GetComponent<PlayerController>();
        if (player.IsHoldingTrash) return;

        player.CurrentTrash = this;
        IsBeingHeld = true;
        BroadcastMessage("Stop");
        StartCoroutine(GoToHands());
    }

    public void GoToPosition() => StartCoroutine(GoToHands());

    IEnumerator GoToHands()
    {
        Vector3 initialPos = transform.localPosition;
        float t = 0;
        while (t < 1)
        {
            t += Time.fixedDeltaTime * _lerpSpeed;
            transform.localPosition = Vector3.Lerp(initialPos, Vector3.zero, t);
            yield return null;
        }
    }
}
