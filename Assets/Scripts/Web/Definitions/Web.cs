using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web {
    private static int NUMNODES = 3;  // the number of total nodes
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
        for (int i = 0; i < NUMNODES; i++)
        {
            string nodeName = "node#" + i + ", ";
            nodes[i] = new Node(nodeName, i, i, i, i);
        }

        edges[0, 1] = 1;
        edges[1, 2] = 1;
        edges[2, 0] = 1;
        edges[0, 2] = 1;
    }

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
