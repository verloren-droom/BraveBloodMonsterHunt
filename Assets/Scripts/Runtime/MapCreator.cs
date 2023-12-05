using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BraveBloodMonsterHunt
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField,Required] private Tilemap map;
        [SerializeField, Required] private TileBase floorTile;
        
        [SerializeField] private int step = 5;
        [SerializeField] private int seed = 666;

        private Dictionary<Vector2Int, int> m_Load = new();

        [Button]
        public void GenFloor()
        {
            map.ClearAllTiles();
            
            // var layout = RandomChunkLayout(5);
            // // layout = NormalizeLayout(layout);
            // var maxX = layout.Max(p=>p.x) - layout.Min(p=>p.x);
            // var maxY = layout.Max(p=>p.y) - layout.Min(p=>p.y);
            // byte[,] map = new byte[maxX, maxY];
            //
            // for (int i = 0; i < maxX; i++)
            // {
            //     for (int j = 0; j < maxY; j++)
            //     {
            //         map[i, j] = 255;
            //     }
            // }
            //
            // foreach(var layoutPosition in layout)
            // {
            //     var leftBottom = new Vector2Int(layoutPosition.x * 30, layoutPosition.y * 30);
            //
            //     for (var x = leftBottom.x; x < leftBottom.x + 30; x++)
            //     for (var y = leftBottom.y; y < leftBottom.y + 30; y++)
            //         map[x, y] = 0;
            // }
        }
        
        private Vector2Int[] RandomChunkLayout(int count) {
            var result = new Vector2Int[count];
            result[0] = new Vector2Int(0, 0);
            var edge = new List<Vector2Int>(); 
            edge.AddRange(GetNeighbors4(result[0]));
            
            var random = new System.Random(seed);
            for (int i = 1; i < count; i++)
            {
                var curr = edge[random.Next(0, edge.Count)];
                result[i] = curr;
                edge.Remove(curr);
                
                foreach (var neighbor in GetNeighbors4(curr).Where(neighbor => !Array.Exists(result, v => v.Equals(neighbor))).Where(neighbor => !edge.Contains(neighbor)))
                {
                    edge.Add(neighbor);
                }
            }

            return result;
        }

        private Vector2Int[] GetNeighbors4(Vector2Int pos)
        {
            return new Vector2Int[] { pos + Vector2Int.up, pos + Vector2Int.down, pos + Vector2Int.left, pos + Vector2Int.right };
        }
    }
}