using System.Collections.Generic;
using System.Linq;

namespace _4zadanie.ViewModels
{
    public class UsersSelectionViewModel
    {
        public List<EditUserViewModel> Users { get; set; }
        public UsersSelectionViewModel()
        {
            this.Users = new List<EditUserViewModel>();
        }

        public IEnumerable<int> getSelectedIds()
        {
            return (from p in this.Users where p.Selected select p.Id).ToList();
        }
    }
}
