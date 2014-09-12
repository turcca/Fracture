using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    private List<NavNode> navNodes = new List<NavNode>();

    public NavNetwork(Location[] locations)
    {
        foreach (Location loc in locations)
        {
            NavNode node = new NavNode(loc.id);
            node.position = loc.position;
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
                if ((source.position - destination.position).magnitude < 30)
                {
                    source.links.Add(destination);
                }
            }
        }
    }

    public List<Location> getLocationsFrom(Location l, int distance)
    {
        NavNode originNode = getNavNodeFor(l);

        List<Location> rv = new List<Location>();
        foreach (NavNode node in getNodesFrom(originNode, distance))
        {
            rv.Add(Game.universe.locations[node.id]);
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
        //List<NavNode> rv = new List<NavNode>();
        //rv.Add(to);
        //return rv;

        List<NavNode> visited = new List<NavNode>();
        List<NavNode> workList = new List<NavNode>() { from };
        Dictionary<NavNode, NavNode> connections = new Dictionary<NavNode, NavNode>();

        while (workList.Count != 0)
        {
            NavNode pNode = workList[0];
            visited.Add(pNode);
            foreach (NavNode neighbor in pNode.links)
            {
                if (!workList.Contains(neighbor) && !visited.Contains(neighbor))
                {
                    workList.Add(neighbor);
                    connections.Add(neighbor, pNode);
                }
            }
            workList.RemoveAt(0);
        }

        List<NavNode> rv = new List<NavNode>();
        while (connections.ContainsKey(to) && connections[to] != null)
        {
            rv.Add(to);
            to = connections[to];
        }
        rv.Reverse();
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
