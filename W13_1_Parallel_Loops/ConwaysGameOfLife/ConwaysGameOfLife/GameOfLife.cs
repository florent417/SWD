using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class GameOfLife
    {
        private int _gridSize;
        // bool shows DOA
        private bool[,] _grid;
        private int _aliveOdds;

        GameOfLife(int size, int oddsOfBeingAlive)
        {
            _gridSize = size;

            _grid = new bool[_gridSize,_gridSize];

            Random randNr = new Random();

            for (int rowIndex = 0; rowIndex < _gridSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < _gridSize; colIndex++)
                {
                    if (randNr.Next(1, 100) <= 25)
                    {
                        _grid[rowIndex, colIndex] = true;
                    }
                    else
                    {
                        _grid[rowIndex, colIndex] = false;
                    }
                }
                
            }
        }

        public void CalcLivingNeighbours(int row, int col)
        {
            int livingNeighbours = 0;

            if (row==0)
            {
                if (col == 0)
                {
                    if (_grid[row + 1, col])
                    {
                        ++livingNeighbours;
                    }
                    if (_grid[row, col + 1])
                    {
                        ++livingNeighbours;
                    }
                    if (_grid[row + 1, col + 1])
                    {
                        ++livingNeighbours;
                    }
                }

                if (_grid[row + 1, col])
                {
                    ++livingNeighbours;
                }
                if (_grid[row, col + 1])
                {
                    ++livingNeighbours;
                }
                if (_grid[row, col - 1])
                {
                    ++livingNeighbours;
                }
                if (_grid[row + 1, col + 1])
                {
                    ++livingNeighbours;
                }
                if (_grid[row + 1, col - 1])
                {
                    ++livingNeighbours;
                }
            }


            if (row == _gridSize -1)
            {
                if (col == 0)
                {
                    if (_grid[row - 1, col])
                    {
                        ++livingNeighbours;
                    }
                    if (_grid[row, col + 1])
                    {
                        ++livingNeighbours;
                    }
                    if (_grid[row - 1, col + 1])
                    {
                        ++livingNeighbours;
                    }
                }

                if (_grid[row - 1, col])
                {
                    ++livingNeighbours;
                }
                if (_grid[row, col + 1])
                {
                    ++livingNeighbours;
                }
                if (_grid[row, col - 1])
                {
                    ++livingNeighbours;
                }
                if (_grid[row - 1, col + 1])
                {
                    ++livingNeighbours;
                }
                if (_grid[row - 1, col - 1])
                {
                    ++livingNeighbours;
                }
            }

            if (col == 0)
            {
                if (_grid[row - 1, col + 1])
                {
                    ++livingNeighbours;
                }
            }



            if (_grid[row + 1,col])
            {
                ++livingNeighbours;
            }

            if (_grid[row - 1, col])
            {
                ++livingNeighbours;
            }
        }


    }
}
