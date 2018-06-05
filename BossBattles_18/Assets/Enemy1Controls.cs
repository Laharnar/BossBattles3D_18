﻿using System;
using UnityEngine;

public partial class Enemy1Controls : UnitControls {
    public Transform tTarget;
    PlayerControls player;
    
    public AttackAction lightAttack;
    public SpawnAction aoeAttack;

    public override Vector3 GetDirSmooth() {
        PlayerControls player = GetPlayer();
        return (player.transform.position - tTarget.position).normalized;
    }

    private PlayerControls GetPlayer() {
        if (!player) {
            player = GameObject.FindObjectOfType<PlayerControls>();
        }
        return player;
    }

    public override UnitAction[] GetActions() {
        return new UnitAction[] { lightAttack, aoeAttack };
    }
    public override bool[] GetInputs() {
        PlayerControls player = GetPlayer();
        
        bool lightAttack = player && Vector3.Distance(player.transform.position, tTarget.position)<2f;
        return new bool[] { lightAttack, aoeAttack.attack.IsReady() };
    }

    public override Vector3 GetRawDirectionInput() {
        PlayerControls player = GetPlayer();
        return (player.transform.position - tTarget.position).normalized;
    }

    public override void PostMoveUpdate() {
        //nothing
    }

}
