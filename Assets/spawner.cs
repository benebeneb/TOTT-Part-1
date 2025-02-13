/*
 * Name: Assignment 1 Tools of the Trade part 1 spawner.cs
 * Author: Benjamin D. Bolster
 * Date: 2/12/2025
 * Class: CMSC 436
 * Prof: Rebecca Williams
 * Assighnment: Assignment 1 Tools of the Trade part 1
 * Desc: a program that calls too Initalize from gridBlock.cs to spawn a number 
 * of gridblocks with the values given in countries & values. and places them 
 * in a grid pattern in the scene.
*/
using UnityEngine;
using System;
using UnityEngine.UIElements;
//using static Unity.Burst.Intrinsics.X86;
//using static UnityEngine.Rendering.DebugUI.Table;

public class spawner : MonoBehaviour 
{
    public GameObject prefab; // Connects too the gridBlock prefab in the game
                              // scene.

    // a simple helper function to find the index of a name in the unsorted list.
    public int FindIndex(string name, string[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] == name) {
                return i;
            }
        }
        return -1;
    }

    // this is the actual helper function that adds the gridBlock itself and
    // moves the position for the next Grid Block
    public void AddGrid(string name, int data, ref Vector3 position) {
        GameObject newGrid = Instantiate(prefab, position, Quaternion.identity);
        newGrid.GetComponent<gridBlock>().Initalize(name, data);

        position.x += 20;
        if (position.x > 20)
        {
            position.x = -100;
            position.y -= 22;
        }
    }

    // starts as soon as the scean starts this takes the data in countries and
    // values and uses them with create the grid block objects and places them
    // in a consistent grid 
    private void Start()
    {
        // inital position of the first element Austria in this case it's the
        // top left position.
        int posX = -100;
        int posY = 30;

        // used to check if an item is on the omit list.
        bool passOmit = true;
        int foundIndex;

        // data for holland that needs to be added manually because it's not in
        // the original dataset for some reason. also not in the place that
        // follows the pattern of the rest of the items
        string addName = "Holland";
        int addData = 99;
        string placeAfter = "Lithuania";

        // data from the final UK total
        string nameEU = "EU(AVG)";
        int EUData = 0;

        double[] percentagesData = {
            30.7, 37.3, 41.2, 46, 48.1, 49.2, 56.7, 58.6, 60.2, 60.9, 64, 65.4,
            69.8, 71.7, 72.7, 74.9, 75.7, 77.7, 79.6, 95.4, 96, 98, 98.2, 99,
            99.8, 100, 100, 0, 83.9, 74.3, 23.7, 49.9, 99.3, 0, 0, 0, 0, 0, 0
        };


        // at this point both the United Kingdoms -> UK, European Union - 28
        // contries -> EU(AVG), and Germany(until 1990 former territory of the
        // FRG) -> Germany were changed when they were put in as data

        // also to note Holland needed to be added to the end because it wasn't
        // in the original dataset.
        string[] countriesData = {
            "Iceland", "Croatia", "Latvia", "Poland", "Greece", "Sweden",
            "Hungary", "UK", "Portugal", "Slovakia", "Spain", "Bulgaria",
            "France", "Slovenia", "Czechia", "EU(AVG)", "Lithuania", "Ireland",
            "Estonia", "Luxembourg", "Netherlands", "Finland", "Denmark",
            "Belgium", "Germany", "Austria", "Liechtenstein",
            "Euro area (19 countries)", "Italy", "Cyprus", "Malta", "Romania",
            "Norway", "Switzerland", "Montenegro", "North Macedonia", "Albania",
            "Serbia", "Turkey"
        };


        // make a copy of countriesData and then sort that data so our finished
        // product can be in alphabetical order
        string[] sortedCountriesData = new string[countriesData.Length];
        Array.Copy(countriesData, sortedCountriesData, countriesData.Length);
        Array.Sort(sortedCountriesData);

        string[] omit = {
            "Cyprus", "Liechtenstein", "Luxembourg", "Malta", "Netherlands", "EU(AVG)"
        };



        // actual place the first element (Austria) goes.
        Vector3 position = new Vector3(posX, posY, 0);

        // Omits the data from the omit list and if they have zero in there
        // percent counter
        for (int i = 0; i < sortedCountriesData.Length; i++) {
            passOmit = true;

            foundIndex = FindIndex(sortedCountriesData[i], countriesData);
            
            if (percentagesData[foundIndex] != 0) {

                
                for (int j = 0; j < omit.Length; j++) {

                    if (omit[j] == sortedCountriesData[i]) {
                        passOmit = false;
                    }

                }

                if (passOmit) {

                    AddGrid(sortedCountriesData[i], (int)Math.Round(percentagesData[foundIndex], MidpointRounding.AwayFromZero), ref position);

                }

                // This hunk of code is to specifically add hollands data after
                // Lithuania both because Holland wasn't in the originall data
                // and because it's not in alphabetical order like the rest of
                // the data.
                if (sortedCountriesData[i] == placeAfter) {

                    AddGrid(addName, addData, ref position);

                } else if (sortedCountriesData[i] == nameEU) {
                    // this section is for grabbing the data for UK(AVG) so we
                    // can print that data at the end.
                    EUData = (int)Math.Round(percentagesData[foundIndex], MidpointRounding.AwayFromZero);

                }
            }
            
        }

        // This section is for ensuring that the
        AddGrid(nameEU, EUData, ref position);
    }
}
