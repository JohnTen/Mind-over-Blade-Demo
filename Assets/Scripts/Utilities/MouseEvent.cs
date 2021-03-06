﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseEvent : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] UnityEvent MouseUp;
	[SerializeField] UnityEvent MouseDown;
	[SerializeField] UnityEvent MouseEnter;
	[SerializeField] UnityEvent MouseOver;
	[SerializeField] UnityEvent MouseExit;

	private void OnMouseUp()
	{
		if (MouseUp != null)
			MouseUp.Invoke();
	}

	private void OnMouseDown()
	{
		if (MouseDown != null)
			MouseDown.Invoke();
	}

	private void OnMouseEnter()
	{
		if (MouseEnter != null)
			MouseEnter.Invoke();
	}

	private void OnMouseOver()
	{
		if (MouseOver != null)
			MouseOver.Invoke();
	}

	private void OnMouseExit()
	{
		if (MouseExit != null)
			MouseExit.Invoke();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (MouseUp != null)
			MouseUp.Invoke();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (MouseDown != null)
			MouseDown.Invoke();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (MouseEnter != null)
			MouseEnter.Invoke();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (MouseExit != null)
			MouseExit.Invoke();
	}
}
