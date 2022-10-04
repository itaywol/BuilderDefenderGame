using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float zoomAmount;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minOrthographicSize,maxOrthographicSize;

    private float orthographicSize;
    private void Start() {

        orthographicSize = virtualCamera.m_Lens.OrthographicSize;
    }

    void Update()
    {
        float x,y;
        x = Input.GetAxisRaw( "Horizontal" );
        y = Input.GetAxisRaw( "Vertical" );

        Vector3 MoveDir = new Vector3( x, y,0 ).normalized;
        transform.position += moveSpeed * Time.deltaTime * MoveDir;

        orthographicSize += Input.mouseScrollDelta.y * zoomAmount ;
        orthographicSize = Mathf.Clamp( orthographicSize, minOrthographicSize, maxOrthographicSize );
        virtualCamera.m_Lens.OrthographicSize = orthographicSize;

    }
}
