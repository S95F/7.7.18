using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
	public bool dZone = true; 
	public void OnPointerEnter(PointerEventData eventData) {
		if(eventData.pointerDrag == null)
			return;
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null) {
			d.placeholderParent = this.transform;
		}
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		if(eventData.pointerDrag == null)
			return;
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null && d.placeholderParent==this.transform) {
			d.placeholderParent = d.parentToReturnTo;
		}
	}
	
	public void OnDrop(PointerEventData eventData) {
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null) {
			d.parentToReturnTo = this.transform;
		}
		if (dZone) {
			eventScript e = eventData.pointerDrag.GetComponent<eventScript> ();
			// need to take this feedback and communicate it to the hbar
			Debug.Log (e.cardAbility);
			Destroy (eventData.pointerDrag.gameObject);
		}
	}
}
