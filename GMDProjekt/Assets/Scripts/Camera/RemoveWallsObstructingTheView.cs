using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using UnityEngine;

public class RemoveWallsObstructingTheView : MonoBehaviour
{
    public GameObject Player;

    private List<GameObject> removedWalls;
    private float timeSinceLastCheck;
    private float checkInterval = 0.1f; // minimum time in seconds between calls to HandleIfObstructingView

    private void Awake()
    {
        removedWalls = new List<GameObject>();
    }

    // DONT ASK ! :P This is horrible, but it works for now, and i dont think it was worth the time to fix something 
    // that works, i would much rather do some cool stuff
    private void LateUpdate()
    {
        if (Player == null)
        {
            return;
        }
        timeSinceLastCheck += Time.deltaTime;
        if (timeSinceLastCheck >= checkInterval)
        {
            var objectThatBlocked = HandleIfObstructingView();
            if (objectThatBlocked != null)
            {
                removedWalls.Add(objectThatBlocked);
            }

            timeSinceLastCheck = 0f;
            for (int i = removedWalls.Count - 1; i >= 0; i--)
            {
                GameObject wall = removedWalls[i];
                wall.SetActive(true);
                objectThatBlocked = HandleIfObstructingView();
                if (!objectThatBlocked)
                {
                    removedWalls.RemoveAt(i);
                }
            }
        }
    }
    
    //This makes a cone so it will hit any wall close to my player obstructing the view
    [CanBeNull]
    private GameObject HandleIfObstructingView()
    {
        float maxAngle = 45f; 
        int numRays = 5;
        Vector3 direction = Player.transform.position - transform.position;
        for (int i = 0; i < numRays; i++)
        {
            float angle = Mathf.Lerp(-maxAngle, maxAngle, (float)i / (numRays - 1));
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 rayDirection = rotation * direction;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rayDirection, out hit))
            {
                if (hit.transform.gameObject.CompareTag(TAGS.WALL_TAG))
                {
                    hit.transform.gameObject.SetActive(false);
                    return hit.transform.gameObject;
                }
            }
        }

        return null;
    }
}