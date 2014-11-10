using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class Cell
		{
				public	int i; // north/south
				public int j; // east/west
				public int k;
				public GameObject CellTerrainGameObject;

				public string TerrainName {
						get { 
								return revealed ? TerrainName_ : "cloaked";
						}
						set { 
								TerrainName_ = value;
								SetMat ();
						}
				}

				string TerrainName_ = "";
				TerraGen terraGen;

				public bool revealed {
						set {
								revealed_ = value;
								SetMat ();
						}
						get { return revealed_;}
				}

				bool revealed_ = false;

				public Cell (int i_, int j_, int k_, TerraGen tg_)
				{
						i = i_;
						j = j_;
						k = k_;
						terraGen = tg_;
			
						Vector3 p = terraGen.grid.GridToWorld (new Vector3 (i, k, j));
						p.y *= TerraGen.Y_RATIO;
						CellTerrainGameObject = (GameObject)GameObject.Instantiate (terraGen.cellGameObject, p, Quaternion.identity);
			
						TerrainName = "";
				}

		#region distance

				public float MaxDistance { get { return 1.1f * terraGen.ShortDiameter; } }

				public float DistanceTo (Cell c)
				{  
						return (GroundPosition - c.GroundPosition).magnitude;
				}
		
				public Transform transform { get { return CellTerrainGameObject.transform; } }
		
				public Vector3 GroundPosition { get { return terraGen.grid.GridToWorld (new Vector3 (i, 0, j)); } }

		public Vector3 position { get { return transform.position; }}

		#endregion

				public string ToString ()
				{
						return string.Format ("Cell {0}, {1}", i, j);

				}
				                                                                       
		#region neighbors
		
				public Dictionary<string, Cell> Neighbors {
						get {
								Dictionary<string, Cell> neighbors = new Dictionary<string, Cell> (); 
				
								// Debug.Log ("MaxDistance: " + MaxDistance);
								//	Debug.Log ("N");
								if (HasNorthCell && DistanceTo (NorthCell) <= MaxDistance)
										neighbors ["N"] = (NorthCell);
				
								//Debug.Log ("S");
								if (HasSouthCell && DistanceTo (SouthCell) <= MaxDistance)
										neighbors ["S"] = (SouthCell);
				
								//	Debug.Log ("E");
								if (HasEastCell && DistanceTo (EastCell) <= MaxDistance)
										neighbors ["E"] = EastCell;
				
								//	Debug.Log ("W");
								if (HasWestCell && DistanceTo (WestCell) <= MaxDistance)
										neighbors ["W"] = WestCell;
				
								//Debug.Log ("NE - distance " + DistanceTo (NorthEastCell));
								if (HasNorthEastCell && DistanceTo (NorthEastCell) <= MaxDistance)
										neighbors ["NE"] = (NorthEastCell);
				
								//		Debug.Log ("NW");
								if (HasNorthWestCell && DistanceTo (NorthWestCell) <= MaxDistance)
										neighbors ["NW"] = (NorthWestCell);
				
								//	Debug.Log ("SE");
								if (HasSouthEastCell && DistanceTo (SouthEastCell) <= MaxDistance)
										neighbors ["SE"] = (SouthEastCell);
				
								//	Debug.Log ("SW");
								if (HasSouthWestCell && DistanceTo (SouthWestCell) <= MaxDistance)
										neighbors ["SW"] = (SouthWestCell);
				
								//Debug.Log ("Radius: " + terraGen.grid.radius);
								foreach (string key in neighbors.Keys) {
										Cell n = neighbors [key];
										//	Debug.Log (string.Format ("Dir {0}: distance {1}", key, DistanceTo (n)));
								}
								return neighbors;
						}
				}
               
				int North { get { return j + 1; } }
				
				int South { get { return j - 1; } }
				
				int East { get { return i + 1; } }
				
				int West { get { return i - 1; } }
				
				bool HasSouthCell{ get { return terraGen.HasCell (i, South); } }
				
				Cell SouthCell { get {
								return terraGen.GetCell (i, South);
						} }
				
				bool HasNorthCell { get { return terraGen.HasCell (i, North); } }
				
				Cell NorthCell {
						get {
								return terraGen.GetCell (i, North);
						}
				}
				
				bool HasEastCell { get { return terraGen.HasCell (East, j); } }
				
				Cell EastCell { get { return terraGen.GetCell (East, j); } }
				
				bool HasWestCell { get { return terraGen.HasCell (West, j); } }
				
				Cell WestCell { get {
								return terraGen.GetCell (West, j);
						} }
				
				bool HasSouthEastCell{ get { return  terraGen.HasCell (East, South); } }
				
				Cell SouthEastCell {
						get {
								return terraGen.GetCell (East, South);
						}
				}
				
				bool HasSouthWestCell { get { return terraGen.HasCell (West, South); } }
				
				Cell SouthWestCell{ get {
								return terraGen.GetCell (West, South);
						} }
				
				bool HasNorthEastCell { get { return terraGen.HasCell (East, North); } }
				
				Cell NorthEastCell{ get {
								return terraGen.GetCell (East, North);
						} }
				
				bool HasNorthWestCell { get { return terraGen.HasCell (West, North); } }
				
				Cell NorthWestCell { get {
								return terraGen.GetCell (West, North);
						} }
				#endregion
				
				public string SetTerrain ()
				{
						List<string> terrainNames = new List<string> ();
						float height = k / ((float)TerraGen.Y_SIZE);
						foreach (string s in terraGen.terrains.Keys) {
								TerraGen.Terrain t = terraGen.terrains [s];
								float chance = 0;
								if (height >= 0) {
										chance = Mathf.Lerp (t.plainsChance, t.highChance, height);
								} else {
										chance = Mathf.Lerp (t.plainsChance, t.deepChance, -height);
								}
								foreach (int stub in Utils.Range (0, 10 * chance))
										terrainNames.Add (s); // adding s "chance"* 10 times. 
						}
					
						int index = Random.Range (0, terrainNames.Count);
						return terrainNames [index];
				}
				
				public void	SetMat ()
				{
						Material myMat = terraGen.terrains [TerrainName].mat;
						for (int i = 0; i < CellTerrainGameObject.transform.childCount; ++i) {
								GameObject gc = CellTerrainGameObject.transform.GetChild (i).gameObject;
								MeshRenderer mr = gc.GetComponent<MeshRenderer> ();
								if (gc.renderer)
										gc.renderer.material = myMat;
								if (mr) {
										foreach (int ri in Utils.Range(0, mr.materials.Length - 1)) {
												mr.materials [ri].color = myMat.color;
												mr.materials [ri].SetTexture ("_MainTex", myMat.GetTexture ("_MainTex"));
										}
								}
								//else
								//	Debug.Log ("No meshRenderer in " + gc.name);
						}
				}
        
		}

}