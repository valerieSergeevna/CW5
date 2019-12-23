using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3
{
    public class TSP
    {
        public TSP()
        {
            visitedVertices = new Stack<Vertex>();
            verticesStack = new Stack<Vertex>();
            shortestPath = new List<Vertex>();
            minDistance = 0;
        }

        public Graph graph
        {
            get;
            set;
        }
        public int minDistance
        {
            get;
            private set;
        }

        
        List<Vertex> shortestPath;
        Vertex startVertex;
        Stack<Vertex> visitedVertices;
        Stack<Vertex> verticesStack;
        public List<Vertex> ShortestPath
        {
            get { return shortestPath; }
            set { }
            }
        int count = 0;
        int CurDistance = 0;
        public List<Vertex> FindShortestPathFromAll()
        {
            foreach (Vertex vertex in graph.vertices)//начинаем искать циклы от каждой вешины
            {
                startVertex = vertex;
                
               
                if (NearestNeighbour(startVertex) && (minDistance == 0 || CurDistance < minDistance)) //если получившийся цикл меньше по весу, чем был найден доэтого, то зпоминаем его
                {
                    minDistance = CurDistance;
                    shortestPath.Clear();
                    shortestPath.AddRange(verticesStack.ToList());
                }
            }

            return shortestPath;
        }

       

        private bool NearestNeighbour(Vertex startVx)
        {
            count++;
            visitedVertices.Push(startVx);//добавляем вершину в стек посещенных
            verticesStack.Push(startVx);//добавляем вершину в стек вершин

            Edge nextEdge = null;

            foreach (Edge edge in startVx.neighbors)
            {
                if (edge.vertex2 == startVertex)//если цикл замкнулся, то начинаем собирать его и высчитывать его вес
                {
                    if (count == graph.vertices.Count)
                    {
                        verticesStack.Push(edge.vertex2);
                        CurDistance += edge.distance;
                        return true;
                    }
                }

                if (!visitedVertices.Contains(edge.vertex2))//если смежна вершина была непосещенная и вес ребра между ними меньше, 
                    //чем между другими смежными вершинами, то запоминаем ребро
                {
                    if (nextEdge == null || nextEdge.distance > edge.distance)
                    {
                        nextEdge = edge;
                    }
                }
            }

            if (nextEdge != null)
            {
                if (NearestNeighbour(nextEdge.vertex2))//если сосед был, то  идем дальше от vertex2
                {
                    CurDistance += nextEdge.distance;//собираем вес цикла
                    return true;
                }
            }

            visitedVertices.Pop();//если цикла по данному пути нет, то возвращаемся назад
            verticesStack.Pop();
            count--;
            return false;
        }
    }
}
