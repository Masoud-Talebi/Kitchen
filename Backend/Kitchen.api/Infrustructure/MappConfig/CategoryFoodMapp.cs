using Kitchen.api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitchen.api;

public class CategoryFoodMapp : IEntityTypeConfiguration<CategoryFood>
{
    public void Configure(EntityTypeBuilder<CategoryFood> builder)
    {
        builder.HasKey(p=> new {p.FoodId,p.CategoryId});
    }
}
