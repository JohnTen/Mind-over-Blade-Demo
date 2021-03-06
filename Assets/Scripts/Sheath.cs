﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility;

public class Sheath : MonoBehaviour
{
	[SerializeField] int numberOfKnife;
	[SerializeField] Transform launchPosition;
	[SerializeField] Transform[] sheathPositions;
	[SerializeField] Queue<Knife> knifesInSheath;
	[SerializeField] List<Knife> allKnifes;
	[SerializeField] Knife knifePrefab;

	public float ReloadSpeed { get; set; } = 3;
	public Transform LaunchPosition => launchPosition;
	public int knifeCount => knifesInSheath.Count;

	public event Action<Knife> OnRecievedKnife;

	bool reloading;

	private void Awake()
	{
		knifesInSheath = new Queue<Knife>();
		allKnifes = new List<Knife>();

		for (int i = 0; i < numberOfKnife; i++)
		{
			var knife = Instantiate(knifePrefab.gameObject).GetComponent<Knife>();
			knife.transform.parent = sheathPositions[i];
			knife.transform.position = sheathPositions[i].position;
			knife.transform.rotation = sheathPositions[i].rotation;
			knife.SetSheath(this);
			knifesInSheath.Enqueue(knife);
			allKnifes.Add(knife);
		}
	}

	public void UpdateFacingDirection(bool right)
	{
		if (right && this.transform.eulerAngles.y != 0)
		{
			this.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else if(!right && this.transform.eulerAngles.y == 0)
		{
			this.transform.rotation = Quaternion.Euler(0, 180, 0);
		}
	}

	public Knife TakeKnife(bool force)
	{
		if ((!force && reloading) || knifesInSheath.Count <= 0)
			return null;

		StartCoroutine(ReloadKnife());
		knifesInSheath.Peek().transform.parent = null;
		return knifesInSheath.Dequeue();
	}

	public void PutBackKnife(Knife knife)
	{
		knife.transform.parent = sheathPositions[knifesInSheath.Count];
		knife.transform.position = sheathPositions[knifesInSheath.Count].position;
		knife.transform.rotation = sheathPositions[knifesInSheath.Count].rotation;
		knifesInSheath.Enqueue(knife);
		if (OnRecievedKnife != null)
			OnRecievedKnife.Invoke(knife);
	}

	IEnumerator ReloadKnife()
	{
		reloading = true;

		float timer = 0;

		while (timer < 1)
		{
			yield return null;
			int index = 0;
			timer += Time.deltaTime * ReloadSpeed;
			foreach (var item in knifesInSheath)
			{
				item.transform.parent = sheathPositions[index];
				item.transform.position = Vector3.Lerp(sheathPositions[index + 1].position, sheathPositions[index].position, timer);
				item.transform.rotation = Quaternion.Lerp(sheathPositions[index + 1].rotation, sheathPositions[index].rotation, timer);
				index++;
				if (index >= 2) break;
			}
		}

		reloading = false;
	}
}
