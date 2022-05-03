namespace DemExBumagaProgramm.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ProductSale = new HashSet<ProductSale>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        public int TypeId { get; set; }

        [Required]
        [StringLength(20)]
        public string Articul { get; set; }

        public int CountOfPeople { get; set; }

        public int CehNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal MinPriceForAgent { get; set; }

        public virtual ProductType ProductType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSale> ProductSale { get; set; }
    }
}
