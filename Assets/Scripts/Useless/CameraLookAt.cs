using System;
using UnityEngine;

namespace Useless
{
    public class CameraLookAt : MonoBehaviour
    {
        public Vector2 speed;
        
        private void Update()
        {
            Vector2 velocity = Vector2.zero;
            if (Input.GetKey(KeyCode.W)) velocity.x += transform.forward.normalized.x;
            if (Input.GetKey(KeyCode.S)) velocity.x += -transform.forward.normalized.x;
            if (Input.GetKey(KeyCode.D)) velocity.x += transform.right.normalized.x;
            if (Input.GetKey(KeyCode.A)) velocity.x += -transform.right.normalized.x;
            
            if (Input.GetKey(KeyCode.Space)) velocity.y += 1f;
            if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl)) velocity.y += -1;

            velocity *= speed * Time.deltaTime;

            transform.position += (Vector3)velocity;

        }
    }
    
}
