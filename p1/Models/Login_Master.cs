//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace p1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Login_Master
    {
        public int login_id { get; set; }
        [Required(ErrorMessage="Username is required")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password is required")]

        public string password { get; set; }
        public int role_id { get; set; }
    
        public virtual Role Role { get; set; }
    }
}
