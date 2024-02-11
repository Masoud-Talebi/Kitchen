using AutoMapper;
using Kitchen.api.Application.DTOs;
using Kitchen.api.Application.Tools;
using Kitchen.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.api;

public class FoodService : IFoodService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment environment;
    public FoodService(IMapper mapper, ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
    {
        _context = context;
        _mapper = mapper;
        this.environment = environment;
    }


    public async Task<IEnumerable<FoodDTO>> GetAllFood()
    {
        IEnumerable<Food> Food = await _context.Foods.ToListAsync();
        IEnumerable<FoodDTO> FoodDto = _mapper.Map<IEnumerable<FoodDTO>>(Food);
        foreach (var item in FoodDto)
        {
            List<CategoryDTO> categories = await GetCategoryFoods(item.Id);
            item.Categorys = categories;
        }
        return FoodDto;
    }

    public async Task<FoodDTO> GetFoodbyID(int Id)
    {
        Food Food = await _context.Foods.FindAsync(Id);
        FoodDTO FoodDto = _mapper.Map<FoodDTO>(Food);
        List<CategoryDTO> categories = await GetCategoryFoods(FoodDto.Id);
        FoodDto.Categorys = categories;
        return FoodDto;
    }

    public async Task<FoodDTO> AddFood(AddFoodDTO AddFood)
    {
        Food food = _mapper.Map<Food>(AddFood);
        await _context.Foods.AddAsync(food);
        await _context.SaveChangesAsync();
        var Imageaddres = await FileSaver.FileSavers(AddFood.Image, "FoodImage", food.Id);
        food.Image = Imageaddres;
        _context.Foods.Update(food);
        await _context.SaveChangesAsync();
        await AddFoodCategory(food.Id, AddFood.CategoryIds);
        return _mapper.Map<FoodDTO>(food);
    }

    public async Task<FoodDTO> UpdateFood(UpdateFoodDTO updateFood)
    {
        var Imageaddres = await FileSaver.FileSavers(updateFood.Image, "FoodImage", updateFood.Id);
        Food food = _mapper.Map<Food>(updateFood);
        food.Image = Imageaddres;
        _context.Foods.Update(food);
        await _context.SaveChangesAsync();
        return _mapper.Map<FoodDTO>(food);
    }

    public async Task<bool> RemoveFood(int Id)
    {
        Food Food = await _context.Foods.FindAsync(Id);
        if (Food.Image != null)
        {
            string deletePath = environment.WebRootPath + Food.Image;

            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }
        }
        _context.Foods.Remove(Food);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task AddFoodCategory(List<int> CategoryId, int FoodId)
    {
        await AddFoodCategory(FoodId, CategoryId);
    }

    public async Task DeleteFoodCategory(int CategoryId, int FoodId)
    {
        var Food_Cat = _context.CategoryFoods.FirstOrDefault(p => p.FoodId == FoodId && p.CategoryId == CategoryId);
        _context.CategoryFoods.Remove(Food_Cat);
        await _context.SaveChangesAsync();
    }
    

    #region  FoodCategory
    public async Task RemoveFoodCategory(int FoodId)
    {
        List<CategoryFood> CategoryFoods = await _context.CategoryFoods.Where(p => p.FoodId == FoodId).ToListAsync();
        foreach (var FoodCategory in CategoryFoods)
        {
            _context.CategoryFoods.Remove(FoodCategory);
            await _context.SaveChangesAsync();
        }
    }
    public async Task AddFoodCategory(int FoodId, List<int> categoryId)
    {
        foreach (var item in categoryId)
        {
            CategoryFood category = new CategoryFood();
            category.FoodId = FoodId;
            category.CategoryId = item;
            await _context.CategoryFoods.AddAsync(category);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<List<CategoryDTO>> GetCategoryFoods(int FoodId)
    {
        List<CategoryDTO> categories = new List<CategoryDTO>();
        var prod_cat = await _context.CategoryFoods.Where(p => p.FoodId == FoodId).ToListAsync();
        foreach (var item in prod_cat)
        {
            var category = await _context.Categories.Where(p => p.Deleted == false).FirstOrDefaultAsync(p => p.Id == item.CategoryId);
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            categories.Add(categoryDTO);
        }
        return categories;
    }
    #endregion

}
