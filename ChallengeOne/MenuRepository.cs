using System.Collections.Generic;

namespace ChallengeOne.Repository
{
    public class MenuRepository
    {
        protected readonly List<MenuItem> _menuDirectory = new List<MenuItem>();


        //returns true if menuItem was added to list, otherwise false
        public bool AddMenuItem(MenuItem menuItem)
        {
            int startingCount = _menuDirectory.Count;

            if(menuItem != null)
                _menuDirectory.Add(menuItem);
            
            bool wasAdded = (startingCount < _menuDirectory.Count) ? true : false;
            return wasAdded;        
        }

        public MenuItem GetSingleMenuItem(int id)
        {
            foreach (var menuItem in _menuDirectory)
            {
                if (menuItem.Id == id)
                {
                    return menuItem;
                }
            }

            return null;
        }

        public List<MenuItem> GetAllMenuItems()
        {
            return _menuDirectory;
        }

        public bool RemoveMenuItem(MenuItem menuItem)
        {
            bool resultDeleted = _menuDirectory.Remove(menuItem);
            return resultDeleted;
        }






    }
}
