using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundParticlesController : MonoBehaviour {

    ParticleSystem emitter;

    public void BurstUp(Vector3 pos) {
        transform.position = pos;
        transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        emitter.Play();
    }
    public void BurstDown(Vector3 pos) {
        transform.position = pos;
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        emitter.Play();
    }


    void Start () {
        emitter = GetComponent<ParticleSystem>();
	}

}
