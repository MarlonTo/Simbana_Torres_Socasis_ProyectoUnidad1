using System.Collections.Generic;
using System.ServiceModel;
using Entities;

[ServiceContract]
public interface IService
{
    // Métodos para Productos
    [OperationContract]
    Product CreateProduct(Product product);

    [OperationContract]
    Product RetrieveProductById(int id);

    [OperationContract]
    bool UpdateProduct(Product product);

    [OperationContract]
    bool DeleteProduct(int id);

    [OperationContract]
    List<Product> RetrieveAllProducts();

    // Métodos para Categorías
    [OperationContract]
    Category CreateCategory(Category category);

    [OperationContract]
    Category RetrieveCategoryById(int id);

    [OperationContract]
    bool UpdateCategory(Category category);

    [OperationContract]
    bool DeleteCategory(int id);

    [OperationContract]
    List<Category> RetrieveAllCategories();
}
