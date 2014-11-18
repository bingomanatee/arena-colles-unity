using UnityEngine;
using System.Collections;
using ArenaColles;

namespace WLLForms
{
		public class Button : MonoBehaviour
		{

				public SpriteRenderer inactiveSprite;
				public SpriteRenderer baseSprite;
				public SpriteRenderer overSprite;
				public SpriteRenderer downSprite;
				public bool isActive_ = false;

				public bool IsActive {
						get {
								return isActive_;
						}
						set {
								isActive_ = value;
								if (state != null)
								if (value)
										state.Change (STATE_BASE);
								else
										state.Change (STATE_INACTIVE);
						}
				}				

				// Use this for initialization
				void Start ()
				{
						InitState ();
						IsActive = true;
				}
	
				// Update is called once per frame
				void Update ()
				{
			
				}

		#region state
		
				public State state;
				const string STATE_BASE = "base";
				const string STATE_OVER = "over";
				const string STATE_DOWN = "down";
				const string STATE_INACTIVE = "inactive";

				void OnStateChange (StateChange change)
				{
			
						inactiveSprite.renderer.enabled = false;
						baseSprite.renderer.enabled = false;
						overSprite.renderer.enabled = false;
						downSprite.renderer.enabled = false;
			
				
						switch (change.state) {
			
						case STATE_INACTIVE:
								inactiveSprite.renderer.enabled = true;
								break;
				
						case STATE_BASE: 
								baseSprite.renderer.enabled = true;
								break;
				
						case STATE_OVER:
								overSprite.renderer.enabled = true;
								break;
				
						case STATE_DOWN: 
								downSprite.renderer.enabled = true;
								break;
			
						}
				}
		
				void InitState ()
				{
						state = StateList.Init ("wll button state", STATE_INACTIVE, STATE_BASE, STATE_OVER, STATE_DOWN);
						state.StateChangedEvent += OnStateChange;
						state.Change (IsActive ? STATE_BASE : STATE_INACTIVE);
				}
#endregion

		#region mouse
		
				public void OnMouseEnter ()
				{
						if (IsActive)
								state.Change (STATE_OVER);
				}
				
				public void OnMouseDown ()
				{
						if (IsActive)
								state.Change (STATE_DOWN);
				}
				
				public void OnMouseUp ()
				{
						if (IsActive)
								state.Change (STATE_OVER);
				}
				
				public void OnMouseExit ()
				{
						if (IsActive)
								state.Change (STATE_BASE);
				}
				
				public void OnMouseUpAsButton ()
				{
						if (IsActive && (ClickEvent != null))
								ClickEvent ();
				}
		
#endregion

		#region Events
		
				public delegate void ButtonClickDelegate ();
		
				/// <summary>An event that gets fired </summary>
				public event ButtonClickDelegate ClickEvent;

    #endregion

		}
}