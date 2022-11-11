using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.Platformer
{

	public class Box : MonoBehaviour
	{
		public float level_bottom = -20f;
		public LayerMask ground_layer;
		public bool respawn_on_death = true;

		private Rigidbody2D rigid;
		private Vector3 start_pos;
		private ContactFilter2D contact_filter;
		private float kill_timer = 0f;
		private bool dead = false;

		void Awake()
		{
			rigid = GetComponent<Rigidbody2D>();
			start_pos = transform.position;
		}

		private void Start()
		{
			contact_filter = new ContactFilter2D();
			contact_filter.layerMask = ground_layer;
			contact_filter.useLayerMask = true;
			contact_filter.useTriggers = false;
			
		}

		private void FixedUpdate()
		{

		}

		private bool DetectGrounded()
		{
			bool grounded = false;
			Vector2[] raycastPositions = new Vector2[3];

			Vector2 raycast_start = rigid.position;
			float radius = transform.localScale.y * 1f;

			Vector2 side1 = Vector2.left * radius / 2f;
			Vector2 side2 = Vector2.right * radius / 2f;
			raycastPositions[0] = raycast_start + side1;
			raycastPositions[1] = raycast_start;
			raycastPositions[2] = raycast_start + side2;

			RaycastHit2D[] hitBuffer = new RaycastHit2D[5];
			for (int i = 0; i < raycastPositions.Length; i++)
			{
				Physics2D.Raycast(raycastPositions[i], Vector2.down, contact_filter, hitBuffer, radius);
				Debug.DrawRay(raycastPositions[i], Vector2.down * radius);
				for (int j = 0; j < hitBuffer.Length; j++)
				{
					if (hitBuffer[j].collider != null)
						grounded = true;
				}
			}
			return grounded;
		}

		void Update()
		{
			//Kill when fall
			if (!dead && transform.position.y < level_bottom)
			{
				Kill();
			}

			//Reset after death
			if (dead && respawn_on_death)
			{
				kill_timer += Time.deltaTime;
				if (kill_timer > 5f)
				{
					transform.position = start_pos;
					dead = false;
					kill_timer = 0f;
				}
			}
		}


		public void Kill()
		{
			dead = true;
			kill_timer = 0f;
		}
	}

}