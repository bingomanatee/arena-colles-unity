using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WLLForms;

namespace ArenaColles
{



/**
 * A task is a specific instance of a job being worked.
 */
		public class TaskPanel : MonoBehaviour
		{
		
				public string output = "";
				public State state;
				public Dome dome;
				public List<Colonist> workers = new List<Colonist> ();
				public int DaysRequired = 1;
				public int minWorkers = 1;
				public List<Requirement> Requirements = new List<Requirement> ();
				public float timeSpent = 0;
				public int turnStarted;
				public Button doButton;
		 
				// Use this for initialization
				void Start ()
				{
						InitState ();
						foreach (Requirement req in Requirements) {
								req.Task = this;
						}
				}
	
				// Update is called once per frame
				void Update ()
				{
						CheckReqs ();
						// Debug.Log (string.Format ("Panel for {0} IsAllReqsGood = {1}", output, IsAllReqsGood)); 
						doButton.IsActive = IsAllReqsGood;
				}
				
				#region state

				public const string TASK_STATE_NAME = "task state";
				public const string STATE_PLANNING = "planning";
				public const string STATE_WORKABLE = "workable";
				public const string STATE_WORKING = "working";
				public const string STATE_ABANDONED = "abandoned";
				public const string STATE_COMPLETED = "completed";

				bool IsAllReqsGood {
						get {
								foreach (Requirement req in Requirements) {
										if (!req.IsSatisfied) {
												//	Debug.Log (string.Format ("output {0} not satisfied: {1}", output, req.resourceName));
												return false;
										}
								}
								return true;
						}
				}

				void InitState ()
				{
						if (!StateList.HasList (TASK_STATE_NAME)) 
								StateList.Create (TASK_STATE_NAME, STATE_PLANNING, STATE_WORKABLE, STATE_WORKING, STATE_COMPLETED, STATE_ABANDONED)
						.Constrain (STATE_WORKING, STATE_PLANNING)
						.Constrain (STATE_COMPLETED, STATE_WORKING)
						.Constrain (STATE_ABANDONED, STATE_WORKING, STATE_PLANNING);
		
						state = new State (TASK_STATE_NAME, STATE_PLANNING);
			
				}

				public void CheckReqs ()
				{
						if (state == null)
								return;
						if (state.In (STATE_PLANNING, STATE_WORKABLE)) {
								if (IsAllReqsGood)
										state.Change (STATE_WORKABLE);
								else 
										state.Change (STATE_PLANNING);
						}
						doButton.IsActive = IsAllReqsGood;
				}
				
				#endregion

				public void AddTask ()
				{
						if (!Game.game.ActiveDome) {
								throw new UnityException ("No active dome to add task to.");
						}
						
						Game.game.ActiveDome.AddTask (new Task (this));
				}
		}
}