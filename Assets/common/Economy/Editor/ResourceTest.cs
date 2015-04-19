using System;
using System.Collections;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

namespace NewEconomy
{
    [TestFixture]
    [Category("Location Domain Tests")]
    internal class ResourceTierPoolTest
    {
        ResourceTierPool pool;
        ResourceTierPool emptyPool;

        [SetUp]
        public void Setup()
        {
            pool = new ResourceTierPool(10.0f, 1.0f, 10.0f, 20.0f, 30.0f);
            emptyPool = new ResourceTierPool(0.0f, 1.0f, 10.0f, 20.0f, 30.0f);
        }
        [Test]
        public void TickReducesAmount()
        {
            pool.tick(1.0f);
            Assert.That(pool.get(), Is.EqualTo(9.0f));
        }
        [Test]
        public void ProductionIncreaseAmount()
        {
            pool.setProduction(2.0f);
            pool.tick(1.0f);
            Assert.That(pool.get(), Is.EqualTo(11.0f));
        }
        [Test]
        public void NegativeResourcesAsDeficit()
        {
            emptyPool.tick(1.0f);
            Assert.That(emptyPool.getDeficit(), Is.EqualTo(1.0f));
        }
        [Test]
        public void ResourcesOverGrowLimitAsOverflow()
        {
            emptyPool.setProduction(100.0f);
            emptyPool.tick(1.0f);
            Assert.That(emptyPool.getOverflow(), Is.EqualTo(79.0f));
        }
        public void ResourcesOverTargetAsExcess()
        {
            emptyPool.setProduction(100.0f);
            emptyPool.tick(1.0f);
            Assert.That(emptyPool.getExcess(), Is.EqualTo(89.0f));
        }
    }

    [TestFixture]
    [Category("Location Domain Tests")]
    internal class ResourceTest
    {
        Resource domain;
            
        [SetUp]
        public void Setup()
        {
            ResourceTierPool tier1 = new ResourceTierPool(5.0f, 1.0f, 10.0f, 20.0f, 30.0f);
            ResourceTierPool tier2 = new ResourceTierPool(5.0f, 1.0f, 10.0f, 20.0f, 30.0f);
            ResourceTierPool tier3 = new ResourceTierPool(10.0f, 1.0f, 10.0f, 20.0f, 30.0f);
            domain = new Resource(Resource.Type.Culture, new ResourceTierPool[] { tier1, tier2, tier3 });
        }
        [Test]
        public void TickTicksAllPools()
        {
			Debug.Log ("TICK DISABLED: Yeah, this kinda broke - 'tick' needs 2 Location instance as a second parameter");
            //domain.tick(1.0f);
            Assert.That(domain.getResources(1), Is.EqualTo(4.0f));
            Assert.That(domain.getResources(2), Is.EqualTo(4.0f));
            Assert.That(domain.getResources(3), Is.EqualTo(9.0f));
        }
        [Test]
        public void DeficitFromLowerTierIsSubstractedFromUpperAndReseted()
        {
			Debug.Log ("TICK DISABLED: Yeah, this kinda broke - 'tick' needs 2 Location instance as a second parameter");
            //domain.tick(6.0f);
            Assert.That(domain.getResources(1), Is.EqualTo(0.0f));
            Assert.That(domain.getResources(2), Is.EqualTo(0.0f));
            Assert.That(domain.getResources(3), Is.EqualTo(2.0f));
            Assert.That(domain.getPool(1).getDeficit(), Is.EqualTo(0.0f));
            Assert.That(domain.getPool(2).getDeficit(), Is.EqualTo(0.0f));
            Assert.That(domain.getPool(3).getDeficit(), Is.EqualTo(0.0f));
        }
        [Test]
        public void FailureToCompensateDeficitLeadsToShortage()
        {
			Debug.Log ("TICK DISABLED: Yeah, this kinda broke - 'tick' needs 2 Location instance as a second parameter");
            //domain.tick(1.0f);
            Assert.That(domain.state, Is.EqualTo(Resource.State.Sustain));
			Debug.Log ("TICK DISABLED: Yeah, this kinda broke - 'tick' needs 2 Location instance as a second parameter");
			//domain.tick(6.0f);
            Assert.That(domain.state, Is.EqualTo(Resource.State.Shortage));
        }
        [Test]
        public void AllTiersAtGrowLimitLeadsToReadyToUpgrade()
        {
            domain.getPool(1).add(16.0f);
            domain.getPool(2).add(20.0f);
            domain.getPool(3).add(30.0f);
			Debug.Log ("TICK DISABLED: Yeah, this kinda broke - 'tick' needs 2 Location instance as a second parameter");
			//domain.tick(1.0f);
            Assert.That(domain.state, Is.EqualTo(Resource.State.ReadyToUpgrade));
        }
    }
}