using UnityEngine;
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
				public Dictionary<string, Terrain> terrains = new Dictionary<string, Terrain> ();
				public const int Y_SIZE = 10;

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

				public struct Cell
				{
						public	int i;
						public int j;
						public int k;
						public GameObject g;
						public string terrainName;
						TerraGen tg;

						public Cell (int i_, int j_, int k_, TerraGen tg_)
						{
								i = i_;
								j = j_;
								k = k_;
								tg = tg_;

								Vector3 p = tg.grid.GridToWorld (new Vector3 (i, k, j));
								p.y *= Y_RATIO;
								g = (GameObject)Instantiate (tg.cellGameObject, p, Quaternion.identity);

								terrainName = "";
						}

						string SetTerrain ()
						{
								List<string> terrainNames = new List<string> ();
								float height = k / ((float)Y_SIZE);
								foreach (string s in tg.terrains.Keys) {
										Terrain t = tg.terrains [s];
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
								terrainName = SetTerrain ();
								Material myMat = tg.terrains [terrainName].mat;
								for (int i = 0; i < g.transform.childCount; ++i) {
										GameObject gc = g.transform.GetChild (i).gameObject;
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

				public Cell[,] cells;

				#region loops
				// Use this for initialization
				void Start ()
				{
						terrains ["scrib"] = new Terrain (scrub, 0.5f, 1, 0);
						terrains ["desert"] = new Terrain (desert, 1, 0.5f, 0.25f);
						terrains ["mountain"] = new Terrain (mountain, 1, 0, 1);

						cells = new Cell[X_SIZE * 2 + 1, Z_SIZE * 2 + 1];
						grid = GetComponent<GFHexGrid> ();
						foreach (int i in Utils.Range(-X_SIZE, X_SIZE)) {
								foreach (int j in Utils.Range (-Z_SIZE, Z_SIZE)) {
										MakeCell (i, j);
								}
						}
				}

				// Update is called once per frame
				void Update ()
				{

				}
				#endregion   

				void MakeCell (int i, int j)
				{
						float f = Mathf.PerlinNoise (i / ((float)X_SIZE), j / ((float)Z_SIZE));
						//Debug.Log (string.Format ("Perlin values at {0}, {1}: {2}", i, j, f));
						int k = Mathf.RoundToInt (f * Y_SIZE);
						Cell c = new Cell (i, j, k, this);
						c.g.transform.parent = transform;
						c.SetMat ();
						cells [i + X_SIZE, j + X_SIZE] = c;

						
				}

				public Cell GetRandomCell ()
				{
						int i = Random.Range (0, cells.GetLength (0));
						int j = Random.Range (0, cells.GetLength (1));

						return cells [i, j];
				}

				public Cell GetCellNearCenter ()
				{
						int i = Mathf.RoundToInt (Random.Range (X_SIZE / 2, X_SIZE * 3 / 2));
						int j = Mathf.RoundToInt (Random.Range (Z_SIZE / 2, Z_SIZE * 3 / 2));
						return cells [i, j];
				}
		}
	
}