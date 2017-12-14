using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private string name;                // The name of the node
    private int[] herbivoreFoodSource;  // i == 0 berries, i == 1 nuts, i == 2 grass, i == 3 leaves, 0 (default) means speceis cannot eat food type at given index
    private int carnivoreFoodSource;    // integer between 1 and 500 (subject to change) that limits what size prey you can eat
    private int requiredCalories;       // amount of food to survive
    private int creatureSize;           // 1 is tiny, 2 - 100 is small, 101 - 200 is medium, 201 - 300 is large, 301 - 400 is humongous
    private int litterSize;             // population growth per reproduction
    private int reproductionRate;       // Mating frequency
    private int mutationChance;         // mate attachment (0-100% chance)
    private int carnivorous;            // 0 == can't eat meat
    private int offspringSize;          // size of the offspring (calorie worth and which size class it belongs to)
    private int altitude;               // the height that your creature can go up to (or how high you can fly, for aviaries)
    private int canFly;                 // 0 == can't fly at all (unable to cross water)
    private int dexterity;              // defensive trait
    private int maxPerTile;             // max number of individuals of a species in a given tile, -1 is unlimited
    private int peckingOrder;           // determines when the species eats in the eating algorithm
    private int offspringSurvivalChance;// the chance that any given offspring will survive to adulthood
    private int canSwim;                // 0 == can't swim (unable to cross water)

    public Node(string name)
    {
        this.name = name;
        herbivoreFoodSource = new int[] {0,0,0,0};
        carnivoreFoodSource = 0;
        requiredCalories = 0;
        creatureSize = 0;
        maxPerTile = 0;
        litterSize = 0;
        reproductionRate = 0;
        mutationChance = 0;
        peckingOrder = 0;
        offspringSurvivalChance = 0;
        canSwim = 0;

    }

    public string getName()
    {
        return name;
    }
    public int[] getHerbivoreFoodSource()
    {
        return herbivoreFoodSource;
    }
    public void setHerbivoreFoodSource(int[] change)
    {
        herbivoreFoodSource = change;
    }

    public int getCarnivoreFoodSource()
    {
        return carnivoreFoodSource;
    }
    public void setCarnivoreFoodSource(int change)
    {
        carnivoreFoodSource = change;
    }

    public int getRequiredCalories()
    {
        return requiredCalories;
    }
    public void setRequiredCalories(int change)
    {
        requiredCalories = change;
    }

    public int getCreatureSize()
    {
        return creatureSize;
    }
    public void setCreatureSize(int change)
    {
        creatureSize = change;
    }

    public int getLitterSize()
    {
        return litterSize;
    }
    public void setLitterSize(int change)
    {
        litterSize = change;
    }

    public int getReproductionRate()
    {
        return reproductionRate;
    }
    public void setReproductionRate(int change)
    {
        reproductionRate = change;
    }

    public int getMutationChance()
    {
        return mutationChance;
    }
    public void setMutationChance(int change)
    {
        mutationChance = change;
    }

    public int getCarnivorous()
    {
        return carnivorous;
    }
    public void setCarnivorous(int change)
    {
        carnivorous = change;
    }

    public int getOffspringSize()
    {
        return offspringSize;
    }
    public void setOffspringSize(int change)
    {
        offspringSize = change;
    }

    public int getAltitude()
    {
        return altitude;
    }
    public void setAltitude(int change)
    {
        altitude = change;
    }

    public int getCanFly()
    {
        return canFly;
    }
    public void setCanFly(int change)
    {
        canFly = change;
    }

    public int getDexterity()
    {
        return dexterity;
    }
    public void setDexterity(int change)
    {
        dexterity = change;
    }

    public int getMaxPerTile()
    {
        return maxPerTile;
    }
    public void setMaxPerTile(int change)
    {
        maxPerTile = change;
    }

    public int getPeckingOrder()
    {
        return peckingOrder;
    }
    public void setPeckingOrder(int change)
    {
        peckingOrder = change;
    }

    public int getOffspringSurvivalChance()
    {
        return offspringSurvivalChance;
    }
    public void setOffspringSurvivalChance(int change)
    {
        offspringSurvivalChance = change;
    }

    public int getCanSwim()
    {
        return canSwim;
    }
    public void setCanSwim(int change)
    {
        canSwim = change;
    }
}
