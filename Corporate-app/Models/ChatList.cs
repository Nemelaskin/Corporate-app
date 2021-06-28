using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate_app.Models
{
    public class ChatList
    {
        [Key]
        public int ChatListId { get; set; }
        public string Name{ get; set; }
        //public int Id_User { get; set; } // пользователь, к которому прикреплен этот лист
        public List<User> User = new List<User>();

    }
}
