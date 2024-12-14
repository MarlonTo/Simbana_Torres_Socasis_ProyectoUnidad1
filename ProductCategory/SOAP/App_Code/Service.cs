using System.Collections.Generic;
using System.Linq;
using BLL;
using Entities;

public class Service : IService
{
    private readonly ProductsLogic _productsLogic ;
    private readonly CategoriesLogic _categoriesLogic;

    // Métodos para Productos
    public Product CreateProduct(Product product)
    {
        return _productsLogic.CreateProduct(product);
    }

    public Product RetrieveProductById(int id)
    {
        return _productsLogic.GetProductById(id);
    }

    public bool UpdateProduct(Product product)
    {
        return _productsLogic.EditProduct(product);
    }

    public bool DeleteProduct(int id)
    {
        return _productsLogic.DeleteProduct(id);
    }

    public List<Product> RetrieveAllProducts()
    {
        return _productsLogic.EditProduct().Cast<Product>().ToList();
    }



    // Métodos para Categorías
    public Category CreateCategory(Category category)
    {
        return _categoriesLogic.CreateCategories(category);
    }

    public Category RetrieveCategoryById(int id)
    {
        return _categoriesLogic.GetCategoryById(id);
    }

    public bool UpdateCategory(Category category)
    {
        return _categoriesLogic.EditCategories(category);
    }

    public bool DeleteCategory(int id)
    {
        return _categoriesLogic.DeleteCategories(id);
    }


    public List<Category> RetrieveAllCategories()
    {
        return _categoriesLogic.EditCategories().Cast<Category>().ToList();
    }


}
