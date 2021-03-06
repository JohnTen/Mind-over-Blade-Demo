﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility.Platformer
{
	public abstract class BaseGroundDetector : MonoBehaviour
	{
		[SerializeField] protected UpdateType updateType = UpdateType.Update;

		public virtual event Action OnStayGround;
		public virtual event Action OnLanding;
		public virtual event Action OnTakingoff;

		public virtual bool OnGround { get; set; }
		public virtual Vector3 Normal { get; set; }

		public virtual void CheckOnLandStatus()
		{
			var ground = IsOnGround();

			if (ground)
			{
				if (!OnGround && OnLanding != null)
					OnLanding.Invoke();

				if (OnStayGround != null)
					OnStayGround.Invoke();
			}
			else
			{
				if (OnGround && OnTakingoff != null)
					OnTakingoff.Invoke();
			}

			OnGround = ground;
		}

		protected abstract bool IsOnGround();
		
		protected virtual void Update()
		{
			if (updateType != UpdateType.Update) return;
			CheckOnLandStatus();
		}

		protected virtual void FixedUpdate()
		{
			if (updateType != UpdateType.FixedUpdate) return;
			CheckOnLandStatus();
		}

		protected virtual void LateUpdate()
		{
			if (updateType != UpdateType.LateUpdate) return;
			CheckOnLandStatus();
		}
	}
}
