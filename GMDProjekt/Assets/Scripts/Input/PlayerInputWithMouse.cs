using System;
using System.Collections;
using System.Collections.Generic;
using Combat.AttackManager;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputWithMouse : MonoBehaviour, IPlayerInput
{
    //private Vector3? _latestMovementClick;
    [SerializeField] private GameObject toSpawnWhenClicked;
    private delegate void ResetIntents();

    private IMovement _movement;
    private IAttackManager _attackManager;
    
    private ResetIntents _resetIntents;
    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<IMovement>();
        _attackManager = GetComponent<IAttackManager>();

        _resetIntents = resetIntent;
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    void OnPrimary(InputValue value)
    {
        _resetIntents.Invoke();
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag(TAGS.GROUND_TAG))
            {
               var _latestMovementClick = new Vector3(hit.point.x, hit.point.y, hit.point.z);
               _movement.Move(_latestMovementClick); 
               Instantiate(toSpawnWhenClicked, new Vector3(hit.point.x, hit.point.y + 0.0f, hit.point.z), Quaternion.identity);
            } 
            
            if (hit.transform.CompareTag(TAGS.ENEMY_TAG))
            {
                var enemy = hit.transform.GetComponent<IAttackable>();
                _attackManager.PrimaryAttack(enemy);
                _resetIntents += _attackManager.StopPrimaryAttackIntent;
            }
        }
    }

    void OnSecondary(InputValue value)
    {
        var hit = actionTaken();
        if (hit == null)
        {
            return;
        }

        if (hit.Value.transform.CompareTag(TAGS.GROUND_TAG))
        {
            _attackManager.SecondaryAttack(hit.Value.transform.position);
        } else if (hit.Value.transform.CompareTag(TAGS.ENEMY_TAG))
        {
            _attackManager.SecondaryAttack(hit.Value.transform.position, hit.Value.transform.GetComponent<IAttackable>());
        }
    }

    private RaycastHit? actionTaken()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit;
        }

        return null;
    }



    void resetIntent()
    {
      //  _latestMovementClick = null;
        _resetIntents = resetIntent;
    }
}

