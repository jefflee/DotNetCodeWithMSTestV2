﻿using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace GameEngine.Tests
{
    [TestClass]
    public class BossEnemyShould
    {
        [TestMethod]
        public void HaveCorrectSpecialAtackPower()
        {
            var sut = new BossEnemy();
            Assert.AreEqual(166.6, sut.SpecialAttackPower, 0.07);
        }
    }
}
