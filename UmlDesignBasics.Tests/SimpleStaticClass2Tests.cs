﻿using System;
using System.Reflection;
using NUnit.Framework;

namespace UmlDesignBasics.Tests
{
    [TestFixture]
    public class SimpleStaticClass2Tests
    {
        private Type classType;

        [SetUp]
        public void SetUp()
        {
            var assembly = typeof(CreatingSimpleClass2).Assembly;
            this.classType = assembly.GetType("UmlDesignBasics.SimpleStaticClass2");
        }

        [Test]
        public void IsClass()
        {
            Assert.IsTrue(this.classType.IsClass);
        }

        [Test]
        public void IsStaticClass()
        {
            Assert.IsTrue(this.classType.IsAbstract);
            Assert.IsTrue(this.classType.IsSealed);
        }

        [Test]
        public void IsPublic()
        {
            Assert.IsTrue(this.classType.IsPublic);
        }

        [Test]
        public void InheritsObject()
        {
            Assert.AreEqual(typeof(object), this.classType.BaseType);
        }

        [Test]
        public void HasRequiredMembers()
        {
            Assert.AreEqual(0, this.classType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Length);
            Assert.AreEqual(4, this.classType.GetFields(BindingFlags.Static | BindingFlags.Public).Length);
            Assert.AreEqual(4, this.classType.GetFields(BindingFlags.Static | BindingFlags.NonPublic).Length);

            var flags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            Assert.AreEqual(0, this.classType.GetMethods(flags | BindingFlags.DeclaredOnly).Length);
            Assert.AreEqual(0, this.classType.GetConstructors(flags).Length);
            Assert.AreEqual(0, this.classType.GetEvents(flags).Length);
            Assert.AreEqual(0, this.classType.GetProperties(flags).Length);
        }

        [TestCase("intField", typeof(int), false)]
        [TestCase("longField", typeof(long), false)]
        [TestCase("floatField", typeof(float), false)]
        [TestCase("doubleField", typeof(double), false)]
        [TestCase("CharField", typeof(char), true)]
        [TestCase("BooleanField", typeof(bool), true)]
        [TestCase("StringField", typeof(string), true)]
        [TestCase("ObjectField", typeof(object), true)]
        public void HasRequiredField(string fieldName, Type expectedType, bool isPublic)
        {
            // Arrange
            BindingFlags flags = isPublic
                ? BindingFlags.Static | BindingFlags.Public
                : BindingFlags.Static | BindingFlags.NonPublic;

            // Act
            var fieldInfo = this.classType.GetField(fieldName, flags);

            // Assert
            Assert.NotNull(fieldInfo);
            Assert.IsTrue(fieldInfo.IsStatic);
            Assert.AreEqual(expectedType, fieldInfo.FieldType);
        }
    }
}
