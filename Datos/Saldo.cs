namespace Datos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Saldo")]
    public partial class Saldo
    {
        [Key]
        public int dni { get; set; }

        [Required]
        public string nombre_apellido { get; set; }

        [Column("saldo")]
        public decimal saldo1 { get; set; }
    }
}
