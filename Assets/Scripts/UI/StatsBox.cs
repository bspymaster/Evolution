using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatsBox : MonoBehaviour
{
    public GameObject windowManagerPrefab;
    public GameObject infoPanelPrefab;
    public Text berryFoodPrefab;
    public Text biomePrefab;
    public Text enviroPrefab;
    public Text animalPrefab;
    public GameObject noClickPrefab;
    public GameObject exitButtonPrefab;
    bool foundPlayer = false;

    public string getText(int[] speciesArray)
    {
        Debug.Log("Entered getText");
        Debug.Log("Original ID " + speciesArray[0]);
        string ID;
        string size;
        string meatSize;
        string relation = "";
        string foods = "";
        //Gets species ID
        if(speciesArray[7] == 0)
        {
            ID = "Player";
        }
        else
        {
            ID = speciesArray[0].ToString();
        }
        Debug.Log("New ID " + ID);
        //Gets species size
        size = checkSize(speciesArray[1]);
        meatSize = checkSize(speciesArray[6]);

        if (speciesArray[7] == 1)
        {
            relation = "   Behaviour: Cohabitor";
        }
        if (speciesArray[7] == 2)
        {
            relation = "   Behaviour: Competitor";
        }

        if(speciesArray[2] == 1)
        {
            foods += "Berries   ";
        }
        if (speciesArray[3] == 1)
        {
            foods += "Nuts   ";
        }
        if (speciesArray[4] == 1)
        {
            foods += "Grass   ";
        }
        if (speciesArray[5] == 1)
        {
            foods += "Leaves   ";
        }
        if (speciesArray[6] != -1)
        {
            foods += (meatSize + " Meat");
        }


        string sendText = "Species ID: " + ID + "   Size: " + size + relation + "\n" + "Food Consumption:\n" + foods + "\n\n";
        Debug.Log("Full Text" + sendText);
        return sendText;
    }

    public string checkSize(int num)
    {
        Debug.Log("Entered checkSize");
        string size;
        if (num > 0 && num < 2)
        {
            size = "Tiny";
        }
        else if (num > 1 && num < 101)
        {
            size = "Small";
        }
        else if (num > 100 && num < 201)
        {
            size = "Medium";
        }
        else if (num > 200 && num < 301)
        {
            size = "Large";
        }
        else if (num > 300 && num < 401)
        {
            size = "Huge";
        }
        else
        {
            size = "";
        }
        return size;
    }

    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Gets the values from the selected tile.
            Global.cameraLock = true;
            int numBerries = this.GetComponent<TileData>().getNumBerries();
            int numGrass = this.GetComponent<TileData>().getNumGrass();
            int numNuts = this.GetComponent<TileData>().getNumNuts();
            int numLeaves = this.GetComponent<TileData>().getNumLeaves();
            int numMeat = this.GetComponent<TileData>().getNumAmbientMeat();
            string biome = this.GetComponent<TileData>().getTileType();
            int altitude = this.GetComponent<TileData>().getAltitude();
            int temperature = this.GetComponent<TileData>().getTemperature();
            GameObject windowManager = Instantiate(windowManagerPrefab, new Vector3(0, 100, 5), Quaternion.identity) as GameObject;
            windowManager.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            windowManager.gameObject.tag = "WindowManager";

            GameObject infoPanel = Instantiate(infoPanelPrefab, new Vector3(0, -125, -5), Quaternion.identity) as GameObject;
            infoPanel.transform.SetParent(GameObject.FindGameObjectWithTag("WindowManager").transform, false);

            //Defines the contents of each text box in the tile info window.
            string foodTypes = "   Food Sources                                 Top Three Species" + "\n\nBerries: " + numBerries.ToString() + "\nGrass: " + numGrass.ToString() + "\nNuts: " + numNuts.ToString() + "\nLeaves: " + numLeaves.ToString() + "\nMeat: " + numMeat.ToString();
            string biomeType = biome;
            string enviroType = "Altitude: " + altitude.ToString() + "     Temperature(F): " + temperature.ToString();

            //Defines the positioning and sets parents of each text box in the tile info window.
            Text foods = Instantiate(berryFoodPrefab, new Vector3(0, 25, -5), Quaternion.identity) as Text;
            foods.transform.SetParent(GameObject.FindGameObjectWithTag("WindowManager").transform, false);

            Text biomeTitle = Instantiate(biomePrefab, new Vector3(0, 140, -5), Quaternion.identity) as Text;
            biomeTitle.transform.SetParent(GameObject.FindGameObjectWithTag("WindowManager").transform, false);

            Text enviro = Instantiate(enviroPrefab, new Vector3(0, 75, -5), Quaternion.identity) as Text;
            enviro.transform.SetParent(GameObject.FindGameObjectWithTag("WindowManager").transform, false);

            //Creates window and exit button.            
            GameObject noClick = Instantiate(noClickPrefab, new Vector3(500, 300, -1), Quaternion.identity) as GameObject;
            noClick.gameObject.tag = "WindowManager";

            GameObject exitButton = Instantiate(exitButtonPrefab, new Vector3(450, 150, -5), Quaternion.identity) as GameObject;
            exitButton.transform.SetParent(GameObject.FindGameObjectWithTag("WindowManager").transform, false);

            foods.text = foodTypes;
            biomeTitle.text = biomeType;
            enviro.text = enviroType;


            List<int[]> speciesData = this.GetComponent<TileData>().getSpeciesData();
            print("List Received");
            int numSpecies = 3;
            bool noHostile = false;

            string presentSpecies = "";

            foreach(int[] sp in speciesData)
            {
                Debug.Log("Entered Player search");
                if (sp[7] == 0)
                {
                    string temp = getText(sp);
                    presentSpecies += temp;
                    numSpecies--;
                    Debug.Log("Found Player");
                }
            }
            if(numSpecies > 0)
            {
                foreach (int[] sp in speciesData)
                {
                    if (sp[7] == 2 && numSpecies > 0)
                    {
                        string temp = getText(sp);
                        presentSpecies += temp;
                        numSpecies--;
                    }
                }
            }
            if (numSpecies > 0)
            {
                foreach (int[] sp in speciesData)
                {
                    if (sp[7] == 1 && numSpecies > 0)
                    {
                        string temp = getText(sp);
                        presentSpecies += temp;
                        numSpecies--;
                    }
                }
            }
            /*
            while (numSpecies > 0)
            {
                if(noHostile == false)
                {
                    foreach (int[] sp in speciesData)
                    {
                        Debug.Log("Entered Hostile search");
                        if (sp[7] == 2)
                        {
                            presentSpecies += getText(sp);
                            Debug.Log("Found Hostile");
                            speciesData.Remove(sp);
                            numSpecies--;
                        }
                        return;
                    }
                    noHostile = true;
                }
                foreach (int[] sp in speciesData)
                {
                    Debug.Log("Entered Cohabit search");
                    if (sp[7] == 1)
                    {
                        presentSpecies += getText(sp);
                        Debug.Log("Found Cohabit");
                        speciesData.Remove(sp);
                        numSpecies--;
                    }
                    return;
                }
            }
            */
            Text animals = Instantiate(animalPrefab, new Vector3(175, -40, -5), Quaternion.identity) as Text;
            animals.transform.SetParent(GameObject.FindGameObjectWithTag("WindowManager").transform, false);

            animals.text = presentSpecies;
        }
    }
}