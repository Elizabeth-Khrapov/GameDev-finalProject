using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMotion : MonoBehaviour
{
    private float _speed;
    private float _angularSpeed = 1f;
    private float _rotationAngle;
    private GameObject _train;
    private float _minX,_minZ,_maxX,_maxZ;

    // Start is called before the first frame update
    void Start()
    {
        _speed = 0f;
        _rotationAngle = 0f;
        _train = GetComponent<GameObject>();
        _minX = Terrain.activeTerrain.terrainData.bounds.min.x;
        _minZ = Terrain.activeTerrain.terrainData.bounds.min.z;
        _maxX = Terrain.activeTerrain.terrainData.bounds.min.x + Terrain.activeTerrain.terrainData.size.x;
        _maxZ = Terrain.activeTerrain.terrainData.bounds.min.z + Terrain.activeTerrain.terrainData.size.z;

    }

    // Update is called once per frame
    void Update()
    {
        // get mouse X-coordinate
        float mouse_x = Input.GetAxis("Mouse X");

        if (Input.GetKey(KeyCode.W))
            _speed += 0.05f;
        else if (Input.GetKey(KeyCode.S))
            _speed -= 0.05f;

        // sets sight direction by means of transform.Rotate
        _rotationAngle += mouse_x * _angularSpeed *Time.deltaTime;
        transform.Rotate(0,_rotationAngle, 0);

        // Translate is one of transformatioins that uses Vector3
        Vector3 point = Vector3.forward * Time.deltaTime * _speed;
        if (transform.position.x <= _minX || transform.position.x >= _maxX || transform.position.z <= _minZ ||
           transform.position.z >= _maxZ) point.y = 0;
        else // update height to terrain height in point (position.x,,position.z)
        {
            Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
            point.y = 1.6f+Terrain.activeTerrain.SampleHeight(pos)-transform.position.y; // delta in Y direction
         }
        transform.Translate(point);

        // // we shall use CharacterController to move and to stop if camera collides with another object
        // Vector3 direction = transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
        // _characterController.Move(direction);
    }
}