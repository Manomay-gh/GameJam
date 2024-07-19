using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private CharacterController controller;
    private Vector3 move;

    private Vector3 horizontalInput;
    private Vector3 verticalInput;

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        controller.Move(move * speed * Time.fixedDeltaTime);
    }
}
