﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{

    public float speed = 6;
    public int playerNum = 0;
    public bool useMouse = false;
    int floorMask;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        floorMask = LayerMask.GetMask("RaycastFloor");
        rb = GetComponent<Rigidbody>();
    }

    private void Movement()
    {
        Vector3 movement = Vector3.zero;
        float h = Input.GetAxis("Horizontal_" + playerNum.ToString());
        float v = Input.GetAxis("Vertical_" + playerNum.ToString());
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    private void AxisTurn()
    {
        Vector3 turn = Vector3.zero;
        float h = Input.GetAxis("AimHorizontal_" + playerNum.ToString());
        float v = Input.GetAxis("AimVertical_" + playerNum.ToString());
        turn.Set(h, 0f, v);
        Turning(turn);
    }

    private void MouseTurn()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, 100f, floorMask))
        {
            Vector3 lookVector = floorHit.point - transform.position;
            print(lookVector);
            Turning(lookVector);
        }
    }

    private void Turning(Vector3 lookVector)
    {
        lookVector.y = 0f;
        if (!lookVector.Equals(Vector3.zero))
        {
            Quaternion newRotation = Quaternion.LookRotation(lookVector);
            rb.MoveRotation(newRotation);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        if (useMouse)
        {
            MouseTurn();
        }
        else
        {
            AxisTurn();
        }
    }
}