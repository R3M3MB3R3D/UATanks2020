using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public PowerUp powerup;
    public AudioClip FeedbackAudioClip;
    private Transform tf;

    private void Start()
    {
        tf = gameObject.GetComponent<Transform>();
    }

    public void OnTriggerEnter(Collider other)
    {
        PowerControl powerControl = other.gameObject.GetComponent<PowerControl>();
        if (powerControl != null)
        {
            powerControl.AddPower(powerup);
            if (FeedbackAudioClip != null)
            {
                AudioSource.PlayClipAtPoint(FeedbackAudioClip, tf.position, 1.0f);
            }
            Destroy(this.gameObject);
        }
    }
}
