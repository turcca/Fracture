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

        List<Navigation.NavNode> navPoints = new List<Navigation.NavNode>(); 
        //public int getNavPointCount() { return navPoints.Count; }

        public bool free { get; private set; }
        public bool isTradeShip { get; private set; }
        public bool isGoingToDestination { get; private set; }
        public float downtime { get; set; }
        public bool isVisible { get; private set; }

        public TradeShip tradeShip { get; set; }


        private float simulationStepBuffer = 0;

        public NPCShip(Location home)
        {
            this.home = home;
            this.destination = home;
            this.position = home.position;
            this.captain = NameGenerator.getName(home.ideology.getHighestIdeologyAndValue().Key);
            this.cargoSpace = Parameters.cargoHoldMul;
            this.free = true;
            this.isTradeShip = true; ///@todo military ships
            this.downtime = UnityEngine.Random.value*5;
            this.isVisible = false;
        }

        public void tick(float days)
        {
            simulationStepBuffer += days;

            if (downtime > 0f)
            {
                setVisibilistyToStarmap(false);
                downtime = (downtime > days) ? downtime - days : 0f;
            }

            if (navPoints.Count == 0)
            {
                return;
            }
            else
            {
                setVisibilistyToStarmap(true);

                Vector3 dir = navPoints[0].position - position;
                Vector3 dirNormalized = dir.normalized;
                float endPoint = dir.magnitude; // distance to the next navPoint
                float currentPoint = (dirNormalized * days * Parameters.getNPCShipSpeed()).magnitude; // travel distance in this tick

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
                        position += dirNormalized * days * Parameters.getNPCShipSpeed();
                        //deviation = navPoints[0].directionPerpendicularNormalized * (endPoint-currentPoint);
                        deviation = Quaternion.Euler(0, 90, 0) * dirNormalized *2;
                    }
                    // under half-point, increase deviation
                    else
                    {
                        position += dirNormalized * days * Parameters.getNPCShipSpeed();
                        //deviation = navPoints[0].directionPerpendicularNormalized * (endPoint-currentPoint);
                        deviation = Quaternion.Euler(0, 90, 0) * dirNormalized;
                    }
                }
            }
        }
        public void sendFreeShip()
        {
            if (simulationStepBuffer >= 0.3f)
            {
                simulationStepBuffer = 0;

                if (free)
                {
                    if (downtime <= 0)
                    {
                        if (isTradeShip)
                        {
                            // checks for trade mission scoring to meet the treshold, set destination (if no partner: destination = home)
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
                }
                // might be at destination and deeds to be sent back
                else
                {
                    // unloading cargo on destination
                    if (downtime <= 0 /*&& */)
                    {
                        if (!isGoingToDestination && navPoints.Count == 0)
                        {
                            if (isTradeShip)
                            {
                                embarkTo();
                            }
                        }
                    }
                }
            }
        }

        private void arrived()
        {
            setVisibilistyToStarmap(false);
            downtime = UnityEngine.Random.Range(1.5f, 3.5f); // downtime range

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
                    // moved ---> embarkTo();
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
                tradeShip.setVisibilityToStarmap(false);
            }
            if (!isVisible && visible) 
            {
                isVisible = true;
                tradeShip.setVisibilityToStarmap(true);
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
                    if (item.amount < 0.1f) rv += Enum.GetName(typeof(Data.Resource.Type), item.type) + ": "+ Mathf.Round (item.amount*100.0f)/100.0f +"\n";
                    else rv += Enum.GetName(typeof(Data.Resource.Type), item.type) + ": "+ Mathf.Round (item.amount*10.0f)/10.0f +"\n";
                }
            }
            return rv;            
        }
    }
}
