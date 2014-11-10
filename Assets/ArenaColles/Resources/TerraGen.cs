using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class TerraGen : MonoBehaviour
		{
				public const int X_SIZE = 10;
				public const  int Z_SIZE = 10;
				public  const float Y_RATIO = 0.2f;
				public  GameObject cellGameObject;
				public GFHexGrid grid;
				public Material mountain;
				public Material scrub;
				public Material desert;
				public Material cloaked;
				public Dictionary<string, Terrain> terrains = new Dictionary<string, Terrain> ();
				public	bool TestMode = false;
				public bool Flatten = false;
				public const int Y_SIZE = 10;
				

		#region substructs
				public struct Terrain
				{
						public 	float deepChance;
						public 	float plainsChance;
						public 	float highChance;
						public 	Material mat;

						public Terrain (Material mat_, params float[] chance)
						{
								mat = mat_;
								deepChance = chance [0]; 
								plainsChance = chance [1];
								highChance = chance [2];
						}
				}

			

		#endregion

				public Cell[,] Cells;

				#region loops
				// Use this for initialization
				void Start ()
				{
						terrains ["scrib"] = new Terrain (scrub, 0.5f, 1, 0);
						terrains ["desert"] = new Terrain (desert, 1, 0.5f, 0.25f);
						terrains ["mountain"] = new Terrain (mountain, 1, 0, 1);
						terrains ["cloaked"] = new Terrain (cloaked, 0, 0, 0);

						grid = GetComponent<GFHexGrid> ();
			
						if (!TestMode)
								InitCells ();
				}

				// Update is called once per frame
				void Update ()
				{

				}
				#endregion   

				public float ShortRadius { get { return Mathf.Cos (Mathf.Deg2Rad * 30) * grid.radius; } }

				public float ShortDiameter { get { return 2 * ShortRadius; } }

		#region cells

				public void InitCells ()
				{
						Cells = new Cell[X_SIZE * 2 + 1, Z_SIZE * 2 + 1];
						foreach (int i in Utils.Range(-X_SIZE, X_SIZE)) {
								foreach (int j in Utils.Range (-Z_SIZE, Z_SIZE)) {
										MakeCell (i, j);
								}
						}
				}

				void MakeCell (int i, int j)
				{
						float f = Flatten ? 0 : Mathf.PerlinNoise (i / ((float)X_SIZE), j / ((float)Z_SIZE));
						//Debug.Log (string.Format ("Perlin values at {0}, {1}: {2}", i, j, f));
						int k = Mathf.RoundToInt (f * Y_SIZE);
						Cell c = new Cell (i, j, k, this);
						c.CellTerrainGameObject.transform.parent = transform;
						c.TerrainName = c.SetTerrain ();
						c.SetMat ();
						Cells [i + X_SIZE, j + X_SIZE] = c;

						
				}

				public Cell GetRandomCell ()
				{
						int i = UnityEngine.Random.Range (0, Cells.GetLength (0));
						int j = UnityEngine.Random.Range (0, Cells.GetLength (1));

						return Cells [i, j];
				}

				public Cell GetCellNearCenter ()
				{
						int i = Mathf.RoundToInt (UnityEngine.Random.Range (X_SIZE / 2, X_SIZE * 3 / 2));
						int j = Mathf.RoundToInt (UnityEngine.Random.Range (Z_SIZE / 2, Z_SIZE * 3 / 2));
						return Cells [i, j];
				}

				public Cell GetCell (int i, int j)
				{
						if (i < -X_SIZE || i > X_SIZE) {
								string msg = string.Format ("Cannot get i({0}); must be in range [{1}..{2}].", i, -X_SIZE, X_SIZE);
								throw new ArgumentOutOfRangeException ("i", msg);
						}

						if (j < -Z_SIZE || j > Z_SIZE) {
				
								string msg = string.Format ("Cannot get j({0}); must be in range [{1}..{2}].", j, -Z_SIZE, Z_SIZE);
								throw new ArgumentOutOfRangeException ("i", msg);
						}
						if (Cells [i + X_SIZE, j + Z_SIZE] == null) {
								throw new Exception ("No cell in " + i + "," + j);
						}
			
						return Cells [i + X_SIZE, j + Z_SIZE];
				}

				public bool HasCell (int i, int j)
				{
						bool has = !((i < -X_SIZE || i > X_SIZE) || (j < -Z_SIZE || j > Z_SIZE));
						//Debug.Log (string.Format ("Has {0},{1} : {2}", i, j, has));

						return has;
				}

		#endregion   

		}
	
}