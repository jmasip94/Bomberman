﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="FSM/Decisions/PutBomb")]
public class PutBombDecision : Decision {

    public override bool Decide(CreepFSM controller)
    {
        bool deployBomb = CheckNearDestroyableWall(controller);
        return deployBomb;
    }

    private bool CheckNearDestroyableWall(CreepFSM controller)
    {
        if (Physics2D.OverlapCircle(controller.transform.position, 2.0f, LayerMask.GetMask("Bombs")) != null)
            return false;
        Debug.DrawRay(controller.transform.position, Vector3.up, Color.red);
        Debug.DrawRay(controller.transform.position, Vector3.right, Color.red);
        Debug.DrawRay(controller.transform.position, Vector3.left, Color.red);
        Debug.DrawRay(controller.transform.position, Vector3.down, Color.red);

        Vector2 boxSize = new Vector2(controller.grid.cellSize.x * 0.9f, controller.grid.cellSize.y * 0.9f);
        float distance = 0.1f;
        LayerMask Mask = LayerMask.GetMask("Destroyable Walls");
        RaycastHit2D hitUp = Physics2D.BoxCast(controller.transform.position, boxSize, 0.0f, Vector2.up, distance, Mask);
        RaycastHit2D hitDown = Physics2D.BoxCast(controller.transform.position, boxSize, 0.0f, Vector2.down, distance, Mask);
        RaycastHit2D hitRight = Physics2D.BoxCast(controller.transform.position, boxSize, 0.0f, Vector2.right, distance, Mask);
        RaycastHit2D hitLeft = Physics2D.BoxCast(controller.transform.position, boxSize, 0.0f, Vector2.left, distance, Mask);

        if(hitUp.collider != null || hitDown.collider != null || hitRight.collider != null || hitLeft.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}