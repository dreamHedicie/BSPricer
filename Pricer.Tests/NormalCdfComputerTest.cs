using System;
using NUnit.Framework;

namespace Pricer.Tests
{
    [TestFixture]
    public class NormalCdfComputerTest
    {
        [Test]
        public void TestCompute()
        {
            var computer = new NormalCdfComputer();

            Assert.That(computer.Compute(0), Is.EqualTo(0.5));
            Assert.That(Math.Round(computer.Compute(2.3), 8), Is.EqualTo(0.98927589));
            Assert.That(Math.Round(computer.Compute(-2.3), 8), Is.EqualTo(0.01072411));
            Assert.That(Math.Round(computer.Compute(0.32), 8), Is.EqualTo(0.62551583));
            Assert.That(Math.Round(computer.Compute(-0.32), 8), Is.EqualTo(0.37448417));
        }
    }
}
