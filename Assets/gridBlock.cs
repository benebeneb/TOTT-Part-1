/*
 * Name: Assignment 1 Tools of the Trade part 1 gridBlock.cs
 * Author: Benjamin D. Bolster
 * Date: 2/12/2025
 * Class: CMSC 436
 * Prof: Rebecca Williams
 * Assighnment: Assignment 1 Tools of the Trade part 1
 * Desc: A program that generates a grid component for a Vis about plastic bag 
 * Recicling in the EU.
*/

using UnityEngine;  // Unity connection
using TMPro;        // Needed for manipulation textboxes with code in Unity

public class gridBlock : MonoBehaviour // MonoBehaviour lets Unity use this code.
{
    public GameObject[] grid;// Connected to the 100 square sprites abouve the
                             // GridBlock(clones) with bottom left being grid[0]
                             // and top right being grid[99].
    public TextMeshProUGUI theText;// The TextMeshProUGUI element under the
                                   // GridBlock(clones).
    public Color changeColor;// Variable for changing color.

    public void Initalize(string name, int percent)
    {
        int redD, redO, greenD, greenO, blueD, blueO;
        int loop = 10;

        // This section is what changes the color of the grids the average for
        // the EU is white, above 90% is orange, and everything else is blue.
        // As the grid fills out going up the grid the color changes in a
        // gradient with redO being the original color and redD being the
        // diffrence every 10 cells.

        // I had to guess on what the threashold for the orange coleration It
        // could be somewere else or even another quality causing the orange
        // but with the available information it is what I thought caused the
        // color change

        if (name == "EU(AVG)") {// This one is an exception for the average to
                                // make it white.
            redO = 255;
            greenO = 255;
            blueO = 255;

            // no change in color gradient over time becse in the original it
            // was all the same white.
            redD = 0;
            greenD = 0;
            blueD = 0;

        } else if (percent >= 90) {// This makes any grid over the 90%
                                   // threshhold to be orange instead of blue
            redO = 255;
            greenO = 240;
            blueO = 80;

            // these original and diffrence values were found by using the
            // dropper tool from google slides taking that RGB value of both
            // the bottom and to rows than averageing out the diffrence between
            // the two with some rounding so it doesn't change by 6.3 or some
            // other irregular number. Starting as a orange yello to a warmer
            // orange value.
            redD = 0;
            greenD = -6;
            blueD = 1;
        }
        else {// This is the default color for the grids blue
            redO = 255;
            greenO = 255;
            blueO = 255;

            // These original and diffrence values were found using the same
            // method as above starting as white and somewhat quickly becoming
            // a rich blue
            redD = -20;
            greenD = -8;
            blueD = 0;
        }

        // the color we use for the Grid change using the RGB values selected
        // above.
        changeColor = new Color32( (byte)redO, (byte)greenO, (byte)blueO, 255);

        // generates the text for the name and percentage using one line because
        // using two text boxes for this seemed weird to me.
        theText.text = name + "<br><b>" + percent + "<sup>%</sup></b>";

        // Goes through all 100 square sprites and changes there color based on
        // changeColor every 10 squares itll change changeColor by redD, greenD,
        // and blueD.
        for (int i = 0; i < 100; i++) {
            if (percent > 0)
            {
                grid[i].GetComponent<Renderer>().material.color = changeColor;
                percent--;
            }
            else {
                grid[i].GetComponent<Renderer>().material.color = Color.gray;
            }

            loop--;

            // part that does the actual color change every 10 squares.
            if (loop == 0) {
                redO += redD;
                greenO += greenD;
                blueO += blueD;
                changeColor = new Color32((byte)redO, (byte)greenO, (byte)blueO, 255);
                loop = 10;
            }
        }

        
        theText.color = changeColor;
    }
}
