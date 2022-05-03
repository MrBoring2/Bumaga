namespace DemExBumagaProgramm.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductSale")]
    public partial class ProductSale
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int AgentId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DatefRealization { get; set; }

        public int Count { get; set; }

        public virtual Agent Agent { get; set; }

        public virtual Product Product { get; set; }
    }
}
