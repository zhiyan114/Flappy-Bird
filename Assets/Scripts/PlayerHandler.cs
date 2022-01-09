using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    private PlayerInput PlrInput;
    private Rigidbody2D rb;
    private void Awake()
    {
        PlrInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Jump()
    {
        rb.velocity = Vector2.up * 65;
    }
    public void JumpInput(InputAction.CallbackContext cb)
    {
        if(cb.phase == InputActionPhase.Started)
            Jump();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("BOI");
    }
}
