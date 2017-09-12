using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    private string name;
    private int food_meat;
    private int food_leaves;
    private int food_fruit;
    private int climate;
    
    public Node(string name)
    {
        this.name = name;
        this.food_meat = 0;
        this.food_leaves = 0;
        this.food_fruit = 0;
        this.climate = 0;
    }
    public Node(string name,int food_meat, int food_leaves,  int food_fruit, int climate)
    {
        this.name = name;
        this.food_meat = food_meat;
        this.food_leaves = food_leaves;
        this.food_fruit = food_fruit;
        this.climate = climate;
    }

    public string getName()
    {
        return name;
    }
    public int getMeat()
    {
        return food_meat;
    }
    public int getLeaves()
    {
        return food_leaves;
    }
    public int getFruit()
    {
        return food_fruit;
    }
    public int getClimate()
    {
        return climate;
    }
}
