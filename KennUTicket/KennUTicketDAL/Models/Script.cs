using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KennUTicket.Models
{
    public class Script
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string ScriptText { get; set; }
    }
}
