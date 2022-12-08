using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Work in progress planet generation, very similar to the code from last hw, that intentional. I figure
//something like this could work, may have to adjust some stuff but overall might be good.
enum TileType
{
    PLANET = 0,
    SPACE = 1
}
public class PlanetGen : MonoBehaviour
{
    public int width = 10; //Size of level, in 1000s
    public int length = 10; //Size of level, in 1000s
    // Start is called before the first frame update

    //field/variables
    private int num_planets = 0;
    private List<int[]> pos_planets;

    private void Shuffle<T>(ref List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

    }
    void Start()
    {

        num_planets = 0;
        List<TileType>[,] grid = new List<TileType>[width,length]; //act 10,000 x 10,000 but for simplicity we can just update values by 1000
        List<int[]> unassigned = new List<int[]>();
        
        num_planets = width * length / 20 + 1;
        pos_planets = new List<int[]>();

        bool success = false;
        while (!success)
        {
            for(int v = 0; v < num_planets; v++)
            {
                while (true)
                {
                    int wr = Random.Range(1,width - 1);
                    int lr = Random.Range(1,length - 1);

                    if(grid[wr,lr] == null)
                    {
                        grid[wr, lr] = new List<TileType> {TileType.PLANET };
                        pos_planet.Add(new int [2] {wr, lr });
                        break;
                    }
                }


            }

            success = BackTrackingSearch(grid, unassigned);
            if (!success)
            {
                Debug.Log("Could not find valid solution, the big bang will happen again");
                unassigned.Clear();
                grid = new List<TileType>[width, length];
            }

        }
        //This will place planets in the grid/world.
        BigBang(grid);

    }
    
    //We never want two planets on the same row since they could orbit on the same direction and crash into another
    bool ArePlanetsOnTheSameRow(List<TileType>[,] grid)
    {
        int count = 0;

        for (int w = 0; w < width -1; w++)
        {
            for (int l = 0; l < length-1; l++)
            {

                if(grid[w,l][0] == TileType.PLANET)
                {

                    count += 1;

                }
            }
            
            if(count > 1)
            {

                return true;

            }

        }

        return false;

    }
    //Code I still need to review should work ideally but good to go voer it since it still needs to be modified to our specs.
    bool CheckConsistency(List<TileType>[,] grid, int[] cell_pos, TileType t)
    {
        int w = cell_pos[0];
        int l = cell_pos[1];

        List<TileType> old_assignment = new List<TileType>();
        old_assignment.AddRange(grid[w, l]);
        grid[w, l] = new List<TileType> { t };

        bool areWeConsistent = !ArePlanetsOnTheSameRow(grid);

        grid[w, l] = new List<TileType>();
        grid[w, l].AddRange(old_assignment);
        return areWeConsistent;
    }

    bool BackTrackingSearch(List<TileType>[,] grid,  List<int[]> unassigned)
    {

        if (function_calls++ > 100000)       
            return false;

        if (unassigned.Count == 0)
            return true;

        int[] cell_pos = unassigned[0];
        unassigned.RemoveAt(0);
        
        foreach(TileType t in grid[cell_pos[0],cell_pos[1]]){

            if(CheckConsistency(grid ,cell_pos , t )){

                List<TileType> old_assignment = new List<TileType>();
                old_assignment.AddRange(grid[cell_pos[0],cell_pos[1]]);
                grid[cell_pos[0],cell_pos[1]] = new List<TileType> { t };
                if(BackTrackingSearch(grid, unassigned)){
                    return true;
                }                
                grid[cell_pos[0],cell_pos[1]] = old_assignment;

            }
        }
        return false;

    }
}
