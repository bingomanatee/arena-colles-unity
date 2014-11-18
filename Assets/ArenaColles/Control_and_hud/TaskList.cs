using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace ArenaColles
{
		public class TaskList : MonoBehaviour
		{

				public List<TaskListItemLabel> items = new List<TaskListItemLabel> ();
		
				// Use this for initialization
				void Start ()
				{
						Clear ();
				}
				
				void Update ()
				{
						ReflectDome ();
				}

				public void ReflectDome ()
				{
						Clear ();
						
						if (Game.GameActiveDome) {
								//	Debug.Log (string.Format ("Reflecting {0} tasks.", Game.GameActiveDome.Tasks.Length));
				
								for (int i = 0; i < Game.GameActiveDome.Tasks.Length; ++i) {
										Task task = Game.GameActiveDome.Tasks [i];
					
										//		Debug.Log (string.Format ("reflecting item {0}: ({1})", i, task.ToString ()));
										items [i].Task = task;
								}
						} else {
								Debug.Log ("TaskList has no dome");
						}
				}
				
				void Clear ()
				{
						foreach (TaskListItemLabel taskLabel in items) {
								taskLabel.Clear ();
						}
				}
		}
}