using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KennUTicket.Models
{
    public class Ticket
    {
        //could be a GUID, int for now
        public int ID { get; set; }

        //desc = reasons for calling
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "SKU is required.")]
        public string ProductID { get; set; }

        public string CustomerAddress { get; set; }
        [Required(ErrorMessage = "Order Number is required.")]
        public string OrderNumber { get; set; }

        public int StatusID { get; set; }
        [ForeignKey("StatusID")]
        public TicketStatus Status { get; set; }

        public string CreatedByID { get; set; }
        [ForeignKey("CreatedByID")]
        public User CreatedBy { get; set; }

        public string AssignedToID { get; set; }
        [ForeignKey("AssignedToID")]
        public User AssignedTo { get; set; }

        public string LastUpdatedByID { get; set; }
        [ForeignKey("LastUpdatedByID")]
        public  User LastUpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        [Required(ErrorMessage = "Priority is required.")]
        public string Priority { get; set; }
        public string ProductInfo { get; set; }

        public override string ToString()
        {
            return this.Title + ": " + this.CreatedDate.ToString();
        }
    }
}