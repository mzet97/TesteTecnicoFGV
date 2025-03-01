using Bogus;
using DesafioFGV.Domain.Entities;

namespace DesafioFGV.Domain.Test.Fakes;

public class OrderFaker
{

    public IEnumerable<Order> CreateValid(int qtd)
    {
        // Inicializa o Faker para a classe 'Order'
        var orderFaker = new Faker<Order>("pt_BR")
            .CustomInstantiator(f => new Order(
                Id: Guid.NewGuid(),
                description: f.Lorem.Word(),
                value: f.Random.Decimal(min: 1, max: 1000),
                dateOrder: f.Date.Between(
                    new DateTime(1900, 1, 1),
                    new DateTime(3000, 12, 31)
                ),
                idUser: Guid.NewGuid(),
                user: null,
                createdAt: DateTime.Now,
                updatedAt: null,
                deletedAt: null,
                isDeleted: false
            ));

        // Gera a quantidade solicitada de pedidos (orders)
        return orderFaker.Generate(qtd);
    }
}
