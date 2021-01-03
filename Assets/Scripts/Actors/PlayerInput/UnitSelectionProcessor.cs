using System.Collections.Generic;
using Actors.Command.Components;
using Actors.Components;
using Pixeye.Actors;
using UnityEngine;

namespace Actors.PlayerInput
{
    public class UnitSelectionProcessor : Processor, ITick
    {
        private Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        private Vector2 lmbDownMousePos; //screen coordinates
        private Vector2 currentMousePos; //screen coordinates
        private Rect selectionRect; //screen coordinates
        private bool lmbClickedDown;
        private bool boxSelectionInitiated;
        private float timeOfClick;
        private Group<UnitComponent, NavigationComponent> _actors;
        private readonly LayerMask unitsLayerMask = LayerMask.GetMask("Units");

        private const float CLICK_TOLERANCE = .5f; //the player has this time to release the mouse button for it to be registered as a click

        public void Tick(float dt)
        {
	        currentMousePos = Input.mousePosition;

            if(Input.GetMouseButtonDown(0)) {
				lmbDownMousePos = currentMousePos;
				timeOfClick = UnityEngine.Time.unscaledTime;
				lmbClickedDown = true;
			}

			//-------------- LEFT MOUSE BUTTON HELD DOWN --------------
			if(lmbClickedDown
			   && Vector2.Distance(lmbDownMousePos, currentMousePos) > .1f) {
				UIManager.Instance.ToggleSelectionRectangle(true);
				boxSelectionInitiated = true;
				lmbClickedDown = false; //this will avoid repeating this block every frame
			}

			if(boxSelectionInitiated) {
				//draw the screen space selection rectangle
				Vector2 rectPos = new Vector2(
					                  (lmbDownMousePos.x + currentMousePos.x) * .5f,
					                  (lmbDownMousePos.y + currentMousePos.y) * .5f);
				Vector2 rectSize = new Vector2(
					                   Mathf.Abs(lmbDownMousePos.x - currentMousePos.x),
					                   Mathf.Abs(lmbDownMousePos.y - currentMousePos.y));
				selectionRect = new Rect(rectPos - (rectSize * .5f), rectSize);

				UIManager.Instance.SetSelectionRectangle(selectionRect);
			}


			//-------------- LEFT MOUSE BUTTON UP --------------
			if(Input.GetMouseButtonUp(0)) {

				if(boxSelectionInitiated) {
					//consider the mouse release as the end of a box selection
					var selectedActors = new List<GameObject>();

					foreach (var actorEntity in _actors)
					{
						var unitComponent = actorEntity.Get<UnitComponent>();
						if(unitComponent.health < 1) return;
						Vector2 screenPos = Camera.main.WorldToScreenPoint(actorEntity.transform.position);
						if(selectionRect.Contains(screenPos)) {
							selectedActors.Add(actorEntity.transform.gameObject);
						}
					}

					Entity.Create().Set<SelectionCommand>().selectedActors = selectedActors;

					//hide the box
					UIManager.Instance.ToggleSelectionRectangle(false);
				} else {
					if(UnityEngine.Time.unscaledTime < timeOfClick + CLICK_TOLERANCE) {
						//consider the mouse release as a click
						RaycastHit hit;
						Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

						if(Physics.Raycast(ray, out hit, Mathf.Infinity, unitsLayerMask)) {
							if(hit.collider.gameObject.CompareTag("Locals")) {
								// Unit newSelectedUnit = hit.collider.GetComponent<Unit>();
								// GameManager.Instance.AddToSelection(newSelectedUnit);
								// newSelectedUnit.SetSelected(true);
								//gameState.selectedActors.Add(_actors.Get2(actorIndex).navMeshAgent);
							}
						}
					}
				}

				lmbClickedDown = false;
				boxSelectionInitiated = false;
			}
        }

        private bool GetMouseOnGroundPlane(out Vector3 thePoint)
        {
            thePoint = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (groundPlane.Raycast(ray, out rayDistance))
            {
                thePoint = ray.GetPoint(rayDistance);
                return true;
            }

            return false;
        }
    }
}
