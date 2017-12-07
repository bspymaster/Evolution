using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class buildWeb : MonoBehaviour
{

    private static int NUMNODES = 110;
    private static string NODEDATAPATH = "Scripts/Web/Implementations/NodeData.xml";
    private Web web;

    // Initialize on startup
    void Start()
    {
        // Create an empty web
        web = new Web(NUMNODES);

        XmlNode rootNode = loadNodeData(NODEDATAPATH);

        // Create a list of Nodes
        web.setNodes(generateNodes(rootNode));

        // Create an edges graph linking the nodes
        web.setEdges(generateEdges(rootNode));
    }

    public Web getWeb()
    {
        return web;
    }

    public int getNumNodes()
    {
        return NUMNODES;
    }

    // Generates an empty edges graph with a given number of nodes
    private int[,] makeBaseEdgeGraph(int numNodes)
    {
        int[,] edges = new int[numNodes, numNodes];
        // Assume no connection
        for (int row = 0; row < numNodes; row++)
        {
            for (int col = 0; col < numNodes; col++)
            {
                edges[row, col] = 0;
            }
        }
        return edges;
    }

    private Node[] generateNodes(XmlNode rootNode)
    {
        // Init return value
        Node[] nodes = new Node[NUMNODES];

        if (rootNode != null)
        {

            for (int i = 0; i < NUMNODES; i++)
            {
                XmlNode nodeData = rootNode.ChildNodes[i];
                // Get node data
                nodes[i] = new Node(nodeData["name"].InnerText);
                try
                {
                    string[] stringArray = nodeData["herbivoreFoodSource"].InnerText.Split(',');
                    int[] intArray = new int[4];
                    int value;
                    for(value = 0; value < 4; value++)
                    {
                        intArray[value] = int.Parse(stringArray[value]);
                    }
                    nodes[i].setHerbivoreFoodSource(intArray);
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setCarnivoreFoodSource(int.Parse(nodeData["carnivoreFoodSource"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setRequiredCalories(int.Parse(nodeData["requiredCalories"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setCreatureSize(int.Parse(nodeData["creatureSize"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setLitterSize(int.Parse(nodeData["litterSize"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setReproductionRate(int.Parse(nodeData["reproductionRate"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setMutationChance(int.Parse(nodeData["mutationChance"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setCarnivorous(int.Parse(nodeData["carnivorous"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setOffspringSize(int.Parse(nodeData["offspringSize"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setAltitude(int.Parse(nodeData["altitude"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setCanFly(int.Parse(nodeData["canFly"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setCreatureSize(int.Parse(nodeData["creatureSize"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setDexterity(int.Parse(nodeData["dexterity"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setMaxPerTile(int.Parse(nodeData["maxPerTile"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setPeckingOrder(int.Parse(nodeData["peckingOrder"].InnerText));
                }
                catch (System.NullReferenceException) { }
                try
                {
                    nodes[i].setPeckingOrder(int.Parse(nodeData["offspringSurvivalChance"].InnerText));
                }
                catch (System.NullReferenceException) { }
            }
        }

        return nodes;
    }

    private int[,] generateEdges(XmlNode rootNode)
    {
        // Create an empty edge list
        int[,] edges = makeBaseEdgeGraph(NUMNODES);

        int childNodeIndex;

        if (rootNode != null)
        {
            for (int i = 0; i < NUMNODES; i++)
            {
                XmlNode nodeData = rootNode.ChildNodes[i];
                // Get node children
                foreach (XmlNode childNode in nodeData["childNodes"])
                {
                    childNodeIndex = int.Parse(childNode.InnerText);
                    edges[i, childNodeIndex] = 1;
                }
            }
        }

        return edges;
    }

    private XmlNode loadNodeData(string nodeDataFileName)
    {
        // Create the file path
        string filePath = Path.Combine(Application.dataPath, nodeDataFileName);

        if (File.Exists(filePath))
        {
            // Read the xml from the file
            string dataString = File.ReadAllText(filePath);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(dataString);

            // Get the root node
            XmlNode root = doc.SelectSingleNode("root");

            return root;
        }
        else
        {
            // Couldn't get the file; throw an error
            Debug.LogError("Cannot load game data!");
            return null;
        }
    }
}
