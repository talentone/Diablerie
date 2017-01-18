﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Character character;

    [HideInInspector]
    static public GameObject hover;

    Iso iso;

	void Awake() {
		if (character == null)
			character = GameObject.FindWithTag("Player").GetComponent<Character>();
		SetCharacter(character);
	}

	void Start () {
	}

	void SetCharacter (Character character) {
		this.character = character;
		iso = character.GetComponent<Iso>();
	}

	void Update () {
		Vector3 targetTile;
		if (Usable.hot != null) {
			targetTile = Iso.MapToIso(Usable.hot.transform.position);
		} else {
			targetTile = IsoInput.mouseTile;
		}
		Iso.DebugDrawTile(targetTile, Tilemap.instance[targetTile] ? Color.green : Color.red, 0.1f);
		Pathing.BuildPath(iso.tilePos, targetTile, character.directionCount);

		if (Input.GetKeyDown(KeyCode.F4)) {
			character.Teleport(IsoInput.mouseTile);
		}

		if (Input.GetMouseButton(1) || (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0))) {
			character.Attack();
		}
		else if (Input.GetMouseButton(0)) {
			if (Usable.hot != null) {
				character.Use(Usable.hot);
			} else {
				character.GoTo(targetTile);
			}
		}

		character.LookAt(IsoInput.mousePosition);

		if (Input.GetKeyDown(KeyCode.Tab)) {
			foreach (Character character in GameObject.FindObjectsOfType<Character>()) {
				if (this.character != character) {
					SetCharacter(character);
					return;
				}
			}
		}

        GameObject newHover = null;
        var mousePos = Camera.current.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit)
        {
            newHover = hit.collider.gameObject;
        }

        if (newHover != hover)
        {
            if (hover != null)
            {
                var spriteRenderer = hover.GetComponent<SpriteRenderer>();
                spriteRenderer.material.SetFloat("_SelfIllum", 1.0f);
            }
            hover = newHover;
            if (hover != null)
            {
                var spriteRenderer = hover.GetComponent<SpriteRenderer>();
                spriteRenderer.material.SetFloat("_SelfIllum", 1.75f);
            }
        }
	}
}