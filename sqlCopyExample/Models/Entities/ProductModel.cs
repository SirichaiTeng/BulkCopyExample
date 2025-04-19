using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sqlCopyExample.Models.Entities;

public class ProductModel
{
    [Key]
    public int ProductModelID { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Column(TypeName = "xml")]
    public string CatalogDescription { get; set; }

    [Column(TypeName = "xml")]
    public string Instructions { get; set; }

    public Guid rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    public ProductModel()
    {
        rowguid = Guid.NewGuid();
        ModifiedDate = DateTime.Now;
    }
}
