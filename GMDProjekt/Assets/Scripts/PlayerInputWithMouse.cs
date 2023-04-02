using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputWithMouse : MonoBehaviour, IPlayerInput
{
    public Vector3 MovementIntent { get; private set; }
    private Vector3 _latestMovementClick;
    public GameObject ToSpawnWhenClicked;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        setMovementIntent();
    }

    void OnMove(InputValue value)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 clicked = 
            _latestMovementClick = new Vector3(hit.point.x, hit.point.y, hit.point.z);;
            Instantiate(ToSpawnWhenClicked, new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z), Quaternion.identity);

        }
    }

    private void setMovementIntent()
    {
        float acceptedErrorMargin = 0.1f;
        
        if (!CheckEqualWithinErrorMargin(_latestMovementClick, transform.position, acceptedErrorMargin))
        {
            MovementIntent = _latestMovementClick - transform.position;
        }
        else
        {
            MovementIntent = new Vector3(0, 0, 0);
        }
        
    }

    private bool CheckEqualWithinErrorMargin(Vector3 v1, Vector3 v2, float acceptedError)
    {
        if (Mathf.Abs(v1.x - v2.x) > acceptedError)
        {
            return false;
        }

        if (Mathf.Abs(v1.y - v2.y) > acceptedError)
        {
            return false;
        }

        if (Mathf.Abs(v1.z - v2.z) > acceptedError)
        {
            return false;
        }

        return true;
    }
}

