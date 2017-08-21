<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        [Required]
        [Display(Name = "Purchased")]
        public DateTime PurchaseDate { get; set; }

        // DateTime type, denotes date computer is decommissioned, exccessible from edit View on Comuters/Edit.cshtml
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Decommissioned")]
        public DateTime? DecommissionedDate { get; set; }

        // Collection from Joined Table EmployeeComputer to list current or passed employees per computer
        public IEnumerable<EmployeeComputer> EmployeeComputers { get; set; }

     

    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandsomeHedgehogHoedown.Models
{
    // Model tp build DB Table
    // Includes ComputerID PF, Manufacturer, Make, PurchaseDate, and Collection of EmployeeComputer
    // Authored by : Jason Smith
    public class Computer
    {
        // PK
        [Key]
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
        [Required]
        public DateTime PurchaseDate { get; set; }
        // Collection from Joined Table EmployeeComputer to list current or passed employees per computer
        public ICollection<EmployeeComputer> EmployeeComputers { get; set; }
    }
}
>>>>>>> master
