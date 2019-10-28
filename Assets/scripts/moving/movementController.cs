using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementController : RayCastController {

	public float maxAngle = 75;


	public LayerMask collisionMask;

	public CollisionInfo collisions;

	public override void Start(){
		base.Start ();
		collisions.faceDir = 1;
	}

	public void dash(float distance, Vector3 v){
		Vector3 velocity = new Vector3 (v.x *distance, 0, v.z * distance);
		move (velocity);
	}

	public void move(Vector3 velocity, bool standingOnPlatform = false){
		UpdateRaycastOrigings ();
		collisions.Reset ();
		collisions.velocityOld = velocity;

       
            collisions.faceDir = (int)Mathf.Sign(velocity.x);
       
        if (velocity.z != 0)
        {
            ZCollisions(ref velocity);
            collisions.faceDirZ = (int)Mathf.Sign(velocity.z);
        }

        HorizontalCollisions(ref velocity);




        transform.Translate (velocity);
		if (standingOnPlatform) {
			collisions.below = true;
		}
	}

    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i2 = 0; i2 < ZRayCount; i2++)
        {
            for (int i = 0; i < VRayCount; i++)
            {
                Vector3 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeftBack : raycastOrigins.topLeftBack;
                rayOrigin += transform.forward * (ZRaySpacing * i2);
                rayOrigin += transform.right * (VRaySpacing * i + velocity.x);
                RaycastHit hit;
                Physics.Raycast(rayOrigin, transform.up * directionY, out hit, rayLength, collisionMask);
                Debug.DrawRay(rayOrigin, transform.up * directionY, Color.red);
                if (hit.collider)
                {
                    velocity.y = (hit.distance - skinWidth) * directionY;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngel * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                    }

                    collisions.above = directionY == 1;
                    collisions.below = directionY == -1;
                    collisions.lastTag = hit.collider.tag;
                }
            }
        }
    }

    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = collisions.faceDir;
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        if (Mathf.Abs(velocity.x) < skinWidth)
        {
            rayLength = skinWidth * 2;
        }
        for (int i = 0; i < HRayCount; i++)
        {
            for (int i2 = 0; i2 < ZRayCount; i2++)
            {
                Vector3 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeftBack : raycastOrigins.bottomRightBack;
                rayOrigin += transform.forward * (ZRaySpacing * i2);
                rayOrigin += transform.up * (HRaySpacing * i);
                RaycastHit hit;
                Physics.Raycast(rayOrigin, transform.right * directionX, out hit, rayLength, collisionMask);
                Debug.DrawRay(rayOrigin, transform.right * directionX, Color.red);
                if (hit.collider)
                {

                    float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

                    //if (!collisions.climbingSlope || slopeAngle > maxAngle)
                    velocity.x = (hit.distance - skinWidth) * directionX;

                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        velocity.y = Mathf.Tan(collisions.slopeAngel * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    collisions.right = directionX == 1;
                    collisions.left = directionX == -1;
                    collisions.lastTag = hit.collider.tag;
                }
            }
        }
    }

    void ZCollisions(ref Vector3 velocity)
    {
        float directionZ = Mathf.Sign(velocity.z);
        float rayLength = Mathf.Abs(velocity.z) + skinWidth;

        if (Mathf.Abs(velocity.z) < skinWidth)
        {
            rayLength = skinWidth * 2;
        }

        for (int i2 = 0; i2 < VRayCount; i2++)
        {
            for (int i = 0; i < HRayCount; i++)
            {
                Vector3 rayOrigin = (directionZ == -1) ? raycastOrigins.bottomLeftBack : raycastOrigins.bottomLeftFront;
                rayOrigin += transform.right * (VRaySpacing * i2);
                rayOrigin += transform.up * (HRaySpacing * i);
                RaycastHit hit;
                Physics.Raycast(rayOrigin, transform.forward * directionZ, out hit, rayLength, collisionMask);
                Debug.DrawRay(rayOrigin, transform.forward * directionZ, Color.red);
                if (hit.collider)
                {

                    float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

                    //if (!collisions.climbingSlope || slopeAngle > maxAngle) 
                    velocity.z = (hit.distance - skinWidth) * directionZ;

                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        velocity.y = Mathf.Tan(collisions.slopeAngel * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    collisions.front = directionZ == 1;
                    collisions.back = directionZ == -1;
                    collisions.lastTag = hit.collider.tag;
                }
            }
        }
    }
    /*
    void ClimbSlope(ref Vector2 velocity, float slopeAngel){
		float moveDistance = Mathf.Abs (velocity.x);
		float velocityY = Mathf.Sin (slopeAngel * Mathf.Deg2Rad) * moveDistance;
		if (velocity.y <= velocityY) {
			velocity.y = velocityY;
			velocity.x = Mathf.Cos (slopeAngel * Mathf.Deg2Rad) * moveDistance * Mathf.Sign (velocity.x);
			collisions.below = true;
			collisions.climbingSlope = true;
			collisions.slopeAngel = slopeAngel;
		}
	}
	void DescendSlope(ref Vector2 velocity){

		RaycastHit2D maxSlopeHitLeft = Physics2D.Raycast (raycastOrigins.bottomLeft, Vector2.down, Mathf.Abs (velocity.y) + skinWidth, collisionMask);
		RaycastHit2D maxSlopeHitRight = Physics2D.Raycast (raycastOrigins.bottomRight, Vector2.down, Mathf.Abs (velocity.y) + skinWidth, collisionMask);
		if (maxSlopeHitLeft ^ maxSlopeHitRight) {
			SlideDownMaxSlope (maxSlopeHitLeft, ref velocity);
			SlideDownMaxSlope (maxSlopeHitRight, ref velocity);
		}
		if (!collisions.slidingDownMaxSlope) {
			float directionX = Mathf.Sign (velocity.x);
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);
			if (hit) {
				float slopeAngle = Vector2.Angle (hit.normal, Vector2.up);
				if (slopeAngle != 0 && slopeAngle <= maxAngle) {
					if (Mathf.Sign (hit.normal.x) == directionX) {
						if (hit.distance - skinWidth <= Mathf.Tan (slopeAngle * Mathf.Deg2Rad) * Mathf.Abs (velocity.x)) {
							float moveDistance = Mathf.Abs (velocity.x);
							float velocityY = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * moveDistance;
							velocity.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign (velocity.x);
							velocity.y -= velocityY;
							collisions.slopeAngel = slopeAngle;
							collisions.descendingSlope = true;
							collisions.below = true;

						}
					}
				}
			}
		}
	}

	void SlideDownMaxSlope(RaycastHit2D hit, ref Vector2 velocity){
		if(hit){
			float slopeAngle = Vector2.Angle (hit.normal, Vector2.up);
			if (slopeAngle != 0 && slopeAngle > maxAngle) {
				velocity.x = Mathf.Sign(hit.normal.x) *(Mathf.Abs (velocity.y) - hit.distance) / Mathf.Tan (slopeAngle * Mathf.Deg2Rad);
				collisions.slopeAngel = slopeAngle;
				collisions.slidingDownMaxSlope = true;
			}
		}
	}*/
    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public bool front, back;

        public bool hangingledge;

        public bool climbingSlope;
        public bool descendingSlope;
        public bool slidingDownMaxSlope;

        public Vector3 velocityOld;
        public float slopeAngel, slopeAngelOld;
        public int faceDir, faceDirZ;

        public string lastTag;
        public void Reset()
        {
            above = below = false;
            left = right = false;
            front = back = false;

            climbingSlope = false;
            descendingSlope = false;
            slidingDownMaxSlope = false;
            hangingledge = false;
            slopeAngelOld = slopeAngel;
            slopeAngel = 0;
        }
    }

}
