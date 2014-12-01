using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

internal class Node
{
    IList<NodeConnection> _connections;

    internal string Name { get; private set; }

    internal double DistanceFromStart { get; set; }
    internal Node previous;

    internal IEnumerable<NodeConnection> Connections
    {
        get { return _connections; }
    }

    internal Node(string name)
    {
        Name = name;
        _connections = new List<NodeConnection>();
        previous = null;
    }

    internal void AddConnection(Node targetNode, double distance, bool twoWay)
    {
        if (targetNode == null) throw new ArgumentNullException("targetNode");
        if (targetNode == this)
            throw new ArgumentException("Node may not connect to itself.");
        if (distance <= 0) throw new ArgumentException("Distance must be positive.");

        _connections.Add(new NodeConnection(targetNode, distance));
        if (twoWay) targetNode.AddConnection(this, distance, false);
    }
}

internal class NodeConnection
{
    internal Node Target { get; private set; }
    internal double Distance { get; private set; }

    internal NodeConnection(Node target, double distance)
    {
        Target = target;
        Distance = distance;
    }
}

public class Graph
{
    internal IDictionary<string, Node> Nodes { get; private set; }

    public Graph()
    {
        Nodes = new Dictionary<string, Node>();
    }

    public void AddNode(string name)
    {
        var node = new Node(name);
        Nodes.Add(name, node);
    }

    public void AddConnection(string fromNode, string toNode, int distance, bool twoWay)
    {
        Nodes[fromNode].AddConnection(Nodes[toNode], distance, twoWay);
    }
}

//public class PathCalculator
//{
//    public List<string> CalculatePath(Graph graph, string startingNode, string targetNode)
//    {
//        if (!graph.Nodes.Any(n => n.Key == startingNode))
//            throw new ArgumentException("Starting node must be in graph.");

//        InitialiseGraph(graph, startingNode);
//        ProcessGraph(graph, startingNode, targetNode);
//    }

//    private void InitialiseGraph(Graph graph, string startingNode)
//    {
//        foreach (Node node in graph.Nodes.Values)
//            node.DistanceFromStart = double.PositiveInfinity;
//        graph.Nodes[startingNode].DistanceFromStart = 0;
//    }

//    private void ProcessGraph(Graph graph, string startingNode, string targetNode)
//    {
//        bool finished = false;
//        var queue = graph.Nodes.Values.ToList();
//        while (!finished)
//        {
//            Node nextNode = queue.OrderBy(n => n.DistanceFromStart).FirstOrDefault(
//                n => !double.IsPositiveInfinity(n.DistanceFromStart));
//            if (nextNode != null)
//            {
//                ProcessNode(nextNode, queue);
//                queue.Remove(nextNode);
//            }
//            else
//            {
//                finished = true;
//            }
//        }
//    }

//    private void ProcessNode(Node node, List<Node> queue)
//    {
//        var connections = node.Connections.Where(c => queue.Contains(c.Target));
//        foreach (var connection in connections)
//        {
//            double distance = node.DistanceFromStart + connection.Distance;
//            if (distance < connection.Target.DistanceFromStart)
//                connection.Target.DistanceFromStart = distance;
//        }
//    }
//}

public class DistanceCalculator
{
    public IDictionary<string, double> CalculateDistances(Graph graph, string startingNode)
    {
        if (!graph.Nodes.Any(n => n.Key == startingNode))
            throw new ArgumentException("Starting node must be in graph.");

        InitialiseGraph(graph, startingNode);
        ProcessGraph(graph, startingNode);
        return ExtractDistances(graph);
    }

    public IList<string> CalculatePath(Graph graph, string startingNode, string targetNode)
    {
        if (!graph.Nodes.Any(n => n.Key == startingNode))
            throw new ArgumentException("Starting node must be in graph.");

        InitialiseGraph(graph, startingNode);
        ProcessGraph(graph, startingNode);
        return ExtractShortestPath(graph, targetNode);
    }

    private void InitialiseGraph(Graph graph, string startingNode)
    {
        foreach (Node node in graph.Nodes.Values)
        {
            node.DistanceFromStart = double.PositiveInfinity;
            node.previous = null;
        }
        graph.Nodes[startingNode].DistanceFromStart = 0;
    }

    private void ProcessGraph(Graph graph, string startingNode)
    {
        bool finished = false;
        var queue = graph.Nodes.Values.ToList();
        while (!finished)
        {
            Node nextNode = queue.OrderBy(n => n.DistanceFromStart).FirstOrDefault(
                n => !double.IsPositiveInfinity(n.DistanceFromStart));
            if (nextNode != null)
            {
                ProcessNode(nextNode, queue);
                queue.Remove(nextNode);
            }
            else
            {
                finished = true;
            }
        }
    }

    private void ProcessNode(Node node, List<Node> queue)
    {
        var connections = node.Connections.Where(c => queue.Contains(c.Target));
        foreach (var connection in connections)
        {
            double distance = node.DistanceFromStart + connection.Distance;
            if (distance < connection.Target.DistanceFromStart)
            {
                connection.Target.DistanceFromStart = distance;
                connection.Target.previous = node;
            }
        }
    }

    private IDictionary<string, double> ExtractDistances(Graph graph)
    {
        return graph.Nodes.ToDictionary(n => n.Key, n => n.Value.DistanceFromStart);
    }

    private IList<string> ExtractShortestPath(Graph graph, string targetNode)
    {
        List<string> path = new List<string>() {};
        Node n = graph.Nodes[targetNode];
        while (n.previous != null)
        {
            path.Insert(0, n.Name);
            n = n.previous;
        }
        return path;
    }
}

public class Dijkstra
{
}
