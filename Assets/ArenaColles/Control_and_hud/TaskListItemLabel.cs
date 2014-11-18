using UnityEngine;
using System.Collections;
using TMPro;

namespace ArenaColles
{
		public class TaskListItemLabel : MonoBehaviour
		{
		
				public TextMesh label;
				public TextMesh daysLeft;

#region task
		
				Task task;

				public Task Task {
						get {
								return task;
						}
						set {
								task = value;
								label.text = task.ProperTaskName ();
								daysLeft.text = task.DaysLeft.ToString ();
						}
				}
		
#endregion

				// Use this for initialization
				void Start ()
				{
						Clear ();
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}
				
				public void Clear ()
				{
						label.text = "";
						daysLeft.text = "";
				}
		}
}