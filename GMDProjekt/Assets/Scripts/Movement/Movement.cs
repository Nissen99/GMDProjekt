using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Movement : MonoBehaviour, IMovement
{
    private Vector3 _movementIntent;
    private Animator _animator;
    [SerializeField] private int speed;
    [SerializeField] private int rotationSpeed;
    private static readonly int animatorSpeed = Animator.StringToHash("Speed");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_movementIntent != new Vector3(0, 0, 0))
        {      
            var rotation = Quaternion.LookRotation(_movementIntent);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
            _animator.SetFloat(animatorSpeed, _movementIntent.magnitude);
        }
        else
        {
            _animator.SetFloat(animatorSpeed, 0);
        }
    }


    public void Move(Vector3 placeToMove)
    {
        float acceptedErrorMargin = 0.1f;


        if (!CheckEqualWithinErrorMargin(placeToMove, transform.position, acceptedErrorMargin))
        {
            _movementIntent = placeToMove - transform.position;
            _movementIntent.y = 0;
        }
        else
        {
            _movementIntent = new Vector3(0, 0, 0);
        }
    }

    public void StopMoving()
    {
        _movementIntent = new Vector3(0, 0, 0);
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
