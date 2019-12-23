using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3
{
    public class Vertex //вершины
    {
        public Vertex()
        {
            this.neighbors = new List<Edge>();
        }
        public int index { get; set; }
   
        public List<Edge> neighbors { get; set; }//список соседних вершин через ребра
    }

    public class Edge//ребра
    {
        public Edge()
        {
            this.vertex1 = new Vertex();
            this.vertex2 = new Vertex();
        }
        public int distance { get; set; }//вес ребра

        //смежные вершины (инциндентные ребру)
        public Vertex vertex1 { get; set; }
        public Vertex vertex2 { get; set; }
    }

    public class Graph
    {
        public Graph()
        {
            this.vertices = new List<Vertex>();
            this.edges = new List<Edge>();
        }

        public List<Vertex> vertices
        {
            get;
            set;
        }

        public List<Edge> edges
        {
            get;
            set;
        }

        //заполнение графа из матрицы смежности
        public Graph CreateGraph(int size, int[,] adjacencyMatrix)
        {
            Graph graph = new Graph();

            //определяем список вершин графа
            for (int i = 0; i < size; i++)
            {
                Vertex vertex = new Vertex();
                vertex.index = i;
                graph.vertices.Add(vertex);
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (adjacencyMatrix[j, i] != 0) //если вершина смежна с какой-то
                    {
                        Edge edge = new Edge();
                        edge.distance = adjacencyMatrix[j, i];//записываем вес ребра между смежными вершинами

                        //запоминаем смежные вершины 
                        edge.vertex1 = graph.vertices[i];
                        edge.vertex2 = graph.vertices[j];

                        graph.vertices[i].neighbors.Add(edge);//записываем , что вторая вершина это сосед первой и запоминаем ребро

                       
                        graph.edges.Add(edge);
                    }
                }
            }

            return graph;
        }
    }
}
