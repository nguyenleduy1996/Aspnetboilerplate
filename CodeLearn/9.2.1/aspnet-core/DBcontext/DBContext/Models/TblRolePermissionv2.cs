using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LearnAPI.Repos.Models;

[Table("tbl_rolepermissionv2")]
public partial class TblRolePermissionv2
{
    [Key]
    [Column("permission")]
    [StringLength(50)]
    public string permission { get; set; }

    [Column("code")]
    [StringLength(50)]
    public string code { get; set; }


    [Column("name")]
    [StringLength(50)]
    public string name { get; set; }

    [Column("type")]
    [StringLength(50)]
    public string type { get; set; } = null!;

    [Column("status")]
    public int status { get; set; }

    [Column("url")]
    public string url { get; set; }

    [Column("icon")]
    public string? icon { get; set; } = null!;
}
