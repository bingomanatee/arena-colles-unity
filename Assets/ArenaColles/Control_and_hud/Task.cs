using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WLLForms;

namespace ArenaColles
{



/**
 * A task is a specific instance of a job being worked.
 */
		public class Task : MonoBehaviour
		{
		
				public string output = "";
				public State state;
				public Dome dome;
				public List<Colonist> workers = new List<Colonist> ();
				public float timeNeeded = 1;
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
						// Debug.Log (string.Format ("Reqs are good = {0}", (IsAllReqsGood ? "true" : "false")));
						doButton.IsActive = IsAllReqsGood;
				}
				
				#region state

				const string TASK_STATE_NAME = "task state";
				string STATE_PLANNING = "planning";
				string STATE_WORKABLE = "workable";
				string STATE_WORKING = "working";
				string STATE_ABANDONED = "abandoned";
				string STATE_COMPLETED = "completed";

				bool IsAllReqsGood {
						get {
								foreach (Requirement req in Requirements) {
										if (!req.IsSatisfied) {
												Debug.Log (string.Format ("output {0} not satisfied: {1}", output, req.resourceName));
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
						Debug.Log (string.Format ("Checking Reqs for {0}; result {1}", output, IsAllReqsGood));
						if (state.In (STATE_PLANNING, STATE_WORKABLE)) {
								if (IsAllReqsGood)
										state.Change (STATE_WORKABLE);
								else 
										state.Change (STATE_PLANNING);
						}
						doButton.IsActive = IsAllReqsGood;
				}
				
				#endregion
				
				public void CommitWorkers ()
				{
						Colonist[] idleWorkers = dome.IdleWorkers (minWorkers);
						foreach (Colonist worker in idleWorkers) {
								{	
										workers.Add (worker);
										worker.myTask = this;
								}
						}
				}
		}
}