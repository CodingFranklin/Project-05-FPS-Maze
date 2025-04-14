using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MazeGenerator : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject player;
    public GameObject enemy;
    public GameObject Gun;
    public GameObject Sphere;
    public GameObject Exit;
    public GameObject Boost;

    const int N = 1, E = 2, S = 4, W = 8;
    Dictionary<Vector2, int> cell_walls = new Dictionary<Vector2, int>();

    public float tile_size = 10f;
    public int width = 10;
    public int height = 10;

    List<List<int>> map = new List<List<int>>();

    void Start()
    {
        cell_walls[new Vector2(0, -1)] = N;
        cell_walls[new Vector2(1, 0)] = E;
        cell_walls[new Vector2(0, 1)] = S;
        cell_walls[new Vector2(-1, 0)] = W;

        MakeMaze();

        // Spawn player
        Instantiate(player, new Vector3(5,1,5), Quaternion.identity);

        // Spawn Gun
        Instantiate(Gun, new Vector3(45,2,45), Quaternion.identity);
        Instantiate(Sphere, new Vector3(45,2,45), Quaternion.identity);

        // Spawn Exit
        Instantiate(Exit, new Vector3(95,2,95), Quaternion.identity);

        // Spawn Boosts
        Instantiate(Boost, new Vector3(25,2,15), Quaternion.identity);
        Instantiate(Boost, new Vector3(35,2,5), Quaternion.identity);
        Instantiate(Boost, new Vector3(55,2,65), Quaternion.identity);
        Instantiate(Boost, new Vector3(75,2,25), Quaternion.identity);
        Instantiate(Boost, new Vector3(85,2,45), Quaternion.identity);

        // Spawn enemies at different maze locations
        PlaceEnemyAtCell(2, 2);
        PlaceEnemyAtCell(3, 7);
        PlaceEnemyAtCell(6, 4);
        PlaceEnemyAtCell(8, 8);
        PlaceEnemyAtCell(1, 5);
        PlaceEnemyAtCell(7, 0);
        PlaceEnemyAtCell(4, 3);
    }

    private void MakeMaze()
    {
        List<Vector2> unvisited = new List<Vector2>();
        List<Vector2> stack = new List<Vector2>();

        for (int i = 0; i < width; i++)
        {
            map.Add(new List<int>());
            for (int j = 0; j < height; j++)
            {
                map[i].Add(N | E | S | W);
                unvisited.Add(new Vector2(i, j));
            }
        }

        Vector2 current = new Vector2(0, 0);
        unvisited.Remove(current);

        while (unvisited.Count > 0)
        {
            List<Vector2> neighbors = CheckNeighbors(current, unvisited);

            if (neighbors.Count > 0)
            {
                Vector2 next = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
                stack.Add(current);

                Vector2 dir = next - current;

                map[(int)current.x][(int)current.y] -= cell_walls[dir];
                map[(int)next.x][(int)next.y] -= cell_walls[-dir];

                current = next;
                unvisited.Remove(current);
            }
            else if (stack.Count > 0)
            {
                current = stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);
            }
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(tiles[map[i][j]]);
                tile.transform.parent = transform;
                tile.transform.position = CellToWorldPosition(i, j);
                tile.name = $"Tile {i} {j}";

                // ✅ Build navmesh on each tile again
                tile.GetComponentInChildren<NavMeshSurface>().BuildNavMesh();
            }
        }
    }

    private List<Vector2> CheckNeighbors(Vector2 cell, List<Vector2> unvisited)
    {
        List<Vector2> list = new List<Vector2>();
        foreach (var n in cell_walls.Keys)
        {
            if (unvisited.Contains(cell + n))
                list.Add(cell + n);
        }
        return list;
    }

    private Vector3 CellToWorldPosition(int row, int col)
    {
        return new Vector3(col * tile_size, 0, row * tile_size);
    }

    private void PlaceEnemyAtCell(int row, int col)
    {
        Vector3 pos = CellToWorldPosition(row, col) + Vector3.up;
        Instantiate(enemy, pos, Quaternion.identity);
    }

    
}
