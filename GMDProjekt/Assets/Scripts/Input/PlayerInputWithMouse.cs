using System;
using System.Collections;
using System.Collections.Generic;
using Combat.AttackManager;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputWithMouse : MonoBehaviour, IPlayerInput
{
    private Vector3? _latestMovementClick;
    [SerializeField] private GameObject toSpawnWhenClicked;
    private delegate void ResetIntents();

    private IPlayerMovement _playerMovement;
    private IAttackManager _attackManager;
    
    private ResetIntents _resetIntents;
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GetComponent<IPlayerMovement>();
        _attackManager = GetComponent<IAttackManager>();

        _resetIntents = resetIntent;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_latestMovementClick != null)
        {
           _playerMovement.Move(_latestMovementClick.Value);
        }
    }

    void OnPrimary(InputValue value)
    {
        _resetIntents.Invoke();
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag(TAGS.GROUND))
            {
                _latestMovementClick = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                Instantiate(toSpawnWhenClicked, new Vector3(hit.point.x, hit.point.y + 0.0f, hit.point.z), Quaternion.identity);
            } 
            
            if (hit.transform.CompareTag(TAGS.ENEMY))
            {
                var enemy = hit.transform.GetComponent<IAttackable>();
                _attackManager.PrimaryAttack(enemy);
                _resetIntents += _attackManager.StopPrimaryAttackIntent;
            }
        }
    }



    void resetIntent()
    {
        _latestMovementClick = null;
        _resetIntents = resetIntent;
    }
}

