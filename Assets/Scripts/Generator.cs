using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject groundPrefab;
    public GameObject vallPrefab;
    public float fragmentation;
    public int fixGround;
    public int fixWall;
    public int fixDirect = 6;

    static public int[,] MAP;


    void Awake()
    {
        width = 21;
        height = 11;

        GameObject Maze = new GameObject("Maze");
        Transform MAZE = Maze.transform;
        CreateMaze();
        FixedWall();
        FixedWall();
        FixedGround();
        FixedGround();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {

                if (MAP[i, j] == 1 || MAP[i, j] == 10)
                {
                    GameObject go = Instantiate<GameObject>(vallPrefab);
                    go.transform.SetParent(MAZE);
                    go.transform.position = new Vector3(i, j, 0);
                }
                else
                {
                    GameObject go = Instantiate<GameObject>(groundPrefab);
                    go.transform.SetParent(MAZE);
                    go.transform.position = new Vector3(i, j, 0);
                }
            }
        }
    }

    private void CreateMaze()
    {
        MAP = new int[width, height];
        GameObject Maze = new GameObject("Maze");
        Transform MAZE = Maze.transform;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || j == 0 || i == width - 1 || j == height - 1)
                {
                    MAP[i, j] = 10;
                }

                else if (i % 2 == 0 && j % 2 == 0)
                {
                    if (Random.value > 0.1f)
                    {
                        MAP[i, j] = 1;

                        int a = Random.value < fragmentation ? 0 : (Random.value < fragmentation ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < fragmentation ? -1 : 1);
                        MAP[i + a, j + b] = 1;
                    }
                }

                if (MAP[i, j] != 1) MAP[i, j] = 0;
            }
        }
    }

    private void FixedGround()
    {
        for (int i = 1; i < width - 1; i++)
        {
            for (int j = 1; j < height - 1; j++)
            {
                if (MAP[i, j] == 0)
                {
                    int k = 0;
                    int a = MAP[i - 1, j];
                    int b = MAP[i + 1, j];
                    int c = MAP[i, j - 1];
                    int d = MAP[i, j + 1];
                    k = (a + b + c + d);

                    for (int l = k; l > fixGround; l--)
                    {
                        float rnd = Random.value;
                        if (MAP[i, j - 1] == 1 && rnd > 0.75)
                        {
                            int x = i;
                            int y = j - 1;
                            TileReplacement(x, y);
                        }
                        else if (MAP[i + 1, j] == 1 && rnd > 0.5 && rnd < 0.75)
                        {
                            int x = i + 1;
                            int y = j;
                            TileReplacement(x, y);
                        }
                        else if (MAP[i, j + 1] == 1 && rnd > 0.25 && rnd < 0.5)
                        {
                            int x = i;
                            int y = j + 1;
                            TileReplacement(x, y);
                        }
                        else if (MAP[i - 1, j] == 1)
                        {
                            int x = i - 1;
                            int y = j;
                            TileReplacement(x, y);
                        }
                    }
                }
            }
        }
    }

    private void FixedWall()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || j == 0 || i == width - 1 || j == height - 1 || i == width || j == height)
                {
                    MAP[i, j] = 1;
                }
                else if (MAP[i, j] == 1)
                {
                    int k = 0;
                    int a = MAP[i - 1, j];
                    int b = MAP[i + 1, j];
                    int c = MAP[i, j - 1];
                    int d = MAP[i, j + 1];
                    k = (a + b + c + d);


                    for (int l = k; l < fixWall; l++)
                    {
                        float rnd = Random.value;
                        if (MAP[i, j - 1] == 0 && rnd > 0.75)
                        {
                            int x = i;
                            int y = j - 1;
                            TileReplacement(x, y);
                        }
                        else if (MAP[i + 1, j] == 0 && rnd > 0.5 && rnd < 0.75)
                        {
                            int x = i + 1;
                            int y = j;
                            TileReplacement(x, y);
                        }
                        else if (MAP[i, j + 1] == 0 && rnd > 0.25 && rnd < 0.5)
                        {
                            int x = i;
                            int y = j + 1;
                            TileReplacement(x, y);
                        }
                        else if (MAP[i - 1, j] == 0)
                        {
                            int x = i - 1;
                            int y = j;
                            TileReplacement(x, y);
                        }
                    }
                }
                else if (i == 2)
                {
                    int k = 0;
                    int b = MAP[i + 1, j];
                    int c = MAP[i, j - 1];
                    int d = MAP[i, j + 1];
                    int e = MAP[i + 1, j + 1];
                    int f = MAP[i + 1, j - 1];
                    k = b + c + d + e + f;
                    if (k == 1 && b == 1) TileReplacement(i, j);
                }
                else if (j == 2)
                {
                    int k = 0;
                    int a = MAP[i - 1, j];
                    int b = MAP[i + 1, j];
                    int d = MAP[i, j + 1];
                    int e = MAP[i + 1, j + 1];
                    int f = MAP[i - 1, j + 1];
                    k = a + b + d + e + f;
                    if (k == 1 && d == 1) TileReplacement(i, j);
                }
                else if (i == height - 1)
                {
                    int k = 0;
                    int a = MAP[i - 1, j];
                    int b = MAP[i + 1, j];
                    int c = MAP[i, j - 1];
                    int e = MAP[i + 1, j - 1];
                    int f = MAP[i - 1, j - 1];
                    k = a + b + c + e + f;
                    if (k == 1 && c == 1) TileReplacement(i, j);
                }
                else if (j == width - 1)
                {
                    int k = 0;
                    int a = MAP[i - 1, j];
                    int c = MAP[i, j - 1];
                    int d = MAP[i, j + 1];
                    int e = MAP[i - 1, j + 1];
                    int f = MAP[i - 1, j - 1];
                    k = a + c + d + e + f;
                    if (k == 1 && a == 1) TileReplacement(i, j);
                }
                else if (MAP[i, j] == 0)
                {
                    int k = 0;
                    int a = MAP[i - 1, j];
                    int b = MAP[i + 1, j];
                    int c = MAP[i, j - 1];
                    int d = MAP[i, j + 1];
                    int ac = MAP[i - 1, j - 1];
                    int ad = MAP[i - 1, j + 1];
                    int bc = MAP[i + 1, j - 1];
                    int bd = MAP[i + 1, j + 1];
                    k = a + c + d + ac + ad + bc + bd;
                    if (k < 3) TileReplacement(i, j);
                }
            }
        }
    }

    private void TileReplacement(int x, int y)
    {
        if (x > 0 && x < width - 1 && y > 0 && y < height - 1)
        {
            if (MAP[x, y] == 0)
            {
                MAP[x, y] = 1;
            }
            else if (MAP[x, y] == 1)
            {
                MAP[x, y] = 0;
            }
            else if (MAP[x, y] == 10) return;
        }
        else return;
    }

    public static int[,] GetMap ()
    {
        return MAP;
    }

    public static int width { get; set; }
    public static int height { get; set; }
}
