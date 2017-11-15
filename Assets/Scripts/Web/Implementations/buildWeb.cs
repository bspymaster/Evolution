using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildWeb : MonoBehaviour
{

    private static int NUMNODES = 95;
    private Web web;

    // Initialize on startup
    void Start()
    {
        // Create an empty web
        web = new Web(NUMNODES);

        // Create a list of Nodes
        web.setNodes(generateNodes());

        // Create an edges graph linking the nodes
        web.setEdges(generateEdges());
    }

    public Web getWeb()
    {
        return web;
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

    private Node[] generateNodes()
    {
        // Init return value
        Node[] nodes = new Node[NUMNODES];

        //==========CENTRAL NODE==========\\
        nodes[0] = new Node("Chordata");

        //===========CORE TREES===========\\

        // Carnivores
        nodes[1] = new Node("Single Jaw Structure");
        nodes[2] = new Node("Chew Food");
        nodes[3] = new Node("Short & Fast Digestive Tract");

        nodes[4] = new Node("Double Jaw Structure");
        nodes[5] = new Node("Swallow Whole");
        nodes[6] = new Node("Teeth with Venom");
        nodes[7] = new Node("Intense Stomach Acids");

        nodes[8] = new Node("Large Stomachs with Specialized Acids");
        nodes[9] = new Node("Foreign Virus Immunity");
        nodes[10] = new Node("Eats Tiny Animals");
        nodes[11] = new Node("Eats Small Animals");
        nodes[12] = new Node("Eats Medium Animals");
        nodes[13] = new Node("Eats Large Animals");
        nodes[14] = new Node("Eats humongous Animals");

        // Herbivores
        nodes[15] = new Node("Chew Food");
        nodes[16] = new Node("Specialized Digestive Tract");
        nodes[17] = new Node("Eats Berries");
        nodes[18] = new Node("Eats Nuts");
        nodes[19] = new Node("Eats Grass/Seeds");
        nodes[20] = new Node("Eats Leaves");


        //==========AVIARY TREES==========\\
        nodes[21] = new Node("Hard Eggs");
        nodes[22] = new Node("Beak");
        nodes[23] = new Node("Feathers");
        nodes[24] = new Node("Hollow Bones");
        nodes[25] = new Node("Glide Feathers");
        nodes[26] = new Node("Webbing");
        nodes[27] = new Node("Flapping Ability");  // Feathers
        nodes[28] = new Node("Flapping Ability");  // Webbing
        nodes[29] = new Node("Flight Feathers");
        nodes[30] = new Node("Alula");

        nodes[31] = new Node("Hooked Beak");  // Optimized for meat
        nodes[32] = new Node("Prying Beak");  // Optimized for fruit/nuts
        nodes[33] = new Node("Pointed Beak");  // Optimized for seads/plants

        // Glide modifiers
        nodes[34] = new Node("Improved Glide");
        nodes[35] = new Node("Improved Glide");
        nodes[36] = new Node("Improved Glide");
        nodes[37] = new Node("Improved Glide");
        nodes[38] = new Node("Improved Glide");

        // Speed modifiers
        nodes[39] = new Node("Improved Speed");
        nodes[40] = new Node("Improved Speed");
        nodes[41] = new Node("Improved Speed");
        nodes[42] = new Node("Improved Speed");
        nodes[43] = new Node("Improved Speed");


        //========MAMMILIAN TREES========\\
        nodes[44] = new Node("Hair/Fur");
        nodes[45] = new Node("live-born babies");
        nodes[46] = new Node("Larger Size");  // smaller increase
        nodes[47] = new Node("Thick Hide");
        nodes[48] = new Node("Defensive Horn");
        nodes[49] = new Node("Larger Size");  // larger increase
        nodes[50] = new Node("Defensive Tusks");
        nodes[51] = new Node("Elongated Mouth");
        nodes[52] = new Node("Prehensile Trunk");
        nodes[53] = new Node("Improved Appendage Muscles");
        nodes[54] = new Node("Smaller Size");
        nodes[55] = new Node("Improved Senses");
        nodes[56] = new Node("Lighter Body");
        nodes[57] = new Node("Larger Feet");
        nodes[58] = new Node("Claws");
        nodes[59] = new Node("Foot Padding");
        nodes[60] = new Node("Improved Stealth");
        nodes[61] = new Node("Retractable Claws");
        nodes[62] = new Node("Opposable Thumbs");
        nodes[63] = new Node("Prehensile Tail");
        nodes[64] = new Node("Bipedal Walk");
        nodes[65] = new Node("Removed Tail");  // Overrides Prehensile Tail
        nodes[66] = new Node("Tool Usage");
        nodes[67] = new Node("Creativity");
        nodes[68] = new Node("Imagination");
        nodes[69] = new Node("Self-Awareness");


        //========REPTILIAN TREES=========\\
        nodes[70] = new Node("Ectothermic");
        nodes[71] = new Node("Improved Swimming");  // "Webbing" instead?
        nodes[72] = new Node("Water Dependance");
        nodes[73] = new Node("Improved Jumping Abilities");
        nodes[74] = new Node("Smaller Size");
        nodes[75] = new Node("Hardened Skin");
        nodes[76] = new Node("Increased Lifespan");
        nodes[77] = new Node("Shells");
        nodes[78] = new Node("Scales");
        nodes[79] = new Node("Claws");
        nodes[80] = new Node("Armored Scales");
        nodes[81] = new Node("Sturdy Teeth");
        nodes[82] = new Node("Increased Size");  // Large increase
        nodes[83] = new Node("Powerful Jaw Muscles");
        nodes[84] = new Node("Flexible Skeleton");
        nodes[85] = new Node("Loss of Legs");
        nodes[86] = new Node("Threatening Colors/Patterns");
        nodes[87] = new Node("Retractible Fangs");
        nodes[88] = new Node("Strong Venom");
        nodes[89] = new Node("Increased Size");  // Medium increase
        nodes[90] = new Node("Improved Camoflague");
        nodes[91] = new Node("Strengthened Body Muscles");
        nodes[92] = new Node("Constricting Ability");
        nodes[93] = new Node("Threatening Colors/Patterns");
        nodes[94] = new Node("Poison-Secreting Skin");


        return nodes;
    }

    private int[,] generateEdges()
    {
        // Create an empty edge list
        int[,] edges = makeBaseEdgeGraph(NUMNODES);

        //===========CORE TREES===========\\

        // Carnivores
        edges[0, 1] = 1;
        edges[1, 2] = 1;
        edges[2, 3] = 1;

        edges[0, 4] = 1;
        edges[4, 5] = 1;
        edges[5, 6] = 1;
        edges[5, 7] = 1;

        edges[3, 8] = 1;
        edges[6, 8] = 1;
        edges[7, 8] = 1;
        edges[8, 9] = 1;
        edges[9, 10] = 1;
        edges[10, 11] = 1;
        edges[11, 12] = 1;
        edges[12, 13] = 1;
        edges[13, 14] = 1;

        // Herbivores
        edges[0, 15] = 1;
        edges[15, 16] = 1;
        edges[16, 17] = 1;
        edges[16, 18] = 1;
        edges[16, 19] = 1;
        edges[19, 20] = 1;

        //==========AVIARY TREES==========\\

        edges[0, 21] = 1;
        edges[21, 22] = 1;
        edges[21, 23] = 1;
        edges[23, 24] = 1;
        edges[24, 25] = 1;
        edges[24, 26] = 1;
        edges[26, 28] = 1;
        edges[25, 27] = 1;
        edges[27, 29] = 1;
        edges[29, 30] = 1;

        // Beak types
        edges[22, 31] = 1;
        edges[22, 32] = 1;
        edges[22, 33] = 1;

        // Wing optimization
        // -Glide
        edges[30, 34] = 1;
        edges[34, 35] = 1;
        edges[35, 36] = 1;
        edges[36, 37] = 1;
        edges[37, 38] = 1;
        // -Speed
        edges[30, 39] = 1;
        edges[39, 40] = 1;
        edges[40, 41] = 1;
        edges[41, 42] = 1;
        edges[42, 43] = 1;

        //========MAMMILIAN TREES========\\

        edges[0, 44] = 1;
        edges[44, 45] = 1;
        edges[45, 46] = 1;
        edges[45, 53] = 1;
        edges[45, 49] = 1;

        edges[46, 47] = 1;
        edges[46, 48] = 1;
        edges[47, 80] = 1;  // mammals -> reptiles

        edges[49, 50] = 1;
        edges[49, 51] = 1;
        edges[51, 52] = 1;

        edges[53, 54] = 1;
        edges[53, 56] = 1;
        edges[53, 62] = 1;
        edges[54, 55] = 1;
        edges[55, 26] = 1;  // mammals -> aviaries

        edges[56, 57] = 1;
        edges[56, 58] = 1;
        edges[57, 59] = 1;
        edges[59, 60] = 1;

        edges[62, 63] = 1;
        edges[62, 64] = 1;
        edges[64, 65] = 1;
        edges[64, 66] = 1;
        edges[66, 67] = 1;
        edges[67, 68] = 1;
        edges[68, 69] = 1;

        //========REPTILIAN TREES=========\\

        edges[0, 70] = 1;
        edges[70, 71] = 1;
        edges[70, 75] = 1;

        edges[71, 72] = 1;
        edges[71, 73] = 1;
        edges[73, 74] = 1;
        edges[73, 93] = 1;
        edges[93, 94] = 1;

        edges[75, 76] = 1;
        edges[75, 78] = 1;
        edges[76, 77] = 1;
        edges[77, 83] = 1;

        edges[78, 79] = 1;
        edges[78, 80] = 1;
        edges[78, 81] = 1;
        edges[78,84] = 1;
        edges[81, 82] = 1;
        edges[82, 83] = 1;

        edges[84, 85] = 1;
        edges[85, 83] = 1;
        edges[85, 86] = 1;
        edges[85, 89] = 1;

        edges[86, 87] = 1;
        edges[87, 88] = 1;

        edges[89, 90] = 1;
        edges[89, 91] = 1;
        edges[91, 92] = 1;

        return edges;
    }
}
