using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace ArenaColles
{
		public class Task
		{
				static int nextTaskId = 0;
				public List<TaskResource> resources = new List<TaskResource> ();
				string output;
				public Dome dome;
				public int DayStarted;
				public int DaysRequired;
				public int id;
				private Dictionary<string, string> taskProperNames = null;
				
				public Task (TaskPanel tp)
				{
						id = ++nextTaskId;
						dome = tp.dome;
						DaysRequired = tp.DaysRequired;
						output = tp.output;
						foreach (Requirement req in tp.Requirements) {
								resources.Add (new TaskResource (req, this));
						}
						DayStarted = Game.game.Day;
						Debug.Log ("Created task " + output + " on day " + DayStarted);
				}
				
				public int DaysLeft {
						get {
								return (DayStarted + DaysRequired) - Game.game.Day;
						}
				}
	
				public string Output {
						get {
								return ProperTaskName (output);
						}
						set {
								output = value;
						}
				}

				string ProperTaskName (string output)
				{
						if (taskProperNames == null)
								InitProperTaskNames ();
						return taskProperNames [output];
				}

				void InitProperTaskNames ()
				{
						taskProperNames = new Dictionary<string, string> ();
						taskProperNames ["rover"] = "Create Rover";
				}
				
				public string ToString ()
				{
						return string.Format ("Task {0}: creating {1} in {2} days (started at {3})", id, Output, DaysRequired, DayStarted);
				}
		}
}