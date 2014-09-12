﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCShip
{
    public Location home;
    public Location destination;
    public Vector3 position;
    public Vector3 currentTarget;
    List<NavNode> navPoints = new List<NavNode>();

    public NPCShip(Location homeLocation)
    {
        home = homeLocation;
        position = home.position;
        destination = home;

        Location[] arr = new Location[Game.universe.locations.Count];
        Game.universe.locations.Values.CopyTo(arr, 0);
        embarkTo(arr[Random.Range(0, arr.Length - 1)]);
    }

    public void tick(float days)
    {
        if (navPoints.Count == 0) return;

        Vector3 dir = navPoints[0].position - position;
        if ((dir.normalized * days).magnitude > dir.magnitude)
        {
            navPoints.RemoveAt(0);
            if (navPoints.Count == 0)
            {
                arrived();
            }
        }
        else
        {
            position = position + dir.normalized * days;
        }
    }

    private void arrived()
    {
        Location[] arr = new Location[Game.universe.locations.Count];
        Game.universe.locations.Values.CopyTo(arr, 0);
        embarkTo(arr[Random.Range(0, arr.Length - 1)]);
    }

    public void embarkTo(Location to)
    {
        navPoints = Game.universe.tradeNetwork.getPath(Game.universe.tradeNetwork.getNavNodeFor(destination),
                                                       Game.universe.tradeNetwork.getNavNodeFor(to));
        destination = to;
    }
}