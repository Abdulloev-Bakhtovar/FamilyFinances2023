using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinances
{
    class SaveDataInDataGrid
    {
        public string Id;
        string Login;
        string Password;
        string Role;
        public SaveDataInDataGrid(string Id,string Login,string Password,string Role)
        { 
            this.Id = Id;
            this.Login = Login;
            this.Password = Password;
            this.Role = Role;
        }
    }
}
