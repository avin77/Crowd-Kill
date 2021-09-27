using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    [SerializeField]
    private Vector2 acceleration;
    [SerializeField]
    private Vector2 sensitivity;
    [SerializeField]
    private float inputLagPeriod;
    private Vector2 rotation;
    private bool gameStarted;
    [SerializeField]
    private float x_min, x_max;
    [SerializeField]
    private float y_min, y_max;
    private Vector2 velocity;
    private Vector2 lastInputEvent;
    private float inputLagTimer;
    void Start()
    {
     //   Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) gameStarted = true;
        if (gameStarted)
        {
            Vector2 wantedVelocity = GetInput() * sensitivity;
           // rotation += wantedVelocity * Time.deltaTime;
            transform.localEulerAngles = new Vector3(Mathf.Clamp(rotation.y,y_min,y_max), Mathf.Clamp(rotation.x,x_min,x_max), 0);
            velocity = new Vector2(Mathf.MoveTowards(velocity.x, wantedVelocity.x, acceleration.x * Time.deltaTime),
           Mathf.MoveTowards(velocity.y, wantedVelocity.y, acceleration.y * Time.deltaTime));
            rotation += velocity * Time.deltaTime;
        }
    }

    private Vector2 GetInput() {
        inputLagTimer += Time.deltaTime;
        Vector2 input=new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
        if ((Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y)) == false || inputLagTimer >= inputLagPeriod) {
            lastInputEvent = input;
            inputLagTimer = 0;
        }
        return lastInputEvent;
    }
}
