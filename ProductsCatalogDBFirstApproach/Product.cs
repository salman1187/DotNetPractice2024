namespace ProductsCatalogDBFirstApproach
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public int ProductID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int Price { get; set; }

        public string Brand { get; set; }

        public string Country { get; set; }

        public bool InStock { get; set; }

        public int? Category_CategoryID { get; set; }

        public int? Supplier_SupplierID { get; set; }

        public virtual Category Category { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
