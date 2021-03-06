﻿using UnityEngine;

namespace UnityUtility
{
    public abstract class GlobalSingleton<T> : MonoBehaviour where T : GlobalSingleton<T>
	{
        protected static T _instance;
		public static T Instance
        {
            get
            {
                if (_instance) return _instance;

                _instance = FindObjectOfType<T>();
                if (_instance) return _instance;

                _instance = GlobalObject.GetOrAddComponent<T>();
                return _instance;
            }
        }

		protected virtual void Awake()
		{
			if (Instance != this)
			{
				Destroy(this.gameObject);
				return;
			}

			if (!Instance.transform.GetComponentInParent<GlobalObject>())
			{
				Debug.LogWarning(typeof(T).Name + " isn't attached on GlobalObject!");
			}
		}
	}
}
