﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{

    public string path;
    public Character playerPrefab;
    public GameObject monsterPrefab;

    void Start ()
    {
        var result = DS1.Import("Assets/d2/data/global/tiles/" + path, monsterPrefab);
        var playerPos = result.entry;

        var player = Instantiate(playerPrefab, playerPos, Quaternion.identity);
        PlayerController.instance.SetCharacter(player);
	}
}