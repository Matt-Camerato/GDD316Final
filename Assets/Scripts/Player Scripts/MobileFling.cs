using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileFling : MonoBehaviour
{
    private FlingController _flingController;

    private void Awake()
    {
        transform.root.TryGetComponent(out _flingController);
    }

    private void OnMouseDrag()
    {
        if(Input.GetMouseButtonDown(1)) return;
        _flingController._startPosition = _flingController.rb.transform.position;
        _flingController._isDragging = true;
    }

    private void OnMouseUp()
    {
        _flingController.releasedDrag = true;
    }
}
