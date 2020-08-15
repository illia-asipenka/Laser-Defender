using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float _backScrollSpeed = 0.5f;
    private Material _myMaterial;
    private Vector2 _offset;
    
    void Start()
    {
        _myMaterial = GetComponent<Renderer>().material;
        _offset = new Vector2(0f, _backScrollSpeed);
    }

    void Update()
    {
        _myMaterial.mainTextureOffset += _offset * Time.deltaTime;
    }
}
