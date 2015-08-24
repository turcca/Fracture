using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Simulation
{
    public class NPCShip
    {
        public Location home { get; private set; }
        public Location destination { get; internal set; }
        public Vector3 position { get; private set; }
        public Vector3 deviation { get; private set; }
        public string captain { get; private set; }
        public float cargoSpace { get; private set; }

        public List<Data.TradeItem> tradeList = new List<Data.TradeItem>();
        //public List<string> wantedCommodityList = new List<string>();
        //public CommodityInventory inventory = new CommodityInventory();

        List<Navigation.NavNode> navPoints = new List<Navigation.NavNode>();

        public bool free { get; private set; }
        public bool isTradeShip { get; private set; }
        public bool isGoingToDestination { get; private set; }
        public float downtime { get; set; }
        private bool isVisible { get; set; }

        public TradeShip tradeShip { get; set; }


        public NPCShip(Location home)
        {
            this.home = home;
            this.destination = home;
            this.position = home.position;
            this.captain = NameGenerator.getName("");
            this.cargoSpace = 2.0f;
            this.free = true;
            this.isTradeShip = true; ///@todo military ships
            this.downtime = UnityEngine.Random.value*5;
        }

        public void tick(float days)
        {
            /// - we probably do not need to track real positions, since trading
            ///   is done directly through location resources
            if (navPoints.Count == 0) 
            {
                return;
            }

            if (downtime > 0)
            {
                setVisibilistyToStarmap(false);

                if (downtime > days)
                {
                    downtime = downtime-days;
                }
                else
                {
                    downtime = 0;
                }
            }
            else
            {
                setVisibilistyToStarmap(true);

                Vector3 dir = navPoints[0].position - position;
                Vector3 dirNormalized = dir.normalized;
                float endPoint = dir.magnitude;
                float currentPoint = (dirNormalized * days * Parameters.shipMovementMultiplier).magnitude;

                // navPoint reached
                if (currentPoint > endPoint)
                {
                    navPoints.RemoveAt(0);
                    if (navPoints.Count == 0)
                    {
                        arrived();
                    }
                    else 
                    {
                        // new navpoint - recalculate new direction and perpendicular direction
                        navPoints[0].recalculateNormalizedValues((navPoints[0].position - position).normalized, dirNormalized);
                    }
                }
                // between navPoints
                else
                {
                    // over half-point, reducing deviation
                    if (currentPoint > endPoint * 0.5f)  //mistä lasketaan puoliväli?
                    {
                        position += dirNormalized * days * Parameters.shipMovementMultiplier;
                        //deviation = navPoints[0].directionPerpendicularNormalized * (endPoint-currentPoint);
                        deviation = Quaternion.Euler(0, 90, 0) * dirNormalized *2;
                    }
                    // under half-point, increase deviation
                    else
                    {
                        position += dirNormalized * days * Parameters.shipMovementMultiplier;
                        //deviation = navPoints[0].directionPerpendicularNormalized * (endPoint-currentPoint);
                        deviation = Quaternion.Euler(0, 90, 0) * dirNormalized;
                    }
                }
            }
        }
        public void sendFreeShips(float days)
        {
            if (free) 
            {
                if (downtime <= 0)
                {
                    if (isTradeShip)
                    {  
                        // checks for trade mission scoring to meet the treshold, set destination (no partner, destination = home)
                        LocationTrade.setTradePartnerForShip(this);
                        if (destination != home)
                        {
                            Trade.tradeResources(this);
                            isGoingToDestination = true;
                            embarkTo();
                        }
                    }
                    else
                    {
                        ///@todo send military ships
                    } 
                }
                else
                {
                    downtime = downtime > days ? downtime-days : 0;
                }
            }
        }

        private void arrived()
        {
            setVisibilistyToStarmap(false);
            downtime = UnityEngine.Random.Range(0.5f, 1.5f);

            // return home
            if (!isGoingToDestination)
            {
                tradeList.Clear ();
                destination = home;
                free = true;
            }
            // arrive at the destination
            else
            {
                if (isTradeShip)
                {
                    // noop
                    ///@todo create a delay of couple of days before returning?
                    isGoingToDestination = false;
                    embarkTo();
                }
            }
        }

        public void embarkTo(Location to = null)
        {
            free = false;
            Navigation.Path path;

            // manually setting destination
            if (to != null)
            {
                Debug.Log ("sending ship to "+to.id+"");
                path = Root.game.navNetwork.getPath(Root.game.navNetwork.getNavNodeFor(destination),
                                                                    Root.game.navNetwork.getNavNodeFor(to));
                destination = to;
            }
            // destination is pre-set
            else
            {
                if (isGoingToDestination) path = Root.game.navNetwork.getPath(Root.game.navNetwork.getNavNodeFor(home),
                                                                    Root.game.navNetwork.getNavNodeFor(destination));

                else path = Root.game.navNetwork.getPath(Root.game.navNetwork.getNavNodeFor(destination),
                                                                         Root.game.navNetwork.getNavNodeFor(home));

                //if (isGoingToDestination) Debug.Log ("sent ship from "+home.id+" to "+destination.id+" distance (nodes): "+ path.nodes.Count);
                //else Debug.Log ("ship returning to "+home.id+" from "+destination.id+" distance (nodes): "+ path.nodes.Count);
            }
            navPoints = path.nodes;
        }

        void setVisibilistyToStarmap(bool visible)
        {
            if (isVisible && !visible) 
            {
                isVisible = false;
                tradeShip.setVisibilistyToStarmap(false);
            }
            if (!isVisible && visible) 
            {
                isVisible = true;
                tradeShip.setVisibilistyToStarmap(true);
            }
        }

        internal string cargoToDebugString()
        {
            string rv = "";
            foreach (Data.TradeItem item in tradeList)
            {
                if (item.amount > 0)
                {
                    if (item.isExported)
                    {
                        if (isGoingToDestination) rv+= "Exporting ";
                        else rv+= "(exported) ";
                    }
                    else 
                    {
                        if (isGoingToDestination) rv+= "(getting) ";
                        else rv+= "Importing ";
                    }

                    rv += Enum.GetName(typeof(Data.Resource.Type), item.type) + ": "+ item.amount +"\n";
                }
            }
            return rv;            
        }
    }
}
