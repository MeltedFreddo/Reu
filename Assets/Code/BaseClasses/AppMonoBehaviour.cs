using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.BaseClasses
{
	public class AppMonoBehaviour : MonoBehaviour {

		public App AppPrefab;

		void Awake()
		{
			ScaffoldTempAppIfRequired ();
		}

		private void ScaffoldTempAppIfRequired()
		{
			// Is the controlling app already in existance?
			if ( GameObject.Find("App") == null )
			{
				Instantiate(AppPrefab);
			}	
		}
	}
}

