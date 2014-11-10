using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class ColonyPoll : MonoBehaviour
		{
				public Game game;
				public Dome firstColony;
				
				// Use this for initialization
				void Start ()
				{
						game.terrain.InitCells ();
						game.FirstTurn ();
						
						// round one -- the default starting scenario count of plants and colonists
						firstColony = game.colonies [0];
						string sr = string.Join (",", firstColony.SlotReport);
						string exp = string.Join (",", new string[] {
				"colonist 0",
				"colonist 1",
				"colonist 2",
				"plant 0",
				"plant 1"
			});
						Debug.Log (string.Format ("Getting slot report 1: {0} == {1}", sr, exp));
						
						IntegrationTest.Assert (gameObject, (sr == exp), "Bad slot report 1");
						
						// round two - decreasing plants
						firstColony.NumberOfPlants = 1;
						
						exp = string.Join (",", new string[] {
								"colonist 0",
								"colonist 1",
								"colonist 2",
								"plant 0"
						});
						sr = string.Join (",", firstColony.SlotReport);
						Debug.Log (string.Format ("Getting slot report 2: {0} == {1}", sr, exp));
						
						IntegrationTest.Assert (gameObject, (sr == exp), "Bad slot report 2");
						
						// round three - increasing colonist number
						firstColony.NumberOfColonists = 4;
			
						exp = string.Join (",", new string[] {
						"colonist 0",
						"colonist 1",
						"colonist 2",
						"colonist 3",
						"plant 0"
					});
						sr = string.Join (",", firstColony.SlotReport);
						Debug.Log (string.Format ("Getting slot report 2: {0} == {1}", sr, exp));
			
						IntegrationTest.Assert (gameObject, (sr == exp), "Bad slot report 3");
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}
		}
}