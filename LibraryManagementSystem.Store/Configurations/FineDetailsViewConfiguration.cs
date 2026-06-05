using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FineDetailsViewConfiguration
    : IEntityTypeConfiguration<FineDetailsView>
{
    public void Configure(
        EntityTypeBuilder<FineDetailsView> builder)
    {
        builder.HasNoKey();

        builder.ToView("vw_FineDetails");
    }
}