using System;
using ChallengeOne.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChallengeOne.UnitTests
{
    [TestClass]
    public class MenuRepositoryTests
    {
        [TestMethod]
        public void AddMenuItem_MenuItemAdded_ReturnsTrue()
        {
            var menuItem = new MenuItem();
            var menuRepository = new MenuRepository();

            bool wasAdded = menuRepository.AddMenuItem(menuItem);

            Assert.IsTrue(wasAdded);
        }

        [TestMethod]
        public void AddMenuItem_MenuItemNull_ReturnsFalse()
        {
            MenuItem menuItem = null;
            var menuRepository = new MenuRepository();
            
            bool wasAdded = menuRepository.AddMenuItem(menuItem);

            Assert.IsFalse(wasAdded);
        }

        [TestMethod]
        public void GetSingleMenuItem_MenuItemFound_ReturnsMenuItem()
        {
            var menuItem = new MenuItem();
            var menuRepository = new MenuRepository();
            menuItem.Id = 3;
            menuRepository.AddMenuItem(menuItem);

            var menuItemReturned = menuRepository.GetSingleMenuItem(3);

            Assert.AreEqual(menuItem, menuItemReturned);
        }

        [TestMethod]
        public void GetSingleMenuItem_MenuItemNotFound_ReturnsNull()
        {
            var menuItem = new MenuItem();
            var menuRepository = new MenuRepository();
            menuItem.Id = 3;
            menuRepository.AddMenuItem(menuItem);

            var menuItemReturned = menuRepository.GetSingleMenuItem(10);

            Assert.IsNull(menuItemReturned);
        }

        [TestMethod]
        public void GetAllMenuItems_MenuItemsReturned_ReturnsList()
        {
            var firstMenuItem = new MenuItem();
            var secondMenuItem = new MenuItem();
            var menuRepository = new MenuRepository();
            menuRepository.AddMenuItem(firstMenuItem);
            menuRepository.AddMenuItem(secondMenuItem);

            var itemsReturned = menuRepository.GetAllMenuItems().Count;

            Assert.AreEqual(2, itemsReturned);
        }

        [TestMethod]
        public void RemoveMenuItems_MenuItemRemoved_ReturnsTrue()
        {
            var firstMenuItem = new MenuItem();
            var menuRepository = new MenuRepository();
            menuRepository.AddMenuItem(firstMenuItem);

            var wasDeleted = menuRepository.RemoveMenuItem(firstMenuItem);

            Assert.IsTrue(wasDeleted);
        }
        
        [TestMethod]
        public void RemoveMenuItems_MenuItemNotRemoved_ReturnsFalse()
        {
            var firstMenuItem = new MenuItem();
            var menuRepository = new MenuRepository();
            menuRepository.AddMenuItem(firstMenuItem);

            var wasDeleted = menuRepository.RemoveMenuItem(new MenuItem());

            Assert.IsFalse(wasDeleted);
        }



    }
}
