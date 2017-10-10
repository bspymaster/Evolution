using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web {
    private static int NUMNODES = 11;  // the number of total nodes
    private Node[] nodes;  // an array containing all possible nodes
    private int[,] edges;  // a 2D array of integers, where the row index is the staring node and the column index is the destination node, and the value contained is 0 of there is no edge, or 1 if there is an edge

    // Constructor
    public Web()
    {
        edges = new int[NUMNODES, NUMNODES];
        // Assume no connection
        for (int row=0; row < NUMNODES; row++)
        {
            for (int col = 0; col < NUMNODES; col++)
            {
                edges[row, col] = 0;
            }
        }

        nodes = new Node[NUMNODES];
        makeNodes();
    }

    // Helper function to make all the nodes and join their edges
    private void makeNodes()
    {
        //==========CENTRAL NODE==========\\
        nodes[0] = new Node("CORE NAME");

        //==========CORE TREES==========\\

        // Carnivores
        nodes[1] = new Node("Single Jaw Structure");
        nodes[2] = new Node("Chew Food");
        nodes[3] = new Node("Short & Fast Digestive Tract");
        edges[0, 1] = 1;
        edges[1, 2] = 1;
        edges[2, 3] = 1;

        nodes[4] = new Node("Double Jaw Structure");
        nodes[5] = new Node("Swallow Whole");
        nodes[6] = new Node("Teeth with Venom");
        nodes[7] = new Node("Intense Stomach Acids");
        edges[0, 4] = 1;
        edges[4, 5] = 1;
        edges[5, 6] = 1;
        edges[5, 7] = 1;

        nodes[8] = new Node("Large Stomachs w/ Specialized Acid");
        nodes[9] = new Node("Foreign Virus Immunity");
        nodes[10] = new Node("Eats Small Animals");
        edges[3, 8] = 1;
        edges[6, 8] = 1;
        edges[7, 8] = 1;
        edges[8, 9] = 1;
        edges[9, 10] = 1;
    }

    // Debug function to print out the web in the console
    public void printWeb()
    {
        string nodeNames = "";
        for (int i = 0; i < NUMNODES; i++)
        {
            nodeNames += nodes[i].getName();
        }
        Debug.Log(nodeNames + "\n");

        string rowString;
        for (int i = 0; i < NUMNODES; i++)
        {
            rowString = "";
            for (int j = 0; j < NUMNODES; j++)
            {
                rowString += (edges[i, j] + " ");
            }
            Debug.Log(rowString);
        }
    }
}
