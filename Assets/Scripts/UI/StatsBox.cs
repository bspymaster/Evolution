using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatsBox : TileData
{
    public GameObject windowManagerPrefab;
    public GameObject infoPanelPrefab;
    public Text berryFoodPrefab;
    public Text biomePrefab;
    public Text enviroPrefab;
    public GameObject noClickPrefab;
    public GameObject exitButtonPrefab;


    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Gets the values from the selected tile.
            Global.cameraLock = true;
            int numBerries = getNumBerries();
            int numGrass = getNumGrass();
            int numNuts = getNumNuts();
            int numLeaves = getNumLeaves();
            int numMeat = getNumAmbientMeat();
            string biome = getTileType();
            int altitude = getAltitude();
            int temperature = getTemperature();
            GameObject windowManager = Instantiate(windowManagerPrefab, new Vector3(0, 0, 5), Quaternion.identity) as GameObject;
            windowManager.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            windowManager.gameObject.tag = "WindowManager";

            //Defines the contents of each text box in the tile info window.
            string foodTypes = "Berries: " + numBerries.ToString() + "\nGrass: " + numGrass.ToString() + "\nNuts: " + numNuts.ToString() + "\nLeaves: " + numLeaves.ToString() + "\nMeat: " + numMeat.ToString();
            string biomeType = biome;
            string enviroType = "Altitude: " + altitude.ToString() + "     Temperature(F): " + temperature.ToString();

            //Defines the positioning and sets parents of each text box in the tile info window.
            Text foods = Instantiate(berryFoodPrefab, new Vector3(-200, -50, -5), Quaternion.identity) as Text;
            foods.transform.SetParent(GameObject.FindGameObjectWithTag("WindowManager").transform, false);

            Text biomeTitle = Instantiate(biomePrefab, new Vector3(0, 140, -5), Quaternion.identity) as Text;
            biomeTitle.transform.SetParent(GameObject.FindGameObjectWithTag("WindowManager").transform, false);

            Text enviro = Instantiate(enviroPrefab, new Vector3(0, 75, -5), Quaternion.identity) as Text;
            enviro.transform.SetParent(GameObject.FindGameObjectWithTag("WindowManager").transform, false);

            //Creates window and exit button.
            GameObject infoPanel = Instantiate(infoPanelPrefab, new Vector3(0, 0, 5), Quaternion.identity) as GameObject;
            infoPanel.transform.SetParent(GameObject.FindGameObjectWithTag("WindowManager").transform, false);

            GameObject noClick = Instantiate(noClickPrefab, new Vector3(500, 300, -1), Quaternion.identity) as GameObject;
            noClick.gameObject.tag = "WindowManager";

            GameObject exitButton = Instantiate(exitButtonPrefab, new Vector3(335, 150, -5), Quaternion.identity) as GameObject;
            exitButton.transform.SetParent(GameObject.FindGameObjectWithTag("WindowManager").transform, false);


            foods.text = foodTypes;
            biomeTitle.text = biomeType;
            enviro.text = enviroType;
        }       
    }
}
























    /*
    private bool clickedOn = false;

    private void OnGUI()
    {
        if(!clickedOn)
        {
            return;
        }

        Vector3 screenPos = transform.position;
        Rect menuRect = new Rect(screenPos.x, screenPos.y, 1, 1);

        GUI.Label(menuRect, "Fuck you.");
        clickedOn = false;
    }
    private void OnMouseDown()
    {
        clickedOn = true;
    }
    */

