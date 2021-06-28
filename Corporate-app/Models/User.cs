using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate_app.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Phone { get; set; }
        public List<ChatList> ChatList = new List<ChatList>();
        public string ConfirmationCode { get; set; }
    }
}
