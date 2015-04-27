using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Simulation
{
    public class NPCShip
    {
        public Location home { get; private set; }
        public Location destination { get; private set; }
        public Vector3 position { get; private set; }
        public string captain { get; private set; }
        public float cargoSpace { get; private set; }

        public List<string> wantedCommodityList = new List<string>();
        public CommodityInventory inventory = new CommodityInventory();

        List<Navigation.NavNode> navPoints = new List<Navigation.NavNode>();

        public bool free { get; private set; }

        public NPCShip(Location home)
        {
            this.home = home;
            this.destination = home;
            this.position = home.position;
            this.captain = NameGenerator.getName("");
            this.cargoSpace = 10.0f;
            this.free = true;
        }

        public void tick(float days)
        {
            /// - we probably do not need to track real positions, since trading
            ///   is done directly through location resources
            /// - we need to add speed

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
            if (destination == home)
            {
                free = true;
            }
            else
            {
                // noop
            }
        }

        public void embarkTo(Location to)
        {
            Navigation.Path path = Root.game.navNetwork.getPath(Root.game.navNetwork.getNavNodeFor(destination),
                                                                  Root.game.navNetwork.getNavNodeFor(to));
            navPoints = path.nodes;
            destination = to;
            free = false;
        }
    }
}
