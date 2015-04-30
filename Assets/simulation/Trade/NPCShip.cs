using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Simulation
{
    public class NPCShip
    {
        public Location home { get; private set; }
        public Location destination { get; internal set; }
        public Vector3 position { get; private set; }
        public string captain { get; private set; }
        public float cargoSpace { get; private set; }

        public List<Data.TradeItem> tradeList = new List<Data.TradeItem>();
        public List<string> wantedCommodityList = new List<string>();
        public CommodityInventory inventory = new CommodityInventory();

        List<Navigation.NavNode> navPoints = new List<Navigation.NavNode>();

        public bool free { get; private set; }
        public bool tradeShip { get; private set; }
        public bool isGoingToDestination { get; private set; }

        public NPCShip(Location home)
        {
            this.home = home;
            this.destination = home;
            this.position = home.position;
            this.captain = NameGenerator.getName("");
            this.cargoSpace = 10.0f;
            this.free = true;
            this.tradeShip = true; ///@todo military ships
        }

        public void tick(float days)
        {
            /// - we probably do not need to track real positions, since trading
            ///   is done directly through location resources
            /// - we need to add speed
            if (navPoints.Count == 0) return;

            Vector3 dir = navPoints[0].position - position;
            if ((dir.normalized * days * Parameters.shipMovementMultiplier).magnitude > dir.magnitude)
            {
                navPoints.RemoveAt(0);
                if (navPoints.Count == 0)
                {
                    arrived();
                }
            }
            else
            {
                position += dir.normalized * days * Parameters.shipMovementMultiplier;
            }
        }
        public void sendFreeShips()
        {
            if (free)
            {
                if (tradeShip)
                {  
                    LocationTrade.setTradePartnerForShip(this);
                    // checks for trade mission scoring to meet the treshold
                    if (destination != home)
                    {
                        Trade.tradeResources(this);
                        embarkTo(destination);
                        isGoingToDestination = true;
                    }
                }
                else
                {
                    ///@todo send military ships
                } 
            }
        }

        private void arrived()
        {
            // return home
            if (!isGoingToDestination)
            {
                tradeList.Clear ();
                free = true;
            }
            // arrive at the destination
            else
            {
                if (tradeShip)
                {
                    // noop
                    ///@todo create a delay of couple of days before returning?
                    isGoingToDestination = false;
                    embarkTo(home);
                }
            }
        }

        public void embarkTo(Location to)
        {
            Debug.Log ("sent ship from "+home.id+" to "+destination.id+"");
            Navigation.Path path = Root.game.navNetwork.getPath(Root.game.navNetwork.getNavNodeFor(destination),
                                                                  Root.game.navNetwork.getNavNodeFor(to));
            navPoints = path.nodes;
            free = false;
        }
    }
}
