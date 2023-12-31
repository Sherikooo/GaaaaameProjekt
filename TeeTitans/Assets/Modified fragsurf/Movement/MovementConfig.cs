﻿using UnityEngine;
namespace Fragsurf.Movement {

    [System.Serializable]
    public class MovementConfig {

        [Header ("Jumping and gravity")]
        public bool autoBhop = true;
        public float gravity = 20f;
        public float jumpForce = 8.5f;
        public int maxJumps = 2;
        
        [Header ("General physics")]
        public float friction = 6f;
        public float maxSpeed = 6f;
        public float maxVelocity = 50f;
        [Range (30f, 75f)] public float slopeLimit = 45f;

        [Header ("Air movement")]
        public bool clampAirSpeed = true;
        public float airCap = 0.4f;
        public float airAcceleration = 0.5f;
        public float airFriction = 0.4f;

        [Header ("Ground movement")]
        public float walkSpeed = 7f;
        public float sprintSpeed = 12f;
        public float acceleration = 14f;
        public float deceleration = 10f;

        [Header ("Crouch movement")]
        public float crouchSpeed = 4f;
        public float crouchAcceleration = 8f;
        public float crouchDeceleration = 4f;
        public float crouchFriction = 3f;

        [Header ("Sliding")]
        public float minimumSlideSpeed = 9f;
        public float maximumSlideSpeed = 18f;
        public float slideSpeedMultiplier = 1.75f;
        public float slideFriction = 14f;
        public float downhillSlideSpeedMultiplier = 2.5f;
        public float slideDelay = 0.5f;
        
    }

}
