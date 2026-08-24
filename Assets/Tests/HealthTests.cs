using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthTests
{
    private GameObject _dummyObject;
    private Health _healthComponent;

    [SetUp]
    public void Setup()
    {
        _dummyObject = new GameObject("DummyPlayer");
        _healthComponent = _dummyObject.AddComponent<Health>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(_dummyObject);
    }

    [UnityTest]
    public IEnumerator TakeDamage_WhenHit_LosesLifeCorrectly()
    {
        yield return null;

        int initialHealth = _healthComponent.CurrentHealth; 
        
        _healthComponent.TakeDamage(1);

        Assert.AreEqual(initialHealth - 1, _healthComponent.CurrentHealth, "Life doesnt decrease correctly!");
        Assert.IsFalse(_healthComponent.IsDead);
    }

    [UnityTest]
    public IEnumerator TakeDamage_KillsEntity_WhenDamageEqualsMaxHealth()
    {
        yield return null;

        _healthComponent.TakeDamage(3);

        Assert.AreEqual(0, _healthComponent.CurrentHealth);
        Assert.IsTrue(_healthComponent.IsDead);
    }

    [UnityTest]
    public IEnumerator TakeDamage_InvokesOnDeathEvent_WhenHealthReachesZero()
    {
        yield return null;

        bool eventWasCalled = false;
        _healthComponent.OnDeath += () => eventWasCalled = true;

        _healthComponent.TakeDamage(3);

        Assert.IsTrue(eventWasCalled, "OnDeath action was not invoked!");
    }
}