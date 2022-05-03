namespace DemExBumagaProgramm.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Agent")]
    public partial class Agent : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agent()
        {
            ProductSale = new HashSet<ProductSale>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int TypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string Addres { get; set; }

        public int Priority { get; set; }

        [Required]
        [StringLength(80)]
        public string Director { get; set; }

        [Required]
        [StringLength(40)]
        public string INN { get; set; }

        [Required]
        [StringLength(40)]
        public string KPP { get; set; }
        public byte[] Image { get; set; }
        public virtual AgentType AgentType { get; set; }
        public int CountOfSales => ProductSale.Count;
        public int Discount
        {
            get
            {
                var sales = ProductSale.Sum(p => p.Product.MinPriceForAgent * p.Count);
                if (sales >= 0 && sales < 10000)
                {
                    return 0;
                }
                else if (sales >= 10000 && sales < 50000)
                {
                    return 5;
                }
                else if (sales >= 50000 && sales < 150000)
                {
                    return 10;
                }
                else if (sales >= 150000 && sales < 500000)
                {
                    return 20;
                }
                else if (sales > 500000)
                {
                    return 25;
                }
                else return 0;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSale> ProductSale { get; set; }
    }
}
