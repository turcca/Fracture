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
        ResourcePool pool;
        ResourcePool emptyPool;

        [SetUp]
        public void Setup()
        {
            Data.Resource data =
                Data.Resource.generateDebugData(Data.Resource.Type.Food);
            data.resources = 10.0f;
            pool = new ResourcePool(data, 1.0f, 10.0f, 20.0f, 30.0f);
            Data.Resource emptyData =
                Data.Resource.generateDebugData(Data.Resource.Type.Food);
            emptyPool = new ResourcePool(emptyData, 1.0f, 10.0f, 20.0f, 30.0f);
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
        Resource resource;
        Data.Resource data;
    
        [SetUp]
        public void Setup()
        {
            data = Data.Resource.generateDebugData(Data.Resource.Type.Food);
            data.resources = 5.0f;
            //resource = new Resource(data, new ResourcePool(data, 1.0f, 10.0f, 20.0f, 30.0f));
        }
        [Test]
        public void TickTicksPool()
        {
            //resource.tick(1.0f);
            //Assert.That(resource.getResources(), Is.EqualTo(4.0f));
        }
        [Test]
        public void DeficitIsReseted()
        {
            //resource.tick(6.0f);
            //Assert.That(resource.getResources(), Is.EqualTo(0.0f));
        }
        [Test]
        public void DeficitLeadsToShortage()
        {
            resource.tick(1.0f);
            Assert.That(data.state, Is.EqualTo(Data.Resource.State.Sustain));
			resource.tick(6.0f);
            Assert.That(data.state, Is.EqualTo(Data.Resource.State.Shortage));
        }
        [Test]
        public void GrowLimitLeadsToReadyToUpgrade()
        {
            //resource.pool.add(16.0f);
            //resource.tick(1.0f);
            //Assert.That(resource.state, Is.EqualTo(Resource.State.AtGrowLimit));
        }
    }
}