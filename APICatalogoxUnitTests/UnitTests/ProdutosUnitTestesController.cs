using APICatalogo.Context;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APICatalogoxUnitTests.UnitTests;

public class ProdutosUnitTestesController
{
    private IUnitOfWork repository;
    private IMapper mapper;
    public static DbContextOptions<AppDbContext> dbContextOptions { get; }

    public static string ConnectionString =
        "Server=localhost;DataBase=CatalogoDB;Uid=root;pwd=********"; // Não commitar com a senha do banco

    static ProdutosUnitTestesController()
    {
        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
            .Options;
    }

    public ProdutosUnitTestesController()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProdutoDTOMappingProfile());
        });

        mapper = config.CreateMapper();

        var context = new AppDbContext(dbContextOptions);
        repository = new UnitOfWork(context);
    }
}
