﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniGame_DodgeAsteroids : MiniGame {

    public GameObject otterUfoPrefab; // das otterUfoPrefab wird in Unity zugewiesen.
    public MiniGame_DA_OtterUfo ufoScript;

    private List<GameObject> asteroids; // Kreiert nur einen Zeiger!!
    public GameObject asteroid1Prefab;
    public GameObject asteroid2Prefab;
    public GameObject asteroid3Prefab;

    private List<GameObject> landscapes;
    public GameObject landscape1Prefab;
    public GameObject landscape2Prefab;
    public GameObject landscape3Prefab;
    public GameObject landscape4Prefab;
    private GameObject landscape;

    private List<GameObject> stars;
    public GameObject starPrefab;
    public GameObject mondPrefab;
    private GameObject mond;

    public float timeToAppear;
    private float time;
    private float time2;

    // Use this for initialization
    void Start () {
        GameObject go = GameObject.Instantiate(otterUfoPrefab);
        go.transform.parent = transform;
        ufoScript = go.GetComponent<MiniGame_DA_OtterUfo>();

        timeToAppear *= timeFactor + Random.Range(-0.25f, 0.25f);

        asteroids = new List<GameObject>(); // !
        asteroids.Add(asteroid1Prefab);
        asteroids.Add(asteroid2Prefab);
        asteroids.Add(asteroid3Prefab);

        landscapes = new List<GameObject>();
        landscapes.Add(landscape1Prefab);
        landscapes.Add(landscape2Prefab);
        landscapes.Add(landscape3Prefab);
        landscapes.Add(landscape4Prefab);
        landscape = GameObject.Instantiate(landscapes[Random.Range(0,landscapes.Count)]);
        landscape.transform.parent = transform;

        stars = new List<GameObject>();
        for (int i = 0; i < 30; i++)
        {
            go = GameObject.Instantiate(starPrefab);
            go.transform.parent = transform.FindChild("Sterne").transform;
            go.transform.localPosition = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 6f), 0);
            float scale = Random.Range(-0.15f, 0.15f);
            go.transform.localScale = new Vector3(0.5f + scale, 0.5f + scale, 1);
            go.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1 + scale*2 - 0.3f);
            if (0.5f + scale <= 0.5f) go.GetComponent<SpriteRenderer>().sortingOrder = 1;
            stars.Add(go);
        }

        go = GameObject.Instantiate(mondPrefab);
        go.transform.parent = transform.FindChild("Sterne").transform;
        go.transform.localPosition = new Vector3(8, Random.Range(-2f, 3f), 0);
        mond = go;

        time = 0;

    }
	
	// Update is called once per frame
	void Update () {
        GameObject go;
        time += Time.deltaTime;
        time2 += Time.deltaTime;
        if (time >= timeToAppear)
        {
            int r = Random.Range(0, 3);
            go = GameObject.Instantiate(asteroids[r]);
            go.transform.parent = transform;
            time = 0;
        }

        if (time2 > 15)
        {
            Win();
            foreach (Transform child in transform) Destroy(child.GetComponent<GameObject>()); // Für jedes Transform item(child) in transform.
        }

        if (ufoScript.lives == 0)
        {
            Lose();
            foreach (Transform child in transform) Destroy(child.GetComponent<GameObject>()); // Für jedes Transform item(child) in transform.
        }

        landscape.transform.Translate(Vector3.left * Time.deltaTime*10);
        if (landscape.transform.localPosition.x <= -26)
        {
            GameObject.Destroy(landscape);
            landscape = GameObject.Instantiate(landscapes[Random.Range(0, landscapes.Count)]);
            landscape.transform.parent = transform;
        }
        
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].transform.Translate((-stars[i].transform.localScale.x * 0.01f), 0, 0); // Je größer der Stern, desto schneller soll er sich bewegen.
            if (stars[i].transform.localPosition.x < -8.3f) stars[i].transform.localPosition = new Vector3(8.3f, stars[i].transform.localPosition.y, 0);
            float color = Mathf.Sin(time2 * i * 0.2f) * 0.4f; // Sterneflackern Opacity
            stars[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, stars[i].GetComponent<SpriteRenderer>().color.a + color);
        }

        mond.transform.Translate(Vector3.left * Time.deltaTime*0.3f);
        mond.transform.localEulerAngles = new Vector3(0, 0, mond.transform.localEulerAngles.z + 0.017f); //.localEulerAngles.Set geht nicht!!
    }
}