using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HandsomeHedgehogHoedown.Models
{
    // Model tp build DB Table
    // Includes ComputerID PF, Manufacturer, Make, PurchaseDate, and Collection of EmployeeComputer
    // Authored by : Jason Smith
    public class Computer
    {
        // PK
        [Key]
        [Display(Name ="Computer ID")]
        public int ComputerId { get; set; }

        // Denotes Manufacturer name char length limited to 20
        [Required]
        [StringLength(20)]
        public string Manufacturer { get; set; }

        // Denotes Make of Computer char length limited to 20
        [Required]
        [StringLength(20)]
        public string Make { get; set; }

        // DateTime type, denotes date purchased
        [DataType(DataType.Date)]
        [Display(Name = "Date Purchased")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime PurchaseDate { get; set; }

        // DateTime type, denotes date computer is decommissioned, exccessible from edit View on Comuters/Edit.cshtml
        // Added DataAnnotations for Title format and date format   T.L.
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Decommissioned")]
        public DateTime? DecommissionedDate { get; set; }

        // Collection from Joined Table EmployeeComputer to list current or passed employees per computer
        public IEnumerable<EmployeeComputer> EmployeeComputers { get; set; }

    }
}
