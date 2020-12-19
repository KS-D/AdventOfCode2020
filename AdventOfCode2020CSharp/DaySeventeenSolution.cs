using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020CSharp
{
    class CubeGrid
    {
        public List<List<char>> GridList { get; set; } = new();
        public int Y => GridList.Count;

        public int X => GridList.Count > 0 ? GridList[0].Count : 0;

        public List<char> this[int i]
        {
            get => GridList[i];
            set => GridList[i] = value;
        }

        public void Add(List<char> toAdd)
        {
            GridList.Add(toAdd);
        }

        public void AddNewCubes()
        {
            foreach (var grid in GridList)
            {
                grid.Insert(0, '.');
                grid.Add('.');
            }

            List<char> emptyX = new List<char>();
            for (int i = 0; i < X; i++)
            {
                emptyX.Add('.');
            }
            GridList.Add(emptyX);
            GridList.Insert(0, emptyX.ToList()); // ToList creates a copy
        }

        public static CubeGrid GenerateGrid(int x, int y)
        {
            CubeGrid cg = new();
            for (int i = 0; i < y; i++)
            {
                List<char> newList = new();

                for (int j = 0; j < x; j++)
                {
                    newList.Add('.');
                }
                cg.Add(newList);
            }

            return cg;
        }

        public CubeGrid Clone()
        {
            CubeGrid cg = new();
            foreach (var gl in GridList)
            {
                cg.Add(gl.ToList()); // to list clone
            }

            return cg;
        }

        public override string ToString()
        {
            StringBuilder sb = new("");
            foreach (var gl in GridList)
            {
                sb.Append(gl.ToArray());
                sb.Append($"{Environment.NewLine}");
            }

            return sb.ToString();
        }
    }
    
    class DaySeventeenSolution
    {
        public List<CubeGrid> CubeLayers { get; set; } = new();
        
        // Need to make this 4 dimensional
        public List<List<CubeGrid>> HyperCube { get; set; } = new();

        private void AddNewHyperCubeItems()
        {
            int z = HyperCube[0].Count;
            int y = HyperCube[0][0].X;
            int x = HyperCube[0][0].Y;

            List<CubeGrid> cgList = new(z);
            List<CubeGrid> cgList2 = new(z); // todo: should probably preallocate the other lists
            var cubeGrid = CubeGrid.GenerateGrid(x, y);
            for (int i = 0; i < z; i++)
            {
                cgList.Add(cubeGrid.Clone());
                cgList2.Add(cubeGrid.Clone()); 
            }
                       
            HyperCube.Insert(0, cgList);
            HyperCube.Add(cgList2);

        }

        private List<List<CubeGrid>> CloneHyperCube()
        {
            int w = HyperCube.Count;
            int z = HyperCube[0].Count;
            int y = HyperCube[0][0].Y;
            int x = HyperCube[0][0].X;
            List<List<CubeGrid>> hyperClone = new(w);

            foreach (var cube in HyperCube)
            {
                List<CubeGrid> Cubes3D = new(z);
                foreach (var square in cube)
                {
                    Cubes3D.Add(square.Clone());
                }
                hyperClone.Add(Cubes3D);
            }

            return hyperClone;
        }
        
        public void Parse(string fileName)
        {
            using StreamReader sr = new(fileName);
            CubeGrid cubeGrid = new();
            while (!sr.EndOfStream)
            {
                string temp = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    cubeGrid.Add(temp.ToList());
                }
            }
            CubeLayers.Add(cubeGrid);
        }

        private List<CubeGrid> CloneCubeGrids()
        {
            List<CubeGrid> cg = new();

            foreach (var cubeGrid in CubeLayers)
            {
                cg.Add(cubeGrid.Clone());
            }

            return cg;
        }
        
        public void UpdateCubeGrid3D()
        {
            foreach (var cubeGrid in CubeLayers)
            {
                cubeGrid.AddNewCubes();
            }

            int x = CubeLayers[0].X;
            int y = CubeLayers[0].Y;
            
            // todo, add this to update4d
            CubeLayers.Add(CubeGrid.GenerateGrid(x, y));
            CubeLayers.Insert(0, CubeGrid.GenerateGrid(x, y));

            var cubeStackClone = CloneCubeGrids();
            
            for(int i = 0; i < CubeLayers.Count; i++)
            {
                for(int j = 0; j < CubeLayers[0].Y; j++)
                {
                    for (int p = 0; p < CubeLayers[0].X; p++)
                    {
                        CheckEachCube3D(p, j, i, cubeStackClone);                        
                    }
                }
            }

            CubeLayers = cubeStackClone;
        }

        public int CheckEachCube3D(int x, int y, int z, List<CubeGrid> cubeGrids)
        {
            bool active = cubeGrids[z][y][x] == '#';
            HashSet<(int x, int y, int z)> neighbors = GenerateNeighbors3D(x,y,z);

            int activeNeighborCount = 0;
            foreach (var neighbor in neighbors)
            {
                if (CubeLayers[neighbor.z][neighbor.y][neighbor.x] == '#')
                    activeNeighborCount++;
            }

            if (active && activeNeighborCount < 2
                        || activeNeighborCount > 3)
            {
                cubeGrids[z][y][x] = '.';
            }
            else if (!active && activeNeighborCount == 3)
            {
                cubeGrids[z][y][x] = '#';
            }

            return activeNeighborCount;
        }

        public void PrintCubes(List<CubeGrid> cubeGrids)
        {
            int offset = (cubeGrids.Count/2) - cubeGrids.Count + 1;
            foreach (var cl in cubeGrids)
            {
                Console.WriteLine($"Z = {offset}");
                Console.WriteLine(cl.ToString());
                Console.WriteLine($"{Environment.NewLine + Environment.NewLine}");
                offset++;
            }
        }

        // how to add w: foreach tuple add, create one that has w, w-1, w+1, then you need
        // the base case where x,y,z, w+|-1 to make the total (3 * 26) + 2 = 80 
        private HashSet<(int x, int y, int z)> GenerateNeighbors3D(int x, int y, int z)
        {
            var xyz = new HashSet<(int x, int y, int z)>
            {   // all 26 combinations 
                (x + 1, y, z),
                (x + 1, y, z + 1),
                (x + 1, y, z - 1),
                (x + 1, y + 1, z),
                (x + 1, y + 1, z + 1),
                (x + 1, y + 1, z - 1),
                (x + 1, y - 1, z),
                (x + 1, y - 1, z + 1),
                (x + 1, y - 1, z - 1),
                (x, y, z + 1),
                (x, y, z - 1),
                (x, y + 1, z),
                (x, y + 1, z + 1),
                (x, y + 1, z - 1),
                (x, y - 1, z),
                (x, y - 1, z + 1),
                (x, y - 1, z - 1),
                (x - 1, y, z),
                (x - 1, y + 1, z),
                (x - 1, y - 1, z),
                (x - 1, y + 1, z + 1),
                (x - 1, y + 1, z - 1),
                (x - 1, y - 1, z + 1),
                (x - 1, y - 1, z - 1),
                (x - 1, y, z + 1),
                (x - 1, y, z - 1)
            };

            return xyz
                .Where(point => 
                        point.x >= 0 && 
                        point.x < CubeLayers[0].X &&
                        point.y >=0 && 
                        point.y < CubeLayers[0].Y && 
                        point.z >= 0 && 
                        point.z < CubeLayers.Count
                    )
                    .ToHashSet();
        }

        private HashSet<(int x, int y, int z, int w)> GenerateNeighbors4D(
                                                                        int x,
                                                                        int y,
                                                                        int z,
                                                                        int w)
        {
            HashSet<(int x, int y, int z, int w)> neighbors4D 
                                = new HashSet<(int x, int y, int z, int w)>();
            int wBound = HyperCube.Count;
            var neighbors3D = GenerateNeighbors3D(x, y, z);
            foreach (var neighbors in neighbors3D)
            {
                neighbors4D.Add((neighbors.x,neighbors.y, neighbors.z, w));
                if (w - 1 >= 0)
                    neighbors4D.Add((neighbors.x,neighbors.y, neighbors.z, w - 1));
                if (w + 1 < wBound)
                    neighbors4D.Add((neighbors.x,neighbors.y, neighbors.z, w + 1));
            }

            if (w - 1 >= 0) neighbors4D.Add((x, y, z, w - 1));
            if(w + 1 < wBound) neighbors4D.Add((x, y, z, w + 1));
            
            return neighbors4D;
        }
        
        public void UpdateCubeGrid4D()
        {

            foreach (var cubes3D in HyperCube)
            {
                foreach (var cubeGrid in cubes3D)
                {
                    cubeGrid.AddNewCubes();
                } 
                cubes3D.Add(CubeGrid.GenerateGrid(cubes3D[0].X, cubes3D[0].Y));
                cubes3D.Insert(0, CubeGrid.GenerateGrid(cubes3D[0].X, cubes3D[0].Y));
            } 
            
            AddNewHyperCubeItems();

            List<List<CubeGrid>> hyperCubeClone = CloneHyperCube();
            
            int x = HyperCube[0][0].X;
            int y = HyperCube[0][0].Y;
            
            for(int w = 0; w < HyperCube.Count; w++)
            {
                for (int i = 0; i < HyperCube[0].Count; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        for (int p = 0; p < x; p++)
                        {
                            //CheckEachCube4D2(p, j, i, w, hyperCubeClone);
                            CheckEachCube4D(p, j, i, w, hyperCubeClone);
                        }
                    }
                }
            }

            HyperCube = hyperCubeClone;
        }

        private void CheckEachCube4D2(int x, int y, int z, int w, List<List<CubeGrid>> hyperCubeClone)
        {
            bool active = hyperCubeClone[w][z][y][x] == '#';
            HashSet<(int x, int y, int z, int w)> neighbors = GenerateNeighbors4D(x,y,z,w);

            int activeNeighborCount = 0;
            foreach (var neighbor in neighbors)
            {
                if (HyperCube[neighbor.w][neighbor.z][neighbor.y][neighbor.x] == '#')
                    activeNeighborCount++;
            }

            if (active && activeNeighborCount < 2
                        || activeNeighborCount > 3)
            {
                hyperCubeClone[w][z][y][x] = '.';
            }
            else if (!active && activeNeighborCount == 3)
            {
                hyperCubeClone[w][z][y][x] = '#';
            }

        }
        
        private void CheckEachCube4D(int x, int y, int z, int w, List<List<CubeGrid>> hyperCubeClone)
        {
            bool isActive = hyperCubeClone[w][z][y][x] == '#';
            char previousState = ' ';
            int activeCubes = 0;
            if (w + 1 < hyperCubeClone.Count)
            {
                previousState = hyperCubeClone[w + 1][z][y][x];
                if (previousState == '#')
                {
                    activeCubes++;
                }
                activeCubes += CheckEachCube3D(x, y, z, hyperCubeClone[w + 1]);
                hyperCubeClone[w + 1][z][y][x] = previousState;
            }
            if (w - 1 >= 0)
            {
                previousState = hyperCubeClone[w - 1][z][y][x];
                if (previousState == '#')
                {
                    activeCubes++;
                }
                activeCubes += CheckEachCube3D(x, y, z, hyperCubeClone[w - 1]);
                hyperCubeClone[w - 1][z][y][x] = previousState;
            }
            
            activeCubes += CheckEachCube3D(x, y, z, hyperCubeClone[w]); 
            if (isActive && activeCubes < 2
                || activeCubes > 3)
            {
                hyperCubeClone[w][z][y][x] = '.';
            }
            else if (!isActive && activeCubes == 3)
            {
                hyperCubeClone[w][z][y][x] = '#';
            }
        }


        public long CountActive3D(List<CubeGrid> cubeGrids)
        {
            long activeSum = 0;

            foreach (var grids in cubeGrids)
            {
                activeSum += grids.GridList
                    .SelectMany(x => x)
                    .Count(y => y == '#');
            }

            return activeSum;
        }

        public void PrintHyperCube()
        {
            int offset = (HyperCube.Count/2) - HyperCube.Count + 1; 
            foreach (var cube in HyperCube)
            {
                Console.WriteLine($"W: {offset}");
                PrintCubes(cube);
                offset++;
            }
        }
    }
}
