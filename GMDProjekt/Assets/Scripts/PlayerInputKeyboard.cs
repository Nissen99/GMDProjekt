using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputKeyboard : MonoBehaviour, IPlayerInput
{

    public Vector3 MovementIntent { get; set; }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnMovement(InputValue value)
    {
        Vector2 inputMovement = value.Get<Vector2>();
        
        MovementIntent = new Vector3(inputMovement.x, 0, inputMovement.y);
    }
}
