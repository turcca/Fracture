using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NavNode
{
    public string id { get; private set; }
    public Vector3 position { get; set; }
    public List<NavNode> links = new List<NavNode>();
    public NavNode(string _id)
    {
        id = _id;
    }
}

public class NavNetwork
{
    public List<NavNode> navNodes = new List<NavNode>();
    private Graph graph = new Graph();

    public NavNetwork(Location[] locations, List<NavpointId> navpoints)
    {
        foreach (Location loc in locations)
        {
            graph.AddNode(loc.id);
            NavNode node = new NavNode(loc.id);
            node.position = loc.position;
            navNodes.Add(node);
        }
        foreach (NavpointId nav in navpoints)
        {
            graph.AddNode(nav.getId());
            NavNode node = new NavNode(nav.getId());
            node.position = nav.gameObject.transform.position;
            navNodes.Add(node);
        }
        foreach (NavNode source in navNodes)
        {
            foreach (NavNode destination in navNodes)
            {
                if (destination == source)
                {
                    continue;
                }
                if ((source.position - destination.position).magnitude < 150)
                {
                    source.links.Add(destination);
                    graph.AddConnection(source.id, destination.id, (int)(source.position - destination.position).magnitude, false);
                }
            }
        }
    }

    public List<Location> getNeighbours(Location l)
    {
        NavNode originNode = getNavNodeFor(l);

        List<Location> rv = new List<Location>();
        foreach (NavNode node in getNodesFrom(originNode, 1))
        {
            rv.Add(Game.universe.locations[node.id]);
        }
        return rv;
    }

    public List<Location> getNearestLocations(Location l)
    {
        List<Location> rv = new List<Location>();

        DistanceCalculator calc = new DistanceCalculator();
        Dictionary<string, double> ids = (Dictionary<string, double>)calc.CalculateDistances(graph, l.id);

        var tp = from pair in ids
                 orderby pair.Value ascending
                 select pair.Key;

        foreach (string id in tp)
        {
            if (Game.universe.locations.ContainsKey(id))
            {
                rv.Add(Game.universe.locations[id]);
            }
        }
        return rv;
    }

    public List<NavNode> getNodesFrom(NavNode from, int distance)
    {
        List<NavNode> rv = new List<NavNode>();
        List<NavNode> pNodeList = new List<NavNode>() { from };
        List<int> pDistList = new List<int> { distance };

        while (pNodeList.Count != 0)
        {
            NavNode pNode = pNodeList[0];
            int pDist = pDistList[0];
            rv.Add(pNode);
            foreach (NavNode node in pNode.links)
            {
                if (!pNodeList.Contains(node) && !rv.Contains(node) && pDist > 0)
                {
                    pNodeList.Add(node);
                    pDistList.Add(pDist - 1);
                }
            }
            pNodeList.RemoveAt(0);
            pDistList.RemoveAt(0);
        }

        return rv;
    }

    public List<NavNode> getPath(NavNode from, NavNode to)
    {
        List<NavNode> rv = new List<NavNode>();
        //rv.Add(to);
        //return rv;

        DistanceCalculator calc = new DistanceCalculator();
        List<string> ids = (List<string>)calc.CalculatePath(graph, from.id, to.id);

        foreach (string id in ids)
        {
            foreach (NavNode n in navNodes)
            {
                if (n.id == id)
                {
                    rv.Add(n);
                }
            }
        }
        //Debug.Log("Shortest path from " + from.id + " to " + to.id + ": ");
        //foreach(NavNode n in rv)
        //{
        //    Debug.Log(n.id);
        //}
        return rv;
    }

    public NavNode getNavNodeFor(Location location)
    {
        NavNode rv = new NavNode("");
        foreach (NavNode node in navNodes)
        {
            if (node.id == location.id)
            {
                rv = node;
            }
        }
        if (rv.id == "")
        {
            Tools.error("Location not found on trade network: " + location.id);
        }
        return rv;
    }
}

