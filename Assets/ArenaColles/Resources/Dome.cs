using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class Dome : MonoBehaviour
		{
				Cell cell_;
				public Colonist colonistTemplate;
				public Plant plantTemplate;
				public List<Colonist> ColonistsInColony = new List<Colonist> ();
				public List<Plant> PlantsInColony = new List<Plant> ();
				public List<Rover> RoversInColony = new List<Rover> ();
				public GameObject focus;
				public bool showColonistInfo;
				public Rover roverTemplate;
				public bool hasCell = false;
				public ItemSlot[] slots = new ItemSlot[6];
				int storedO2_ = 0;

				public void MaxStorage ()
				{
						Debug.Log ("Maxing storage for " + ColonyName);
						QuantityOfPlastics = PlasticsStorageCap;
						storedO2 = maxO2;
						Debug.Log (string.Format ("O2 = {0}; plastics = {1}", storedO2, QuantityOfPlastics));
				}

#region o2

				public int storedO2 {
						get { return storedO2_;}
						set { storedO2_ = value; }
				}

				public int maxO2 {
						get { return 6; }
				}

				public int o2production {
						get { 
								return PlantsInColony.Count * 2;
						}
				}
		#endregion
		
		#region plants
		
				public int NumberOfPlants {
						get { return PlantsInColony.Count;}
						set {
								value = Mathf.Max (0, value);
								while (PlantsInColony.Count > value)
										PlantsInColony.RemoveAt (0);
								if (colonistTemplate)
										while (PlantsInColony.Count < value) {
					
												GameObject newCol = (GameObject)Instantiate (plantTemplate.gameObject);
												Plant c = newCol.GetComponent<Plant> ();
												newCol.transform.parent = transform;
												newCol.transform.localPosition = Vector3.zero;
												newCol.transform.localScale = Vector3.one;
												PlantsInColony.Add (c);
										}
								FillSlots ();
						}
				}
				
				public int NumberOfRovers {
						get {
								return RoversInColony.Count;
						}
				}

				void RedistributePlants ()
				{
						foreach (Plant p in PlantsInColony) {
								ItemSlot foundSlot = null;
								foreach (ItemSlot slot in slots) {
										if (slot.occupant == null && (!foundSlot)) {
												foundSlot = slot;
										}
								}
								if (foundSlot)
										foundSlot.occupant = p.gameObject;
						}
				}
				
				public int PlantNumber (Plant p)
				{
			
						for (int i = 0; i < PlantsInColony.Count; i++) {
								Plant plant = PlantsInColony [i];
								if (p.gameObject.GetInstanceID () == plant.gameObject.GetInstanceID ())
										return i;
						}
						return -1;
				}
				
				
				
#endregion

#region colonists

				public int NumberOfColonists {
						get { return ColonistsInColony.Count;}
						set {
								value = Mathf.Max (0, value);
								while (ColonistsInColony.Count > value)
										ColonistsInColony.RemoveAt (0);
								if (colonistTemplate)
										while (ColonistsInColony.Count < value) {
										
												GameObject newCol = (GameObject)Instantiate (colonistTemplate.gameObject);
												Colonist c = newCol.GetComponent<Colonist> ();
												newCol.transform.parent = transform;
												newCol.transform.localPosition = Vector3.zero;
												newCol.transform.localScale = Vector3.one;
												ColonistsInColony.Add (c);
										}
								Debug.Log (string.Format ("set colonists to {0}; now have {1}colonists", value, NumberOfColonists));
								FillSlots ();
						}
				}

				void FillSlots ()
				{
						EmptySlots ();
			
						for (int i = 0; i < ColonistsInColony.Count; i++) {
								Colonist c = ColonistsInColony [i];
								if (i < slots.Length)
										slots [i].occupant = c.gameObject;
						}
						//@TODO - manage overflow.
						
						RedistributePlants ();
				}
				
				int ColonistIndex (Colonist c)
				{
						for (int i = 0; i < ColonistsInColony.Count; i++) {
								Colonist col = ColonistsInColony [i];
								if (c.gameObject.GetInstanceID () == col.gameObject.GetInstanceID ())
										return i;
						}
						return -1;
				}
				
				public Colonist[] IdleWorkers (int required = 1)
				{
						List<Colonist> idleWorkerList = new List<Colonist> ();
			
						foreach (Colonist c in ColonistsInColony) {
								if (c.IsIdle) {
										idleWorkerList.Add (c);
								}
								if (idleWorkerList.Count >= required)
										return idleWorkerList.ToArray ();
						}
						
						return idleWorkerList.ToArray ();
				}
		
#endregion
		
#region loops
		
				// Use this for initialization
				void Start ()
				{
						++colCount;
						colonyID = colCount;
				}
		
				// Update is called once per frame
				void Update ()
				{
			
				}
		
				void OnMouseDown ()
				{
						if ((!Game.game.ActiveDome) || (Game.game.ActiveDome != this))
								Game.game.ActiveDome = this;
				}
		

#endregion

		#region slots
		
				void EmptySlots ()
				{
						foreach (ItemSlot slot in slots)
								slot.occupant = null;
								
				}
				
				public string[] SlotReport {
						get {
								List<string> report = new List<string> ();
								for (int i = 0; i < slots.Length; ++i) {
										GameObject o = slots [i].occupant;
										if (o) {
												if (o.GetComponent<Colonist> ()) {
														report.Add ("colonist " + ColonistIndex ((Colonist)o.GetComponent<Colonist> ()).ToString ());
												} else if (o.GetComponent<Plant> ()) {
														report.Add ("plant " + PlantNumber ((Plant)o.GetComponent<Plant> ()).ToString ());
												} else {
														report.Add (string.Format ("other ({0})", o.name));
												}
										}
								}
								return report.ToArray ();
						}
				}
				
#endregion

		#region cells
		
				public Cell cell {
						set {
								cell_ = value;
								hasCell = (value != null);
								transform.parent = cell.CellTerrainGameObject.transform;
								transform.localScale = Vector3.one;
								transform.localPosition = Vector3.zero;
								value.revealed = true;
						}
						get { return cell_;}
				}
		
				public void SetCell (Cell cell__)
				{
						cell = cell__;
				}
				
		# endregion
				
		#region identity 
		
				static int colCount = 0;
				int colonyID = 0;

				public string ColonyName { get { return "Colony " + colonyID; } }


		#endregion

				public bool SpawnRover (string direction)
				{
						if (!hasCell) {
								Debug.Log ("Colony has no cell");
								return false;
						}
						Dictionary<string, Cell> neighbors = cell.Neighbors;
						if (neighbors.ContainsKey (direction)) {
								Rover rover = (Rover)Instantiate (roverTemplate);
								rover.Cell = (neighbors [direction]);
								RoversInColony.Add (rover);
								Debug.Log ("Colony Spawned rover at " + direction);
								return true;
						} else {
								return false;
						}
				}

				int plastics = 0;

				public int QuantityOfPlastics {
						set {
								plastics = Mathf.Max (0, value);
						}
						
						get { return plastics; }
				}

				public int PlasticsStorageCap {
						get { return 20; }
				}

				public int PlasticsProduction {
						get { return 0; }
				}
				
				public int PlasticsWastageThreshold {
						get { return  2 * NumberOfColonists; }
				}

				public int PlasticsConsumption {
						get {
								if (QuantityOfPlastics > PlasticsWastageThreshold) {
										return (1 + (Mathf.FloorToInt (Mathf.Sqrt (QuantityOfPlastics - PlasticsWastageThreshold))));
								} else {
										return 0;
								}
						}
				}
		}
}
