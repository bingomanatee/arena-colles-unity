using UnityEngine;
using System.Collections;
using ArenaColles;

namespace WLLForms
{
		public class Checkbox : MonoBehaviour
		{
				public SpriteRenderer offSprite;
				public SpriteRenderer onSprite;
				public SpriteRenderer overSprite;
				public SpriteRenderer overOnSprite;
				State state;
				public bool isChecked_ = false;
				bool isMouseOver_ = false;
				public bool IsLocked = false;

				public bool IsMouseOver {
						get {
								return isMouseOver_;
						}
						set {
								isMouseOver_ = value;
								UpdateVisualState ();
						}
				}

				public bool IsChecked {
						get {
								return isChecked_;
						}
						set {
								isChecked_ = value;
								if (CheckChangedEvent != null)
										CheckChangedEvent (isChecked_);
								UpdateVisualState ();
						}
				}
		
				// Use this for initialization
				void Start ()
				{
						InitState ();
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}
				
				#region state

				const string WLL_CB_STATE_NAME = "wll checkbox";
				const string WLL_CB_OFF = "off";
				const string WLL_CB_OFF_HOVER = "hover";
				const string WLL_CB_ON = "on";
				const string WLL_CB_ON_HOVER = "on hover";

				void InitState ()
				{
						state = StateList.Init (WLL_CB_STATE_NAME, WLL_CB_OFF, WLL_CB_OFF_HOVER, WLL_CB_ON, WLL_CB_ON_HOVER);
						state.StateChangedEvent += StateChange;
						state.Change (!IsChecked ? WLL_CB_OFF : WLL_CB_ON);
				}

				void StateChange (StateChange change)
				{
						offSprite.enabled = false;
						onSprite.enabled = false;
						overSprite.enabled = false;
						overOnSprite.enabled = false;
						
						switch (change.state) {
						case WLL_CB_OFF:
								offSprite.enabled = true;
								break;
				
						case WLL_CB_OFF_HOVER:
								overSprite.enabled = true;
								break;
								
						case WLL_CB_ON_HOVER:
								overOnSprite.enabled = true;
								break;
				
						case WLL_CB_ON:
								onSprite.enabled = true;
								break;
				
						default:
								Debug.Log ("bad checkbox state: " + change.state);
								break;
						}
				}

				void UpdateVisualState ()
				{
						if (IsMouseOver) {
								if (IsChecked) {
										state.Change (WLL_CB_ON_HOVER);
								} else {
										state.Change (WLL_CB_OFF_HOVER);
								}
						} else {
								if (IsChecked) {
										state.Change (WLL_CB_ON);
								} else {
										state.Change (WLL_CB_OFF);
								}
					
						}
				}
				#endregion
				
#region mouse

				/*		
		
			OnMouseDrag	
			*/
			
				void OnMouseEnter ()
				{
						if (!IsLocked)
								IsMouseOver = true;
						UpdateVisualState ();
				}
		
				void OnMouseExit ()
				{
			
						if (!IsLocked)
								IsMouseOver = false;
						UpdateVisualState ();
				}
				
				void OnMouseDown ()
				{
						if (!IsLocked)
								IsChecked = !IsChecked;
						UpdateVisualState ();
				}
				 
				/*
				OnMouseUpAsButton
				OnMouseExit	
				
				OnMouseOver	
				
					OnMouseUp	
					
						OnMouseUpAsButton */

#endregion

		#region Events
		
				public delegate void CheckChangeDelegate (bool isChecked);
		
				/// <summary>An event that gets fired </summary>
				public event CheckChangeDelegate CheckChangedEvent;
		
		#endregion

		}
}