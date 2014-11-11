using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class Task
		{

				public List<TaskResource> resources = new List<TaskResource> ();
				public string output;
				public Dome dome;
				public int timeStarted;
				
				public Task (TaskPanel tp)
				{
						dome = tp.dome;
				}
	
		}
}