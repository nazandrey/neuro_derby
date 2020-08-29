using System.Collections;
using Scripts.Models;
using NUnit.Framework;
using UnityEngine.TestTools;


namespace Tests.EditMode
{
    [TestFixture]
    public class HealthBarModelTest
    {
        [Test]
        public void HealthBar_Default_Has1PercentageHealth()
        {
            var healthBarModel = new HealthBarModel();
            
            Assert.AreEqual(1, healthBarModel.HealthPercentage);
        }
        
        [Test]
        public void HealthBar_InitValue_HasMaxHealthByInitValue([Range(0,100,10)]float maxHealth)
        {
            var healthBarModel = new HealthBarModel(maxHealth);
            
            Assert.AreEqual(maxHealth, healthBarModel.MaxHealth);
        }
        
        [Test]
        public void HealthBar_CurrentHealthAreEqualToMaxHealth([Range(0,100,10)]float maxHealth)
        {
            var healthBarModel = new HealthBarModel(maxHealth);
            
            Assert.AreEqual(healthBarModel.MaxHealth, healthBarModel.CurrentHealth);
        }
        
        [TestCase(10, 1, 0.9f)]
        [TestCase(60, 30, 0.5f)]
        [TestCase(100, 2, 0.98f)]
        public void HealthBar_SubtractHealth_HealthPercentageSubtracted(float maxHealth, float healthToSubtract, float expectedHealthPercentage)
        {
            var healthBarModel = new HealthBarModel(maxHealth);

            healthBarModel.SubtractHealth(healthToSubtract);
            
            Assert.AreEqual(expectedHealthPercentage, healthBarModel.HealthPercentage);
        }
    }
}