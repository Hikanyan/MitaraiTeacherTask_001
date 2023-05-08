using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - _player.transform.position;
    }

    private void Update()
    {
        transform.position = _player.transform.position + offset;
    }
}
