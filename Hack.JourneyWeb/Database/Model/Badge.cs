using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hack.JourneyWeb.Database.Model
{
    public class Badge
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public TypeBadge TypeBadge { get; set; }
        public int Points { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}