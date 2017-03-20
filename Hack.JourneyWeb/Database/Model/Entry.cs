using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hack.JourneyWeb.Database.Model
{
    public class Entry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Comments { get; set; }
        public byte[] Photos { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}