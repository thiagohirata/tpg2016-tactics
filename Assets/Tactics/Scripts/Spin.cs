using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.up, 360f * Time.deltaTime);
	}
}
