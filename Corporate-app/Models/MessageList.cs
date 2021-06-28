using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate_app.Models
{
    public class MessageList
    {
        [Key]
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ChatListId { get; set; }
        public ChatList ChatList { get; set; }
        public string Сontent { get; set; }
        public DateTime DateCreate { get; set; }

    }
}
