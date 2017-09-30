using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour {

    int size = 20;
    LineRenderer line;
    Queue<Vector3> positions;
    Vector3[] positionsArray;

    private void Start() {
        line = GetComponent<LineRenderer>();
        positions = new Queue<Vector3>();
    }

    private void Update() {
        positions.Enqueue(transform.position);
        if (positions.Count > size) {
            positions.Dequeue();
        }
        positionsArray = positions.ToArray();
        Array.Reverse(positionsArray);
        line.positionCount = positionsArray.Length;
        line.SetPositions(positionsArray);
    }
}
