using System;
class Vertex
{
    public string Data;
    public bool IsVisted;
    public Vertex(string Vertexdata)
    {
        Data = Vertexdata;
    }

}



public class Graph
{
    private const int Number = 10; // Maxmium node number
    private Vertex[] vertiexes;
    public int[,] adjmatrix;
    int numVerts = 7; // Current node number
    // initial graph
    public Graph()
    {
        adjmatrix = new Int32[10, 10];
        vertiexes = new Vertex[10];
        for (int i = 0; i < Number; i++)
        {
            for (int j = 0; j < Number; j++)
            {
                adjmatrix[i, j] = 0;
            }
        }
    }

    // add node
    public void AddVertex(String v)
    {
        vertiexes[numVerts] = new Vertex(v);
        numVerts++;
    }
    // add edge
    public void AddEdge(int vertex1, int vertex2)
    {
        adjmatrix[vertex1, vertex2] = 1;
        //adjmatrix[vertex2, vertex1] = 1;
    }

    // show node
    public void DisplayVert(int vertexPosition)
    {
        Console.WriteLine(vertiexes[vertexPosition] + " ");
    }
}



namespace Dijkstra.Client
{
    class Program
    {
        static void Main()

        {
            FindShortestPath();
        }

        static void FindShortestPath()
        {
            // Step 1: Create node
            var nodeA = new Node<string>("Station A", 0);
            var nodeB = new Node<string>("Station B", 1);
            var nodeC = new Node<string>("SStation C", 2);
            var nodeD = new Node<string>("Station D", 3);
            var nodeE = new Node<string>("Station E", 4);
            var nodeF = new Node<string>("Station F", 5);

            // Step 2: Create graph data
            var graph = new Graph<string>();

            // Step 3: Add node

            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddNode(nodeC);
            graph.AddNode(nodeD);
            graph.AddNode(nodeE);
            graph.AddNode(nodeF);



            // Step 4: Add edges
            graph.AddEdge(nodeA, nodeB, 0, 3);
            graph.AddEdge(nodeA, nodeC, 1, 5);
            graph.AddEdge(nodeB, nodeA, 2, 3);
            graph.AddEdge(nodeB, nodeC, 3, 1);
            graph.AddEdge(nodeB, nodeD, 4, 2);
            graph.AddEdge(nodeC, nodeA, 5, 5);
            graph.AddEdge(nodeC, nodeB, 6, 1);
            graph.AddEdge(nodeC, nodeD, 7, 3);
            graph.AddEdge(nodeC, nodeE, 8, 6);
            graph.AddEdge(nodeD, nodeB, 9, 2);
            graph.AddEdge(nodeD, nodeC, 10, 3);
            graph.AddEdge(nodeD, nodeE, 11, 4);
            graph.AddEdge(nodeE, nodeC, 12, 6);
            graph.AddEdge(nodeE, nodeD, 13, 4);
            graph.AddEdge(nodeF, nodeC, 14, 6);
            graph.AddEdge(nodeF, nodeD, 15, 4);

            /////////add OD pairs/////
            graph.AddOD(nodeA, nodeF, 0, 100);
            graph.AddOD(nodeA, nodeD, 1, 100);
            ////////////////
            graph.CreateNodeEdgeRelation();
            // Step 5: Create path engine and call function
            var pathEngine = new PathEngine<string>(graph);
            var pathResult = pathEngine.FindShortestPath(nodeA, nodeF);

            for (int w = 0; w < graph.ODs.Count; ++w)
            {
                var sp = pathEngine.FindShortestPath(graph.ODs[w].Origin, graph.ODs[w].Dest);
                Console.WriteLine(graph.ODs[w]);
                for (int i = 0; i < sp.PathEdgeList.Count; i++)
                {
                    Console.WriteLine(sp.PathEdgeList[i].Id);
                    graph.Edges[sp.PathEdgeList[i].Id].Flow = graph.ODs[w].Demand + graph.Edges[sp.PathEdgeList[i].Id].Flow;
                    Console.WriteLine("flow ={0}", graph.Edges[sp.PathEdgeList[i].Id].Flow);
                    graph.Edges[sp.PathEdgeList[i].Id].getCost();
                }


            }

            for (int i = 0; i < graph.Nodes[0].OutGoingEdges.Count; i++)
            {
                int edgeid = graph.Nodes[0].OutGoingEdges[i].Id;
                Console.WriteLine("edge {0} leaving node 0/A", graph.Nodes[0].OutGoingEdges[i].Id);

            }
            // Display path result
            Console.WriteLine($"Total cost from {pathResult.From} to {pathResult.To} is {pathResult.TotalCost}");
            Console.WriteLine("============================================");

            foreach (var path in pathResult.PathEdgeList)
            {
                Console.WriteLine(path.ToString());
                Console.WriteLine(path.Id);
            }

            Console.WriteLine("============================================");
            Console.ReadKey();
        }
    }
}
