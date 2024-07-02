using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LearnAPI.Repos.Models;

[Table("tbl_userpermission")]
public partial class TblUserPermission
{
    [Key]
    [Column("id")]
    [StringLength(36)]
    public string id { get; set; }

    [Column("username")]
    [StringLength(50)]
    public string username { get; set; }

    [Column("code")]
    [StringLength(50)]
    public string code { get; set; }

    [Column("permission")]
    [StringLength(50)]
    public string permission { get; set; }

    [Column("status")]
    public int status { get; set; }

}
