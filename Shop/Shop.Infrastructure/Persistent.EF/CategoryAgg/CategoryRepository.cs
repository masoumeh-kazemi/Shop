using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAgg;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.EF.CategoryAgg;

public class CategoryRepository : BaseRepository<Category> , ICategoryRepository    
{
    public CategoryRepository(ShopContext context) : base(context)
    {
    }

    public async Task<bool> DeleteCategory(long categoryId)
    {
        var category = await Context.Categories
            .Include(c=>c.Childs).ThenInclude(c=>c.Childs)
            .FirstOrDefaultAsync(f => f.Id == categoryId);

        if (category == null)
            return false;

        var isProductExist =  await Context.Products.AnyAsync(c =>
            c.CategoryId == categoryId
            || c.SubCategoryId == categoryId
            || c.SecondarySubCategoryId == categoryId);

        if (isProductExist)
            return false;

        if (category.Childs.Any(c => c.Childs.Any()))
        {
            Context.RemoveRange(category.Childs.SelectMany(c=>c.Childs));
        }
        
        Context.RemoveRange(category.Childs);
        Context.RemoveRange(category);
        return true;
    }


}