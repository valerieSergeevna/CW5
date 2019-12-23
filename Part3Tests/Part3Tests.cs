using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Part3;

namespace Part3Tests
{
    [TestClass]
    public class Part3Tests
    {
        [TestMethod]
        public void NeighborTest()
        {
            int n = 4;

            int[,] adjMatrix = {
                { 0, 10, 15, 20 },
                { 10, 0, 35, 25 },
                { 15, 35, 0, 30 },
                { 20, 25, 30, 0 }
                };

            Graph graph = new Graph();
            graph = graph.CreateGraph(n, adjMatrix);

            TSP nearestNeighbor = new TSP();
            nearestNeighbor.graph = graph;
            nearestNeighbor.FindShortestPathFromAll();
            Assert.AreEqual(80, nearestNeighbor.minDistance);
        }

        [TestMethod]
        public void NeighborTest2()
        {
            int n = 6;

            int[,] adjMatrix = {
                     { 0, 7, 0, 0, 7,3 },
                     { 3, 0, 1, 0,0,8 },
                     { 0, 8, 0, 1,0,1 },
                     { 0, 0, 5, 0, 3,5 },
                     { 8,0,0,2,0,2},
                     {2,7,0,1,0,0 }
                     };

            Graph graph = new Graph();
            graph = graph.CreateGraph(n, adjMatrix);

            TSP nearestNeighbor = new TSP();
            nearestNeighbor.graph = graph;
            nearestNeighbor.FindShortestPathFromAll();
            Assert.AreEqual(16, nearestNeighbor.minDistance);
        }


        [TestMethod]
        public void PathCheckTest()
        {
            int n = 6;

            int[,] adjMatrix = {
                     { 0, 7, 0, 0, 7,3 },
                     { 3, 0, 1, 0,0,8 },
                     { 0, 8, 0, 1,0,1 },
                     { 0, 0, 5, 0, 3,5 },
                     { 8,0,0,2,0,2},
                     {2,7,0,1,0,0 }
                     };

            Graph graph = new Graph();
            graph = graph.CreateGraph(n, adjMatrix);

            TSP nearestNeighbor = new TSP();
            nearestNeighbor.graph = graph;
            nearestNeighbor.FindShortestPathFromAll();
            int[] path = { 2, 3, 4, 5, 0, 1, 2 };
            int i = 0;
            foreach (Vertex vertex in nearestNeighbor.ShortestPath)
            {
                Assert.AreEqual(vertex.index, path[i++]);
                
            }
            
        }
    }
}
    

