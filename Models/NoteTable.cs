﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FBWeb.Models
{
    public partial class NoteTable
    {
        [Key]
        [Column("note_id")]
        public int NoteId { get; set; }
        [Column("noteTitle")]
        [StringLength(100)]
        public string NoteTitle { get; set; }
        [Column("noteText")]
        [StringLength(250)]
        public string NoteText { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("NoteTable")]
        public virtual User User { get; set; }
    }
}