using Tiles;
using UnityEngine;

public class TileFenwickTree2D
{
        private int[,] tree;
        public int row { get; private set; }
        public int col { get; private set; }

        public TileFenwickTree2D(BaseTile[,] tiles)
        {
                row = tiles.GetLength(0);
                col = tiles.GetLength(1);
                tree = new int[row+1, col+1];

                for (int i = 0; i < row; i++)
                {
                        for (int j = 0; j < col; j++)
                        {
                                Update(i, j, (tiles[i, j] is AirTile) ? 0 : 1);
                        }
                }
                
        }

        public void Update(int x, int y,int delta)
        {
                x += 1;
                y += 1;
                for (int i = x; i <= row; i += GetLowBit(i))
                {
                        for (int j = y; j <= col; j += GetLowBit(j))
                        {
                                tree[i, j] += delta;
                        }
                }
        }

        public int Query(int x, int y)
        {
                int sum = 0;
                x += 1;
                y += 1;

                for (int i = x; i > 0; i-= GetLowBit(i))
                {
                        for (int j = y; j> 0; j-= GetLowBit(j))
                        {
                                sum += tree[i, j];
                        }     
                }
                return sum;
        }

        public int QueryRange(int x, int y, int w, int h)
        {
                return Query(x+w-1, y+h-1) - Query(x-1,y+h-1) - Query(x+w-1,y-1) + Query(x-1,y-1);
        }

        public bool QueryRangeIsAir(int x, int y, int w, int h)
        {
                return QueryRange(x,y,w,h) == 0;
        }
        

        private int GetLowBit(int x)
        {
                return x & -x;
        }
}